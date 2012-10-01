using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BusinessWebSite;
using CommonWebSite;

public partial class Admin_Modules_F001_HT_BackupDatabase : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindGrid();
        }
    }
    protected void BindGrid()
    {
        BackupFileCollection collection = GetAllBackupFiles();
        gvBackups.DataSource = collection;
        gvBackups.DataBind();
    }
    protected string GetFileSizeInfo(long byteCount)
    {
        return string.Format("{0:F2} Mb", byteCount / 1024f / 1024f);
    }
    protected void gvBackups_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBackups.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    public BackupFileCollection GetAllBackupFiles()
    {
        var collection = new BackupFileCollection();

        string path = string.Format("{0}Admin\\Modules\\F001\\backups\\", HttpContext.Current.Request.PhysicalApplicationPath);
        foreach (var fullFileName in System.IO.Directory.GetFiles(path))
        {
            var fileName = Path.GetFileName(fullFileName);
            if (fileName == "Index.htm")
                continue;

            var info = new FileInfo(fullFileName);
            collection.Add(new BackupFile()
            {
                FullFileName = fullFileName,
                FileName = fileName,
                FileSize = info.Length
            });
        }

        return collection;
    }
    protected void imbAdd_Click(object sender, ImageClickEventArgs e)
    {
        string fileName = string.Format("database_{0}_{1}.bak", DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"), CommonHelper.GenerateRandomDigitCode(4));
        BackupHelper bk = new BackupHelper();
        lblMessage.Text = bk.BackupDatabase("kimbinhdien_danone", "kimbinhdien_danone", "kimbinhdien", "kimbinhdien.com.vn, 1455", Server.MapPath("backups/" + fileName));
        BindGrid();
    }
    protected void DownloadButton_OnCommand(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "Download")
        {
            string path = string.Format("{0}Admin\\Modules\\F001\\backups\\", HttpContext.Current.Request.PhysicalApplicationPath);
            string fileName = e.CommandArgument.ToString();
            Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
            Response.TransmitFile(Server.MapPath("backups/" + fileName));
            Response.End();
        }
    }
    protected void RestoreButton_OnCommand(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "Restore")
        {
            try
            {
                string fileName = e.CommandArgument.ToString();
                RestoreHelper rs = new RestoreHelper();
                rs.RestoreDatabase("kimbinhdien_danone", "kimbinhdien_danone", "kimbinhdien", "kimbinhdien.com.vn, 1455", Server.MapPath("backups/" + fileName));
                lblMessage.Text = "Backup for database " + fileName + " successful!";
            }
            catch (Exception exc)
            {
                lblMessage.Text = "có lỗi: " + exc.Message;
            }
        }
    }
    protected void DeleteButton_OnCommand(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            try
            {
                string fileName = e.CommandArgument.ToString();
                if (File.Exists(fileName))
                    File.Delete(fileName);
                BindGrid();
                lblMessage.Text = "Deleted Backup successful!";
            }
            catch (Exception exc)
            {
                lblMessage.Text = "có lỗi " + exc.Message;
            }
        }
    }

    protected void gvBackups_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
}
