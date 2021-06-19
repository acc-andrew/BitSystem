﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BitSystem
{
    public partial class sale_chickout_product : System.Web.UI.Page
    {
        
        SqlDataAdapter da = new SqlDataAdapter();       //SQL 資料庫的連接與執行命令
        DataSet ds = new DataSet();
        SqlCommand cmd = new SqlCommand();
        SqlConnection conn = new SqlConnection();
        int low_price;

        protected void Page_Load(object sender, EventArgs e)
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
                Response.Redirect("memberLoginForm.aspx");
            }

            //Session["user"] = "Tom";
            //Session["member_ID"] = "13";
            // pre-fetch picture pathname from Market_product2 DB

            fetchProductInfo();
            SQL_readActionProduct("Sale_net_Jun18_2021_betaConnectionString");
            GridView1.DataSource = ds; //將DataSet的資料載入到GridView1內
            GridView1.DataBind();


            total_price.Text = low_price.ToString();
        }//protected void Page_Load(object sender, EventArgs e)


        


        protected void SQL_readActionProduct(string connString)
        {
            cmd.Connection = conn;   //將SQL執行的命令語法程式CMD與CONN與SQL連接

            //設定連線IP位置、資料表，帳戶，密碼
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;
            conn.ConnectionString = s_data; //"Data Source=127.0.0.1;Initial Catalog=NorthwindChinese;Persist Security Info=True";
            //這一行可依連線的字串不同而去定義它該連線到哪個資料庫!!

            cmd.CommandText = $"SELECT pic_pathname,product,total_number,low_price from Action_product where bid_winner_ID ='" +Session["member_ID"]+"'";   //執行SQL語法進行查詢
            da.SelectCommand = cmd;            //da選擇資料來源，由cmd載入進來
            da.Fill(ds, "Action_product");            //da把資料填入ds裡面

        }// protected void SQL_readActionProduct()


        protected void Button3_Click(object sender, EventArgs e)
        {
            // if user hasn't logged, redirect to memberLoginForm
            if (((string)Session["member_ID"]) == "")
            {
                Response.Redirect("memberLoginForm.aspx");
            }
            else
            {
                Response.Redirect("sale_chickout_member.aspx");
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }

        private void fetchProductInfo()
        {
            // SQL DB
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Sale_net_Jun18_2021_betaConnectionString"].ConnectionString;

            //new一個SqlConnection物件，是與資料庫連結的通道(其名為Connection)，以s_data內的連接字串連接所對應的資料庫。
            SqlConnection connection = new SqlConnection(s_data);

            // bug1: SQL content
            string sql_statement = $"SELECT pic_pathname,product,total_number,low_price from Action_product where bid_winner_ID =" + Session["member_ID"];

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
                    
                    low_price = int.Parse(Reader1["low_price"].ToString());




                }// while (Reader.Read())

            }// if (Reader.HasRows) login name match
            else
            {
                Response.Write("<script>alert('商品資料庫 Action_product 無此帳號！');</script>");

            }// if (Reader.HasRows) login name mismatch
            //關閉與資料庫連接的通道
            connection.Close();
        }// private void fetchGoodPicsPathname()

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
            Response.Redirect("list_view.aspx");
        }

        protected void book_Click(object sender, EventArgs e)
        {
            Session["classify"] = "book";
            Response.Redirect("list_view.aspx");
        }

        protected void life_Click(object sender, EventArgs e)
        {
            Session["classify"] = "life";
            Response.Redirect("list_view.aspx");
        }

        protected void bag_Click(object sender, EventArgs e)
        {
            Session["classify"] = "bag";
            Response.Redirect("list_view.aspx");
        }

        protected void shoes_Click(object sender, EventArgs e)
        {
            Session["classify"] = "shoes";
            Response.Redirect("list_view.aspx");
        }

        protected void car_Click(object sender, EventArgs e)
        {
            Session["classify"] = "car";
            Response.Redirect("list_view.aspx");
        }

        protected void entertainment_Click(object sender, EventArgs e)
        {
            Session["classify"] = "entertainment";
            Response.Redirect("list_view.aspx");
        }

        protected void pet_Click(object sender, EventArgs e)
        {
            Session["classify"] = "pet";
            Response.Redirect("list_view.aspx");
        }

        protected void others_Click(object sender, EventArgs e)
        {
            Session["classify"] = "others";
            Response.Redirect("list_view.aspx");
        }

    }
}