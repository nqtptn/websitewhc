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

public partial class Admin_Modules_F002_ProductUC : System.Web.UI.UserControl
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            LoadDSProduct();
        }
    }
    protected void AddNew()
    {
        hidChanged.Text = "1";
        txfPhoto.Text = "";
        txfFull.Text = "";
        txtName.Text = "";
        txtShort.Text = "";
        txtViewer.Text = "";
        txtTag.Text = "";
        cbbThuocDanhmuc.SelectedItem.Value = "-1";
        hidId.Text = "-1";
    }
    public void LoadDSProduct()
    {
        List<Category> ds = new List<Category>();
        Category dm = new Category();
        dm.ID = "-1";
        dm.Name = "Chọn Danh mục cha";
        ds.Add(dm);
        List<Category> dschung = HeThongBL.GetListCategory().OrderBy(item => item.Name).ToList();
        foreach (Category i in dschung)
        {
            if (string.IsNullOrEmpty(i.IDParent))
            {
                i.Name = i.Name;
                ds.Add(i);
                AddNode(ds, dschung, i);

            }
        }
        stDm.DataSource = ds;
        stDm.DataBind();
        cbbThuocDanhmuc.SelectedItem.Value = "-1";
    }
    protected void AddNode(List<Category> ds, List<Category> dschung, Category parent)
    {
        foreach (Category i in dschung.FindAll(delegate(Category j) { return j.IDParent == parent.ID; }))
        {
            i.Name = "--" + i.Name;
            ds.Add(i);
            if (!string.IsNullOrEmpty(i.IDParent))
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
        Product cn = HeThongBL.GetProductByID(Id.ToString());
        txfPhoto.Text = cn.Image;
        txfFull.Text = cn.FullDescription;
        txtName.Text = cn.Name;
        txtShort.Text = cn.ShortDescription;
        txtTag.Text = cn.Tag;
        txtViewer.Text = cn.Viewer.ToString();
        if (cn.isComment.HasValue)
            ckbComment.Checked = true;
        else
            ckbComment.Checked = false;

        
        if (!string.IsNullOrEmpty(cn.CategoryID))
            cbbThuocDanhmuc.SelectedItem.Value = cn.CategoryID;


        this.winItemInfo.Show();
    }
    [DirectMethod]
    public void SaveItem()
    {
        try
        {
            bool mess = false;
            Product dm = null;
            if (!string.IsNullOrEmpty(hidId.Text) && hidId.Text != "-1")
            {
                dm = new Product();
                //dm = new HT_ChucNang(Convert.ToInt32(hidId.Text));
                dm.ID = hidId.Text.ToString().Trim();
                dm.CreatedBy = Session["userName"].ToString();
                dm.DateCreated = DateTime.Now;
                dm.Viewer = Convert.ToInt32(txtViewer.Text);
                dm.FullDescription = txfFull.Text;
                dm.Name = txtName.Text;
                dm.ShortDescription = txtShort.Text;
                dm.Tag = txtTag.Text;
                dm.CategoryID = cbbThuocDanhmuc.SelectedItem.Value != "-1" ? cbbThuocDanhmuc.SelectedItem.Value : null;
                //dm.Image = txfPhoto.Text;

                if (!string.IsNullOrEmpty(FileUploadField1.FileName))
                {
                    DeleteOldFile(txfPhoto.Text);
                    dm.Image = FileUpload();
                }
                else
                {
                    dm.Image = string.IsNullOrEmpty(txfPhoto.Text) ? string.Empty : txfPhoto.Text;
                }

                dm.isComment = ckbComment.Checked ? true : false;
                //dm.ShowHomepage = ckbShowHomepage.Checked ? true : false;

                mess = HeThongBL.UpdateProduct(dm);


            }
            else
            {
                dm = new Product();

                dm.ID = Guid.NewGuid().ToString();
                //dm = new HT_ChucNang(Convert.ToInt32(hidId.Text));
                dm.ID = hidId.Text.ToString().Trim();
                dm.CreatedBy = Session["UserName"].ToString();
                dm.DateCreated = DateTime.Now;
                dm.Viewer = Convert.ToInt32(txtViewer.Text);
                dm.FullDescription = txfFull.Text;
                dm.Name = txtName.Text;
                dm.ShortDescription = txtShort.Text;
                dm.Tag = txtTag.Text;
                dm.CategoryID = cbbThuocDanhmuc.SelectedItem.Value != "-1" ? cbbThuocDanhmuc.SelectedItem.Value : null;
                if (!string.IsNullOrEmpty(FileUploadField1.FileName))
                {
                    dm.Image = FileUpload();
                }
                else
                {
                    dm.Image = string.IsNullOrEmpty(txfPhoto.Text) ? string.Empty : txfPhoto.Text;
                }
                dm.isComment = ckbComment.Checked ? true : false;
                //dm.ShowHomepage = ckbShowHomepage.Checked ? true : false;
                mess = HeThongBL.AddProduct(dm);
            }
            hidChanged.Text = "1";
            hidId.Text = dm.ID.ToString();
            if (mess)
                X.MessageBox.Alert("Thông báo", "Thành công").Show();
            else
                X.MessageBox.Alert("Thông báo", "Lỗi").Show();
        }
        catch (Exception ex)
        {
            hidChanged.Text = "0";
            X.MessageBox.Alert("Thông báo", ex.ToString()).Show();
        }

    }

    protected string FileUpload()
    {
        return SaveFile(FileUploadField1.PostedFile, FileUploadField1);
    }

    protected string SaveFile(HttpPostedFile file, FileUploadField filImageUpload)
    {
        string savePath = Server.MapPath("~/admin/Modules/f002/images/");
        string fileName = filImageUpload.FileName;
        string pathToCheck = savePath + fileName;
        string tempfileName = "";
        if (System.IO.File.Exists(pathToCheck))
        {
            int counter = 2;
            while (System.IO.File.Exists(pathToCheck))
            {
                tempfileName = counter.ToString() + fileName;
                pathToCheck = savePath + tempfileName;
                counter++;
            }
            fileName = tempfileName;
        }
        else
        {
        }
        savePath += fileName;
        string imageURL = fileName;
        filImageUpload.PostedFile.SaveAs(savePath);
        return imageURL;
    }
    protected void DeleteOldFile(string emp)
    {
        if (!string.IsNullOrEmpty(emp))
        {
            string url = MapPath(String.Format("~/admin/Modules/f002/images/{0}", emp));
            try
            {
                System.IO.File.Delete(url);
            }
            catch { }
        }
    }
}