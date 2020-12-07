using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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

        public static (staticData.ERRORS, int) PostToDB(string insertSql, params (string, object)[] paramList)
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

                    try
                    {
                        int id = (int) insertCommand.ExecuteScalar();
                        return (staticData.ERRORS.FINISHED_POST, id);
                    }
                    catch (SqlException e)
                    {
                        if (e.Errors.Count > 0)
                        {
                            switch (e.Errors[0].Number)
                            {
                                case 547:
                                    return (staticData.ERRORS.FOREIGN_KEY_OUT_OF_RANGE, -1);
                            }
                        }

                        return (staticData.ERRORS.GENERIC_ERROR, 0);
                    }
                    
                }
            }
        }

        public static string GenerateSessionKey()
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, 32).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
