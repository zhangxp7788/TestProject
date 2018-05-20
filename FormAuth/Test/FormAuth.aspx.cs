using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Test_FormAuth : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string Logon = Request.Form["Logon"];
        if (!string.IsNullOrEmpty(Logon))
        {
            FormsAuthentication.SignOut();
            return;
        }


        string loginName = Request.Form["loginName"];
        if (string.IsNullOrEmpty(loginName))
            return;

        FormsAuthentication.SetAuthCookie(loginName, true);

        var stsdt = DBO.UserRole(loginName, "myrole1");

        var sdfs = DBO.Login_Role();

        var sdgf = DBO.isRole("sdgklfas");
        var sdgf2 = DBO.isRole("myrole1");


    }

    public void Logon()
    {
        FormsAuthentication.SignOut();
    }

    public void NormalLogin()
    {
        // -----------------------------------------------------------------
        // 注意：演示代码为了简单，这里不检查用户名与密码是否正确。
        // -----------------------------------------------------------------

        string loginName = Request.Form["loginName"];
        if (string.IsNullOrEmpty(loginName))
            return;

        FormsAuthentication.SetAuthCookie(loginName, true);

        //TryRedirect();
    }

}