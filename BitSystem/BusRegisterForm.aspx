<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BusRegisterForm.aspx.cs" Inherits="BitSystem.BusRegisterForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>商家註冊畫面</h1>
            <asp:Label ID="Label1" runat="server" Text="商家名稱："></asp:Label>
            <asp:TextBox ID="_BusName" runat="server"></asp:TextBox>
            </br>
            <asp:Label ID="Label2" runat="server" Text="商家密碼："></asp:Label>
            <asp:TextBox ID="_BusPassword" runat="server" TextMode="Password"></asp:TextBox>
            </br>
            <asp:Label ID="Label5" runat="server" Text="確認密碼："></asp:Label>
            <asp:TextBox ID="_ConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
            <asp:Label ID="Label6" runat="server" Text="請與商家密碼相同"></asp:Label>
            </br>
            <asp:Label ID="Label3" runat="server" Text="銀行帳號："></asp:Label>
            <asp:TextBox ID="_BusBankAccount" runat="server"></asp:TextBox>
            <asp:Button ID="RegisterBtn" runat="server" Text="商家註冊" OnClick="RegisterBtn_Click" />
            </br>
            <asp:Label ID="Label4" runat="server" Text="商家編號："></asp:Label>
            <asp:TextBox ID="_BusID" runat="server" Enabled="False"></asp:TextBox>
        </div>
    </form>
</body>
</html>
