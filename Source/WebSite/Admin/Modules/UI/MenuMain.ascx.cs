using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using System.Xml;
using BusinessWebSite;
using ModelWebSite.ModelDB;

public partial class Admin_Modules_UI_MenuMain : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            if (Session["IDNhomND"] != null && Session["IDNhomND"] != string.Empty)
            {
                hidIdNhomND.Text = Session["IDNhomND"].ToString();
                bindMenuV1();
            }
        }
    }
    protected void bindMenuV1()
    {

        List<Funtion> colChucnang = HeThongBL.GetListQuyenByNhom((string)Session["IDNhomND"]);
        var lstMenuR0 = from m in colChucnang
                        where m.IDRoot == null
                        orderby m.sequence ascending
                        select m;
        foreach (Funtion itemR0 in lstMenuR0)
        {
            Ext.Net.Button btnR0 = new Ext.Net.Button(itemR0.funtionsname);
            var lstMenuRi = from m in colChucnang
                            where m.IDRoot == itemR0.ID
                            orderby m.sequence ascending
                            select m;
            if (lstMenuRi.Count() > 0)
            {
                Ext.Net.Menu mnuRi = new Ext.Net.Menu();
                foreach (var itemRi in lstMenuRi)
                {
                    // Add link tro toi trang
                    Ext.Net.MenuItem mnuItem = new Ext.Net.MenuItem(itemRi.funtionsname);
                    string pageUrl = itemRi.folderCN + "/" + itemRi.pageCN + "?fn=" + itemRi.ID.ToString();
                    mnuItem.Listeners.Click.Handler = "loadPage(#{tabPages}, 'mnu" + itemRi.ID.ToString() + "','" + itemRi.funtionsname + "', '" + pageUrl + "');";
                    mnuRi.Add(mnuItem);
                    addSubMenu(colChucnang, mnuItem, itemRi);
                }
                btnR0.Menu.Add(mnuRi);
            }
            else
            {
                // Add link tro toi trang
                string pageUrl = itemR0.folderCN + "/" + itemR0.pageCN + "?fn=" + itemR0.ID.ToString();
                btnR0.Listeners.Click.Handler = "loadPage(#{tabPages}, 'mnu" + itemR0.ID.ToString() + "','" + itemR0.funtionsname + "', '" + pageUrl + "');";
            }
            tbMenu.Items.Add(btnR0);
        }
    }
    protected void addSubMenuV1(List<Funtion> colChucnang, Ext.Net.MenuItem mnuItemR0, Ext.Net.Button btnR0, Funtion menuR0)
    {
        var lstMenuRi = from m in colChucnang
                        where m.IDRoot == menuR0.ID
                        orderby m.sequence ascending
                        select m;

        string pageUrl = "";
        if (lstMenuRi.Count() > 0)
        {
            Ext.Net.Menu mnuRi = new Ext.Net.Menu();
            foreach (Funtion item in lstMenuRi)
            {
                // Add link tro toi trang
                Ext.Net.MenuItem mnuItem = new Ext.Net.MenuItem(item.funtionsname);
                pageUrl = item.folderCN + "/" + item.pageCN + "?fn=" + item.ID.ToString();
                mnuItem.Listeners.Click.Handler = "loadPage(#{tabPages}, 'mnu" + item.ID.ToString() + "','" + item.funtionsname + "', '" + pageUrl + "');";
                mnuRi.Add(mnuItem);
                addSubMenu(colChucnang, mnuItem, item);
            }
            mnuItemR0.Menu.Add(mnuRi);
        }
        else
        {
            // Add link tro toi trang
            pageUrl = menuR0.folderCN + "/" + menuR0.pageCN + "?fn=" + menuR0.ID.ToString();
            mnuItemR0.Listeners.Click.Handler = "loadPage(#{tabPages}, 'mnu" + menuR0.ID.ToString() + "','" + menuR0.funtionsname + "', '" + pageUrl + "');";
        }
    }
    protected void addSubMenu(List<Funtion> colChucnang, Ext.Net.MenuItem mnuItemR0, Funtion menuR0)
    {
        var lstMenuRi = from m in colChucnang
                        where m.IDRoot == menuR0.ID
                        orderby m.sequence ascending
                        select m;

        string pageUrl = "";
        if (lstMenuRi.Count() > 0)
        {
            Ext.Net.Menu mnuRi = new Ext.Net.Menu();
            foreach (var item in lstMenuRi)
            {
                // Add link tro toi trang
                Ext.Net.MenuItem mnuItem = new Ext.Net.MenuItem(item.funtionsname);
                pageUrl = item.folderCN + "/" + item.pageCN + "?fn=" + item.ID.ToString();
                mnuItem.Listeners.Click.Handler = "loadPage(#{tabPages}, 'mnu" + item.ID.ToString() + "','" + item.funtionsname + "', '" + pageUrl + "');";
                mnuRi.Add(mnuItem);
                addSubMenu(colChucnang, mnuItem, item);
            }
            mnuItemR0.Menu.Add(mnuRi);
        }
        else
        {
            // Add link tro toi trang
            pageUrl = menuR0.folderCN + "/" + menuR0.pageCN + "?fn=" + menuR0.ID.ToString();
            mnuItemR0.Listeners.Click.Handler = "loadPage(#{tabPages}, 'mnu" + menuR0.ID.ToString() + "','" + menuR0.funtionsname + "', '" + pageUrl + "');";
        }
    }
}