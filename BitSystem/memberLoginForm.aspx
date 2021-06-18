<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="memberLoginForm.aspx.cs" Inherits="BitSystem.memberLoginForm" %>

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
	<div >
	<div >
	</div>
	</div>
	<div >
	<div >
	</div>
	
	
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
			  <li class=""><a href="#">商城</a></li>
			  <li class=""><a href="list_view.aspx">競標拍賣</a></li>
			  <li class=""><a href="grid_view.aspx">價低拍賣</a></li>
			</ul>
			
				
		  </div>
		</div>
	  </div>
	</div>
<!-- 
Body Section 
-->
<!-- 
Three column view
--><!-- 
Body Section 
-->
	<div class="row">
<div id="sidebar" class="span3">
<div class="well well-small">
	<ul class="nav nav-list">
		<li><a href="Sale_login.aspx"><span class="icon-chevron-right"></span>美食、伴手禮</a></li>
		<li><a href="Sale_login.aspx"><span class="icon-chevron-right"></span>書籍及文創商品</a></li>
		<li><a href="Sale_login.aspx"><span class="icon-chevron-right"></span>居家生活</a></li>
		<li><a href="Sale_login.aspx"><span class="icon-chevron-right"></span>女生包包/精品</a></li>
		<li><a href="Sale_login.aspx"><span class="icon-chevron-right"></span>男女鞋款</a></li>
		<li><a href="Sale_login.aspx"><span class="icon-chevron-right"></span>汽機車零件百貨</a></li>
		<li><a href="Sale_login.aspx"><span class="icon-chevron-right"></span>娛樂、收藏</a></li>
		<li><a href="Sale_login.aspx"><span class="icon-chevron-right"></span>寵物</a></li>
		<li><a href="Sale_login.aspx"><span class="icon-chevron-right"></span>其他類別</a></li>
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
				<a class="zoomTool" href="Sale_product_login.aspx" title="add to cart"><span class="icon-search"></span> QUICK VIEW</a>
				<img src="assets/img/bootstrap-ecommerce-templates.png" alt="bootstrap ecommerce templates">
				<div class="caption">
				  <h4><a class="defaultBtn" href="product_details.html">VIEW</a> <span class="pull-right">$22.00</span></h4>
				</div>
			  </div>
			</li>
			<li style="border:0"> &nbsp;</li>
			<li>
			  <div class="thumbnail">
				<a class="zoomTool" href="Sale_product_login.aspx" title="add to cart"><span class="icon-search"></span> QUICK VIEW</a>
				<img src="assets/img/shopping-cart-template.png" alt="shopping cart template">
				<div class="caption">
				  <h4><a class="defaultBtn" href="Sale_product_login.aspx">VIEW</a> <span class="pull-right">$22.00</span></h4>
				</div>
			  </div>
			</li>
			<li style="border:0"> &nbsp;</li>
			<li>
			  <div class="thumbnail">
				<a class="zoomTool" href="Sale_product_login.aspx" title="add to cart"><span class="icon-search"></span> QUICK VIEW</a>
				<img src="assets/img/bootstrap-template.png" alt="bootstrap template">
				<div class="caption">
				  <h4><a class="defaultBtn" href="Sale_product_login.aspx">VIEW</a> <span class="pull-right">$22.00</span></h4>
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
	<h3> 會員登入</h3>	
	<hr class="soft"/>

	<div class="row">
		<div class="span5">
			<div class="well">
				<h1>建立新的帳號</h1><br/>
				輸入您的電子郵件(e-mail address)以建立一個新帳號<br/><br/><br/>
                <asp:Label ID="Label3" runat="server" Text="E-mail address : "></asp:Label>
				<asp:TextBox ID="_memberEmail"  runat="server" Width="160px"></asp:TextBox>
				<asp:Button ID="_CreaateMemberBtn" runat="server" Text="建立新的會員帳號" OnClick="_CreaateMemberBtn_Click"  />

			</div>
		</div>
		<div class="span5">
			<div class="well">
				<h1>會員登入畫面</h1><br/>
			已經是會員，請輸入您的會員名稱和密碼<br/><br/><br/>
				<asp:Label ID="Label1" runat="server" Text="會員名稱："></asp:Label>
				<asp:TextBox ID="_loginName" runat="server" Width="160px"></asp:TextBox>
				<br/>
				<asp:Label ID="Label2" runat="server" Text="會員密碼："></asp:Label>
				<asp:TextBox ID="_loginPassword" runat="server" TextMode="Password" Width="160px"></asp:TextBox>
				<br/>
				<asp:Button ID="LoginBtn" runat="server" Text="會員登入" OnClick="LoginBtn_Click" />
				<br/>
			</div>
		</div>
	</div>
		<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Sale_netConnectionString %>" SelectCommand="SELECT * FROM [Member]"></asp:SqlDataSource>
	</form>
    <!-- 
    Clients 
    -->
   




    <a href="#" class="gotop"><i class="icon-double-angle-up"></i></a>
    <!-- Placed at the end of the document so the pages load faster -->
    <script src="assets/js/jquery.js"></script>
    <script src="assets/js/bootstrap.min.js"></script>
    <script src="assets/js/jquery.easing-1.3.min.js"></script>
    <script src="assets/js/jquery.scrollTo-1.4.3.1-min.js"></script>
    <script src="assets/js/shop.js"></script>
</body>
</html>
