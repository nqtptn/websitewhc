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
public partial class Admin_Modules_F001_HT_PQNhom : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            bindSearch();
            bindNhomNd();
        }
    }
    protected void bindNhomNd()
    {
        tplNhomND.Root.Clear();
        // Nhom quan tri he thong                
        //HtDonVi dvRoot = new HtDonVi(Session["Madv"]);
        Ext.Net.TreeNode tneAdminGroup = new Ext.Net.TreeNode();
        tneAdminGroup.Text = "UserGroup";
        // Neu dvRoot la don vi cap quan tri he thong(đơn vị cao nhất)

            Ext.Net.TreeNode tneNhomQTHT = new Ext.Net.TreeNode();
            //tneNhomQTHT.NodeID = "tne" + dvRoot.Madv;
            tneNhomQTHT.Text = "System";
            // Lay danh sach nhom nguoi dung quan tri he thong             
            List<UserLevel> lstNhomNDQT = HeThongBL.GetListUserlevel();
            foreach (UserLevel nhom in lstNhomNDQT)
            {
                Ext.Net.TreeNode tneNhom = new Ext.Net.TreeNode();
                tneNhom.Text = nhom.userlevelname;
                tneNhom.NodeID = "tne" + nhom.ID.ToString();
                tneNhom.Listeners.Click.Handler = string.Format("Ext.net.DirectMethods.View('{0}');", nhom.ID);
                tneNhomQTHT.Nodes.Add(tneNhom);
            }
            tneAdminGroup.Nodes.Add(tneNhomQTHT);
            // Danh sach nhom cac cong ty truc thuoc
           
        tplNhomND.Root.Add(tneAdminGroup);
        tplNhomND.ExpandAll();
    }
    protected void bindSearch()
    {
        //XmlDocument xml = new XmlDocument();
        //xml.Load(Server.MapPath("M001.xml"));
        //foreach (XmlNode node in xml.SelectNodes("cols/col"))
        //{
        //    Ext.Net.ListItem item = new Ext.Net.ListItem();
        //    item.Text = node.Attributes["value"].InnerText;
        //    item.Value = node.Attributes["name"].InnerText;
        //    cbSearch.Items.Add(item);
        //}
    }
    [DirectMethod]
    public void View(string IdNhom)
    {
        lblLoading.Html = "";
        //hidMadv.Text = Madv;
        hidNhom.Text = IdNhom.ToString();
        if (!string.IsNullOrEmpty(IdNhom))
        {
            //userlevel nhomR0 = new HtNhomND(Session["IDNhomND"]);
            //StoredProcedure sp = SPs.SpPQNhom(nhomR0.IDNhomND, IdNhom);
            //DataSet dsData = new DataSet();
            List<Sps_PhanQuyenResult> dsData = HeThongBL.GetListFuntionGroupUser(IdNhom);
            stList.DataSource = dsData;
            stList.DataBind();
        }
        else
        {
            stList.RemoveAll();
            stList.DataBind();
        }


    }
    [DirectMethod]
    public void Delete(string IdNhom)
    {
        HeThongBL.DeleteUserLevelPermissionByIDNhomND(IdNhom);
    }
    [DirectMethod]
    public void SavePhanQuyen(string IdNhom, string rows)
    {
        try
        {
            //HtPQNhomCollection colPQ = new HtPQNhomCollection();        
            HeThongBL.DeleteUserLevelPermissionByIDNhomND(IdNhom);
            List<UserLevelPermission> list = new List<UserLevelPermission>();
            List<JsonObject> jsonValues = JSON.Deserialize<List<JsonObject>>(rows);
            foreach (JsonObject jo in jsonValues)
            {

                UserLevelPermission userp = new UserLevelPermission();
                userp.ID = Guid.NewGuid().ToString();
                userp.IDfuntions = jo["ID"].ToString();
                userp.IDuserlevels = IdNhom;
                //userp.Add = Convert.ToBoolean(jo["Add"]);
                //userp.Browse = Convert.ToBoolean(jo["Browse"]);
                //userp.Delete = Convert.ToBoolean(jo["Delete"]);
                //userp.Edit = Convert.ToBoolean(jo["Edit"]);
                userp.Read = Convert.ToBoolean(jo["Read"]);
                //userp.Search = Convert.ToBoolean(jo["Search"]);
                list.Add(userp);
            }
            HeThongBL.AddListUserlevelpermission(list);
            lblLoading.Html = "<b>&nbsp &nbsp &nbsp &nbsp Finished<b>";
            stList.CommitChanges();
            //X.Msg.Alert("Thông báo", "Đã lưu thành công").Show();

        }
        catch (Exception ex)
        {
            X.Msg.Alert("Thông báo", "Error :" + ex.ToString()).Show();
        }
    }
    [DirectMethod]
    public void Save(int i, int n, string IdNhom, string IdCn, bool Xem, bool Them, bool Sua, bool Xoa, bool Tim, bool Duyet)
    {
        try
        {
            ////HtPQNhomCollection colPQ = new HtPQNhomCollection();            
            //HtPQNhom pq = new HtPQNhom();
            //pq.IsNew = true;
            //pq.IDNhomND = IdNhom;
            //pq.IDChucNang = IdCn;
            //pq.Xem = Xem;
            //pq.Them = Them;
            //pq.Sua = Sua;
            //pq.Xoa = Xoa;
            //pq.Tim = Tim;
            //pq.Duyet = Duyet;
            //pq.Save();
            ////
            //if (i == n - 1)
            //{
            //    lblLoading.Html = "<b>&nbsp &nbsp &nbsp &nbsp Đã lưu thành công<b>";
            //    stList.CommitChanges();
            //    //X.Msg.Alert("Thông báo", "Đã lưu thành công").Show();
            //}
            UserLevelPermission userp = new UserLevelPermission();
            userp.ID = Guid.NewGuid().ToString();
            userp.IDfuntions = IdCn;
            userp.IDuserlevels = IdNhom;
            //userp.Add = Them;
            //userp.Browse = Duyet;
            //userp.Delete = Xoa;
            //userp.Edit = Sua;
            userp.Read = Xem;
            //userp.Search = Duyet;
            HeThongBL.AddUserlevelpermission(userp);
            if (i == n - 1)
            {
                lblLoading.Html = "<b>&nbsp &nbsp &nbsp &nbsp Finished<b>";
                stList.CommitChanges();
                //X.Msg.Alert("Thông báo", "Đã lưu thành công").Show();
            }
           // hidNhom.tex
        }
        catch (Exception ex)
        {
            X.Msg.Alert("Thông báo", "Error :" + ex.ToString()).Show();
        }
    }
}