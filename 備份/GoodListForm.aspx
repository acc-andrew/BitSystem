<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodListForm.aspx.cs" Inherits="BitSystem.GoodListForm" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <title>拍賣商品列</title>
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
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body style="background-image:url('assets/img/white_leather.png');background-repeat:repeat;">
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
            <div class="span4">
            
            </div>
            <div class="span4 alignR">
                <p><br> <strong></strong><br><br></p>
                <span ></span>
                <span ></span>
                <span ></span>
                <span ></span>
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
					        <asp:LinkButton ID="sale_list" runat="server" OnClick="sale_list_Click">
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
	    <ul class="breadcrumb">
		    <li><a href="Home.aspx">Home</a> <span class="divider">/</span></li>
		    <li class="active">商品列表</li>
	    </ul>
        <h2>商品列表</h2>

	    </div>
            
        <div class="span9">
            <div class="breadcrumb">
            <div>
                
                <asp:Button ID="_GoodOnShelfBtn" runat="server" Text="商品上架" OnClick="_GoodOnShelfBtn_Click" />
                &nbsp;&nbsp;
                &nbsp;&nbsp;
                <asp:Button ID="_SysAdminBtn" runat="server" Text="系統管理員" />
            </div>
        
            <br/><br/>
            <asp:GridView ID="_GoodsGridView" runat="server"  AutoGenerateColumns="false"
                          CellPadding="4" 
                          AutoGenerateSelectButton="True"
                          OnSelectedIndexChanged="GoodsGridView_SelectedIndexChanged">
                <Columns>
                    <asp:TemplateField HeaderText="圖片" HeaderStyle-Width="200px">
                        <ItemTemplate>
                            <asp:ImageButton ID="img0" runat="server" Height="160"  width="160"  ImageUrl='<%# Eval("pic_pathname") %>' />
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="商品名稱" HeaderStyle-Width="150px">
                        <ItemTemplate>   
                            <asp:Label ID="product_name" runat="server" Text='<%# Eval("product") %>'/>
                        </ItemTemplate> 
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="商品描述" HeaderStyle-Width="500px">
                        <ItemTemplate>   
                            <asp:Label ID="product_desc" runat="server" Text='<%# Eval("description") %>'/>
                            <asp:Label ID="seller_ID" runat="server" Visible ="false" Text='<%# Eval("seller_ID") %>'/>
                            <asp:Label ID="Action_product_ID" runat="server" Visible ="false" Text='<%# Eval("Action_product_ID") %>'/>
                        </ItemTemplate> 
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
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
