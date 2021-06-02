<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sale_product_login.aspx.cs" Inherits="BitSystem.Sale_product_login" %>

<!DOCTYPE html>
<html lang="en">
  <head>
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
<body>
<!-- 
	Upper Header Section 
-->
<div class="navbar-nav ml-auto">
	<div class="topNav">
		<div class="container">
			<div class="alignR">
				<a href="index.html"> <span class="icon-home"></span> 首頁</a> 
				<a href="#"><span class="icon-user"></span> 會員資料</a> 
				<a href="register.html"><span class="icon-edit"></span> 訂單查詢 </a> 
				<a href="contact.html"><span class="icon-envelope"></span> 聯絡我們</a>
				<a href="index.html"><span class="icon-remove"></span> 登出</a>
				<a href="cart.html"><span class="icon-shopping-cart"></span> 購物車 - <span class="badge badge-warning"> $448.42</span></a>
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
	<div class="span4">
	<div class="span4">
	</div>
	</div>
	<div class="span4 alignR">
	<div class="span4">
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
			  <li class=""><a href="index.html">拍賣站</a></li>
			  <li class=""><a href="four-col.html">商城</a></li>
			  <li class=""><a href="list-view.html">競標拍賣</a></li>
			  <li class=""><a href="grid-view.html">價低拍賣</a></li>
			</ul>
			<ul class="nav pull-right">
			<li class="dropdown">
				<a data-toggle="dropdown" class="dropdown-toggle" href="#"><span class="icon-lock"></span> Login <b class="caret"></b></a>
				<div class="dropdown-menu">
				<form class="form-horizontal loginFrm">
				  <div class="control-group">
					<input type="text" class="span2" id="inputEmail" placeholder="Email">
				  </div>
				  <div class="control-group">
					<input type="password" class="span2" id="inputPassword" placeholder="Password">
				  </div>
				  <div class="control-group">
					<label class="checkbox">
					<input type="checkbox"> Remember me
					</label>
					<button type="submit" class="shopBtn btn-block">登入</button>
				  </div>
				</form>
				</div>
			</li>
			</ul>
			<form action="#" class="navbar-search pull-right">
			  <input type="text" placeholder="Search" class="search-query span2">
			</form>
		  </div>
		</div>
	  </div>
	</div>
<!-- 
Body Section 
-->
    <ul class="breadcrumb">
    <li><a href="index.html">首頁</a> <span class="divider">/</span></li>
    <li><a href="products.html">拍賣站</a> <span class="divider">/</span></li>
    <li class="active">價低競標</li>
    </ul>	
	<div class="well well-small">
	<div class="row-fluid">
			<div class="span5">
			<div id="myCarousel" class="carousel slide cntr">
                <div class="carousel-inner">
                  <div class="item active">
                   <a href="#"> <img src="assets/img/a.jpg" alt="" style="width:100%"></a>
                  </div>
                  <div class="item">
                     <a href="#"> <img src="assets/img/b.jpg" alt="" style="width:100%"></a>
                  </div>
                  <div class="item">
                    <a href="#"> <img src="assets/img/e.jpg" alt="" style="width:100%"></a>
                  </div>
                </div>
                <a class="left carousel-control" href="#myCarousel" data-slide="prev">‹</a>
                <a class="right carousel-control" href="#myCarousel" data-slide="next">›</a>
            </div>
			</div>
			<div class="span7">
				<h3>Name of the Item [$140.00]</h3>
				<hr class="soft"/>
				
				<form class="form-horizontal qtyFrm">
				  <div class="control-group">
					<label class="control-label"><span>$140.00</span></label>
					<div class="controls">
					<input type="number" class="span6" placeholder="Qty.">
					</div>
				  </div>
				
				  <div class="control-group">
					<label class="control-label"><span>Color</span></label>
					<div class="controls">
					  <select class="span11">
						  <option>Red</option>
						  <option>Purple</option>
						  <option>Pink</option>
						  <option>Red</option>
						</select>
					</div>
				  </div>
				  <div class="control-group">
					<label class="control-label"><span>Materials</span></label>
					<div class="controls">
					  <select class="span11">
						  <option>Material 1</option>
						  <option>Material 2</option>
						  <option>Material 3</option>
						  <option>Material 4</option>
						</select>
					</div>
				  </div>
				  <h4>100 items in stock</h4>
				  <p>Nowadays the lingerie industry is one of the most successful business spheres.
				  Nowadays the lingerie industry is one of ...
				  <p>
				  <button type="submit" class="shopBtn"><span class=" icon-shopping-cart"></span> Add to cart</button>
				</form>
			</div>
			</div>
				<hr class="softn clr"/>

            <div id="myTabContent" class="tab-content tabWrapper">
            <div class="tab-pane fade active in" id="home">
			  <h4>Product Information</h4>
                <table class="table table-striped">
				<tbody>
				<tr class="techSpecRow"><td class="techSpecTD1">Color:</td><td class="techSpecTD2">Black</td></tr>
				<tr class="techSpecRow"><td class="techSpecTD1">Style:</td><td class="techSpecTD2">Apparel,Sports</td></tr>
				<tr class="techSpecRow"><td class="techSpecTD1">Season:</td><td class="techSpecTD2">spring/summer</td></tr>
				<tr class="techSpecRow"><td class="techSpecTD1">Usage:</td><td class="techSpecTD2">fitness</td></tr>
				<tr class="techSpecRow"><td class="techSpecTD1">Sport:</td><td class="techSpecTD2">122855031</td></tr>
				<tr class="techSpecRow"><td class="techSpecTD1">Brand:</td><td class="techSpecTD2">Shock Absorber</td></tr>
				</tbody>
				</table>
				<p>Raw denim you probably haven't heard of them jean shorts Austin. Nesciunt tofu stumptown aliqua, retro synth master cleanse. Mustache cliche tempor, williamsburg carles vegan helvetica. Reprehenderit butcher retro keffiyeh dreamcatcher synth. Cosby sweater eu banh mi, qui irure terry richardson ex squid. Aliquip placeat salvia cillum iphone. Seitan aliquip quis cardigan american apparel, butcher voluptate nisi qui.</p>

<!-- 
Clients 
-->
<section class="our_client">
	<hr class="soften"/>
	<hr class="soften"/>
	
</section>

<!-- /container -->


<a href="#" class="gotop"><i class="icon-double-angle-up"></i></a>
    <!-- Placed at the end of the document so the pages load faster -->
    <script src="assets/js/jquery.js"></script>
	<script src="assets/js/bootstrap.min.js"></script>
	<script src="assets/js/jquery.easing-1.3.min.js"></script>
    <script src="assets/js/jquery.scrollTo-1.4.3.1-min.js"></script>
    <script src="assets/js/shop.js"></script>
  </body>
</html>
