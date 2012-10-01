using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

/// <summary>
/// Summary description for PageBase
/// </summary>
public class PageBase: System.Web.UI.Page
{
	public PageBase()
	{
		//
		// TODO: Add constructor logic here
		//        
	}
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        if (!IsPostBack)
        {
            if (Session["userName"]==null)
            {
                Response.Redirect(Common.GetSiteRoot()+ "/Admin/Login.aspx" );
            }
        }
    }

}