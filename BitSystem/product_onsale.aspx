﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="product_onsale.aspx.cs" Inherits="BitSystem.product_onsale" %>

<!DOCTYPE html>
<html lang="en">
  <head >
    <meta charset="utf-8">
    <title>product_onsale</title>
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
<body style="background-image:url('assets/img/white_leather.png');background-repeat:repeat;" >
<!-- 
	Upper Header Section 
-->
<div class="navbar-nav ml-auto">
	<div class="topNav">
		<div class="container">
			<div class="alignR">
				<a href="Home.aspx"> <span class="icon-home"></span> 首頁</a> 
				<a href="memberProfile.aspx"><span class="icon-user"></span> 會員資料</a> 
				<a href="memberOrder.aspx"><span class="icon-edit"></span> 訂單查詢 </a> 
				<a href="#"><span class="icon-envelope"></span> 聯絡我們</a>
				<a href="#"><span class="icon-remove"></span> 登出</a>
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
	<a class="logo" href="index.html"><span></span> 
		<img src="assets/img/logo-bootstrap-shoping-cart.png" alt="bootstrap sexy shop">
	</a>
	</h1>
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
			  <li class=""><a href="Home.aspx">拍賣站</a></li>
			  <li class=""><a href="list_view.aspx">競標拍賣</a></li>
			  <li class=""><a href="grid_view.aspx">價低拍賣</a></li>
			</ul>
			
				
			</li>
			</ul>
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
		<li><a href="products.html"><span class="icon-chevron-right"></span>娛樂、收藏</a></li>
		<li><a href="products.html"><span class="icon-chevron-right"></span>寵物</a></li>
		<li><a href="products.html"><span class="icon-chevron-right"></span>其他類別</a></li>
		<!--<li style="border:0"> &nbsp;</li>-->
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
	<div class="span9">
	<ul class="breadcrumb">
		<li><a href="Home.aspx">Home</a> <span class="divider">/</span></li>
		<li class="active">商品上架</li>
	</ul>
	<h3 align ="center"> 商品上架</h3>	
	<hr class="soft"/>
    <form id="form1" runat="server">
        <div align =" center">
			<h1>商品上架資料</h1>
			<asp:Label ID="Label12" runat="server" Text="會員編號："></asp:Label>
			<asp:TextBox ID="memberID" runat="server" Enabled="False"></asp:TextBox>
			<br/>
			<asp:Label ID="Label13" runat="server" Text="產品編號："></asp:Label>
			<asp:TextBox ID="productID" runat="server" Enabled="False"></asp:TextBox>
			<br/>
			<asp:Label ID="Label1" runat="server" Text="會員帳號："></asp:Label>
			<asp:TextBox ID="user_name" runat="server" Enabled="False"></asp:TextBox>
			<br/>
			<asp:Label ID="Label3" runat="server" Text="產品名稱："></asp:Label>
			<asp:TextBox ID="product" runat="server" ></asp:TextBox>
			<br/>
			<asp:Label ID="Label4" runat="server" Text="分類選擇："></asp:Label>
			<asp:DropDownList ID="classify" runat="server"  
            DataSourceID="" DataTextField="classify" DataValueField="classify"  
            AppendDataBoundItems="True">
            <asp:ListItem>請選擇</asp:ListItem></asp:DropDownList>
			<br/>
			<asp:Label ID="Label2" runat="server" Text="商品數量："></asp:Label>
			<asp:DropDownList ID="total_number" runat="server" ></asp:DropDownList>
			<br/>       
			<asp:Label ID="Label11" runat="server" Text="商品狀況："></asp:Label>
			<asp:TextBox ID="product_status" runat="server" ></asp:TextBox>
			<br/>
			<br/>       
			<asp:Label ID="Label6" runat="server" Text="商品市價："></asp:Label>
			<asp:TextBox ID="public_price" runat="server" ></asp:TextBox>
			<br/>
			<asp:Label ID="Label5" runat="server" Text="商品描述："></asp:Label>
			<asp:TextBox ID="description" runat="server" TextMode="MultiLine"  Row="5"></asp:TextBox>
			<br/>
			<div align =" center">
			<asp:Label ID="Label8" runat="server" Text="檔案圖片："></asp:Label>
            <asp:FileUpload ID="FileUpload2" runat="server" />
			</div>
			<br/>
			<div align =" center">
            <asp:Label ID="Label7" runat="server"></asp:Label>
			</div>
			<br/>
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:Button ID="onsale" runat="server" Text="商品上架" OnClick="onsale_Click" />
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:Button ID="exit" runat="server" Text="離開取消" />
			<br/>	
        </div>
		<!-- 
    Clients 
    -->
        
    </form>
    <section class="our_client">
        <hr class="soften"/>
        <h4 class="title cntr"><span class="text">Manufactures</span></h4>
        <hr class="soften"/>
        <div class="row">
            <div class="span2">
                <a href="#"><img alt="" src="assets/img/1.png"></a>
            </div>
            <div class="span2">
                <a href="#"><img alt="" src="assets/img/2.png"></a>
            </div>
            <div class="span2">
                <a href="#"><img alt="" src="assets/img/3.png"></a>
            </div>
            <div class="span2">
                <a href="#"><img alt="" src="assets/img/4.png"></a>
            </div>
            <div class="span2">
                <a href="#"><img alt="" src="assets/img/5.png"></a>
            </div>
            <div class="span2">
                <a href="#"><img alt="" src="assets/img/6.png"></a>
            </div>
        </div>
    </section>
    <!--
    Footer
    -->
    <footer class="footer">
        <div class="row-fluid">
            <div class="span2">
                <h5>Your Account</h5>
                <a href="#">YOUR ACCOUNT</a><br>
                <a href="#">PERSONAL INFORMATION</a><br>
                <a href="#">ADDRESSES</a><br>
                <a href="#">DISCOUNT</a><br>
                <a href="#">ORDER HISTORY</a><br>
            </div>
            <div class="span2">
                <h5>Iinformation</h5>
                <a href="contact.html">CONTACT</a><br>
                <a href="#">SITEMAP</a><br>
                <a href="#">LEGAL NOTICE</a><br>
                <a href="#">TERMS AND CONDITIONS</a><br>
                <a href="#">ABOUT US</a><br>
            </div>
            <div class="span2">
                <h5>Our Offer</h5>
                <a href="#">NEW PRODUCTS</a> <br>
                <a href="#">TOP SELLERS</a><br>
                <a href="#">SPECIALS</a><br>
                <a href="#">MANUFACTURERS</a><br>
                <a href="#">SUPPLIERS</a> <br/>
            </div>
            <div class="span6">
                <h5>The standard chunk of Lorem</h5>
                The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for
                those interested. Sections 1.10.32 and 1.10.33 from "de Finibus Bonorum et 
                Malorum" by Cicero are also reproduced in their exact original form, 
                accompanied by English versions from the 1914 translation by H. Rackham.
            </div>
        </div>
    </footer>
</div><!-- /container -->

<div class="copyright">
    <div class="container">
        <p class="pull-right">
            <a href="#"><img src="assets/img/maestro.png" alt="payment"></a>
            <a href="#"><img src="assets/img/mc.png" alt="payment"></a>
            <a href="#"><img src="assets/img/pp.png" alt="payment"></a>
            <a href="#"><img src="assets/img/visa.png" alt="payment"></a>
            <a href="#"><img src="assets/img/disc.png" alt="payment"></a>
        </p>
        <span>Copyright &copy; 2013<br> bootstrap ecommerce shopping template</span>
    </div>
</div>
    <a href="#" class="gotop"><i class="icon-double-angle-up"></i></a>
    <!-- Placed at the end of the document so the pages load faster -->
    <script src="assets/js/jquery.js"></script>
    <script src="assets/js/bootstrap.min.js"></script>
    <script src="assets/js/jquery.easing-1.3.min.js"></script>
    <script src="assets/js/jquery.scrollTo-1.4.3.1-min.js"></script>
    <script src="assets/js/shop.js"></script>
</body>
</html>
