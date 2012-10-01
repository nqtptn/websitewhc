using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using System.Xml;
using BusinessWebSite;
using ModelWebSite.ModelDB;
public partial class Admin_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //int[][] a=new int[3][2];
        if (!X.IsAjaxRequest)
        {
        }
    }
    [DirectMethod]
    public void txtLogin()
    {
        btnLogin_Click(null, null);
    }
    protected void btnLogin_Click(object sender, DirectEventArgs e)
    {
        // Do some Authentication...
        if (HeThongBL.CheckLogin(txtUsername.Text, txtPassword.Text))
        {
            // Then user send to application
            //HeThongBL.UpdateJobInActive();
            Response.Redirect("Default.aspx");
        }
        else
        {
            X.Msg.Alert("Thông báo", "Username or password not correct!").Show();
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtUsername.Focus();
        }
    }
    protected void lnkResetPass_Click(object sender, DirectEventArgs e)
    {
        Response.Redirect("ForgotPassword.aspx");
    }
}