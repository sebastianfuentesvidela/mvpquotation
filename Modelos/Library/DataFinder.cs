using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Xml;
using System.IO;
using System.Data.SqlClient;
//using BusinessEntity;

namespace Modelos.Library
{
    public class DataFinder: IDisposable
    {
        private String usableConnection;
        public String errstate = "";
        public List<String> errlist = new List<String>();

        public DataFinder(String useconnection)
        {
            usableConnection = useconnection;
        }
        public void Dispose()
        {
            errlist = null;
        }
        private void setErrorState(String errorMessage)
        {
            errlist.Add(errorMessage);
            errstate = "ERROR";
        }

        public DataTable GetRecordset(String commandoSql)
        {
            DataTable myTable = new DataTable();
            
                //"Data Source=hermes;database=qcvalues; Integrated Security=SSPI;");
            try
            {
                using (SqlConnection con = new SqlConnection(usableConnection))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(commandoSql, con))
                    {

                        myTable.Load(cmd.ExecuteReader());

                    }
                    con.Close();
                }
            }
            catch (SqlException exep)
            {
                setErrorState("DataFinder.GetRecordset: " + exep.ToString());
            }
            finally
            {
            }

            return myTable;
        }
        public DataSet GetDataSet(String commandoSql)
        {
            DataSet myDataSet = new DataSet();
            SqlConnection con = new SqlConnection(usableConnection);
            SqlDataAdapter da = new SqlDataAdapter();
            //"Data Source=hermes;database=qcvalues; Integrated Security=SSPI;");
            try
            {
                using (SqlCommand cmd = new SqlCommand(commandoSql, con))
                {
                    da.SelectCommand = cmd;
                    con.Open();
                    da.Fill(myDataSet);
                    con.Close();
                }
            }
            catch (SqlException exep)
            {
                setErrorState("DataFinder.GetDataSet: " + exep.ToString());
            }
            finally
            {
                con = null;
            }
            return myDataSet;
        }
        public int ExecuteCommand(String commandoSql) {
            SqlConnection con = new SqlConnection(usableConnection);
            //"Data Source=hermes;database=qcvalues; Integrated Security=SSPI;");
            int success = 0;
            try
            {

                using (SqlCommand cmd = new SqlCommand(commandoSql, con))
                {

                    success = cmd.ExecuteNonQuery();

                }
            }
            catch (SqlException exep)
            {
                setErrorState("DataFinder.ExecuteCommand: " + exep.ToString());
                success = -1;
            }
            finally
            {
                con = null;
            }
            return success;
        }
    }
}