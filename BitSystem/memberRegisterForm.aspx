<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="memberRegisterForm.aspx.cs" Inherits="BitSystem.memberRegisterForm" %>


<!DOCTYPE html>
<html lang="en">
  <head >
    <meta charset="utf-8">
    <title>Twitter Bootstrap shopping cart</title>
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
<form class="form-horizontal loginFrm" runat="server">
<div class="navbar-nav ml-auto">
	<div class="topNav">
		<div class="container">
			<div class="alignR">
				<div align="right">
					<asp:LinkButton ID="home" runat="server" class="icon-home" OnClick="home_Click">首頁</asp:LinkButton>
					<asp:LinkButton ID="member_info" runat="server" Visible="false" class="icon-user" OnClick="member_info_Click">會員資料</asp:LinkButton>
					<asp:LinkButton ID="order_info" runat="server" Visible="false" class="icon-edit" OnClick="order_info_Click" >訂單查詢</asp:LinkButton>
					<asp:LinkButton ID="my_info" runat="server" Visible="false" class="icon-user" OnClick="my_info_Click">我的會員</asp:LinkButton>
					<asp:LinkButton ID="register" runat="server" Visible="false" class="icon-edit" OnClick="register_Click">免費註冊</asp:LinkButton>
					<asp:LinkButton ID="contantus" runat="server" class="icon-envelope" OnClick="contantus_Click">聯絡我們</asp:LinkButton>
					<asp:LinkButton ID="manager" runat="server" Visible="false" class="icon-lock" OnClick="manager_Click">管理後臺</asp:LinkButton>
					<asp:LinkButton ID="logout" runat="server" Visible="false" class="icon-remove" OnClick="logout_Click">登出</asp:LinkButton>
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
			 <li class=""><a id="sale_home" href="Home.aspx">拍賣站</a></li>
				    <li class=""><a id="sale_list" href="list_view.aspx">價低拍賣</a></li>
				    <li class=""><a id="sale_onshelf" href="PutGoodOnShelfForm.aspx">商品上架</a></li>
				    <li class=""><a id="sale_chichout"  href="sale_chickout_product.aspx">得標結帳</a></li>
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
		<li><asp:LinkButton ID="cloth" runat="server" class="icon-chevron-right" OnClick="cloth_Click">衣服/飾品</asp:LinkButton></li>
		<li><asp:LinkButton ID="book" runat="server" class="icon-chevron-right" OnClick="book_Click">書籍/文創</asp:LinkButton></li>
		<li><asp:LinkButton ID="life" runat="server" class="icon-chevron-right" OnClick="life_Click">居家/生活</asp:LinkButton></li>
		<li><asp:LinkButton ID="bag" runat="server" class="icon-chevron-right" OnClick="bag_Click">包包/精品</asp:LinkButton></li>
		<li><asp:LinkButton ID="shoes" runat="server" class="icon-chevron-right" OnClick="shoes_Click">男女鞋款</asp:LinkButton></li>
		<li><asp:LinkButton ID="car" runat="server" class="icon-chevron-right" OnClick="car_Click">汽機車/零件百貨</asp:LinkButton></li>
		<li><asp:LinkButton ID="entertainment" runat="server" class="icon-chevron-right" OnClick="entertainment_Click">娛樂/收藏</asp:LinkButton></li>
		<li><asp:LinkButton ID="pet" runat="server" class="icon-chevron-right" OnClick="pet_Click">寵物/用品</asp:LinkButton></li>
		<li><asp:LinkButton ID="others" runat="server" class="icon-chevron-right" OnClick="others_Click">其他類別</asp:LinkButton></li>
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
		<li><a href="index.html">Home</a> <span class="divider">/</span></li>
		<li class="active">會員登入</li>
	</ul>
	<h3> 會員註冊</h3>	
	<hr class="soft"/>

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
			<asp:Label ID="Label8" runat="server" Text="出生年&nbsp;&nbsp;&nbsp;："></asp:Label>
			<asp:DropDownList ID="_birthYear_list" runat="server" OnSelectedIndexChanged="YearSelected" AutoPostBack="True"></asp:DropDownList>
			<br/>
			<asp:Label ID="Label9" runat="server" Text="出生月&nbsp;&nbsp;&nbsp;："></asp:Label>
			<asp:DropDownList ID="_birthMonth_list" runat="server" OnSelectedIndexChanged="MonthChanged" AutoPostBack="True"></asp:DropDownList>
			<br/>
			<asp:Label ID="Label10" runat="server" Text="出生日&nbsp;&nbsp;&nbsp;："></asp:Label>
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
<!-- 
    Clients 
    -->
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
