<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="memberLoginForm.aspx.cs" Inherits="BitSystem.memberLoginForm" %>

<!DOCTYPE html>
<html lang="en">
  <head >
    <meta charset="utf-8">
    <title>會員登入</title>
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
	<a class="logo" href="Home.aspx"><span></span> 
		<img src="GoodPics/我們拍賣吧2.png">
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
			  <li class=""><a id="sale_home" href="Home.aspx">拍賣站</a></li>
				    <li class="">
					<asp:LinkButton ID="sale_list" runat="server"  OnClick="sale_list_Click">
						<span>價低拍賣</span>
					</asp:LinkButton>
				</li>
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
<!-- 
Three column view
--><!-- 
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
			 <div class="well well-small">
		<div class="well well-small alert alert-warning cntr">
			<h3>最新得標商品</h3>
		</div>


			<div class="thumbnail">
		<table>	
			<asp:DataList ID="getbid_view" runat="server" Width="100%"  RepeatColumns="4">
				<ItemTemplate>	
						<tr>
							<td align='center'> 
							<asp:ImageButton ID="pic_pathname" width="180" runat="server"  ImageUrl='<%# Eval("pic_pathname") %>' />
							</td>
						</tr>
						<tr>
							<td align='center'>
							<asp:Label ID="Label1" runat="server" Text="市價: "></asp:Label>
							<s><asp:Label ID="official_price"  runat="server" Text='<%# Eval("official_price") %>'/></s>
							</td>
						</tr>
						<tr>
							<td align='center'>
							<asp:Label ID="Label2" runat="server" Text="得標價: "></asp:Label>
							<asp:Label ID="low_price" ForeColor="red"  runat="server" Text='<%# Eval("low_price") %>'/>
							</td>
						</tr>
						<tr>
							<td align='center'>
							<asp:Label ID="Label3" runat="server" Text="得標者: "></asp:Label>
							<asp:Label ID="name"  runat="server" Text='<%# Eval("name") %>'/>
							</td>
						</tr>
						<tr>
							<td><HR SIZE=5></td>
						</tr>
				</ItemTemplate>
			</asp:DataList>
		</table>
		</div>
		</div>
	

	</div>
	<div class="span9">
	<ul class="breadcrumb">
		<li><a href="Home.aspx">Home</a> <span class="divider">/</span></li>
		<li class="active">會員登入</li>
	</ul>
	<h2> 會員登入</h2>	
	<hr class="soft"/>

	<div class="row">
		<div class="span5">
			<div class="well">
				<h1>建立新的帳號</h1><br/>
				輸入您的Member Account以建立一個新帳號<br/><br/><br/>
                <asp:Label ID="Label3" runat="server" Text="Member Account : "></asp:Label>
				<asp:TextBox ID="_memberAccount"  runat="server" Width="160px"></asp:TextBox>
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
