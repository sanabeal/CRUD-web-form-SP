using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PRP.BPL.System.include.config.connection
{
    public class db_bpl
    {

        private Hashtable m_Erroobj;
        private Hashtable _errObj;

        //---------Database Connection---------//    
        public SqlConnection conn = new SqlConnection("Data Source=172.16.1.152;Initial Catalog=BPL;Persist Security Info=True;User ID=sa;Password=live"); //Localhost
        //------------------------------------//
        public DataTable dt = new DataTable();
        public SqlCommand cmd = new SqlCommand();
        public SqlDataAdapter da = new SqlDataAdapter();

        public db_bpl()
        {
            _errObj = new Hashtable();
        }
        public db_bpl(string fy)
        {
            conn = new SqlConnection("Data Source=172.16.1.152;Initial Catalog=" + fy + ";Persist Security Info=True;User ID=sa;Password=sa*1209");
        }

        public SqlConnection getcon
        {
            get { return conn; }
        }

        public DataTable SqlDataTable(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            return dt;
        }

        public int insert(dynamic qurry)
        {
            int newId = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand(qurry, conn))
                {
                    conn.Open();
                    //cmd.ExecuteNonQuery();
                    newId = (int)cmd.ExecuteScalar();
                    conn.Close();
                }
            }
            catch (SqlException ex)
            {
                string msg = "Insert Error:";
                msg += ex.Message;
                newId = 0;
            }
            return newId;
        }

        public void insert_details(dynamic qurry)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(qurry, conn))
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (SqlException ex)
            {
                string msg = "Insert Error:";
                msg += ex.Message;
            }
        }
        public void CallProcedure(Array sqlpra, string procidurname)  // call Procedur whith Paramiter and Procidure Name
        {
            try
            {
                cmd = new SqlCommand(procidurname, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                if (sqlpra != null)
                {
                    cmd.Parameters.AddRange(sqlpra);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }

        }

        // Added by Ahasan ullah 2023-11-03
        #region
        public Hashtable ErrorObject
        {
            get
            {
                return this.m_Erroobj;
            }
        }
        public DataTable GetTable(string SQl)
        {
            //for string : have to edit with proper code
            try
            {
                SqlDataAdapter adp = new SqlDataAdapter();
                DataSet ds = new DataSet();
                adp.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                this.SetError(ex);
                return null;
            }
        }
        public DataSet GetDataSet(String SQL)
        {
            //for string : have to edit with proper code
            try
            {
                SqlDataAdapter adp = new SqlDataAdapter(SQL, conn);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                this.SetError(ex);
                return null;
            }
        }
        public DataTable GetTable(SqlCommand Cmd)
        {
            try
            {
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = Cmd;
                Cmd.Connection = this.conn;
                DataTable dt = new DataTable();
                adp.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                this.SetError(ex);
                return null;
            }
        }
        public DataSet GetDataSet(SqlCommand Cmd)
        {
            try
            {
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = Cmd;
                Cmd.CommandTimeout = 0;
                Cmd.Connection = this.conn;
                DataSet ds = new DataSet();
                adp.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                this.SetError(ex);
                return null;
            }
        }
        public Boolean ExecuteCommand(string SQL)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = SQL;
            cmd.Connection = this.conn;
            try
            {
                this.conn.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                this.SetError(ex);
                return false;
            }
            finally
            {
                this.conn.Close();
            }
        }
        public Boolean ExecuteCommand(SqlCommand cmd)
        {
            cmd.Connection = this.conn;
            cmd.CommandTimeout = 120;
            try
            {
                this.conn.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                this.SetError(ex);
                return false;
            }
            finally
            {
                this.conn.Close();
            }
        }
        public SqlDataReader ExecuteReader(SqlCommand cmd)
        {
            cmd.Connection = this.conn;
            cmd.CommandTimeout = 300;
            try
            {
                this.conn.Close();
                this.conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                this.SetError(ex);
                return null;
            }
        }
        private void SetError(Exception ex)
        {
            this.m_Erroobj["Src"] = ex.Source;
            this.m_Erroobj["Msg"] = ex.Message;
            this.m_Erroobj["Location"] = ex.StackTrace;
        }


        /// <summary>
        /// /
        /// </summary>
        public DataSet Hashtable { get; set; }

        private void SetError2(Hashtable errObject)
        {
            this._errObj["Src"] = errObject["Src"];
            this._errObj["Msg"] = errObject["Msg"];
            this._errObj["Location"] = errObject["Location"];
        }

        private void ClearErrors()
        {
            this._errObj["Src"] = string.Empty;
            this._errObj["Msg"] = string.Empty;
            this._errObj["Location"] = string.Empty;
        }


        ////   CRUD Operation 
        public bool InsertProcess(string comCode, string SQLprocName, string CallType, string mDesc1, string mDesc2, string mDesc3, string mDesc4, string mDesc5, string mDesc6, string mDesc7, string mDesc8, string mDesc9, string mDesc10, string mDesc11)
        {
            try
            {
                this.ClearErrors();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = SQLprocName;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Comp1", comCode));
                cmd.Parameters.Add(new SqlParameter("@CallType", CallType));
                cmd.Parameters.Add(new SqlParameter("@Desc1", mDesc1));
                cmd.Parameters.Add(new SqlParameter("@Desc2", mDesc2));
                cmd.Parameters.Add(new SqlParameter("@Desc3", mDesc3));
                cmd.Parameters.Add(new SqlParameter("@Desc4", mDesc4));
                cmd.Parameters.Add(new SqlParameter("@Desc5", mDesc5));
                cmd.Parameters.Add(new SqlParameter("@Desc6", mDesc6));
                cmd.Parameters.Add(new SqlParameter("@Desc7", mDesc7));
                cmd.Parameters.Add(new SqlParameter("@Desc8", mDesc8));
                cmd.Parameters.Add(new SqlParameter("@Desc9", mDesc9));
                cmd.Parameters.Add(new SqlParameter("@Desc10", mDesc10));
                cmd.Parameters.Add(new SqlParameter("@Desc11", mDesc11));
                bool _result = ExecuteCommand(cmd);
                if (_result == false)  //_result==false
                {
                    this.SetError2(ErrorObject);
                }
                return _result;
            }
            catch (Exception exp)
            {
                this.SetError(exp);
                return false;
            }// try
        }

        #endregion

    }
}