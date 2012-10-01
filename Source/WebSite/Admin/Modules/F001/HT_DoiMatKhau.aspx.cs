using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Xml;
using Ext.Net;
using ModelWebSite.ModelDB;
using BusinessWebSite;
using System.Data.Linq.SqlClient;

public partial class Admin_Modules_F001_HT_DoiMatKhau : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            bindFirst();
        }
    }
    protected void bindFirst()
    {
        if (Session["userName"] != null)
        {
            txfTenDangNhap.Text = Session["userName"].ToString();
            if (Session["TenHienThi"] != null)
            {
                txfTenHienThi.Text = Session["TenHienThi"].ToString();
            }
        }

    }
    [DirectMethod]
    public void changeMK()
    {
        if (txfTenDangNhap.Text != "")
        {
            try
            {
                List<Employee> listemp = HeThongBL.GetListNguoiDung();
                Employee emp = listemp.Where(item => item.Username == txfTenDangNhap.Text).SingleOrDefault();
                emp.Password = txfMatKhau.Text;
                HeThongBL.UpdateNguoiDung(emp);
                X.MessageBox.Alert("Thông báo", "Finished").Show();
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Thông báo","Error: " + ex.Message).Show();
            }
        }
    }
    protected void CheckMatKhau(object sender, RemoteValidationEventArgs e)
    {
        TextField txfReMatKhau = (TextField)sender;
        if (txfReMatKhau.Text != txfMatKhau.Text)
        {
            e.Success = false;
            e.ErrorMessage = "Pasword not correct.Please type again";
            txfReMatKhau.Text = "";
            txfReMatKhau.Focus();
        }
        else
        {
            e.Success = true;
        }
    }
}
