<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BusLoginForm.aspx.cs" Inherits="BitSystem.BusLoginForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>商家登入</h1>
            <asp:Label ID="Label1" runat="server" Text="商家名稱："></asp:Label>
            <asp:TextBox ID="_BusName" runat="server"></asp:TextBox>
            </br>
            <asp:Label ID="Label2" runat="server" Text="商家密碼："></asp:Label>
            <asp:TextBox ID="_BusPassword" runat="server"></asp:TextBox>
            </br>
            <asp:Button ID="_BusLoginBtn" runat="server" Text="商家登入" OnClick="_BusLoginBtn_Click" />
        </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:BitSystem_DBConnectionString %>" SelectCommand="SELECT * FROM [BusAccountTable]"></asp:SqlDataSource>
    </form>
</body>
</html>
