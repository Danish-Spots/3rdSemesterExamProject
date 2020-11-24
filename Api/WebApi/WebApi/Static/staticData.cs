using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi
{
    public static class staticData
    {
        public static string connString =
            "Server=tcp:fevr.database.windows.net,1433;Initial Catalog=FevR;Persist Security Info=False;User ID=fevr;Password=Admin123!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public enum ERRORS
        {
            FINISHED_POST,
            FOREIGN_KEY_OUT_OF_RANGE
        }
    }
}
