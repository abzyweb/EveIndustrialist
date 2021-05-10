using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporationWebConnection;
using EoiData.EoiClasses;
using EoiData.EsiDataClasses;
using EoiData.FileSystemDataClasses;
using EoiData.WebDataClasses;
using EveSwaggerConnection;
using EveSwaggerConnection.ESI_Communication.Operations.Verify;

namespace EoiData.EoiDataClasses
{
    internal class EoiDataUser
    {
        private EoiUser _eoiUser;
        private FileSystemDataUser _fileSystemUser;

        public int Id { get; private set; }
        public string Name { get; set; }
        public bool SynchronizeAssets { get; set; } = true;
        public bool SynchronizeTransactions { get; set; } = true;

        public EoiDataUser(FileSystemDataUser user)
        {
            _fileSystemUser = user;

            if (_fileSystemUser.TokenResponse != null)
            {
                var token = WebDataInterface.RefreshAccessToken(_fileSystemUser.TokenResponse.refresh_token);
                if (token != null)
                {
                    if (this.Id == 0)
                        Authenticate(token);

                    _fileSystemUser.TokenResponse = token;
                    FileSystemDataInterface.ExportUsers();
                }
            }

            if (_fileSystemUser != null && _fileSystemUser.TokenResponse != null)
            {
                CorporationWebInterface.RegisterUser(_fileSystemUser.Name);
            }

            this.Name = user.Name;
        }

        internal EoiUser GetEoiUser()
        {
            if (_eoiUser == null)
            {
                _eoiUser = new EoiUser();

                InitializeEoiUser();
            }

            return _eoiUser;
        }

        internal void VerifyUser(RawEsiVerify verifyResult, RawAccessTokenResponse token)
        {
            _fileSystemUser.TokenResponse = token;
            _fileSystemUser.Name = verifyResult.CharacterName;
            _fileSystemUser.Id = verifyResult.CharacterID;

            FileSystemDataInterface.ExportUsers();

            this.Id = verifyResult.CharacterID;
            this.Name = verifyResult.CharacterName;

            if (_eoiUser != null)
            {
                _eoiUser.Name = verifyResult.CharacterName;
                _eoiUser.IsDefault = false;
                _eoiUser.Authenticated = true;
                _eoiUser.InvokePropertyChanged();
            }

            CorporationWebInterface.RegisterUser(_fileSystemUser.Name);
        }

        internal void Authenticate(RawAccessTokenResponse token)
        {
            var verifyResult = EsiDataInterface.Verify(token.access_token);
            if (verifyResult != null)
            {
                VerifyUser(verifyResult, token);
            }
        }


        private void InitializeEoiUser()
        {
            _eoiUser.Name = this.Name;

            if (_fileSystemUser.TokenResponse != null)
                _eoiUser.Authenticated = true;

            if (this.Name == "Default")
                _eoiUser.IsDefault = true;
        }

        internal string GetAccessToken()
        {
            if (_fileSystemUser != null && _fileSystemUser.TokenResponse != null)
                return _fileSystemUser.TokenResponse.access_token;

            return null;
        }

        internal void CheckAccessToken()
        {
            if (_fileSystemUser.TokenResponse != null)
            {
                var difference = DateTime.Now - _fileSystemUser.TokenResponse.Timestamp;
                if (difference.TotalMinutes >= 15)
                {
                    var token = WebDataInterface.RefreshAccessToken(_fileSystemUser.TokenResponse.refresh_token);
                    if (token != null)
                    {
                        if (this.Id == 0)
                            Authenticate(token);

                        _fileSystemUser.TokenResponse = token;
                        FileSystemDataInterface.ExportUsers();
                    }
                        
                }
            }
            
        }
    }
}
