<%@ Application Language="C#" %>
<%@ Import Namespace="WebSite2" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="System.Web.Routing" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        // 在应用程序启动时运行的代码
        BundleConfig.RegisterBundles(BundleTable.Bundles);
        AuthConfig.RegisterOpenAuth();
        RouteConfig.RegisterRoutes(RouteTable.Routes);
    }
    
    void Application_End(object sender, EventArgs e)
    {
        //  在应用程序关闭时运行的代码

    }

    void Application_Error(object sender, EventArgs e)
    {
        // 在出现未处理的错误时运行的代码

    }

    protected void Application_AuthenticateRequest(Object sender, EventArgs e)
    {
        HttpApplication app = (HttpApplication)sender;
        HttpContext ctx = app.Context; //获取本次Http请求的HttpContext对象  
        if (ctx.User != null)
        {
            if (ctx.Request.IsAuthenticated == true) //验证过的一般用户才能进行角色验证  
            {
                System.Web.Security.FormsIdentity fi = (System.Web.Security.FormsIdentity)ctx.User.Identity;
                System.Web.Security.FormsAuthenticationTicket ticket = fi.Ticket; //取得身份验证票  
                string userData = ticket.UserData;//从UserData中恢复role信息
                string[] roles = userData.Split(','); //将角色数据转成字符串数组,得到相关的角色信息  
                ctx.User = new System.Security.Principal.GenericPrincipal(fi, roles); //这样当前用户就拥有角色信息了
            }
        }
    }

</script>
