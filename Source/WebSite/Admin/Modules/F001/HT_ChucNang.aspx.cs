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

public partial class Admin_Modules_F001_HT_ChucNang : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            bindSearch();
            View();
        }
    }
    protected void bindSearch()
    {
        //XmlDocument xml = new XmlDocument();
        //xml.Load(Server.MapPath("HT_ChucNang.xml"));
        //foreach (XmlNode node in xml.SelectNodes("Columns/Column"))
        //{
        //    Ext.Net.ListItem item = new Ext.Net.ListItem();
        //    item.Text = node.Attributes["Header"].InnerText;
        //    item.Value = node.Attributes["DataIndex"].InnerText;
        //    cbSearch.Items.Add(item);
        //}
    }
    [DirectMethod]
    public void View()
    {
        List<Funtion> colData = HeThongBL.GetListChucNang();
        stList.DataSource = colData;
        stList.DataBind();

    }
    protected void stList_RefreshData(object sender, StoreRefreshDataEventArgs e)
    {
        View();
    }
    [DirectMethod]
    public void AddNew()
    {
        HT_ChucNangUD1.ShowAddNew();
        HT_ChucNangUD1.LoadDSChucNang();
    }
    [DirectMethod]
    public void Edit()
    {
        HT_ChucNangUD1.LoadDSChucNang();
        RowSelectionModel sm = this.gvList.SelectionModel.Primary as RowSelectionModel;
        HT_ChucNangUD1.ShowEdit(sm.SelectedRow.RecordID);
    }
    [DirectMethod]
    public void Delete()
    {
        try
        {
            RowSelectionModel sm = this.gvList.SelectionModel.Primary as RowSelectionModel;
            foreach (SelectedRow row in sm.SelectedRows)
            {
                // Xoa dong chon theo key: row.RecordID
                bool test = HeThongBL.DeleteChucNang(row.RecordID.ToString().Trim());

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
        ////if (cbSearch.SelectedItem.Value.ToString() == "Ma")
        ////{
            
        ////    stList.DataSource = gvList.Store.Where(cbSearch.SelectedItem.Value,SubSonic.Comparison.Like,"%"+txtSearch.Text+"%");
        ////    stList.DataBind();
        ////}
        ////else
        ////{
        ////List<funtion> listf=HeThongBL.GetListChucNang().Where(item => SqlMethods.Like(item.funtionsname, "%" + txtSearch.Text + "%")).ToList();
        //stList.DataSource = HeThongBL.GetListChucNang().Where(item=>item.funtionsname.Contains(txtSearch.Text)).ToList();
        //    //stList.DataSource = gvList.GetStore()..Where(item => SqlMethods.Like(cbSearch.SelectedItem.Value, "%"+txtSearch.Text+"%"));
        //   stList.DataBind();
        ////}
        ////DataView dataview = gvList.Store.Primary as DataView;
        ////HtChucNangCollection colData = null;
        ////if (cbSearch.SelectedItem.Value.ToString() == "Ma")
        ////    colData = new HtChucNangCollection()
        ////        .Where(HtChucNang.Columns.KyHieu, SubSonic.Comparison.Like, "%" + txtSearch.Text + "%")
        ////        .OrderByAsc(HtChucNang.Columns.MaThuTu)
        ////        .Load();
        ////else
        ////    colData = new HtChucNangCollection()
        ////    .Where(HtChucNang.Columns.TenHienThi, SubSonic.Comparison.Like, "%" + txtSearch.Text + "%")
        ////    .OrderByAsc(HtChucNang.Columns.MaThuTu)
        ////    .Load();
        ////stList.DataSource = colData;
        ////stList.DataBind();
    }
}