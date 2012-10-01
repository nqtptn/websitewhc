using System;
using System.Configuration;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace CommonWebSite
{
    public class CommonHelper
    {
        public static string Keywords
        {
            get
            {
                return QueryStringInt("keywords");
            }
        }
        public static string CategoryID
        {
            get
            {
                return QueryStringInt("CategoryID");
            }
        }
        public static string ProductID
        {
            get
            {
                return QueryStringInt("ProductID");
            }
        }
        public static string IDCategoryRoot
        {
            get
            {
                return QueryStringInt("IDCategoryRoot");
            }
        }
        public static string QueryStringInt(string Name)
        {
            string result = QueryString(Name).ToLowerInvariant();
            return result;
        }
        public static string QueryString(string Name)
        {
            string result = string.Empty;
            if (HttpContext.Current != null && HttpContext.Current.Request.QueryString[Name] != null)
                result = HttpContext.Current.Request.QueryString[Name].ToString();
            return result;
        }


        public static bool DateRegex(string str)
        {
            String ZipRegex = @"^\d{2}/\d{2}/\d{4}$";
            if (Regex.IsMatch(str, ZipRegex))
                return true;
            else
                return false;
        }
        public static string GetDateFromDMY(string date)
        {
            string[] str = date.Split('/');

            string dd = str[1], mm = str[0];
            if (Convert.ToInt32(dd) < 10)
                dd = dd.Replace("0", "");
            if (Convert.ToInt32(mm) < 10)
                mm = mm.Replace("0", "");
            date = dd + "/" + mm + "/" + str[2];
            return date;
        }
        public static string GetSEName(string name)
        {
            if (String.IsNullOrEmpty(name))
                return string.Empty;
            string OKChars = "abcdefghijklmnopqrstuvwxyz1234567890 _-";
            name = FilterCharacter(name).Trim().ToLowerInvariant();
            var sb = new StringBuilder();
            foreach (char c in name.ToCharArray())
                if (OKChars.Contains(c.ToString()))
                    sb.Append(c);
            string name2 = sb.ToString();
            name2 = name2.Replace(" ", "-");
            while (name2.Contains("--"))
                name2 = name2.Replace("--", "-");
            while (name2.Contains("__"))
                name2 = name2.Replace("__", "_");
            return HttpContext.Current.Server.UrlEncode(name2);
        }


        //cắt chuỗi
        public static string toStringCut(string str, int lenght)
        {
            string newstr;
            if (str.Length > lenght)
                newstr = str.Substring(0, lenght);
            else
                newstr = str;
            return newstr;
        }
        // lọc khoảng trắng (last/first)
        public static string RemoveWhiteSpace(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                while (str.Contains("  "))
                    str = str.Replace("  ", " ");
                if (str[0] == ' ')
                    str = str.Remove(0, 1);
                if (str[str.Length - 1] == ' ')
                    str = str.Remove(str.Length - 1, 1);
                return str.Replace(" ", "-");
            }
            else
                return str;
        }
        //lọc tiếng việt
        public static string FilterCharacter(string str)
        {
            string[] VietNamChar = new string[]   
        {
            "aAeEoOuUiIdDyY","áàạảãâấầậẩẫăắằặẳẵ","ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ","éèẹẻẽêếềệểễ","ÉÈẸẺẼÊẾỀỆỂỄ","óòọỏõôốồộổỗơớờợởỡ",   
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ","úùụủũưứừựửữ","ÚÙỤỦŨƯỨỪỰỬỮ","íìịỉĩ","ÍÌỊỈĨ","đ","Đ","ýỳỵỷỹ","ÝỲỴỶỸ"  
        };
            //Thay thế và lọc dấu từng char        

            for (int i = 1; i < VietNamChar.Length; i++)
            {
                for (int j = 0; j < VietNamChar[i].Length; j++)
                    str = str.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);
            }
            return str;
        }
        //thêm '.' vào chuỗi
        public static string toDotString(string s)
        {
            int len = s.Length - 1;
            int i = 1;
            while (len > 0)
            {
                if (i % 3 == 0)
                    s = s.Insert(len, ".");
                len--;
                i++;
            }
            return s;
        }
        public static void MakeDirectoryIfExists(string NewDirectory)
        {
            try
            {
                // Check if directory exists
                if (!Directory.Exists(NewDirectory))
                {
                    // Create the directory.
                    Directory.CreateDirectory(NewDirectory);
                }
            }
            catch (IOException)
            {
                throw;
            }
        }
        //
        public static string toMoney(string s)
        {
            try
            {
                if (string.IsNullOrEmpty(s) || s == "00")
                    return "Liên hệ";
                return string.Format("{0:r} VND", toDotString(Convert.ToDouble(s).ToString()));
            }
            catch
            {
                return s;
            }
        }
        //
        public static string toStatus(string s)
        {
            try
            {
                if (string.IsNullOrEmpty(s) || s == "0")
                    return "Hết hàng";
                else
                    return "Còn hàng";
            }
            catch
            {
                return "Hết hàng";
            }
        }
        //random số
        public static string GenerateRandomDigitCode(int Length)
        {
            var random = new Random();
            string s = "";
            for (int i = 0; i < Length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }

        public static string RandomString(int size)
        {
            Random _rng = new Random();
            const string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            char[] buffer = new char[size];

            for (int i = 0; i < size; i++)
            {
                buffer[i] = _chars[_rng.Next(_chars.Length)];
            }
            return new string(buffer);
        }
        //kiểm tra email
        public static bool IsValidEmail(string Email)
        {
            bool result = false;
            if (String.IsNullOrEmpty(Email))
                return result;
            Email = Email.Trim();
            result = Regex.IsMatch(Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            return result;
        }

        #region GetUrl
        public static string GetThisPageURL(bool includeQueryString)
        {
            string URL = string.Empty;
            if (HttpContext.Current == null)
                return URL;

            if (includeQueryString)
            {
                bool useSSL = IsCurrentConnectionSecured();
                string storeHost = GetStoreHost(useSSL);
                if (storeHost.EndsWith("/"))
                    storeHost = storeHost.Substring(0, storeHost.Length - 1);
                URL = storeHost + HttpContext.Current.Request.RawUrl;
            }
            else
            {
                URL = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
            }
            return URL;
        }
        public static string GetStoreLocation()
        {
            bool useSSL = IsCurrentConnectionSecured();
            return GetStoreLocation(useSSL);
        }
        public static bool IsCurrentConnectionSecured()
        {
            bool useSSL = false;
            if (HttpContext.Current != null && HttpContext.Current.Request != null)
            {
                useSSL = HttpContext.Current.Request.IsSecureConnection;
                //when your hosting uses a load balancer on their server then the Request.IsSecureConnection is never got set to true, use the statement below
                //just uncomment it
                //useSSL = HttpContext.Current.Request.ServerVariables["HTTP_CLUSTER_HTTPS"] == "on" ? true : false;
            }

            return useSSL;
        }
        public static string GetStoreLocation(bool UseSSL)
        {
            string result = GetStoreHost(UseSSL);
            if (result.EndsWith("/"))
                result = result.Substring(0, result.Length - 1);
            result = result + HttpContext.Current.Request.ApplicationPath;
            if (!result.EndsWith("/"))
                result += "/";

            return result;
        }
        public static string GetStoreHost(bool UseSSL)
        {
            string result = "http://" + ServerVariables("HTTP_HOST");
            if (!result.EndsWith("/"))
                result += "/";

            if (UseSSL)
            {
                if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["SharedSSL"]))
                {
                    result = ConfigurationManager.AppSettings["SharedSSL"];
                }
                else
                {
                    result = result.Replace("http:/", "https:/");
                }
            }

            if (!result.EndsWith("/"))
                result += "/";

            return result;
        }
        public static string ServerVariables(string Name)
        {
            string tmpS = String.Empty;
            try
            {
                if (HttpContext.Current.Request.ServerVariables[Name] != null)
                {
                    tmpS = HttpContext.Current.Request.ServerVariables[Name].ToString();
                }
            }
            catch
            {
                tmpS = String.Empty;
            }
            return tmpS;
        }
        #endregion GetUrl

        public static void ReloadCurrentPage()
        {
            bool useSSL = IsCurrentConnectionSecured();
            ReloadCurrentPage(useSSL);
        }
        public static void ReloadCurrentPage(bool UseSSL)
        {
            string storeHost = GetStoreHost(UseSSL);
            if (storeHost.EndsWith("/"))
                storeHost = storeHost.Substring(0, storeHost.Length - 1);
            string URL = storeHost + HttpContext.Current.Request.RawUrl;
            HttpContext.Current.Response.Redirect(URL);
        }

        #region html header
        public static void SetFavIcon(Page page)
        {
            string favIconPath = HttpContext.Current.Request.PhysicalApplicationPath + "favicon.ico";
            if (File.Exists(favIconPath))
            {
                string favIconUrl = CommonHelper.GetStoreLocation() + "favicon.ico";

                HtmlLink htmlLink1 = new HtmlLink();
                htmlLink1.Attributes["rel"] = "icon";
                htmlLink1.Attributes["href"] = favIconUrl;

                HtmlLink htmlLink2 = new HtmlLink();
                htmlLink2.Attributes["rel"] = "shortcut icon";
                htmlLink2.Attributes["href"] = favIconUrl;

                page.Header.Controls.Add(htmlLink1);
                page.Header.Controls.Add(htmlLink2);
            }
        }
        public static void AddPoweredBy(Page page)
        {
            StringBuilder poweredBy = new StringBuilder();
            poweredBy.Append(Environment.NewLine);
            poweredBy.Append("<!--Powered by nqtruong-->");
            poweredBy.Append(Environment.NewLine);
            poweredBy.Append("<!--Copyright © 2011-->");
            poweredBy.Append(Environment.NewLine);
            page.Header.Controls.AddAt(page.Header.Controls.Count, new LiteralControl(poweredBy.ToString()));
        }
        public static void addLinkImage(Page page, string url)
        {
            HtmlLink link = new HtmlLink();
            link.Attributes.Add("rel", "image_src");
            link.Attributes.Add("href", url);
            page.Header.Controls.Add(link);
        }
        public static void RenderTitle(Page page, string title, bool IncludeStoreNameInTitle, bool OverwriteExisting)
        {
            if (page == null || page.Header == null)
                return;

            if (IncludeStoreNameInTitle)
                title = "Grocery" + " | " + title;

            if (String.IsNullOrEmpty(title))
                return;

            if (OverwriteExisting)
                page.Title = HttpUtility.HtmlEncode(title);
            else
            {
                if (String.IsNullOrEmpty(page.Title))
                    page.Title = HttpUtility.HtmlEncode(title);
            }
        }
        public static void RenderMetaTag(Page page, string name, string content, bool OverwriteExisting)
        {
            if (page == null || page.Header == null)
                return;

            if (content == null)
                content = string.Empty;

            foreach (var control in page.Header.Controls)
                if (control is HtmlMeta)
                {
                    var meta = (HtmlMeta)control;
                    if (meta.Name.ToLower().Equals(name.ToLower()) && !string.IsNullOrEmpty(content))
                    {
                        if (OverwriteExisting)
                            meta.Content = content;
                        else
                        {
                            if (String.IsNullOrEmpty(meta.Content))
                                meta.Content = content;
                        }
                    }
                }
        }
        public static void RenderHeaderRSSLink(Page page, string title, string href)
        {
            if (page == null || page.Header == null)
                return;

            var link = new HtmlGenericControl("link");
            link.Attributes.Add("href", href);
            link.Attributes.Add("type", "application/rss+xml");
            link.Attributes.Add("rel", "alternate");
            link.Attributes.Add("title", title);
            page.Header.Controls.Add(link);
        }
        public static void SetMetaHttpEquiv(Page page, string httpEquiv, string Content)
        {
            if (page.Header == null)
                return;

            HtmlMeta meta = new HtmlMeta();
            if (page.Header.FindControl("meta" + httpEquiv) != null)
            {
                meta = (HtmlMeta)page.Header.FindControl("meta" + httpEquiv);
                meta.Content = Content;
            }
            else
            {
                meta.ID = "meta" + httpEquiv;
                meta.HttpEquiv = httpEquiv;
                meta.Content = Content;
                page.Header.Controls.Add(meta);
            }
        }
        public static void RenderStyleSheet(Page page, string url)
        {
            HtmlLink css1 = new HtmlLink();
            css1.Href = GetStoreLocation() + url;
            css1.Attributes["rel"] = "stylesheet";
            css1.Attributes["type"] = "text/css";
            css1.Attributes["media"] = "all";
            page.Header.Controls.Add(css1);
        }
        public static void RenderScript(Page page, string url)
        {
            string jquery = GetStoreLocation() + url;
            //add script in body
            //page.ClientScript.RegisterClientScriptInclude(jquery, jquery);
            page.Header.Controls.Add(new LiteralControl(Environment.NewLine));
            HtmlGenericControl javascriptControl = new HtmlGenericControl("script");
            javascriptControl.Attributes.Add("type", "text/Javascript");
            javascriptControl.Attributes.Add("src", jquery);
            page.Header.Controls.Add(javascriptControl);
            page.Header.Controls.Add(new LiteralControl(Environment.NewLine));

        }
        #endregion html header

        #region Cookies
        public static void SetCookie(String cookieName, string cookieValue, TimeSpan ts)
        {
            try
            {
                HttpCookie cookie = new HttpCookie(cookieName);
                cookie.Value = HttpContext.Current.Server.UrlEncode(cookieValue);
                DateTime dt = DateTime.Now;
                cookie.Expires = dt.Add(ts);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc.Message);
            }
        }
        public static String GetCookieString(String cookieName, bool decode)
        {
            if (HttpContext.Current.Request.Cookies[cookieName] == null)
            {
                return String.Empty;
            }
            try
            {
                string tmp = HttpContext.Current.Request.Cookies[cookieName].Value.ToString();
                if (decode)
                    tmp = HttpContext.Current.Server.UrlDecode(tmp);
                return tmp;
            }
            catch
            {
                return String.Empty;
            }
        }
        public static bool GetCookieBool(String cookieName)
        {
            string str1 = GetCookieString(cookieName, true).ToUpperInvariant();
            return (str1 == "TRUE" || str1 == "YES" || str1 == "1");
        }
        public static int GetCookieInt(String cookieName)
        {
            string str1 = GetCookieString(cookieName, true);
            if (!String.IsNullOrEmpty(str1))
                return Convert.ToInt32(str1);
            else
                return 0;
        }
        #endregion Cookies

    }
}
