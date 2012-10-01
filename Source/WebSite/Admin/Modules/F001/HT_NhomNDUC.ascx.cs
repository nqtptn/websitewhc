using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using BusinessWebSite;
using ModelWebSite.ModelDB;

public partial class Admin_Modules_F001_HT_NhomNDUC : System.Web.UI.UserControl
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
    }
    
    protected void AddNew()
    {
        hidId.Text = "";
        hidChanged.Text = "0";
        txfTenNhomND.Text = "";
        txaMoTa.Text = "";
    }
    public void ShowAddNew()
    {
        hidChanged.Text = "0";
        AddNew();
        this.winItemInfo.Show();
    }
    public void ShowEdit(object Id)
    {
        hidChanged.Text = "0";
        hidId.Text = Id.ToString();
        UserLevel item = HeThongBL.GetNhomNDByID(hidId.Text);
        // Đọc thông tin dữ liệu từ DB và thể hiện lên giao điện
        //cbbDonVi.SelectedItem.Value = item.Madv;            
        txfTenNhomND.Text = item.userlevelname;
        txaMoTa.Text = item.Note;
        //
        this.winItemInfo.Show();
    }
    [DirectMethod]
    public void SaveItem()
    {
        try
        {
            bool mess = false;
            UserLevel dm = new UserLevel();
            dm.userlevelname = txfTenNhomND.Text;
            dm.Note = txaMoTa.Text;
            dm.IsAdminGroup = false;

            if (!string.IsNullOrEmpty(hidId.Text))
            {
                dm.ID = hidId.Text;
                mess = HeThongBL.UpdateNhomND(dm);
                
            }
            else
            {
                dm.ID = Guid.NewGuid().ToString();
                mess = HeThongBL.AddNhomND(dm);
                
            }
            hidId.Text = dm.ID.ToString();
            if (mess)
                X.MessageBox.Alert("Thông báo", "Finished").Show();
            else
                X.MessageBox.Alert("Thông báo", "Error").Show();
        }
        catch (Exception ex)
        {
            hidChanged.Text = "0";
            X.MessageBox.Alert("Thông báo", ex.ToString()).Show();
        }
    }
}