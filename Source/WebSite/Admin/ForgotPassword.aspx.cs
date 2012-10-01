using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Xml;
using Ext.Net;
using ModelWebSite.ModelDB;
using BusinessWebSite;
using System.Data.Linq.SqlClient;

public partial class Admin_ForgotPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [DirectMethod]
    public void redirect()
    {
        Response.Redirect("Login.aspx");
    }
    [DirectMethod]
    public void ResetPassToCTV(string userName, string email)
    {
        if (Email.Text == "" || !Regex.IsMatch(Email.Text, @"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"))
        {
            Email.Icon = Icon.Exclamation;
            return;
        }
        else Email.Icon = Icon.None;

        // Kiem tra ten dang nhap va email co trung khop nhau moi send mat khau
        List<Employee> colUser = new List<Employee>();
        colUser = colUser.Where(item => item.Username != userName || item.Email != email).ToList();
        if (colUser.Count == 0)
        {
            X.Msg.Alert("Thông báo", "Người dùng mà emal không đúng!Xin thử lại").Show();
            return;
        }
        try
        {
            
        }
        catch (Exception ex)
        {
            X.Msg.Alert("Thông báo", "Lỗi: " + ex.Message).Show();
            return;
        }

    }
}