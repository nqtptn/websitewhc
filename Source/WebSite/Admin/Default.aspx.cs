using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Ext.Net;

public partial class Admin_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            InitPage();
            // Reset the Session Theme on Page_Load.
            // The Theme switcher will persist the current theme only 
            // until the main Page is refreshed.            
        }
    }
    protected void InitPage()
    {
        if (Session["userName"] != null && Session["userName"] != string.Empty)
        {
            lblUser.Text = Session["userName"].ToString();
            if (Session["TenHienThi"] != null)
            {
                lblUser.Text = Session["TenHienThi"].ToString();
            }
        }
        else
            Response.Redirect("Login.aspx");

    }
    protected void btnLogOut_Click(object sender, DirectEventArgs e)
    {
        Session["userName"] = "";
        Session["IDNhomND"] = null;
        Session["Madv"] = "";
        Response.Redirect("./Login.aspx", true);
    }
}