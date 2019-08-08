using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Microsoft.Reporting.WebForms;
using System.Reflection;

namespace Usda.SmallFruits.Views.Shared
{
    public partial class _Partial : System.Web.Mvc.ViewPage
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            string user = User.Identity.Name;
            //ReportParameter reportParams = new ReportParameter("Report Type", Session["reportType"].ToString());
            //ReportParameter reportId = new ReportParameter("Report Id", Session["genusId"].ToString());
            ////this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4 };)
            
            //string reportViewer = "ReportViewer1";
            //string reportPath = "/USDA Reports/families";

            //Control reporter = FindControl(reportViewer);
            //reporter.GetType().GetProperty("ReportPath").SetValue(reportViewer, reportPath);
            //if(reporter != null) 
            //{
            //    reporter.GetType().InvokeMember(reportViewer, BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty, Type.DefaultBinder, reportViewer, )
            //}
            //PropertyInfo prop = reporter.GetType().GetProperty("ReportPath");
            //prop.SetValue(reporter, "")
            

        }

        protected string GetPath()
        {
            return "/USDA Reports/families";
        }
    }
}