using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BitSystem
{
    public partial class grid_view_test : System.Web.UI.Page
    {
        

        SqlDataAdapter da = new SqlDataAdapter();       //SQL 資料庫的連接與執行命令
        DataSet ds = new DataSet();
        SqlCommand cmd = new SqlCommand();
        SqlConnection conn = new SqlConnection();
 
        

        protected void Page_Load(object sender, EventArgs e)
        {
            //設定會員登入與否顯現標示不同
            

            if (Convert.ToString(Session["Login"]) == "logged")
            {
                member_info.Visible = true;
                order_info.Visible = true;
                logout.Visible = true;
            }
            else {
                my_info.Visible = true;
                register.Visible = true;
                manager.Visible = true;
            }



            //fetchProductInfo("Sale_net_Jun18_2021_betaConnectionString3");
            SQL_readActionProduct_life("Sale_net_Jun18_2021_betaConnectionString3");
            product_view_life.DataSource = ds; //將DataSet的資料載入到GridView1內
            product_view_life.DataBind();
            ds.Clear();
            SQL_readActionProduct_cloth("Sale_net_Jun18_2021_betaConnectionString3");
            product_view_cloth.DataSource = ds; //將DataSet的資料載入到GridView1內
            product_view_cloth.DataBind();
            ds.Clear();
            SQL_readActionProduct_bag("Sale_net_Jun18_2021_betaConnectionString3");
            product_view_bag.DataSource = ds; //將DataSet的資料載入到GridView1內
            product_view_bag.DataBind();
            
            

        }//protected void Page_Load(object sender, EventArgs e)


        protected void SQL_readActionProduct_life(string connString)
        {
            cmd.Connection = conn;   //將SQL執行的命令語法程式CMD與CONN與SQL連接

            //設定連線IP位置、資料表，帳戶，密碼
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;
            conn.ConnectionString = s_data; //"Data Source=127.0.0.1;Initial Catalog=NorthwindChinese;Persist Security Info=True";
            //這一行可依連線的字串不同而去定義它該連線到哪個資料庫!!

            cmd.CommandText = $"SELECT Top 3 pic_pathname,product,official_price,status from Action_product where classify = '居家/生活' and status = '拍賣中'";   //執行SQL語法進行查詢
            da.SelectCommand = cmd;            //da選擇資料來源，由cmd載入進來
            da.Fill(ds, "Action_product"); //da把資料填入ds裡面


        }// protected void SQL_readActionProduct()

        protected void SQL_readActionProduct_cloth(string connString)
        {
            cmd.Connection = conn;   //將SQL執行的命令語法程式CMD與CONN與SQL連接

            //設定連線IP位置、資料表，帳戶，密碼
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;
            conn.ConnectionString = s_data; //"Data Source=127.0.0.1;Initial Catalog=NorthwindChinese;Persist Security Info=True";
            //這一行可依連線的字串不同而去定義它該連線到哪個資料庫!!

            cmd.CommandText = $"SELECT Top 3 pic_pathname,product,official_price,status from Action_product where classify = '衣服/飾品' and status = '拍賣中'";   //執行SQL語法進行查詢
            da.SelectCommand = cmd;            //da選擇資料來源，由cmd載入進來
            da.Fill(ds, "Action_product"); //da把資料填入ds裡面


        }// protected void SQL_readActionProduct()

        protected void SQL_readActionProduct_bag(string connString)
        {
            cmd.Connection = conn;   //將SQL執行的命令語法程式CMD與CONN與SQL連接

            //設定連線IP位置、資料表，帳戶，密碼
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;
            conn.ConnectionString = s_data; //"Data Source=127.0.0.1;Initial Catalog=NorthwindChinese;Persist Security Info=True";
            //這一行可依連線的字串不同而去定義它該連線到哪個資料庫!!

            cmd.CommandText = $"SELECT Top 3 pic_pathname,product,official_price,status from Action_product where classify = '包包/精品' and status = '拍賣中' ";   //執行SQL語法進行查詢
            da.SelectCommand = cmd;            //da選擇資料來源，由cmd載入進來
            da.Fill(ds, "Action_product"); //da把資料填入ds裡面


        }// protected void SQL_readActionProduct()

        private void fetchProductInfo(string connString)
        {
            // SQL DB
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;

            //new一個SqlConnection物件，是與資料庫連結的通道(其名為Connection)，以s_data內的連接字串連接所對應的資料庫。
            SqlConnection connection = new SqlConnection(s_data);

            // bug1: SQL content
            string sql_statement = $"SELECT pic_pathname,product,official_price,status from Action_product";

            // bug2: sqlText
            //new一個SqlCommand告訴這個物件準備要執行什麼SQL指令
            SqlCommand Command = new SqlCommand(sql_statement, connection);

            //與資料庫連接的通道開啟
            connection.Open();

            //new一個DataReader接取Execute所回傳的資料。
            SqlDataReader Reader1 = Command.ExecuteReader();

            //檢查是否有資料列
            if (Reader1.HasRows)
            {
                //使用Read方法把資料讀進Reader，讓Reader一筆一筆順向指向資料列，並回傳是否成功。
                while (Reader1.Read())
                {
                    //DataReader讀出欄位內資料的方式，通常也可寫Reader[0]、[1]...[N]代表第一個欄位到N個欄位。


                }// while (Reader.Read())

            }// if (Reader.HasRows) login name match
            else
            {
                //Response.Write("<script>alert('商品資料庫 Action_product 無此帳號！');</script>");

            }// if (Reader.HasRows) login name mismatch
            //關閉與資料庫連接的通道
            connection.Close();
        }// private void fetchGoodPicsPathname()

        

        //頁首連接功能
        protected void home_Click(object sender, EventArgs e)
        {
            Server.Transfer("Home.aspx");
        }

        protected void member_info_Click(object sender, EventArgs e)
        {
            Server.Transfer("memberProfile.aspx");
        }

        protected void order_info_Click(object sender, EventArgs e)
        {
            Server.Transfer("memberOrder.aspx");
        }

        protected void my_info_Click(object sender, EventArgs e)
        {
            Server.Transfer("memberLoginForm.aspx");
        }

        protected void register_Click(object sender, EventArgs e)
        {
            Server.Transfer("memberRegisterForm.aspx");
        }

        protected void contantus_Click(object sender, EventArgs e)
        {
            // email
        }

        protected void manager_Click(object sender, EventArgs e)
        {
            // back manager
        }
        //登出功能
        protected void logout_Click(object sender, EventArgs e)
        {
            Session["Login"] = null;
            Server.Transfer("Home.aspx");
        }


        //左側連接分類功能
        protected void cloth_Click(object sender, EventArgs e)
        {
            Session["classify"] = "衣服/飾品";
            Response.Redirect("GoodListForm.aspx");
        }

        protected void book_Click(object sender, EventArgs e)
        {
            Session["classify"] = "書籍/文創";
            Response.Redirect("GoodListForm.aspx");
        }

        protected void life_Click(object sender, EventArgs e)
        {
            Session["classify"] = "居家/生活";
            Response.Redirect("GoodListForm.aspx");
        }

        protected void bag_Click(object sender, EventArgs e)
        {
            Session["classify"] = "包包/精品";
            Response.Redirect("GoodListForm.aspx");
        }

        protected void shoes_Click(object sender, EventArgs e)
        {
            Session["classify"] = "男女鞋款";
            Response.Redirect("GoodListForm.aspx");
        }

        protected void car_Click(object sender, EventArgs e)
        {
            Session["classify"] = "汽機車/零件百貨";
            Response.Redirect("GoodListForm.aspx");
        }

        protected void entertainment_Click(object sender, EventArgs e)
        {
            Session["classify"] = "娛樂/收藏";
            Response.Redirect("GoodListForm.aspx");
        }

        protected void pet_Click(object sender, EventArgs e)
        {
            Session["classify"] = "寵物/用品";
            Response.Redirect("GoodListForm.aspx");
        }

        protected void others_Click(object sender, EventArgs e)
        {
            Session["classify"] = "其他類別";
            Response.Redirect("GoodListForm.aspx");
        }

        protected void sale_list_Click(object sender, EventArgs e)
        {
            Session["classify"] = null;
            Response.Redirect("GoodListForm.aspx");
        }

    }
}