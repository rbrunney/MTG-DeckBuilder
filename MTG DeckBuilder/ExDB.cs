using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace WPF
{
    public static class ExDB
    {
        /// <summary>
        /// Opens a SQL Server database connection
        /// </summary>
        /// <param name="strDB">Name of database</param>
        /// <param name="dbConn">reference to SQLDataConnection object</param>
        /// <param name="sqlcmd">reference to SQLCommand object</param>
        /// <returns></returns>
        public static bool OpenDBCommand(string strDB, ref SqlConnection dbConn, ref SqlCommand sqlcmd)
        {
            if (sqlcmd.Connection.State != ConnectionState.Open)
            {
                //dbConn.ConnectionString = "data source=(local);initial catalog=" + strDB + ";Trusted_Connection=True";
                dbConn.ConnectionString = $"data source=(local);user id=admin2;pwd=test;initial catalog={strDB}";
                sqlcmd.Connection.Open();
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Performs a select query on database and returns Datatable
        /// sql = "Select Name from tblNames where id=@ID";
        /// ht.Add("@ID",3);
        /// dt = GetDataTable("AwesomeDB", ht, sql);
        /// </summary>
        /// <param name="strDB">Database name</param>
        /// <param name="htParameters">Hashtable list of parameters</param>
        /// <param name="SQL">Select SQL command</param>
        /// <returns></returns>
        public static DataTable GetDataTable(string strDB, Hashtable htParameters, string SQL)
        {
            SqlConnection dbConn = new SqlConnection();
            DataTable dt = new DataTable();
            SqlDataAdapter myAdap;
            SqlCommand sqlCmd;

            sqlCmd = new SqlCommand(SQL, dbConn);

            foreach (DictionaryEntry hti in htParameters)
            {
                AddParam(ref sqlCmd, hti.Key.ToString(), hti.Value);
            }

            OpenDBCommand(strDB, ref dbConn, ref sqlCmd);
            myAdap = new SqlDataAdapter(sqlCmd);

            myAdap.Fill(dt);
            sqlCmd.Connection.Close();
            dbConn.Close();

            return dt;
        }

        /// <summary>
        /// Executes a SQL command on a database.  
        /// ht.Add("@Name", "Gina");
        /// ht.Add("@Age", 22);
        /// ht.Add("@ID", 1);
        /// sql = "Insert into tblNames (Name,Age) Values(@Name,@Age)";
        /// sql = "Update tblNames set Name=@Name, Age=@Age where ID=@ID";
        /// sql = "Delete from tblNames Where Name=@Name"
        /// ExecuteIt("AwesomeDB", sql, ht);
        ///  </summary>
        ///  <param name="strdb">Database name</param>
        /// <param name="strSQL">SQL command</param>
        /// <param name="htParameters">Hashtable list of fields and parameters</param>
        /// <returns>Returns number of records affected</returns>
        public static long ExecuteIt(string strdb, string strSQL, Hashtable htParameters)
        {
            SqlConnection dbconn = new SqlConnection();
            SqlCommand sqlCmd;
            long x;

            sqlCmd = new SqlCommand(strSQL, dbconn);
            OpenDBCommand(strdb, ref dbconn, ref sqlCmd);

            foreach (DictionaryEntry hti in htParameters)
                AddParam(ref sqlCmd, hti.Key.ToString(), hti.Value);

            x = sqlCmd.ExecuteNonQuery();
            sqlCmd.Connection.Close();
            dbconn.Close();

            return x;
        }

        /// <summary>
        /// Adds a parameter to the SQL Command object
        /// </summary>
        /// <param name="sqlCmd">Reference to Sql Command object</param>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        public static void AddParam(ref SqlCommand sqlCmd, string key, object value)
        {
            if (value == null)
                sqlCmd.Parameters.AddWithValue(key, System.DBNull.Value);
            else
                sqlCmd.Parameters.AddWithValue(key, value);
        }
    }
}
