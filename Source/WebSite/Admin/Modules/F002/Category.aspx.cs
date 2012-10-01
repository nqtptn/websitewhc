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

public partial class Admin_Modules_F002_Category : PageBase
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
        List<Category> colData = HeThongBL.GetListCategory();
        List<Category> colDataSource = new List<Category>();
        if (colData.Count > 0)
        {
            List<Category> colDataTemp = colData;
            foreach (Category cate in colData)
            {
                if (!string.IsNullOrEmpty(cate.IDParent))
                    cate.IDParent = colDataTemp.Where(item => item.ID == cate.IDParent).Select(item=>item.Name).SingleOrDefault();
                colDataSource.Add(cate);
            }
        }
        stList.DataSource = colDataSource;
        stList.DataBind();

    }
    protected void stList_RefreshData(object sender, StoreRefreshDataEventArgs e)
    {
        View();
    }
    [DirectMethod]
    public void AddNew()
    {
        CategoryUC.ShowAddNew();
        CategoryUC.LoadDSCategory();
    }
    [DirectMethod]
    public void Edit()
    {
        CategoryUC.LoadDSCategory();
        RowSelectionModel sm = this.gvList.SelectionModel.Primary as RowSelectionModel;
        CategoryUC.ShowEdit(sm.SelectedRow.RecordID);
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
                bool test = HeThongBL.DeleteCategory(row.RecordID.ToString().Trim());

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
    //[DirectMethod]
    //public string GetParent(string value)
    //{
    //    try
    //    {
    //        Category cate = HeThongBL.GetCategoryByID(value);
    //        if (cate != null)
    //            return cate.Name;
    //        return "";
    //    }
    //    catch (Exception)
    //    {
    //        return "";
    //    }
    //}

}