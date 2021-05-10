using CorporationWebConnection;
using EoiData.Constants;
using EoiData.EoiClasses;
using EoiData.EoiDataClasses;
using EoiData.EsiDataClasses;
using EoiData.MarketerDataClasses;
using EoiData.Settings;
using EveSwaggerConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace EoiData.Helper
{
    public class BackgroundWorkerStatus
    {
        public BackgroundWorkerStatus(string status, int progress)
        {
            this.Status = status;
            this.Progress = progress;
        }

        public int Progress { get; set; }
        public string Status { get; set; }
    }

    internal static class BackgroundWorker
    {
        private static bool _stop;
        private static bool _calculateBlueprints;

        internal static BackgroundWorkerStatus Status { get; set; }
        public static Thread MainThread { get; private set; }

        internal static void Start(Thread mainThread)
        {
            if (mainThread == null)
                return;

            MainThread = mainThread;

            while (!_stop)
            {
                CheckUserAccessTokens();

                if (SettingsInterface.GlobalSettings.EnableAutoUpdater)
                {
                    var updated = false;

                    if (SynchronizeBlueprints())
                        updated = true;

                    CheckUpdated(updated, EoiInterface.BlueprintPropertyChanged);
                    updated = false;

                    Status = new BackgroundWorkerStatus("Synchronisiere Assets", -1);
                    SynchronizeAssets();
                    SynchronizeWallet();
                    CheckUpdated(true, EoiInterface.AssetPropertyChanged);

                    Status = new BackgroundWorkerStatus("Synchronisiere Markt Orders", -1);
                    SynchronizeCharacterOrders();
                    CheckUpdated(true, EoiInterface.BlueprintPropertyChanged);

                    if (SettingsInterface.GlobalSettings.EnableCorporationContractsUpdates && !_stop)
                    {
                        Status = new BackgroundWorkerStatus("Synchronisiere Corporation Verträge", -1);
                        if (SynchronizeCorporationContracts())
                            updated = true;

                        CheckUpdated(updated, EoiInterface.ContractsPropertyChanged);
                        updated = false;
                    }

                    if (SettingsInterface.GlobalSettings.EnableMarketerUpdates && !_stop)
                    {
                        Status = new BackgroundWorkerStatus("Aktualisiert Markt Preise", -1);
                        if (CheckMarketerData())
                            updated = true;

                        CheckUpdated(true, EoiInterface.AssetPropertyChanged);
                        CheckUpdated(updated, EoiInterface.BlueprintPropertyChanged);
                        updated = false;
                    }

                    if (SettingsInterface.GlobalSettings.EnableMarketHistoryUpdates && !_stop)
                    {
                        Status = new BackgroundWorkerStatus("Aktualisiert Markt Verkaufszahlen", -1);
                        if (CheckMarketHistory())
                            updated = true;

                        CheckUpdated(updated, EoiInterface.BlueprintPropertyChanged);
                        updated = false;
                    }
                }

                var preStop = false;
                for (int i = 0; i < 30; i++)
                {
                    if (!_stop)
                    {
                        CheckUserAccessTokens();

                        if (_calculateBlueprints)
                        {
                            Status = new BackgroundWorkerStatus("Berechne Blueprints", -1);
                            _calculateBlueprints = false;
                            EoiDataInterface.CalculateAllBlueprints();
                            CheckUpdated(true, EoiInterface.BlueprintPropertyChanged);
                        }

                        if (EveSwaggerInterface.Working())
                        {
                            var state = EveSwaggerInterface.GetProgressState();
                            Status = new BackgroundWorkerStatus("Download vom Eve Online Server: " + state.FinishedOperations + " / " + state.Operations, state.Progress);
                            preStop = true;
                        }
                        else
                            Status = new BackgroundWorkerStatus("", 0);

                        Thread.Sleep(SettingsInterface.GlobalSettings.AutoUpdaterInterval / 30);

                        if (preStop && !EveSwaggerInterface.Working())
                            i = 30;
                    }
                        
                }
                
            }
        }

        private static void SynchronizeCharacterOrders()
        {
            EoiDataInterface.SynchronizeCharacterOrders();
        }

        private static void SynchronizeAssets()
        {
            EoiDataInterface.SynchronizeAssets();
        }

        private static void SynchronizeWallet()
        {
            EoiDataInterface.SynchronizeWallet();
        }

        private static bool SynchronizeCorporationContracts()
        {
            return EoiDataInterface.SynchronizeCorporationContracts();
        }

        private static bool SynchronizeBlueprints()
        {
            var updated = false;

            if (EoiDataInterface.SynchronizeBlueprints())
                updated = true;

            return updated;
        }

        private static void CheckUpdated(bool updated, Action action)
        {
            if (updated)
            {
                var dispatcher = Dispatcher.FromThread(MainThread);
                if (dispatcher != null)
                {
                    dispatcher.Invoke(DispatcherPriority.Background, action);
                }
            }
        }

        private static void CheckUserAccessTokens()
        {
            EoiDataInterface.CheckUserAccessTokens();
        }

        private static bool CheckMarketHistory()
        {
            EsiDataInterface.RequestMarketHistory();
            return EoiDataInterface.CheckMarketHistory();
        }

        private static bool CheckMarketerData()
        {
            EsiDataInterface.RequestMarketOrders();
            return EoiDataInterface.CheckMarketOrders();
            //MarketerDataInterface.RequestBlueprints();
            //return EoiDataInterface.CheckMarketerData();
        }

        internal static void Stop()
        {
            _stop = true;
        }

        public static void CalculateAllBlueprints()
        {
            _calculateBlueprints = true;
        }
    }
}
