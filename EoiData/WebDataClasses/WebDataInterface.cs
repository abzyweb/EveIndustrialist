using EoiData.EoiClasses;
using EoiData.EoiDataClasses;
using EoiData.FileSystemDataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoiData.WebDataClasses
{
    public static class WebDataInterface
    {
        public static string GetAuthenticationCode(Guid guid)
        {
            return WebDataReader.GetAuthenticationCode(guid);
        }

        public static void GetUserAccessToken(string code)
        {
            var token = WebDataReader.GetUserAccessToken(code);
            if (token != null)
            {
                EoiDataInterface.AuthenticateUser(token);
            }
        }

        public static RawAccessTokenResponse RefreshAccessToken(string refreshToken)
        {
            return WebDataReader.RefreshAccessToken(refreshToken);
        }

        internal static bool IsVersionValid(string version)
        {
            return WebDataReader.GetVersionValidation(version);
        }
    }
}
