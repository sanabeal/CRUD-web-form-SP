using Newtonsoft.Json;
using PRP.BPL.Data.INV.LoadingAndUnloadingLaborBill;
using PRP.BPL.System.include.config.connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BPL.Data.INV.LoadingAndUnloadingLaborBill.Edit
{
    public partial class _default : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //Save Master data
        [WebMethod]
        public static string insertDt(LoadingUnloadingBill master, string[][] array, string[][] array1)
        {
            db_bpl Connstring = new db_bpl();
            string msgbox = "";
            try
            {
                string TermID = "";
                bool _result = Connstring.InsertProcess("10002", "[INV].[LoadingUnloadingBill]", "INSERTMaster", master.BillNo, master.SupplierSerial, master.BillDate.ToString(), master.DeliveryDate.ToString(), master.ChalanNo, master.DeliveryTime, master.ClientCode, master.ReceiverAddress, master.PaperSupplierCode, master.PaperCode, HttpContext.Current.Session["USERID"].ToString());

                if (_result == true)
                {

                    string result = string.Empty;
                    DataTable dt = new DataTable();
                    dt.Columns.Add("TrackNo");
                    dt.Columns.Add("RollQtyInKG");
                    dt.Columns.Add("RollQty");
                    dt.Columns.Add("UnitPrice");
                    dt.Columns.Add("TotalBill");
                    dt.Columns.Add("ID");
                    //-------------Set Data Tbale Value----------------//
                    foreach (var arr in array)
                    {
                        DataRow dr = dt.NewRow();
                        dr["TrackNo"] = arr[0];
                        dr["RollQtyInKG"] = arr[1];
                        dr["RollQty"] = arr[2];
                        dr["UnitPrice"] = arr[3];
                        dr["TotalBill"] = arr[4];
                        dr["ID"] = arr[6];
                        dt.Rows.Add(dr);
                    }

                    string[] new_details_data = array1[1][1].Split('#');
                    foreach (var s_data in new_details_data)
                    {
                        if (s_data != "")
                        {
                            //string sql = @"DELETE FROM Inv.LoadingUnloadingLabourBilllDetail WHERE(ID = '" + s_data + "')";
                            //Connstring.insert_details(sql);
                            //Connstring.conn.Close();
                            _result = Connstring.InsertProcess("10002", "[INV].[LoadingUnloadingBill]", "DeleteByID", s_data,"","","","","","","","","","");
                        }
                    }

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string id = dt.Rows[i]["ID"].ToString().Trim();

                        _result = Connstring.InsertProcess("10002", "[INV].[LoadingUnloadingBill]", "INSERTDetail", master.BillNo, dt.Rows[i]["TrackNo"].ToString().Trim(), dt.Rows[i]["RollQtyInKG"].ToString().Trim(), dt.Rows[i]["RollQty"].ToString().Trim(), dt.Rows[i]["UnitPrice"].ToString().Trim(), dt.Rows[i]["TotalBill"].ToString().Trim(), HttpContext.Current.Session["USERID"].ToString(), id, "", "", "");                      
                    }

                    if (_result == true)
                    {
                        msgbox = "SaveSuccess";
                    }
                    else
                    {
                        msgbox = "SaveFailed";
                    }
                }
            }
            catch (Exception ex)
            {
                msgbox = "SaveFailed";
            }
            return msgbox;
        }

      
        //------------Get report show---------------//
        [WebMethod]
        public static string report_show(string Code)
        {
            HttpContext.Current.Session["Rpt_Name"] = null;
            HttpContext.Current.Session["rptFormula"] = null;
            HttpContext.Current.Session["Rpt_Name"] = "Chalan.rpt";
            HttpContext.Current.Session["rptFormula"] = "{Chalan.No} = '" + Code + "'";
            string voucher_serial = HttpContext.Current.Session["rptFormula"].ToString();
            return voucher_serial;
        }
        

        //---------Set Dropdown List--------------------//
        [WebMethod]
        public static List<ListItem> set_drop_down_list_Product(string db_table, string col_value, string col_text, string condition_field, string condition, string condition_field1, string condition1)
        {
            db_bpl Connstring = new db_bpl();
            string sql;
            if (condition != "" && condition_field != "")
            {
                sql = "SELECT Top 200 " + col_value + ", " + col_text + " FROM " + db_table + " WHERE " + condition_field + "!='" + condition + "'";
            }
            else
            {
                sql = "SELECT Top 200 " + col_value + ", " + col_text + " FROM " + db_table;
            }

            using (SqlConnection con = Connstring.getcon)
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    List<ListItem> customers = new List<ListItem>();
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            customers.Add(new ListItem
                            {
                                Value = reader[col_value].ToString(),
                                Text = reader[col_text].ToString()
                            });
                        }
                    }
                    con.Close();
                    return customers;
                }
            }
        }

        [WebMethod]
        public static string LoadClientAddress(string clientCode)
        {
            db_bpl Connstring = new db_bpl();

            var sql = $@"EXEC [INV].[LoadingUnloadingBill] 'BPL','ClientAddress',null, null, null,'{clientCode}'";

            DataTable Data = new DataTable();
            try
            {
                Data = Connstring.SqlDataTable(sql);
                return JsonConvert.SerializeObject(Data);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [WebMethod]
        public static string LoadPaperDetails(string cboPaper)
        {
            db_bpl Connstring = new db_bpl();

            var sql = $@"EXEC [INV].[LoadingUnloadingBill] 'BPL','PaperDetail',null, null, null,'{cboPaper}'";

            DataTable Data = new DataTable();
            try
            {
                Data = Connstring.SqlDataTable(sql);
                return JsonConvert.SerializeObject(Data);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [WebMethod]
        public static string GetData(string id)
        {
            db_bpl Connstring = new db_bpl();

            var sql = $@"EXEC [INV].[LoadingUnloadingBill] 'BPL','ListByIDMaster',null, null, null,'{id}'";

            DataTable Data = new DataTable();
            try
            {
                Data = Connstring.SqlDataTable(sql);
                return JsonConvert.SerializeObject(Data);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [WebMethod]
        public static string show_details_data(string search_data)
        {
            db_bpl Connstring = new db_bpl();

            var sql = $@"EXEC [INV].[LoadingUnloadingBill] 'BPL','ListByIDDetail',null, null, null,'{search_data}'";

            DataTable Data = new DataTable();
            try
            {
                Data = Connstring.SqlDataTable(sql);
                return JsonConvert.SerializeObject(Data);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}