<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BitProductForm.aspx.cs" Inherits="BitSystem.BitProductForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta charset="utf-8">
    <title>拍賣網站</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="">
    <!-- Bootstrap styles -->
    <link href="assets/css/bootstrap.css" rel="stylesheet"/>
    <!-- Customize styles -->
    <link href="style.css" rel="stylesheet"/>
    <!-- font awesome styles -->
    <link href="assets/font-awesome/css/font-awesome.css" rel="stylesheet">
    <!--[if IE 7]>
        <link href="css/font-awesome-ie7.min.css" rel="stylesheet">
    <![endif]-->

    <!--[if lt IE 9]>
        <script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
    <![endif]-->

    <!-- Favicons -->
    <link rel="shortcut icon" href="assets/ico/favicon.ico">
</head>
        
    
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title></title>

<body style="background-image:url('assets/img/white_leather.png');background-repeat:repeat;">
<!-- 
Upper Header Section 
-->
<div class="navbar-nav ml-auto">
    <div class="topNav">
        <div class="container">
            <div class="alignR">
                <div align="right">
                </div>
            </div>
        </div>
    </div>
</div>
<!-- 
Upper Header Section 
-->
<div class="navbar-nav ml-auto">
	<div class="topNav">
		<div class="container">
			<div class="alignR">
				<div align="right">
				</div>
			</div>
		</div>
	</div>
</div>
<!--
Lower Header Section 
-->
<div class="container">
<div id="gototop"> </div>
<header id="header">
    <div class="row">
        <div class="span4">
            <h1>
                <a class="logo" href="index.html"><span>Twitter Bootstrap ecommerce template</span> 
                    <img src="assets/img/logo-bootstrap-shoping-cart.png" alt="bootstrap sexy shop">
                </a>
            </h1>
        </div>
        <div class="span4">
            <div class="offerNoteWrapper">
                <h1 class="dotmark">
                    <i class="icon-cut"></i>
                </h1>
            </div>
        </div>
        <div class="span4 alignR">
            <p><br> <strong> Support (24/7) :  0800 1234 678 </strong><br><br></p>
            <span class="btn btn-mini">[ 2 ] <span class="icon-shopping-cart"></span></span>
            <span class="btn btn-warning btn-mini">$</span>
            <span class="btn btn-mini">&pound;</span>
            <span class="btn btn-mini">&euro;</span>
        </div>
    </div>
</header>
<!--
Navigation Bar Section 
-->
<div class="navbar">
    <div class="navbar-inner">
        <div class="container">
            <a data-target=".nav-collapse" data-toggle="collapse" class="btn btn-navbar">
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
            </a>
            <div class="nav-collapse">
                <ul class="nav">
                <li class=""><a href="index.html">首頁</a></li>
                <li class=><a href="list-view.html">串列顯示</a></li>
                <li class=""><a href="grid-view.html">格狀顯示</a></li>
                <li class=""><a href="three-col.html">三欄顯示</a></li>
                <li class=""><a href="four-col.html">三欄顯示</a></li>
                <li class="active"><a href="general.html">商品內容</a></li>
            </div>
        </div>
    </div>
</div>
<!-- 
Body Section 
-->
	<div class="row">
<div id="sidebar" class="span3">
<div class="well well-small">
	<ul class="nav nav-list">
		<li><a href="products.html"><span class="icon-chevron-right"></span>衣著</a></li>
		<li><a href="products.html"><span class="icon-chevron-right"></span>美食、伴手禮</a></li>
		<li><a href="products.html"><span class="icon-chevron-right"></span>書籍及文創商品</a></li>
		<li><a href="products.html"><span class="icon-chevron-right"></span>居家生活</a></li>
		<li><a href="products.html"><span class="icon-chevron-right"></span>女生包包/精品</a></li>
		<li><a href="products.html"><span class="icon-chevron-right"></span>男女鞋款</a></li>
		<li><a href="products.html"><span class="icon-chevron-right"></span>汽機車零件百貨</a></li>
		<li><a href="products.html"><span class="icon-chevron-right"></span>娛樂、收藏 </a></li>
		<li><a href="products.html"><span class="icon-chevron-right"></span>寵物</a></li>
		<li><a href="products.html"><span class="icon-chevron-right"></span>其他類別</a></li>
		<li style="border:0"> &nbsp;</li>
		<li> <a class="totalInCart" href="cart.html"><strong>Total Amount  <span class="badge badge-warning pull-right" style="line-height:18px;">$448.42</span></strong></a></li>
	</ul>
