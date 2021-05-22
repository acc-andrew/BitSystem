<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="memberLoginForm.aspx.cs" Inherits="BitSystem.memberLoginForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>會員登入畫面</h1>
            <asp:Label ID="Label1" runat="server" Text="會員名稱："></asp:Label>
            <asp:TextBox ID="_loginName" runat="server" Width="160px"></asp:TextBox>
            <br/>
            <asp:Label ID="Label2" runat="server" Text="會員密碼："></asp:Label>
            <asp:TextBox ID="_loginPassword" runat="server" TextMode="Password" Width="160px"></asp:TextBox>
            <br/>
            <asp:Button ID="LoginBtn" runat="server" Text="會員登入" OnClick="LoginBtn_Click" />
            &ensp;&ensp;&ensp;&ensp;&ensp;&ensp;&ensp;&ensp;
            <asp:Button ID="memRegBtn" runat="server" Text="新會員註冊" />
        </div>
    </form>
</body>
</html>
