using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Static
{
    public static class StaticMethods
    {
        public static void updateOrDeleteFromDB(string sqlQuery, params (string, object)[] paramList)
        {
            using (SqlConnection databaseConnection = new SqlConnection(staticData.connString))
            {
                databaseConnection.Open();
                using (SqlCommand command = new SqlCommand(sqlQuery, databaseConnection))
                {
                    foreach (var pTuple in paramList)
                    {
                        command.Parameters.AddWithValue(pTuple.Item1, pTuple.Item2);
                    }
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void PostToDB(string insertSql, params (string, object)[] paramList)
        {
            using (SqlConnection databaseConnection = new SqlConnection(staticData.connString))
            {
                databaseConnection.Open();
                using (SqlCommand insertCommand = new SqlCommand(insertSql, databaseConnection))
                {
                    foreach (var pTuple in paramList)
                    {
                        insertCommand.Parameters.AddWithValue(pTuple.Item1, pTuple.Item2);
                    }
                    insertCommand.ExecuteNonQuery();
                }
            }
        }
    }
}