</div>

			  <div class="well well-small alert alert-warning cntr">
				  <h2>50% Discount</h2>
				  <p> 
					 only valid for online order. <br><br><a class="defaultBtn" href="#">Click here </a>
				  </p>
			  </div>
			  <div class="well well-small" ><a href="#"><img src="assets/img/paypal.jpg" alt="payment method paypal"></a></div>
			
			<a class="shopBtn btn-block" href="#">Upcoming products <br><small>Click to view</small></a>
			<br>
			<br>
			<ul class="nav nav-list promowrapper">
			<li>
			  <div class="thumbnail">
				<a class="zoomTool" href="product_details.html" title="add to cart"><span class="icon-search"></span> QUICK VIEW</a>
				<img src="assets/img/bootstrap-ecommerce-templates.png" alt="bootstrap ecommerce templates">
				<div class="caption">
				  <h4><a class="defaultBtn" href="product_details.html">VIEW</a> <span class="pull-right">$22.00</span></h4>
				</div>
			  </div>
			</li>
			<li style="border:0"> &nbsp;</li>
			<li>
			  <div class="thumbnail">
				<a class="zoomTool" href="product_details.html" title="add to cart"><span class="icon-search"></span> QUICK VIEW</a>
				<img src="assets/img/shopping-cart-template.png" alt="shopping cart template">
				<div class="caption">
				  <h4><a class="defaultBtn" href="product_details.html">VIEW</a> <span class="pull-right">$22.00</span></h4>
				</div>
			  </div>
			</li>
			<li style="border:0"> &nbsp;</li>
			<li>
			  <div class="thumbnail">
				<a class="zoomTool" href="product_details.html" title="add to cart"><span class="icon-search"></span> QUICK VIEW</a>
				<img src="assets/img/bootstrap-template.png" alt="bootstrap template">
				<div class="caption">
				  <h4><a class="defaultBtn" href="product_details.html">VIEW</a> <span class="pull-right">$22.00</span></h4>
				</div>
			  </div>
			</li>
		  </ul>

	</div>

    <form id="form1" runat="server">
        <div>
            <h1>商品拍賣</h1>
            <asp:Image ID="_ProductImage" runat="server" Width="300px" Height="300px"/>
            <br/>
            <asp:Label ID="Label1" runat="server" Text="商品名稱："></asp:Label>
            <asp:Label ID="_ProductName" runat="server" Text=""></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label3" runat="server" Text="剩餘時間："></asp:Label>
            <asp:Label ID="_LeftTime" runat="server" Text=""></asp:Label>
            <br/><br/>
            <asp:Label ID="Label2" runat="server" Text="商品描述："></asp:Label>
            <asp:Label ID="_ProductDesc" runat="server" Text=""></asp:Label>
            <br/><br/>
            <asp:Label ID="Label4" runat="server" Text="官方售價："></asp:Label>
            <asp:Label ID="_GUI_official_price" runat="server" Text=""></asp:Label>
            <br/>
            <asp:Label ID="Label5" runat="server" Text="目前得標者："></asp:Label>
            <asp:Label ID="_NowBitWinner" runat="server" Text=""></asp:Label>
            &nbsp;&nbsp;
            <asp:Label ID="Label6" runat="server" Text="目前得標價："></asp:Label>
            <asp:Label ID="_NowBitPrice" runat="server" Text=""></asp:Label>     
            <br/>
            <asp:TextBox ID="_BitPrice" runat="server"></asp:TextBox>
            <asp:Button ID="_BitBtn" runat="server" Text="出價" OnClick="_BitBtn_Click" />
        </div>
    </form>
</body>
</html>
