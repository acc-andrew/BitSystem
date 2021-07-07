using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace BitSystem
{
    public partial class Home : System.Web.UI.Page
    {
        SqlDataAdapter da = new SqlDataAdapter();       //SQL 資料庫的連接與執行命令
        DataSet ds_first = new DataSet();
        DataSet ds_sec = new DataSet();
        DataSet ds_third = new DataSet();
        DataSet ds_getbid = new DataSet();
        SqlCommand cmd = new SqlCommand();
        SqlConnection conn = new SqlConnection();

        //設定資料庫資訊
        string connString = "Sale_net_Jun22_2021ConnectionString";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //設定會員登入與否顯現標示不同
                if (Convert.ToString(Session["Login"]) == "logged")
                {
                    member_info.Visible = true;
                    order_info.Visible = true;
                    logout.Visible = true;
                }
                else
                {
                    my_info.Visible = true;
                    register.Visible = true;
                    manager.Visible = true;
                }

                //fetchProductInfo("Sale_net_Jun18_2021_betaConnectionString3");
                // 載入生活熱門
                SQL_readActionProduct_life(connString);
                product_view_life.DataSource = ds_first; //將DataSet的資料載入到datalist內
                product_view_life.DataBind();
                // 載入衣服熱門
                SQL_readActionProduct_cloth(connString);
                product_view_cloth.DataSource = ds_sec; //將DataSet的資料載入到datalist內
                product_view_cloth.DataBind();
                // 載入包包熱門
                SQL_readActionProduct_bag(connString);
                product_view_bag.DataSource = ds_third; //將DataSet的資料載入到datalist內
                product_view_bag.DataBind();
                // 載入以截標熱門
                SQL_readActionProduct_getbid(connString);
                getbid_view.DataSource = ds_getbid; //將DataSet的資料載入到datalist內
                getbid_view.DataBind();

            }

        }
        // 左側以截標商品展示
        protected void SQL_readActionProduct_getbid(string connString)
        {
            cmd.Connection = conn;   //將SQL執行的命令語法程式CMD與CONN與SQL連接
            //設定連線IP位置、資料表，帳戶，密碼
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;
            conn.ConnectionString = s_data; //"Data Source=127.0.0.1;Initial Catalog=NorthwindChinese;Persist Security Info=True";
            //這一行可依連線的字串不同而去定義它該連線到哪個資料庫!!

            cmd.CommandText = $"SELECT Top 3 pic_pathname,official_price,low_price,Member.name " +
                $"FROM Action_product " +
                $"INNER JOIN Member " +
                $"ON Action_product.bid_winner_ID = Member.member_ID " +
                $"ORDER BY Action_product.closedDateTime Desc";   //執行SQL語法進行查詢
            da.SelectCommand = cmd;            //da選擇資料來源，由cmd載入進來

            da.Fill(ds_getbid, "Action_product"); //da把資料填入ds裡面


        }// protected void SQL_readActionProduct()

        protected void SQL_readActionProduct_life(string connString)
        {
            cmd.Connection = conn;   //將SQL執行的命令語法程式CMD與CONN與SQL連接

            //設定連線IP位置、資料表，帳戶，密碼
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;
            conn.ConnectionString = s_data; //"Data Source=127.0.0.1;Initial Catalog=NorthwindChinese;Persist Security Info=True";
            //這一行可依連線的字串不同而去定義它該連線到哪個資料庫!!

            cmd.CommandText = $"SELECT Top 3 pic_pathname,product,official_price,status,description,seller_ID,action_product_ID from Action_product where classify = 'life' and status = 'onsale'";   //執行SQL語法進行查詢
            da.SelectCommand = cmd;            //da選擇資料來源，由cmd載入進來
            da.Fill(ds_first, "Action_product"); //da把資料填入ds裡面


        }// protected void SQL_readActionProduct()


        protected void SQL_readActionProduct_cloth(string connString)
        {
            cmd.Connection = conn;   //將SQL執行的命令語法程式CMD與CONN與SQL連接

            //設定連線IP位置、資料表，帳戶，密碼
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;
            conn.ConnectionString = s_data; //"Data Source=127.0.0.1;Initial Catalog=NorthwindChinese;Persist Security Info=True";
            //這一行可依連線的字串不同而去定義它該連線到哪個資料庫!!

            cmd.CommandText = $"SELECT Top 3 pic_pathname,product,official_price,status,description,seller_ID,action_product_ID from Action_product where classify = 'cloth' and status = 'onsale'";   //執行SQL語法進行查詢
            da.SelectCommand = cmd;            //da選擇資料來源，由cmd載入進來
            da.Fill(ds_sec, "Action_product"); //da把資料填入ds裡面


        }// protected void SQL_readActionProduct()

        protected void SQL_readActionProduct_bag(string connString)
        {
            cmd.Connection = conn;   //將SQL執行的命令語法程式CMD與CONN與SQL連接

            //設定連線IP位置、資料表，帳戶，密碼
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;
            conn.ConnectionString = s_data; //"Data Source=127.0.0.1;Initial Catalog=NorthwindChinese;Persist Security Info=True";
            //這一行可依連線的字串不同而去定義它該連線到哪個資料庫!!

            cmd.CommandText = $"SELECT Top 3 pic_pathname,product,official_price,status,description,seller_ID,action_product_ID from Action_product where classify = 'bag' and status = 'onsale' ";   //執行SQL語法進行查詢
            da.SelectCommand = cmd;            //da選擇資料來源，由cmd載入進來
            da.Fill(ds_third, "Action_product"); //da把資料填入ds裡面


        }// protected void SQL_readActionProduct()

        

        protected void product_view_life_ItemCommand(object source,DataListCommandEventArgs e)
        {
            DataListItem currentItem = e.Item;
            if (e.CommandName == "click")
            {
                product_view_life.SelectedIndex = currentItem.ItemIndex;

                string pic_pathname = ((ImageButton)currentItem.FindControl("pic_pathname")).ImageUrl;
                string product = ((Label)currentItem.FindControl("product")).Text;
                string action_product_ID = ((Label)currentItem.FindControl("action_product_ID")).Text;
                string description = ((Label)currentItem.FindControl("description")).Text;
                string seller_ID = ((Label)currentItem.FindControl("seller_ID")).Text;

                Session["ProductName"] = product;
                Session["ProductDesc"] = description;
                Session["ImageUrl"] = pic_pathname;
                Session["ProductID"] = action_product_ID;
                Session["SellerID"] = seller_ID;

                Response.Redirect("BitProductForm.aspx");
            }
        }

        protected void product_view_cloth_ItemCommand(object source, DataListCommandEventArgs e)
        {
            DataListItem currentItem = e.Item;

            if (e.CommandName == "click")
            {
                product_view_cloth.SelectedIndex = currentItem.ItemIndex;

                string pic_pathname1 = ((ImageButton)currentItem.FindControl("pic_pathname1")).ImageUrl;
                string product = ((Label)currentItem.FindControl("product")).Text;
                string action_product_ID = ((Label)currentItem.FindControl("action_product_ID")).Text;
                string description = ((Label)currentItem.FindControl("description")).Text;
                string seller_ID = ((Label)currentItem.FindControl("seller_ID")).Text;

                Session["ProductName"] = product;
                Session["ProductDesc"] = description;
                Session["ImageUrl"] = pic_pathname1;
                Session["ProductID"] = action_product_ID;
                Session["SellerID"] = seller_ID;

                Response.Redirect("BitProductForm.aspx");
            }
        }

        protected void product_view_bag_ItemCommand(object source, DataListCommandEventArgs e)
        {
            DataListItem currentItem = e.Item;

            if (e.CommandName == "click")
            {
                product_view_bag.SelectedIndex = currentItem.ItemIndex;

                string pic_pathname2 = ((ImageButton)currentItem.FindControl("pic_pathname2")).ImageUrl;
                string product = ((Label)currentItem.FindControl("product")).Text;
                string action_product_ID = ((Label)currentItem.FindControl("action_product_ID")).Text;
                string description = ((Label)currentItem.FindControl("description")).Text;
                string seller_ID = ((Label)currentItem.FindControl("seller_ID")).Text;

                Session["ProductName"] = product;
                Session["ProductDesc"] = description;
                Session["ImageUrl"] = pic_pathname2;
                Session["ProductID"] = action_product_ID;
                Session["SellerID"] = seller_ID;

                Response.Redirect("BitProductForm.aspx");
            }
        }


        //linkbutton 點擊連接網址
        protected void home_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }

        protected void member_info_Click(object sender, EventArgs e)
        {
            Response.Redirect("memberProfile.aspx");
        }

        protected void order_info_Click(object sender, EventArgs e)
        {
            Response.Redirect("memberOrder.aspx");
        }

        protected void my_info_Click(object sender, EventArgs e)
        {
            Response.Redirect("memberLoginForm.aspx");
        }

        protected void register_Click(object sender, EventArgs e)
        {
            Response.Redirect("memberRegisterForm.aspx");
        }

        protected void contantus_Click(object sender, EventArgs e)
        {
            // email
        }

        protected void manager_Click(object sender, EventArgs e)
        {
            // back manager
        }

        protected void logout_Click(object sender, EventArgs e)
        {
            Session["Login"] = null;
            Response.Redirect("Home.aspx");
        }

        //左側連接分類功能
        protected void cloth_Click(object sender, EventArgs e)
        {
            Session["classify"] = "cloth";
            Response.Redirect("GoodListForm.aspx");
        }

        protected void book_Click(object sender, EventArgs e)
        {
            Session["classify"] = "book";
            Response.Redirect("GoodListForm.aspx");
        }

        protected void life_Click(object sender, EventArgs e)
        {
            Session["classify"] = "life";
            Response.Redirect("GoodListForm.aspx");
        }

        protected void bag_Click(object sender, EventArgs e)
        {
            Session["classify"] = "bag";
            Response.Redirect("GoodListForm.aspx");
        }

        protected void shoes_Click(object sender, EventArgs e)
        {
            Session["classify"] = "shoes";
            Response.Redirect("GoodListForm.aspx");
        }

        protected void car_Click(object sender, EventArgs e)
        {
            Session["classify"] = "car";
            Response.Redirect("GoodListForm.aspx");
        }

        protected void entertainment_Click(object sender, EventArgs e)
        {
            Session["classify"] = "habbit";
            Response.Redirect("GoodListForm.aspx");
        }

        protected void pet_Click(object sender, EventArgs e)
        {
            Session["classify"] = "pet";
            Response.Redirect("GoodListForm.aspx");
        }

        protected void others_Click(object sender, EventArgs e)
        {
            Session["classify"] = "other";
            Response.Redirect("GoodListForm.aspx");
        }

        protected void sale_list_Click(object sender, EventArgs e)
        {
            Session["classify"] = null;
            Response.Redirect("GoodListForm.aspx");
        }

        protected void more_hot_Click(object sender, EventArgs e)
        {
            Response.Redirect("GoodListForm.aspx");
        }


    }
}