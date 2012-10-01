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

public partial class Admin_Modules_F001_HT_NguoiDungUC : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {

        }
    }
    protected void CheckMatKhau(object sender, RemoteValidationEventArgs e)
    {
        TextField txfReMatKhau = (TextField)sender;

        if (txfReMatKhau.Text != txfMatKhau.Text)
        {
            e.Success = false;
            e.ErrorMessage = "Password not correct, please type again";
            txfReMatKhau.Text = "";
            txfReMatKhau.Focus();
        }
        //System.Threading.Thread.Sleep(1000);
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
        txfTenDangNhap.Text = "";
        txfMatKhau.Text = "";
        txfAddress.Text = "";
        txfNote.Text = "";
        txfPhoneHome.Text = "";
        txfTenHienThi.Text = "";
        txfEmail.Text = "";
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
        //bindDonVi(Madv);
        hidChanged.Text = "0";
        hidId.Text = Id.ToString();
        Employee item = HeThongBL.GetNguoidungByID(hidId.Text);
        // Đọc thông tin dữ liệu từ DB và thể hiện lên giao điện
       cbbNhomND.SelectedItem.Value = item.IDuserlevels.ToString();
        txfTenDangNhap.Text = item.Username;
        //txfMatKhau.Text = item.MatKhau;
        //txfReMatKhau.Text = item.MatKhau;
        txfTenHienThi.Text = item.FullName;
        txfEmail.Text = item.Email;
        txfNote.Text = item.Notes;
        txfAddress.Text = item.Address;
        txfPhoneHome.Text = item.HomePhone;
        dtbBirthday.Value = string.IsNullOrEmpty(item.BirthDate.ToString()) ? DateTime.Today : (DateTime)item.BirthDate;
        txfPhoto.Text = item.Photo;
        imgPhoto.ImageUrl = CommonHelper.GetStoreLocation()+ string.Format("Admin/Modules/F001/images/{0}", item.Photo);
        cbxActive.Checked = item.Active.Value;
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
        if(string.IsNullOrEmpty(txfTenDangNhap.Text)||string.IsNullOrEmpty(txfTenHienThi.Text)||string.IsNullOrEmpty(cbbNhomND.SelectedItem.Text))
        {
                X.MessageBox.Alert("Thông báo", "empty").Show();
                return false;
        }

        if (string.IsNullOrEmpty(hidId.Text))
        {
            List<Employee> listemp = HeThongBL.GetListNguoiDung();
            listemp = listemp.Where(item => item.Username == txfTenDangNhap.Text).ToList();
            if (listemp.Count > 0)
            {
                X.MessageBox.Alert("Thông báo", "Username is not availiable").Show();
                return false;
            }
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
                bool mess=false;
                Employee item = new Employee();
                item.Username = txfTenDangNhap.Text;
                item.Password = txfMatKhau.Text == string.Empty ? string.Empty : txfMatKhau.Text;
                item.FullName = txfTenHienThi.Text;
                item.Email = txfEmail.Text == string.Empty ? string.Empty : txfEmail.Text;
                item.Notes = txfNote.Text == string.Empty ? string.Empty : txfNote.Text;
                item.Address = txfAddress.Text == string.Empty ? string.Empty : txfAddress.Text;
                item.HomePhone = txfPhoneHome.Text == string.Empty ? string.Empty : txfPhoneHome.Text;
                item.IDuserlevels = cbbNhomND.SelectedItem.Value;
                item.Active = cbxActive.Checked;
                item.BirthDate = dtbBirthday.Value.ToString() == string.Empty ? new DateTime() : (DateTime)dtbBirthday.Value;
                item.IsAdmin = false;
                //item.Photo = FileUpload();
                if (!string.IsNullOrEmpty(hidId.Text))
                {
                    item.ID = hidId.Text;
                    if (!string.IsNullOrEmpty(FileUploadField1.FileName))
                    {
                        DeleteOldFile(txfPhoto.Text);
                        item.Photo = FileUpload();
                    }
                    else
                    {
                        item.Photo = string.IsNullOrEmpty(txfPhoto.Text) ? string.Empty : txfPhoto.Text;
                    }
                    mess = HeThongBL.UpdateNguoiDung(item);
                }
                else
                {
                    item.ID = Guid.NewGuid().ToString();
                    if (!string.IsNullOrEmpty(FileUploadField1.FileName))
                    {
                        item.Photo = FileUpload();
                    }
                    else
                    {
                        item.Photo = string.IsNullOrEmpty(txfPhoto.Text) ? string.Empty : txfPhoto.Text;
                    }
                    mess = HeThongBL.AddNguoiDung(item);
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
        string savePath = Server.MapPath("~/admin/Modules/f001/images/");
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
            string url = MapPath(String.Format("~/admin/Modules/f001/images/{0}", emp));
            try
            {
                System.IO.File.Delete(url);
            }
            catch { }
        }
    }
}