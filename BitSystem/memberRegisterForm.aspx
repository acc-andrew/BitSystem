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
            <asp:TextBox ID="_user_name" runat="server"></asp:TextBox>
            <br/>
            <asp:Label ID="Label2" runat="server" Text="會員密碼："></asp:Label>
            <asp:TextBox ID="_memberPassword" runat="server" TextMode="Password"></asp:TextBox>
            <br/>
            <asp:Label ID="Label3" runat="server" Text="確認密碼："></asp:Label>
            <asp:TextBox ID="_ConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
            <asp:Label ID="Label4" runat="server" Text="請與會員密碼相同"></asp:Label>
            <br/>
            <asp:Label ID="Label5" runat="server" Text="名&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;稱    ："></asp:Label>
            <asp:TextBox ID="_name" runat="server" ></asp:TextBox>
            <br/>
            <asp:Label ID="Label6" runat="server" Text="電子郵件："></asp:Label>
            <asp:TextBox ID="_email" runat="server" TextMode="Email" ></asp:TextBox>
            <br/>            
            <asp:Label ID="Label7" runat="server" Text="手機號碼："></asp:Label>
            <asp:TextBox ID="_cellphoneNo" runat="server" TextMode="Phone" ></asp:TextBox>
            <br/>
            <asp:Label ID="Label8" runat="server" Text="出生年&nbsp;&nbsp;&nbsp;&nbsp;："></asp:Label>
            <asp:DropDownList ID="_birthYear_list" runat="server" OnSelectedIndexChanged="YearSelected" AutoPostBack="True"></asp:DropDownList>
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label9" runat="server" Text="月份："></asp:Label>
            <asp:DropDownList ID="_birthMonth_list" runat="server" OnSelectedIndexChanged="MonthChanged" AutoPostBack="True"></asp:DropDownList>
            &nbsp;&nbsp;
            <asp:Label ID="Label10" runat="server" Text="日期："></asp:Label>
            <asp:DropDownList ID="_birthDate_list" runat="server" ></asp:DropDownList>
            <br/>
            <asp:Label ID="Label11" runat="server" Text="居住地址："></asp:Label>
            <asp:TextBox ID="_address" runat="server" ></asp:TextBox>
            <br/>
            <asp:Label ID="Label12" runat="server" Text="會員狀態："></asp:Label>
            <asp:TextBox ID="_status" runat="server" ></asp:TextBox>
            <br/>
            <asp:Button ID="RegisterBtn" runat="server" Text="會員註冊" OnClick="RegisterBtn_Click"/>
            <br/>
            <asp:Label ID="Label13" runat="server" Text="會員編號："></asp:Label>
            <asp:TextBox ID="_memberID" runat="server" Enabled="False"></asp:TextBox>
        </div>
    </form>
</body>
</html>
