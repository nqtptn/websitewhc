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

public partial class Admin_Modules_F001_HT_NhomND : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            bindFirst();
            bindSearch();
            View();
        }
    }
    protected void bindFirst()
    {
        //HtDonViCollection colDonVi = HtDonViController.getTrees(Session["Madv"].ToString());
        //stDonVi.DataSource = colDonVi;
        //stDonVi.DataBind();
        //hidMadv.Text = Session["Madv"].ToString();
    }
    protected void bindSearch()
    {
        //XmlDocument xml = new XmlDocument();
        //xml.Load(Server.MapPath("HT_NhomND.xml"));
        //foreach (XmlNode node in xml.SelectNodes("Columns/Column"))
        //{
        //    Ext.Net.ListItem item = new Ext.Net.ListItem();
        //    item.Text = node.Attributes["Header"].InnerText;
        //    item.Value = node.Attributes["DataIndex"].InnerText;
        //    cbSearch.Items.Add(item);
        //}
    }
    protected void stList_RefreshData(object sender, StoreRefreshDataEventArgs e)
    {
        View();
    }
    [DirectMethod]
    public void View()
    {
        //htnhomndcollection colnhom = new htnhomndcollection()
        //    .where(htnhomnd.columns.madv, subsonic.comparison.like, "%" + session["madv"].tostring() + "%")
        //    .load();
        List<UserLevel> colNhom = HeThongBL.GetListNhomND();
        stList.DataSource = colNhom;
        stList.DataBind();
    }
    [DirectMethod]
    public void AddNew()
    {
        HT_NhomNDUD.ShowAddNew();
    }
    [DirectMethod]
    public void Edit()
    {
        RowSelectionModel sm = this.gplList.SelectionModel.Primary as RowSelectionModel;
        foreach (SelectedRow row in sm.SelectedRows)
        {
            HT_NhomNDUD.ShowEdit(row.RecordID);  
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
                HeThongBL.DeleteNhomND(row.RecordID);
            }
            View();
        }
        catch (Exception ex)
        {
            X.Msg.Alert("Thông báo", "Error :" + ex.ToString()).Show();
        }
    }
    [DirectMethod]
    public void Search()
    {
        //HtNhomNDCollection lstItems = new HtNhomNDCollection();
        //if (string.IsNullOrEmpty(cbSearch.SelectedItem.Value))
        //{
        //    lstItems = new HtNhomNDCollection()
        //       .Load();
        //}
        //else
        //{
        //    lstItems = new HtNhomNDCollection()
        //        .Where(cbSearch.SelectedItem.Value, SubSonic.Comparison.Like, "%" + txtSearch.Text + "%")
        //        .Load();
        //}
        //stList.DataSource = lstItems;
        //stList.DataBind();
    }
}