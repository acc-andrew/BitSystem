<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="memberRegisterForm.aspx.cs" Inherits="BitSystem.memberRegisterForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>會員註冊畫面</h1>
            <asp:Label ID="Label1" runat="server" Text="會員名稱："></asp:Label>
            <asp:TextBox ID="_memberName" runat="server"></asp:TextBox>
            <br/>
            <asp:Label ID="Label2" runat="server" Text="會員密碼："></asp:Label>
            <asp:TextBox ID="_memberPassword" runat="server" TextMode="Password"></asp:TextBox>
            <br/>
            <asp:Label ID="Label5" runat="server" Text="確認密碼："></asp:Label>
            <asp:TextBox ID="_ConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
            <asp:Label ID="Label6" runat="server" Text="請與會員密碼相同"></asp:Label>
            <br/>
            <asp:Button ID="RegisterBtn" runat="server" Text="會員註冊" OnClick="RegisterBtn_Click"/>
            <br/>
            <asp:Label ID="Label4" runat="server" Text="會員編號："></asp:Label>
            <asp:TextBox ID="_memberID" runat="server" Enabled="False"></asp:TextBox>
        </div>
    </form>
</body>
</html>
