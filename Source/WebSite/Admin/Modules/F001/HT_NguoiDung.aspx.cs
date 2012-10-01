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

public partial class Admin_Modules_F001_HT_NguoiDung : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            bindNhomNd();
        }
    }
    // 
    //protected void CheckMatKhau(object sender, RemoteValidationEventArgs e)
    //{
    //    TextField txfReMatKhau = (TextField)sender;

    //    if (txfReMatKhau.Text != txfMatKhau.Text)
    //    {
    //        e.Success = false;
    //        e.ErrorMessage = "Mật khẩu nhập lại không đúng. Yêu cầu nhập lại";
    //        txfReMatKhau.Text = "";
    //        txfReMatKhau.Focus();
    //    }
    //    //System.Threading.Thread.Sleep(1000);
    //}
    //protected void bindDonVi(string Madv)
    //{
    //    //Session["Madv"]
    //    HtDonViCollection colData = new HtDonViCollection()
    //        .Where(HtDonVi.Columns.Madv, SubSonic.Comparison.Like, Madv + "%")
    //        .Load();
    //    stPB.DataSource = colData;
    //    stPB.DataBind();
    //}
    //protected void bindNhomCB(string Madv)
    //{
    //    List<userlevel> coNhomND = HeThongBL.GetListNhomND();
    //    stNhomND.DataSource = coNhomND;
    //    stNhomND.DataBind();
    //}
    [DirectMethod]
    public void SaveItem()
    {
        //try
        //{
        //    HtNguoiDung item = new HtNguoiDung();
        //    if (!string.IsNullOrEmpty(hidId.Text))
        //    {
        //        item = new HtNguoiDung(hidId.Text);
        //        item.IsNew = false;
        //    }
        //    else
        //    {
        //        item.IsNew = true;
        //        item.MatKhauSalt = UserController.CreateSalt();
        //    }
        //    item.TenDangNhap = txfTenDangNhap.Text;
        //    if (!string.IsNullOrEmpty(txfMatKhau.Text))
        //        item.MatKhau = UserController.CreatePasswordHash(txfMatKhau.Text, item.MatKhauSalt);
        //    item.TenHienThi = txfTenHienThi.Text;
        //    item.Email = Email.Text;
        //    item.IDNhomND = int.Parse(cbbNhomND.SelectedItem.Value);

        //    ///Toanvv start
        //    //if (cbbPB.SelectedIndex != -1)
        //    //    item.Mapb = cbbPB.SelectedItem.Value;
        //    ///Toanvv end
        //    item.Active = cbxActive.Checked;
        //    item.IsAdmin = false;
        //    ///Toanvv start
        //    //if (cbbPB.SelectedIndex != -1)
        //    //{
        //    //    item.Mapb = cbbPB.SelectedItem.Value;
        //    //}
        //    ///Toanvv end
        //    item.Save();
        //    hidChanged.Text = "1";
        //    hidId.Text = item.IDNguoiDung.ToString();
        //    //
        //    View(int.Parse(hidNhom.Text), hidMadv.Text);
        //    //
        //    X.MessageBox.Alert("Thông báo", "Lưu thành công").Show();
        //}
        //catch (Exception ex)
        //{
        //    hidChanged.Text = "0";
        //    X.MessageBox.Alert("Thông báo", ex.ToString()).Show();
        //}
    }
    protected void bindNhomNd()
    {
        tplNhomND.Root.Clear();
        // Nhom quan tri he thong                
        //HtDonVi dvRoot = new HtDonVi(Session["Madv"]);
        Ext.Net.TreeNode tneAdminGroup = new Ext.Net.TreeNode();
        tneAdminGroup.Text = "UserGroup";
        // Neu dvRoot la don vi cap quan tri he thong(đơn vị cao nhất)
        //if (string.IsNullOrEmpty(dvRoot.Madvql))
        //{
        //    Ext.Net.TreeNode tneNhomQTHT = new Ext.Net.TreeNode();
        //    tneNhomQTHT.NodeID = "tne" + dvRoot.Madv;
        //    tneNhomQTHT.Text = "Quản trị Hệ Thống";
        //    // Lay danh sach nhom nguoi dung quan tri he thong             
        //    HtNhomNDCollection lstNhomNDQT = new HtNhomNDCollection()
        //                                    .Where(HtNhomND.Columns.Madv, dvRoot.Madv)
        //                                    .OrderByAsc(HtNhomND.Columns.Madv)
        //                                    .Load();
        //    foreach (HtNhomND nhom in lstNhomNDQT)
        //    {
        //        Ext.Net.TreeNode tneNhom = new Ext.Net.TreeNode();
        //        tneNhom.Text = nhom.TenNhomND;
        //        tneNhom.NodeID = "tne" + nhom.IDNhomND.ToString();
        //        tneNhom.Listeners.Click.Handler = string.Format("Ext.net.DirectMethods.View('{0}','{1}');", nhom.IDNhomND, nhom.Madv);
        //        tneNhomQTHT.Nodes.Add(tneNhom);
        //    }
        //    tneAdminGroup.Nodes.Add(tneNhomQTHT);
        //    // Danh sach nhom cac cong ty truc thuoc
        //    HtDonViCollection lstCompanyGroup = new HtDonViCollection()
        //                            .Where(HtDonVi.Columns.Madvql, dvRoot.Madv)
        //                            .OrderByAsc(HtDonVi.Columns.Madv)
        //                            .Load();
        //    foreach (HtDonVi company in lstCompanyGroup)
        //    {
        //        Ext.Net.TreeNode tneCompany = new Ext.Net.TreeNode();
        //        tneCompany.Text = company.TenDonVi;
        //        tneCompany.NodeID = "tne" + company.Madv;
        //        // Lay danh sach nhom nguoi dung             
        //        HtNhomNDCollection lstNhomND = new HtNhomNDCollection()
        //                                        .Where(HtNhomND.Columns.Madv, company.Madv)
        //                                        .Load();
        //        foreach (HtNhomND nhom in lstNhomND)
        //        {
        //            Ext.Net.TreeNode tneNhom = new Ext.Net.TreeNode();
        //            tneNhom.Text = nhom.TenNhomND;
        //            tneNhom.NodeID = "tne" + nhom.IDNhomND.ToString();
        //            tneNhom.Listeners.Click.Handler = string.Format("Ext.net.DirectMethods.View('{0}','{1}');", nhom.IDNhomND, nhom.Madv);
        //            tneCompany.Nodes.Add(tneNhom);
        //        }
        //        tneAdminGroup.Nodes.Add(tneCompany);
        //    }
        //}
        //// Neu dvRoot la 1 cong ty thi chi tai danh sach nhom nguoi dung
        //else
        //{
        // Lay danh sach nhom nguoi dung             
        List<UserLevel> lstNhomND = HeThongBL.GetListNhomND();

        Ext.Net.TreeNode tneall = new Ext.Net.TreeNode();
        tneall.Text = "All";
        tneall.NodeID = "all";
        tneall.Listeners.Click.Handler = string.Format("Ext.net.DirectMethods.View('{0}');", "all");
        tneAdminGroup.Nodes.Add(tneall);
        foreach (UserLevel nhom in lstNhomND)
        {
            Ext.Net.TreeNode tneNhom = new Ext.Net.TreeNode();
            tneNhom.Text = nhom.userlevelname;
            tneNhom.NodeID = "tne" + nhom.ID.ToString();
            tneNhom.Listeners.Click.Handler = string.Format("Ext.net.DirectMethods.View('{0}');", nhom.ID);
            tneAdminGroup.Nodes.Add(tneNhom);
        }
        //}

        tplNhomND.Root.Add(tneAdminGroup);
        tplNhomND.ExpandAll();
    }
    protected void bindSearch()
    {
        XmlDocument xml = new XmlDocument();
        xml.Load(Server.MapPath("HT_NguoiDung.xml"));
        foreach (XmlNode node in xml.SelectNodes("Columns/Column"))
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem();
            item.Text = node.Attributes["Header"].InnerText;
            item.Value = node.Attributes["DataIndex"].InnerText;
            //cbSearch.Items.Add(item);
        }
    }
    protected void stList_RefreshData(object sender, StoreRefreshDataEventArgs e)
    {
        View(hidNhom.Text);
    }
    [DirectMethod]
    public void View(string IdNhom)
    {
        hidNhom.Text = IdNhom.ToString();
        List<Employee> colNguoiDung = new List<Employee>();
        if (!string.IsNullOrEmpty(IdNhom))
        {
            if (IdNhom == "all")
            {
                colNguoiDung = HeThongBL.GetListNguoiDung();
            }
            else
            {
                colNguoiDung = HeThongBL.GetListNguoiDung().Where(item => item.IDuserlevels == IdNhom).ToList();
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
                Employee emp=HeThongBL.GetNguoidungByID(row.RecordID);
                DeleteOldFile(emp);
                HeThongBL.DeleteNguoiDung(row.RecordID);
            }
            View(hidNhom.Text);
        }
        catch (Exception ex)
        {
            X.Msg.Alert("Thông báo", "Error :" + ex.ToString()).Show();
        }
    }
    protected void DeleteOldFile(Employee emp)
    {
        if (!string.IsNullOrEmpty(emp.Photo))
        {
            string url = MapPath(String.Format("~/admin/Modules/f001/images/{0}", emp.Photo));
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
            HT_NguoiDungUD.ShowAddNew();
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
                HT_NguoiDungUD.ShowEdit(row.RecordID);
            }
        }
        catch (Exception ex)
        {
            X.Msg.Alert("Thông báo", "Error :" + ex.ToString()).Show();
        }
    }
}