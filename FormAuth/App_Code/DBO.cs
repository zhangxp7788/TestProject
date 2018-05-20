using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

///   
/// DBO 的摘要说明  
///   
public class DBO
{
    public DBO()
    {
        //  
        // TODO: 在此处添加构造函数逻辑  
        //  
    }

    public static SqlConnection CreateConn()
    {
        return new SqlConnection(ConfigurationManager.ConnectionStrings["ConString"].ToString());
    }

    public static string Login_Role()
    {
        return HttpContext.Current.User.Identity.Name;
    }

    public static string UserRole(string username, string roles)
    {
        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, username, DateTime.Now, DateTime.Now.AddMinutes(1.0), false, roles, FormsAuthentication.FormsCookieName);
        string str = FormsAuthentication.Encrypt(ticket);
        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, str);
        cookie.Expires = ticket.Expiration;
        HttpContext.Current.Response.Cookies.Add(cookie);
        return FormsAuthentication.GetRedirectUrl(FormsAuthentication.FormsCookieName, false);
    }

    public static bool isRole(string role)
    {
        return HttpContext.Current.User.IsInRole(role);
    }

    public static string MD5_Method(string userpass)
    {
        return FormsAuthentication.HashPasswordForStoringInConfigFile(userpass, "MD5");
    }

    public static void Logout()
    {
        HttpCookie cookie = HttpContext.Current.Response.Cookies[FormsAuthentication.FormsCookieName];

        if (cookie == null)
        {
            cookie = new HttpCookie(FormsAuthentication.FormsCookieName);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        cookie.Expires = DateTime.Now.AddYears(-10);
    }


    public static void checkCompany(string username)
    {
        SqlConnection connection = DBO.CreateConn();
        SqlDataAdapter adapter = new SqlDataAdapter("select usertype,end_time from company where username='" + username + "'", connection);
        DataSet ds = new DataSet();
        adapter.Fill(ds);
        string strType = ds.Tables[0].Rows[0]["usertype"].ToString();
        string strSpan = ds.Tables[0].Rows[0]["end_time"].ToString();

        if (strType == "0")
        {
            DBO.UserRole(username, "c_test");
        }
        else
        {
            DateTime now = DateTime.Now;
            TimeSpan span = (TimeSpan)(DateTime.Parse(strSpan) - now);
            if (span.Hours > 0)
            {
                DBO.UserRole(username, "c_normal");
            }
            else
            {
                DBO.UserRole(username, "c_end");
            }
        }
    }
}