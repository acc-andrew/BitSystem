<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SavePicToDB.aspx.cs" Inherits="BitSystem.SavePicToDB" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Save Picture to SQL DB</h1>
            <asp:FileUpload ID="_FileUpload1" runat="server" />
            <asp:Button ID="_SaveBtn" runat="server" Text="儲存圖片" OnClick="_SaveBtn_Click" />
        </div>
        
    </form>
</body>
</html>
