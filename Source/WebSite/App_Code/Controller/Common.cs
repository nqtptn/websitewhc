using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using Ext.Net;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Data;



/// <summary>
/// Summary description for Common
/// </summary>
public class Common
{
	public Common()
	{
		
	}
    private static NumberFormatInfo _NumberFormatEN;
    private static NumberFormatInfo _NumberFormatVN;
    public static NumberFormatInfo NumberFormatEN
    {

        get
        {
            if (_NumberFormatEN == null)
            {
                _NumberFormatEN = new NumberFormatInfo();
                _NumberFormatEN.NumberDecimalSeparator = ".";
                _NumberFormatEN.CurrencyGroupSeparator = ",";
            }
            return _NumberFormatEN;
        }
    }
    public static string randPass(int len)
    {
        string passwordString = "";
        string allowedChars = "";
        allowedChars = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";
        allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
        //allowedChars += "1,2,3,4,5,6,7,8,9,0,!,@,#,$,%,&,?";
        allowedChars += "1,2,3,4,5,6,7,8,9,0";
        char[] sep = { ',' };
        string[] arr = allowedChars.Split(sep);
        string temp = "";
        Random rand = new Random();
        for (int i = 0; i < len; i++)
        {
            temp = arr[rand.Next(0, arr.Length)];
            passwordString += temp;
        }
        return passwordString;
    }
    public static string getEmailAdmin()
    {
        //string kq = "xoth.it@evnspc.vn";
        //HeThongCollection colHT = new HeThongCollection()
        //        .Load();
        //kq = colHT[0].EmailAdmin;
        //return kq;
        return "";
    }
    public static string sendMailLV(string fromDisplay, string from, string to, string subject, string body)
    {
        string s = string.Empty;
        try
        {
            //HeThongCollection colHT = new HeThongCollection()
            //    .Load();
            //if (colHT.Count > 0)
            //{
            //    s = "Mail của bạn đã được gửi!";
            //    string host = colHT[0].SMTPServer;                 
            //    string user = colHT[0].UserMailSMTP;
            //    string password = colHT[0].PassMailSMTP;
            //    int port = int.Parse(colHT[0].Port);
            //    string sender = fromDisplay;
            //    // AuthTypes: "Basic", "NTLM", "Digest", "Kerberos", "Negotiate"
            //    string authType = "Basic";                
            //    WebUtility.Utilities.SendEmail(from, fromDisplay, to, from, "", subject, body, System.Text.Encoding.UTF8, System.Text.Encoding.UTF8, true,
            //                               host, user, password, port, authType);
            //}

            //Create email content            



        }
        catch (Exception ex)
        {
            s = ex.ToString();
        }
        return s;
    }
    public static string GetSiteRoot()
    {
        string Port = System.Web.HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
        if (Port == null || Port == "80" || Port == "443")
            Port = "";
        else
            Port = ":" + Port;

        string Protocol = System.Web.HttpContext.Current.Request.ServerVariables["SERVER_PORT_SECURE"];
        if (Protocol == null || Protocol == "0")
            Protocol = "http://";
        else
            Protocol = "https://";

        string appPath = System.Web.HttpContext.Current.Request.ApplicationPath;
        if (appPath == "/")
            appPath = "";

        string sOut = Protocol + System.Web.HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + Port + appPath;
        return sOut;
    }
    public static DataTable toDataTableReport(DataTable dtSource,string colGroupName)
    {
        DataTable dtReport = new DataTable();
        int nR = dtSource.Rows.Count;
        int nC = dtSource.Columns.Count;        
        foreach (DataColumn item in dtSource.Columns)
        {
            if (item.DataType == System.Type.GetType("System.Decimal")
                || item.DataType == System.Type.GetType("System.Double")
                || item.DataType == System.Type.GetType("System.Single")
                || item.DataType == System.Type.GetType("System.Int32")
                )
            {
                dtReport.Columns.Add(item.ColumnName, System.Type.GetType("System.String"));
            }
            else
            {
                dtReport.Columns.Add(item.ColumnName, item.DataType);
            }
        }        
        bool isNum = false;
        for (int i = 0; i < nR; i++)
        {
            DataRow row = dtSource.Rows[i];
            object[] arrData = new object[nC];
            for (int j = 0; j < nC; j++)
            {
                DataColumn item = dtSource.Columns[j];
                arrData[j] = row[item.ColumnName];
                isNum = false;
                if (item.DataType == System.Type.GetType("System.Decimal")
                    || item.DataType == System.Type.GetType("System.Double")
                    || item.DataType == System.Type.GetType("System.Single")
                    || item.DataType == System.Type.GetType("System.Int32")
                    )
                {
                    isNum = true;
                }
                if (row[colGroupName] == DBNull.Value)
                {
                    if (isNum && row[item.ColumnName] != DBNull.Value)
                    {
                        if (Convert.ToDecimal(row[item.ColumnName]) == 0)
                            arrData[j] = "";
                        else
                            arrData[j] = "<b>" + String.Format("{0:#,##0.###}", row[item.ColumnName]) + "</b>";
                    }
                    else if (item.DataType == System.Type.GetType("System.String") && row[item.ColumnName] != DBNull.Value)
                        arrData[j] = "<b>" + row[item.ColumnName].ToString() + "</b>";

                }
                else if (isNum && row[item.ColumnName] != DBNull.Value)
                {
                    if (Convert.ToDecimal(row[item.ColumnName]) == 0)
                        arrData[j] = "";
                    else
                        arrData[j] = String.Format("{0:#,##0.###}", row[item.ColumnName]);
                }

            }// End for colunms  
            dtReport.Rows.Add(arrData);
        }
        return dtReport;
    }
    public static string formatNumber(object value)
    {
        return String.Format("{0:#,##0.00}", value);
    }
    public static NumberFormatInfo NumberFormatVN
    {
        get
        {
            if (_NumberFormatVN == null)
            {
                _NumberFormatVN = new NumberFormatInfo();
                _NumberFormatVN.NumberDecimalSeparator = ",";
                _NumberFormatVN.CurrencyGroupSeparator = ".";
            }
            return _NumberFormatVN;
        }
    }
    public static string toTiengVietKoDau(string convertVN)
    {
        const string TextToFind = "áàảãạâấầẩẫậăắằẳẵặđéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵÁÀẢÃẠÂẤẦẨẪẬĂẮẰẲẴẶĐÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴ";
        const string TextToReplace = "aaaaaaaaaaaaaaaaadeeeeeeeeeeeiiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYY";
        string strVietNamese = convertVN;
        int index = -1;
        while ((index = strVietNamese.IndexOfAny(TextToFind.ToCharArray())) != -1)
        {
            int index2 = TextToFind.IndexOf(strVietNamese[index]);
            strVietNamese = strVietNamese.Replace(strVietNamese[index], TextToReplace[index2]);
        }
        return strVietNamese;
    }
    public static void exportToExcel(HttpResponse response, DataTable dt,ref string msg)
    {
        try
        {
            response.Clear();
            //response.AddHeader("content-disposition", "attachment;filename=DuLieu.xls");
            response.Charset = "";
            response.Cache.SetCacheability(HttpCacheability.NoCache);
            response.ContentType = "application/vnd.xls";
            //create a string writer
            //instantiate a datagrid                                                                
            StringWriter stringWrite = new System.IO.StringWriter();
            //create an htmltextwriter which uses the stringwriter
            HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
            System.Web.UI.WebControls.GridView dg = new System.Web.UI.WebControls.GridView();
            dg.DataSource = dt;
            //bind the datagrid
            dg.DataBind();
            //tell the datagrid to render itself to our htmltextwriter               
            dg.RenderControl(htmlWrite);
            //all that//s left is to output the html
            response.Write(stringWrite.ToString());
            response.End();
            return;
        }
        catch (Exception ex)
        {
            msg = ex.ToString();
            return;
        }

    }
    public static void exportToExcel(HttpResponse response, Page p, Control ctlData)
    {
        response.Clear();
        response.AddHeader("content-disposition", "attachment;filename=DuLieu.xls");
        response.Charset = "";
        response.Cache.SetCacheability(HttpCacheability.NoCache);
        response.ContentType = "application/vnd.xls";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        ctlData.RenderControl(htmlWrite);
        response.Write(stringWrite.ToString());
        response.End();

    }
    public static decimal toDecimalEN(object value)
    {
        return Convert.ToDecimal(value, NumberFormatEN);
    }
    public static decimal toDecimalVN(object value)
    {
        return Convert.ToDecimal(value, NumberFormatVN);
    }
    public static string toStringEN(decimal value)
    {
        return value.ToString(NumberFormatEN);
    }

    public static string toStringVN(decimal value)
    {
        return value.ToString(NumberFormatVN);
    }
    public static DateTime toDate(string date, string culture)
    {
        DateTime d = DateTime.Now;
        CultureInfo provider = new CultureInfo(culture);//CultureInfo.InvariantCulture;
        d = DateTime.Parse(date,provider);
        return d;
    }
    public static string[] getListYears()
    {
        string[] lst = new string[6];
        int j = 0;

        for (int i = DateTime.Now.Year; i >= DateTime.Now.Year - 5; i--, j++)
        {
            lst[j] = i.ToString();            
        }        
        return lst;
    }
    public static string GetCustomConfigValue(string sectionName, string keyName)
    {
        string myValue = "";
        NameValueCollection myData = (NameValueCollection)System.Configuration.ConfigurationSettings.GetConfig(sectionName);
        if (myData == null)
        {
            myValue = "";
        }
        myValue = myData.Get(keyName);
        if (myValue == null)
        {
            myValue = "";
        }
        return myValue;
    }
}
public partial class NameValueX
{
    string _name;
    string _value;
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }
    
    public string Value
    {
        get { return this._value; }
        set { this._value = value; }
    }
}