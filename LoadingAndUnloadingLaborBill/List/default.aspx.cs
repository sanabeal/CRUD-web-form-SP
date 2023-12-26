using Newtonsoft.Json;
using PRP.BPL.System.include.config.connection;
using PRP.PPL.System.include.config.connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BPL.Data.INV.LoadingAndUnloadingLaborBill.List
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //--------------Show Details Data----------------//
        [WebMethod]
        public static string show_list_data()
        {
            db_bpl Connstring = new db_bpl();

            var sql = $@"EXEC [INV].[LoadingUnloadingBill] 'BPL','List',null, null, null";

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
        public static string report_show(string No, string Language, string Topic)
        {
            if (Language == "Bangla" && Topic == "1")
            {
                HttpContext.Current.Session["Rpt_Name"] = null;
                HttpContext.Current.Session["rptFormula"] = null;
                HttpContext.Current.Session["Rpt_Name"] = "IOM.rpt";
                HttpContext.Current.Session["rptFormula"] = "{IOMReport.No} = '" + No + "'";
                string voucher_serial = HttpContext.Current.Session["rptFormula"].ToString();

                HttpContext.Current.Session["rptLanguage"] = Language;
                HttpContext.Current.Session["rptName"] = No;
                string rptName = HttpContext.Current.Session["rptName"].ToString();
                return voucher_serial;
            }
            else if (Language == "Bangla" && Topic == "0")
            {
                HttpContext.Current.Session["Rpt_Name"] = null;
                HttpContext.Current.Session["rptFormula"] = null;
                HttpContext.Current.Session["Rpt_Name"] = "IOMImage.rpt";
                HttpContext.Current.Session["rptFormula"] = "{IOMReport.No} = '" + No + "'";
                string voucher_serial = HttpContext.Current.Session["rptFormula"].ToString();

                HttpContext.Current.Session["rptLanguage"] = Language;
                HttpContext.Current.Session["rptName"] = No;
                string rptName = HttpContext.Current.Session["rptName"].ToString();
                return voucher_serial;
            }
            else if (Language == "English" && Topic == "1")
            {
                HttpContext.Current.Session["Rpt_Name"] = null;
                HttpContext.Current.Session["rptFormula"] = null;
                HttpContext.Current.Session["Rpt_Name"] = "IOMEn.rpt";
                HttpContext.Current.Session["rptFormula"] = "{IOMReport.No} = '" + No + "'";
                string voucher_serial = HttpContext.Current.Session["rptFormula"].ToString();
                HttpContext.Current.Session["rptLanguage"] = Language;
                HttpContext.Current.Session["rptName"] = No;
                string rptName = HttpContext.Current.Session["rptName"].ToString();
                return voucher_serial;
            }
            else
            {
                HttpContext.Current.Session["Rpt_Name"] = null;
                HttpContext.Current.Session["rptFormula"] = null;
                HttpContext.Current.Session["Rpt_Name"] = "IOMEnImage.rpt";
                HttpContext.Current.Session["rptFormula"] = "{IOMReport.No} = '" + No + "'";
                string voucher_serial = HttpContext.Current.Session["rptFormula"].ToString();
                HttpContext.Current.Session["rptLanguage"] = Language;
                HttpContext.Current.Session["rptName"] = No;
                string rptName = HttpContext.Current.Session["rptName"].ToString();
                return voucher_serial;
            }
        }
    }
}