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

public partial class Admin_Modules_F003_CollectionImage : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {

        }
    }
    protected void bindFirst()
    {
        List<UserLevel> coNhomND = HeThongBL.GetListNhomND();
        stNhomND.DataSource = coNhomND;
        stNhomND.DataBind();
        hidIsLoadNhomND.Text = "1";
    }

    protected void AddNew()
    {
        hidChanged.Text = "0";
        hidId.Text = "";
        txfName.Text = "";
        txtTitle.Text = "";
    }
    public void ShowAddNew()
    {
        hidChanged.Text = "0";
        bindFirst();
        //bindDonVi(Madv);
        AddNew();
        this.winItemInfo.Show();
    }
    public void ShowEdit(object Id)
    {
        bindFirst();
        hidChanged.Text = "0";
        hidId.Text = Id.ToString();
        CollectionImage item = HeThongBL.GetCollectionImageByID(hidId.Text);
        // Đọc thông tin dữ liệu từ DB và thể hiện lên giao điện
        cbbImageLibrary.SelectedItem.Value = item.ImageLibraryID.ToString();
        txfName.Text = item.Name;
        txtTitle.Text = item.Title;
        imgPhoto.ImageUrl = CommonHelper.GetStoreLocation() + string.Format("Collecttion/{0}", item.Image);
        //
        this.winItemInfo.Show();
    }
    [DirectMethod]
    public void SaveAndNew()
    {
        SaveItem();
        AddNew();
        hidChanged.Text = "1";
    }
    public bool CheckValue()
    {
        if (string.IsNullOrEmpty(txfName.Text) || string.IsNullOrEmpty(txtTitle.Text))
        {
            X.MessageBox.Alert("Thông báo", "empty").Show();
            return false;
        }
        return true;
    }
    [DirectMethod]
    public void SaveItem()
    {
        try
        {
            if (CheckValue())
            {
                bool mess = false;
                CollectionImage item = new CollectionImage();
                item.Name = txfName.Text;
                item.Title = txtTitle.Text;
                //item.Photo = FileUpload();
                if (!string.IsNullOrEmpty(hidId.Text))
                {
                    item.ID = hidId.Text;
                    if (!string.IsNullOrEmpty(FileUploadField1.FileName))
                    {
                        DeleteOldFile(txfPhoto.Text);
                        item.Image = FileUpload();
                    }
                    else
                    {
                        item.Image = string.IsNullOrEmpty(txfPhoto.Text) ? string.Empty : txfPhoto.Text;
                    }
                    mess = HeThongBL.UpdateCollectionImage(item);
                }
                else
                {
                    item.ID = Guid.NewGuid().ToString();
                    if (!string.IsNullOrEmpty(FileUploadField1.FileName))
                    {
                        item.Image = FileUpload();
                    }
                    else
                    {
                        item.Image = string.IsNullOrEmpty(txfPhoto.Text) ? string.Empty : txfPhoto.Text;
                    }
                    mess = HeThongBL.AddCollectionImage(item);
                }

                hidId.Text = item.ID.ToString();
                if (mess)
                    X.MessageBox.Alert("Thông báo", "Finished").Show();
                else
                    X.MessageBox.Alert("Thông báo", "Error").Show();
            }
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
        string savePath = Server.MapPath("~/Collecttion/");
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
            string url = MapPath(String.Format("~/Collecttion/{0}", emp));
            try
            {
                System.IO.File.Delete(url);
            }
            catch { }
        }
    }
     
}