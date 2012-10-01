using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using BusinessWebSite;
using ModelWebSite.ModelDB;
using CommonWebSite;

public partial class Admin_Modules_F002_CommentUC : System.Web.UI.UserControl
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
        }
    }
    public void ShowEdit(object Id)
    {
        hidChanged.Text = "0";
        hidId.Text = Id.ToString();
        Comment cn = HeThongBL.GetCommentByID(Id.ToString());
        txfComment.Text = cn.Comment1;
        txtDate.Text = cn.DateCreated.ToString();
        txtEmail.Text = cn.Email;
        txtName.Text = cn.Name;
        txtPhone.Text = cn.Phone;
        txtProduct.Text = HeThongBL.GetProductByID(cn.ProductID).Name;

        this.winItemInfo.Show();
    }
    [DirectMethod]
    public void Delete()
    {
        try
        {

            if (!string.IsNullOrEmpty(hidId.Text.ToString().Trim()))
            {
                if (HeThongBL.DeleteComment(hidId.Text.ToString().Trim()))

                    X.MessageBox.Alert("Thông báo", "Thành công").Show();
                else
                    X.MessageBox.Alert("Thông báo", "Lỗi").Show();
            }
            else
                X.MessageBox.Alert("Thông báo", "Lỗi bài viết null").Show();
        }
        catch (Exception ex)
        {
            hidChanged.Text = "0";
            X.MessageBox.Alert("Thông báo", ex.ToString()).Show();
        }

    }
}