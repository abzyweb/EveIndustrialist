using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Threading;

namespace EveOnlineIndustrialist.EoiThreadManager
{
    public class ThreadFinishedEventArgs : EventArgs
    {
        public ThreadFinishedEventArgs(bool aborted)
        {
            Aborted = aborted;
        }

        public bool Aborted { get; set; }
    }

    public delegate void ThreadFinishedDelegate(object sender, ThreadFinishedEventArgs args);

    public class EoiThreadManager
    {
        public event ThreadFinishedDelegate ThreadFinished;

        private Dictionary<string, EoiThreadPool> ThreadPools { get; set; } = new Dictionary<string, EoiThreadPool>();

        private Thread MainThread { get; set; }

        public volatile bool _stop;
        private Thread WorkingThread { get; set; }
        public static EoiThreadManager ThreadManager { get; private set; }

        public EoiThreadManager(Thread mainThread = null)
        {
            if (mainThread != null)
                MainThread = mainThread;
            
            WorkingThread = new Thread(Start);
            WorkingThread.IsBackground = true;
            WorkingThread.Start();
        }
        
        public static void Init()
        {
            ThreadManager = new EoiThreadManager(Thread.CurrentThread);
        }

        private void FireThreadFinishedEvent(object sender, bool aborted)
        {
            var dispatcher = Dispatcher.FromThread(MainThread);

            if (dispatcher != null)
            {
                dispatcher.BeginInvoke(DispatcherPriority.Normal, new System.Action(() =>
                {
                    var handler = ThreadFinished;
                    if (handler != null)
                        handler(sender, new ThreadFinishedEventArgs(aborted));
                }));
            }
            
        }

        

        public void Add(EoiThread thread)
        {
            lock (ThreadPools)
            {
                if (!ThreadPools.ContainsKey(thread.ThreadPool))
                    ThreadPools.Add(thread.ThreadPool, new EoiThreadPool());

                ThreadPools[thread.ThreadPool].WaitingThreads.AddLast(thread);
            }
        }

        internal void Stop()
        {
            _stop = true;
            WorkingThread.Join();
        }

        public void Dispose()
        {

        }

        public void Start()
        {
            do
            {
                lock (ThreadPools)
                {
                    foreach (var eoiThreadPool in ThreadPools)
                    {
                        if (eoiThreadPool.Value.ActiveThread != null && !eoiThreadPool.Value.ActiveThread.Thread.IsAlive)
                        {
                            FireThreadFinishedEvent(eoiThreadPool.Value.ActiveThread, false);
                            eoiThreadPool.Value.ActiveThread = null;
                        }


                        if (eoiThreadPool.Value.WaitingThreads.Any())
                        {
                            if (eoiThreadPool.Value.WaitingThreads.Any(x => x.ForceStart) && eoiThreadPool.Value.ActiveThread != null)
                            {
                                eoiThreadPool.Value.ActiveThread.Thread.Abort();
                                eoiThreadPool.Value.ActiveThread.Thread.Join();

                                var handler = ThreadFinished;
                                if (handler != null)
                                    handler(eoiThreadPool.Value.ActiveThread, new ThreadFinishedEventArgs(true));
                            }

                            if (eoiThreadPool.Value.ActiveThread == null)
                            {
                                if (eoiThreadPool.Value.WaitingThreads.Any(x => x.ForceStart))
                                {
                                    eoiThreadPool.Value.ActiveThread = eoiThreadPool.Value.WaitingThreads.First(x => x.ForceStart);
                                }
                                else
                                    eoiThreadPool.Value.ActiveThread = eoiThreadPool.Value.WaitingThreads.First.Value;

                                eoiThreadPool.Value.WaitingThreads.Remove(eoiThreadPool.Value.ActiveThread);

                                eoiThreadPool.Value.ActiveThread.Thread.Start();
                            }
                        }

                    }

                }

                Thread.Sleep(500);
            } while (!_stop);
        }
    }
}
