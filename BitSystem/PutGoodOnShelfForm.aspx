﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PutGoodOnShelfForm.aspx.cs" Inherits="BitSystem.PutGoodOnShelfForm" %>

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
<body style="background-image:url('assets/img/white_leather.png');background-repeat:repeat;" >
<!-- 
Upper Header Section 
-->
<div class="navbar-nav ml-auto">
    <div class="topNav">
        <div class="container">
            <div class="alignR">
                <a href="Sale_login.aspx"> <span class="icon-home"></span> 首頁</a> 
                <a href="memberProfile.aspx"><span class="icon-user"></span> 會員資料</a> 
                <a href="memberOrder.aspx"><span class="icon-edit"></span> 訂單查詢 </a> 
                <a href="#"><span class="icon-envelope"></span> 聯絡我們</a>
                <a href="Sale_unlogin.aspx"><span class="icon-remove"></span> 登出</a>
                <a href="#"><span class="icon-shopping-cart"></span> 購物狀況 - <span class="badge badge-warning"> $448.42</span></a>
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
                    <li class=""><a href="Sale_login.aspx">拍賣站</a></li>
                    <li class=""><a href="Sale_login.aspx">競標拍賣</a></li>
                    <li class=""><a href="Sale_login.aspx">價低拍賣</a></li>
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
    <form id="form1" runat="server">
        <h1>商品上架頁面</h1>
        <asp:Label ID="Label1" runat="server" Text="商品名稱："></asp:Label>
        <asp:TextBox ID="_ProductName" runat="server"></asp:TextBox>
        &nbsp;&nbsp&nbsp;&nbsp;
        <asp:Label ID="Label4" runat="server" Text="商品分類："></asp:Label>
        <asp:DropDownList ID="_Classfy" runat="server" AutoPostBack="True" Height="36px" Width="142px"></asp:DropDownList>
        <br/>
        <asp:Label ID="Label2" runat="server" Text="商品描述："></asp:Label>
        <asp:TextBox ID="_GoodDesc" runat="server" Height="112px" TextMode="MultiLine" Width="429px"></asp:TextBox>
        <br/>
        <asp:Label ID="Label3" runat="server" Text="官方售價："></asp:Label>
        <asp:TextBox ID="_OfficialPrice" runat="server" Width="89px"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label5" runat="server" Text="商品數量："></asp:Label>
        <asp:TextBox ID="_TotalLots" runat="server" Width="84px"></asp:TextBox>
        <br/>
        <asp:Label ID="Label7" runat="server" Text="結標日期："></asp:Label>
        <asp:Calendar ID="_CalendarClosedDate" runat="server" Width="280px"></asp:Calendar>
        <br/>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="_ClosedHr_list" runat="server" AutoPostBack="True" Height="30px" Width="50px"></asp:DropDownList>
        <asp:Label ID="Label8" runat="server" Text="&nbsp;&nbsp; 點 &nbsp;"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="_ClosedMin_list" runat="server" AutoPostBack="True" Height="30px" Width="50px"></asp:DropDownList>
        <asp:Label ID="Label9" runat="server" Text="&nbsp;&nbsp; 分"></asp:Label>
        <br/>
        <br/>
        <asp:Label ID="Label10" runat="server" Text="選取商品圖片："></asp:Label>
        <asp:FileUpload ID="_FileUpload" runat="server"  />
        <br/>
        <asp:Button ID="_SetGoodPicBtn" runat="server" Text="上傳圖片到網站" OnClick="_SetGoodPicBtn_Click"/>
        <asp:Image ID="_ImgGood" runat="server" Width="200" Height="200"/>
        <br/>
        <asp:Button ID="_putOnShelfBtn" runat="server" Text="商品上架" OnClick="_putOnShelfBtn_Click"/>
    </form>
</body>
</html>
