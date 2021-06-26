<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="BitSystem.Home" %>

<!DOCTYPE html>
<html lang="en">
  <head >
    <meta charset="utf-8">
    <title>我們拍賣吧</title>
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
				<li class=""><a id="sale_chickout"  href="sale_chickout_product.aspx">得標結帳</a></li>
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
				  <h2>90% Discount</h2>
				  <p> 
					 only valid for online order. <br><br><a class="defaultBtn" href="#"> </a>
				  </p>
			  </div>
			  <div class="well well-small" ><a href="#"><img src="assets/img/paypal.jpg" alt="payment method paypal"></a></div>
			
			<ul class="nav nav-list promowrapper">
			<li>
			  <div class="thumbnail">
				<h4><span class="">Fine Jewelry</span></h4>
				<img src="pic/A3.jpg" alt="bootstrap ecommerce templates">
				<div class="caption">
				  <h4><a class="defaultBtn" href=""></a> <span class="">$10.00</span></h4>
				</div>
			  </div>
			</li>
			<li style="border:1"> &nbsp;</li>
			<li>
			  <div class="thumbnail">
				<h4><span class="">Tiffany T Smile</span></h4>
				<img src="pic/pexels.jpg" alt="shopping cart template">
				<div class="caption">
				  <h4><a class="defaultBtn" href="product_details.html"></a> <span class="">$10.00</span></h4>
				</div>
			  </div>
			</li>
			<li style="border:1"> &nbsp;</li>
			<li>
			  <div class="thumbnail">
				<h4><span class="">Celine</span> </h4>
				<img src="pic/Bella.jpg" alt="bootstrap template">
				<div class="caption">
				  <h4><a class="defaultBtn" href=""></a> <span class="">$10.00</span></h4>
				</div>
			  </div>
			</li>
		  </ul>
	

	</div>
	<div class="span9">
	<div class="well np">
		<div id="myCarousel" class="carousel slide homCar">
            <div class="carousel-inner">
			  <div class="item">
                <img style="width:800px;height:450px" src="pic/bootstrap_iphone13.png" alt="bootstrap ecommerce templates">
                <div class="carousel-caption">
                      <h4>iphone13</h4>
                      <p><span>Very clean simple to use</span></p>
                </div>
              </div>
			  <div class="item">
                <img style="width:800px;height:450px" src="pic/bootstrap_bicycle.png" alt="bootstrap ecommerce templates">
                <div class="carousel-caption">
                      <h4>bicycle  </h4>
                      <p><span>Highly Google seo friendly</span></p>
                </div>
              </div>
			  <div class="item active">
                <img style="width:800px;height:450px" src="pic/bootstrap_mask.png" alt="bootstrap ecommerce templates">
                <div class="carousel-caption">
                      <h4>mask</h4>
                      <p><span>Very easy to integrate and expand.</span></p>
                </div>
              </div>
              <div class="item">
                <img style="width:800px;height:450px" src="pic/bootstrap_switch.png" alt="bootstrap templates">
                <div class="carousel-caption">
                      <h4>switch</h4>
                      <p><span>Compitable to many more opensource cart</span></p>
                </div>
              </div>
            </div>
            <a class="left carousel-control" href="#myCarousel" data-slide="prev">&lsaquo;</a>
            <a class="right carousel-control" href="#myCarousel" data-slide="next">&rsaquo;</a>
          </div>
        </div>
