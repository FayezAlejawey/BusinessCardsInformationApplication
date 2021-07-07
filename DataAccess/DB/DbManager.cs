using System;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using DataAccess.LogFile;
using System.Security.Cryptography;
using System.Text;

namespace DataAccess.DB {
    public class DbManager {

        private const string _connectionString = @"data source=DESKTOP-VLS8U5D\SQLEXPRESS;initial catalog=BUSINESS_CARDS_INFO_APP; persist security info=True; Integrated Security = SSPI;";

        public static bool IsUserCredentailsValid(string userName, string password) {

            var hashedPassword = GetHashedPassword(password);
            var whereClause = $"{DbConstants.FldUserName} = '{userName}' and {DbConstants.FldPassword} = '{hashedPassword}'";

            try {

                var records = GetRecords(DbConstants.TblUserCredentials, whereClause);
                return records.Count > 0;

            } catch (Exception) {
                throw;
            }
        }

        public static string GetHashedPassword(string password) {

            //The Hashing Algorithm
            var data = Encoding.ASCII.GetBytes(password);
            var sha1 = new SHA1CryptoServiceProvider();
            var sha1Data = sha1.ComputeHash(data);
            ASCIIEncoding ascii = new ASCIIEncoding();
            var hashedPassword = ascii.GetString(sha1Data);

            return hashedPassword;

        }

        public static List<Dictionary<string, object>> GetRecords(string tblName, string whereClause = null) {

            try {

                using(SqlConnection conn = new SqlConnection(_connectionString)) {

                    conn.Open();

                    var where = string.IsNullOrWhiteSpace(whereClause) ? string.Empty : $" WHERE {whereClause}";
                    var query = $"SELECT * FROM {tblName}{where}";
                    var cmd = new SqlCommand(query, conn);
                    string[] restrictions = new string[4] { null, null, tblName, null };
                    var columns = conn.GetSchema("Columns", restrictions).AsEnumerable().Select(s => s.Field<String>("Column_Name")).ToList();

                    using (SqlDataReader reader = cmd.ExecuteReader()) {

                        var records = new List<Dictionary<string, object>>();

                        while (reader.Read()) {

                            var record = new Dictionary<string, object>();
                            foreach(string column in columns) {
                                record[column] = reader[column] is null ? string.Empty : reader[column];
                            }

                            records.Add(record);

                        }

                        return records;

                    }
                }
                
            } catch (Exception ex) {

                var msg = $"Error occured while getting records from [{tblName}] table.";
                Logger.LogError(msg, ex);
                throw new Exception(msg);

            }
        }

        public static void InsertRecord(string tblName, params (string, object)[] fldValPair) {

            try {

                using (SqlConnection conn = new SqlConnection(_connectionString)) {

                    conn.Open();

                    var query = GetProperStatement(tblName, null, StatementType.Insert, fldValPair);
                    var cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();

                }

            } catch (Exception ex) {

                var msg = $"Error occured while inserting a record in [{tblName}] table.";
                Logger.LogError(msg, ex);
                throw new Exception(msg);

            }
        }

        public static void UpdateRecord(string tblName, string whereClause, params (string, object)[] fldValPair) {

            try {

                using (SqlConnection conn = new SqlConnection(_connectionString)) {

                    conn.Open();

                    var query = GetProperStatement(tblName, whereClause, StatementType.Update, fldValPair);
                    var cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();

                }

            } catch (Exception ex) {

                var msg = $"Error occured while updating record(s) in [{tblName}] table.";
                Logger.LogError(msg, ex);
                throw new Exception(msg);

            }
        }

        public static void DeleteRecord(string tblName, string whereClause) {

            try {

                using (SqlConnection conn = new SqlConnection(_connectionString)) {

                    conn.Open();

                    var query = GetProperStatement(tblName, whereClause, StatementType.Delete);
                    var cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();

                }

            } catch (Exception ex) {

                var msg = $"Error occured while deleting record(s) from [{tblName}] table.";
                Logger.LogError(msg, ex);
                throw new Exception(msg);

            }
        }

        private static string GetProperStatement(string tblName, string whereClause, 
            StatementType type, params (string, object)[] fldValPair) {

            switch (type) {
                case StatementType.Insert:
                    return GetInsertStatement(tblName, fldValPair);
                case StatementType.Update:
                    return GetUpdateStatement(tblName, whereClause, fldValPair);
                default:
                    return GetDeleteStatement(tblName, whereClause);
            }
        }

        private static string GetInsertStatement(string tblName, params (string fld, object val)[] fldValPair) {

            var flds = fldValPair.Select(e => e.fld).ToArray();
            var vals = fldValPair.Select(e => $"'{e.val}'").ToArray();

            var fldsStr = string.Join(",", flds);
            var valsStr = string.Join(",", vals);

            var query = $"INSERT INTO {tblName} ({fldsStr}) VALUES ({valsStr})";
            return query;

        }

        private static string GetUpdateStatement(string tblName, string whereClause, params (string fld, object val)[] fldValPair) {

            var fldValPairs = fldValPair.Select(e => $"{e.fld}='{e.val}'").ToArray();
            var fldValPairsStr = string.Join(",", fldValPairs);

            //whereClause variable can have empty/null value, where in this case all the records within the specified table will be updated
            var where = string.IsNullOrWhiteSpace(whereClause) ? string.Empty : $" WHERE {whereClause}";
            var query = $"UPDATE {tblName} SET {fldValPairsStr}{where}";
            return query;

        }

        private static string GetDeleteStatement(string tblName, string whereClause) {

            //whereClause variable can have empty/null value, where in this case all the records within the specified table will be deleted
            var where = string.IsNullOrWhiteSpace(whereClause) ? string.Empty : $" WHERE {whereClause}";
            var query = $"DELETE FROM {tblName}{where}";
            return query;

        }
    }
}
