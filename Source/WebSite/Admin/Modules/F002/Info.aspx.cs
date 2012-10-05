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

public partial class Admin_Modules_F002_Info : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            bindNhomNd();
            bindSearch();
        }
    }

    protected void bindSearch()
    {
        XmlDocument xml = new XmlDocument();
        xml.Load(Server.MapPath("Info.xml"));
        foreach (XmlNode node in xml.SelectNodes("Columns/Column"))
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem();
            item.Text = node.Attributes["Header"].InnerText;
            item.Value = node.Attributes["DataIndex"].InnerText;
            cbSearch.Items.Add(item);
        }
    }
    protected void bindNhomNd()
    {
        tplNhomND.Root.Clear();
        Ext.Net.TreeNode tneAdminGroup = new Ext.Net.TreeNode();
        tneAdminGroup.Text = "Tất cả";
        tneAdminGroup.NodeID = "all";
        List<Category> listFirst = HeThongBL.GetListCategory();
        List<Category> listRoot = listFirst.Where(item => item.IDParent == "0").ToList();
        foreach (Category company in listRoot)
        {
            Ext.Net.TreeNode tneCompany = new Ext.Net.TreeNode();
            tneCompany.Text = company.Name;
            tneCompany.NodeID = "tne" + company.ID;
            tneCompany.Listeners.Click.Handler = string.Format("Ext.net.DirectMethods.View('{0}');", company.ID);
            // Lay danh sach nhom nguoi dung                 
            List<Category> listSub = listFirst.Where(item => item.IDParent == company.ID).ToList();
            foreach (Category sub in listSub)
            {
                Ext.Net.TreeNode tneNhom = new Ext.Net.TreeNode();
                tneNhom.Text = sub.Name;
                tneNhom.NodeID = "tne" + sub.ID.ToString();
                tneNhom.Listeners.Click.Handler = string.Format("Ext.net.DirectMethods.View('{0}');", sub.ID);
                tneCompany.Nodes.Add(tneNhom);
            }
            tneAdminGroup.Nodes.Add(tneCompany);
        }
        //}
        tplNhomND.Root.Add(tneAdminGroup);
        tplNhomND.ExpandAll();
    }
    protected void stList_RefreshData(object sender, StoreRefreshDataEventArgs e)
    {
        View(hidNhom.Text);
    }
    [DirectMethod]
    public void View(string IdNhom)
    {
        hidNhom.Text = IdNhom.ToString();
        List<Product> colNguoiDung = new List<Product>();
        if (!string.IsNullOrEmpty(IdNhom))
        {
            if (IdNhom == "all")
            {
                colNguoiDung = HeThongBL.GetListProduct();
            }
            else
            {
                colNguoiDung = HeThongBL.GetListProduct().Where(item => item.CategoryID == IdNhom).ToList();
            }
            stList.DataSource = colNguoiDung;
            stList.DataBind();
        }
        else
        {
            stList.DataSource = null;
            stList.DataBind();
        }

    }

    [DirectMethod]
    public void Delete()
    {
        try
        {
            RowSelectionModel sm = this.gplList.SelectionModel.Primary as RowSelectionModel;
            foreach (SelectedRow row in sm.SelectedRows)
            {
                // Xoa dong chon theo key: row.RecordID
                Info emp = HeThongBL.GetInfoByID(row.RecordID);
                DeleteOldFile(emp);
                HeThongBL.DeleteInfo(row.RecordID);
            }
            View(hidNhom.Text);
        }
        catch (Exception ex)
        {
            X.Msg.Alert("Thông báo", "Error :" + ex.ToString()).Show();
        }
    }
    protected void DeleteOldFile(Info emp)
    {
        if (!string.IsNullOrEmpty(emp.Image))
        {
            string url = MapPath(String.Format("~/Info/{0}", emp.Image));
            try
            {
                System.IO.File.Delete(url);
            }
            catch { }
        }
    }
    [DirectMethod]
    public void addItem()
    {
        try
        {
            InfoUC.ShowAddNew();
        }
        catch (Exception ex)
        {
            X.Msg.Alert("Thông báo", "Error :" + ex.ToString()).Show();
        }
    }
    [DirectMethod]
    public void editItem()
    {
        try
        {

            RowSelectionModel sm = this.gplList.SelectionModel.Primary as RowSelectionModel;
            foreach (SelectedRow row in sm.SelectedRows)
            {
                InfoUC.ShowEdit(row.RecordID);
            }
        }
        catch (Exception ex)
        {
            X.Msg.Alert("Thông báo", "Error :" + ex.ToString()).Show();
        }
    }
    [DirectMethod]
    public void Search()
    {
        List<Info> listCan = new List<Info>();
        if (cbSearch.SelectedItem.Value.ToString() == "Name")
        {
            listCan = HeThongBL.GetListInfo().Where(item => item.Name.Contains(txtSearch.Text)).ToList();
        }
        else
        {
            listCan = HeThongBL.GetListInfo().Where(item => item.CreatedBy.Contains(txtSearch.Text)).ToList();
        }
        stList.DataSource = listCan;
        stList.DataBind();
    }
}