using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using BusinessWebSite;
using ModelWebSite.ModelDB;

public partial class Admin_Modules_F001_HT_ChucNangUC : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            LoadDSChucNang();
        }
    }
    protected void AddNew()
    {
        hidChanged.Text = "1";
        txtSoTT.Text = "";
        txtKyhieu.Text = "";
        txtTenchucnang.Text = "";
        txtThumucchua.Text = "";
        txtTrangchucnang.Text = "";
        nfCaphienthi.Value = "0";
        cbbThuocChucnang.SelectedItem.Value = "-1";
        hidId.Text = "-1";
    }
    public void LoadDSChucNang()
    {
        List<Funtion> ds = new List<Funtion>();
        Funtion dm = new Funtion();
        dm.ID = "-1";
        dm.funtionsname = "Chọn chức năng";
        ds.Add(dm);
        List<Funtion> dschung = HeThongBL.GetListChucNang().OrderBy(item => item.sequence).ToList();
        foreach (Funtion i in dschung)
        {
            if (string.IsNullOrEmpty(i.IDRoot))
            {
                i.funtionsname = i.funtionsname;
                ds.Add(i);
                AddNode(ds, dschung, i);

            }
        }
        stDm.DataSource = ds;
        stDm.DataBind();
        cbbThuocChucnang.SelectedItem.Value = "-1";
    }
    protected void AddNode(List<Funtion> ds, List<Funtion> dschung, Funtion parent)
    {
        foreach (Funtion i in dschung.FindAll(delegate(Funtion j) { return j.IDRoot == parent.ID; }))
        {
            i.funtionsname = "--" + i.funtionsname;
            ds.Add(i);
            if (!string.IsNullOrEmpty(i.IDRoot))
            {
                AddNode(ds, dschung, i);
            }
        }
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
        Funtion cn = HeThongBL.GetChucnangByID(Id.ToString());
        txtSoTT.Text = cn.sequence;
        txtKyhieu.Text = cn.symbol;
        txtTenchucnang.Text = cn.funtionsname;
        txtThumucchua.Text = cn.folderCN;
        txtTrangchucnang.Text = cn.pageCN;
        if (cn.use.HasValue)
            ckbSuDung.Checked = true;
        else
            ckbSuDung.Checked = false;
        if (!string.IsNullOrEmpty(cn.IDRoot))
            cbbThuocChucnang.SelectedItem.Value = cn.IDRoot;


        this.winItemInfo.Show();
    }
    [DirectMethod]
    public void SaveItem()
    {
        try
        {
            bool mess = false;
            Funtion dm = null;
            if (!string.IsNullOrEmpty(hidId.Text) && hidId.Text != "-1")
            {
                dm = new Funtion();
                //dm = new HT_ChucNang(Convert.ToInt32(hidId.Text));
                dm.ID = hidId.Text.ToString().Trim();
                dm.sequence = txtSoTT.Text;
                dm.funtionsname = txtTenchucnang.Text;
                dm.symbol = txtKyhieu.Text;
                dm.folderCN = txtThumucchua.Text;
                dm.pageCN = txtTrangchucnang.Text;
                if (ckbSuDung.Checked)
                    dm.use = true;
                else
                    dm.use = false;
                if (cbbThuocChucnang.SelectedItem.Value != "-1")
                    dm.IDRoot = cbbThuocChucnang.SelectedItem.Value;
                mess=HeThongBL.UpdateChucNang(dm);


            }
            else
            {
                dm = new Funtion();
                dm.ID = Guid.NewGuid().ToString();
                dm.sequence = txtSoTT.Text;
                dm.funtionsname = txtTenchucnang.Text;
                dm.symbol = txtKyhieu.Text;
                dm.folderCN = txtThumucchua.Text;
                dm.pageCN = txtTrangchucnang.Text;
                if (ckbSuDung.Checked)
                    dm.use = true;
                else
                    dm.use = false;
                if (cbbThuocChucnang.SelectedItem.Value != "-1")
                    dm.IDRoot = cbbThuocChucnang.SelectedItem.Value;
                mess = HeThongBL.AddChucNang(dm);
            }
            hidChanged.Text = "1";
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