<!--
New Products
-->
<div class="well well-small">
	<h3>熱門商品 搶先看 </h3>
		<div class="row-fluid">
		  <div class="thumbnails">
                  <asp:DataList ID="product_view_life" runat="server" Width="100%"  RepeatColumns="3" OnItemCommand="product_view_life_ItemCommand" >
					<ItemTemplate>
						<div class="thumbnail">
							<table  border="0" cellpadding="5" cellspacing="5">
								<tr>
									<td >
									<asp:ImageButton ID="pic_pathname" runat="server" Height="200"  width="200" CommandName="click"  ImageUrl='<%# Eval("pic_pathname") %>' />
									</td>
								</tr>
								<tr>
									<td align='center'>
									<asp:Label ID="product" runat="server" Text='<%# Eval("product") %>'/>
									</td>
								</tr>
								<tr>
									<td align='center'>
									<asp:Label ID="official_price_price" runat="server" Text='市價:'/>
									<asp:Label ID="official_price" runat="server" Text='<%# Eval("official_price") %>'/>
									</td>
								</tr>
								<tr>
									<td align='center'>
									<asp:Label ID="status" runat="server" Text='<%# Eval("status") %>'/>
									<asp:Label ID="description" runat="server" Visible="false" Text='<%# Eval("description") %>'/>
									<asp:Label ID="action_product_ID" runat="server" Visible="false" Text='<%# Eval("action_product_ID") %>'/>
									<asp:Label ID="seller_ID" runat="server" Visible="false" Text='<%# Eval("seller_ID") %>'/>
									</td>
								</tr>
							</table>
						</div>
					</ItemTemplate>
				</asp:DataList>

			  <h3>激熱商品 馬上標</h3>
                  <asp:DataList ID="product_view_cloth" runat="server" Width="100%"  RepeatColumns="3"  OnItemCommand="product_view_cloth_ItemCommand">
					<ItemTemplate>
						<div class="thumbnail">
							<table  border="0" cellpadding="5" cellspacing="5">
								<tr>
									<td>
									<asp:ImageButton ID="pic_pathname1" runat="server" Height="200"  width="200" CommandName="click" ImageUrl='<%# Eval("pic_pathname") %>' />
									</td>
								</tr>
								<tr>
									<td align='center'>
									<asp:Label ID="product" runat="server" Text='<%# Eval("product") %>'/>
									</td>
								</tr>
								<tr>
									<td align='center'>
									<asp:Label ID="official_price_price" runat="server" Text='市價:'/>
									<asp:Label ID="official_price" runat="server" Text='<%# Eval("official_price") %>'/>
									</td>
								</tr>
								<tr>
									<td align='center'>
									<asp:Label ID="status" runat="server" Text='<%# Eval("status") %>'/>
									<asp:Label ID="description" runat="server" Visible="false" Text='<%# Eval("description") %>'/>
									<asp:Label ID="action_product_ID" runat="server" Visible="false" Text='<%# Eval("action_product_ID") %>'/>
									<asp:Label ID="seller_ID" runat="server" Visible="false" Text='<%# Eval("seller_ID") %>'/>
									</td>
								</tr>
							</table>
						</div>
					</ItemTemplate>
				</asp:DataList>

			  <h3>精品商品 低價購</h3>
                  <asp:DataList ID="product_view_bag" runat="server" Width="100%"  RepeatColumns="3" enableEventValidation="true" OnItemCommand="product_view_bag_ItemCommand">
					<ItemTemplate>
						<div class="thumbnail">
							<table  border="0" cellpadding="5" cellspacing="5">
								<tr>
									<td>
									<asp:ImageButton ID="pic_pathname2" runat="server" Height="200"  width="200" CommandName="click"  ImageUrl='<%# Eval("pic_pathname") %>' />
									</td>
								</tr>
								<tr>
									<td align='center'>
									<asp:Label ID="product" runat="server" Text='<%# Eval("product") %>'/>
									</td>
								</tr>
								<tr>
									<td align='center'>
									<asp:Label ID="official_price_price" runat="server" Text='市價:'/>
									<asp:Label ID="official_price" runat="server" Text='<%# Eval("official_price") %>'/>
									</td>
								</tr>
								<tr>
									<td align='center'>
									<asp:Label ID="status" runat="server" Text='<%# Eval("status") %>'/>
									<asp:Label ID="description" runat="server" Visible="false" Text='<%# Eval("description") %>'/>
									<asp:Label ID="action_product_ID" runat="server" Visible="false" Text='<%# Eval("action_product_ID") %>'/>
									<asp:Label ID="seller_ID" runat="server" Visible="false" Text='<%# Eval("seller_ID") %>'/>
									</td>
								</tr>
							</table>
						</div>
					</ItemTemplate>
				</asp:DataList>
		  </div>
		</div>
</div>
</div>
</form>

<!--
Footer
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