<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FormAuth.aspx.cs" Inherits="Test_FormAuth" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>

    <fieldset><legend>普通登录</legend><form action="<%= Request.RawUrl %>" method="post">
    登录名：<input type="text" name="loginName" style="width: 200px" value="Fish" />
    <input type="submit" name="NormalLogin" value="登录" />
</form></fieldset>


<fieldset><legend>用户状态</legend><form action="<%= Request.RawUrl %>" method="post">
    <% if( Request.IsAuthenticated ) { %>
        当前用户已登录，登录名：<%= Context.User.Identity.Name %> <br />   
             
        <input type="submit" name="Logon" value="退出" />
    <% } else { %>
        <b>当前用户还未登录。</b>
    <% } %>            
</form></fieldset>
</body>
</html>
