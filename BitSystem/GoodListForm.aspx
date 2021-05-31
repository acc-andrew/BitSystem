<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodListForm.aspx.cs" Inherits="BitSystem.GoodListForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>商品列表</h1>
            <asp:Button ID="_memberLoginBtn" runat="server" Text="會員登入" />
            &nbsp;&nbsp;
            <asp:Button ID="_memberRegisterBtn" runat="server" Text="會員註冊" />
            &nbsp;&nbsp;
            <asp:Button ID="_MyGoodsBtn" runat="server" Text="我的商城" />
            &nbsp;&nbsp;
            <asp:Button ID="_OnSaleWebSiteBtn" runat="server" Text="拍賣網" />
            &nbsp;&nbsp;
            <asp:Button ID="_SysAdminBtn" runat="server" Text="系統管理員" />
        </div>
        <br/><br/>
        <asp:GridView ID="_GoodsGridView" runat="server" AutoGenerateColumns="false"
                      CellPadding="4">
            <Columns>
                <asp:TemplateField HeaderText="圖片">
                    <ItemTemplate>
                        <asp:Image ID="img0" runat="server"  Height="160"  width="160"  ImageUrl='<%# Eval("pic_pathname") %>' /> 
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="product_name" HeaderText="商品名稱"/>
                <asp:BoundField DataField="total_number" HeaderText="商品數量"/>
                <asp:BoundField DataField="seller_ID" HeaderText="商家名稱"/>
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Sale_netConnectionString %>" SelectCommand="SELECT * FROM [Market_product2]"></asp:SqlDataSource>
    </form>
</body>
</html>
