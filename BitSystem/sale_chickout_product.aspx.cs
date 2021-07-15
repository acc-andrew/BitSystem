using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static BitSystem.BitProductForm;

namespace BitSystem
{
    public partial class sale_chickout_product : System.Web.UI.Page
    {
        //設定資料庫資訊
        string connString = "Sale_net_Jun22_2021ConnectionString";

        SqlDataAdapter da = new SqlDataAdapter(); //SQL 資料庫的連接與執行命令
        DataSet ds = new DataSet();
        SqlCommand cmd = new SqlCommand();
        SqlConnection conn = new SqlConnection();
        DataSet ds_getbid = new DataSet();

        //設定總共價錢
        int total_low_price;
        // 建立得標手續費資料

        protected void Page_Load(object sender, EventArgs e)
        {

            if (IsPostBack == false)
            {
                //設定會員登入與否顯現標示不同
                if (Convert.ToString(Session["Login"]) == "logged")
                {
                    member_info.Visible = true;
                    order_info.Visible = true;
                    logout.Visible = true;

                    fetchProductInfo(connString);
                    SQL_readActionProduct(connString);
                    getbid_product.DataSource = ds; //將DataSet的資料載入到GridView1內
                    getbid_product.DataBind();
                    
                    // 抓取dataset 給後續寄信用
                    Session["getbid_datalist"] = ds;
                    // 抓取total price 給後續寄信用
                    total_price.Text = total_low_price.ToString();
                    Session["total_price"] = total_low_price;

                }
                else
                {
                    my_info.Visible = true;
                    register.Visible = true;
                    manager.Visible = true;
                    Session["logged_to_page"] = "sale_chickout_product.aspx";
                    Response.Redirect("memberLoginForm.aspx");
                }

                // 載入已結標熱門
                SQL_readActionProduct_getbid(connString);
                getbid_view.DataSource = ds_getbid; //將DataSet的資料載入到datalist內
                getbid_view.DataBind();

            }
        }//protected void Page_Load(object sender, EventArgs e)


        // 左測已結標商品展示
        protected void SQL_readActionProduct_getbid(string connString)
        {
            cmd.Connection = conn;
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;
            conn.ConnectionString = s_data;

            cmd.CommandText = $"SELECT Top 3 pic_pathname,official_price,low_price,Member.name " +
                 $"FROM Action_product " +
                 $"INNER JOIN Member " +
                 $"ON Action_product.bid_winner_ID = Member.member_ID " +
                 $"ORDER BY Action_product.closedDateTime Desc";   //執行SQL語法進行查詢

            da.SelectCommand = cmd;            //da選擇資料來源，由cmd載入進來
            da.Fill(ds_getbid, "Action_product"); //da把資料填入ds裡面
        }

        // 載入得標商品資料
        protected void SQL_readActionProduct(string connString)
        {
            cmd.Connection = conn;   //將SQL執行的命令語法程式CMD與CONN與SQL連接

            //設定連線IP位置、資料表，帳戶，密碼
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;
            conn.ConnectionString = s_data; //"Data Source=127.0.0.1;Initial Catalog=NorthwindChinese;Persist Security Info=True";
            //這一行可依連線的字串不同而去定義它該連線到哪個資料庫!!

            cmd.CommandText = $"SELECT * " +
                    $"FROM [Action_product] " +
                    $"where status='getbid' AND bid_winner_ID = " + Session["member_ID"];

            da.SelectCommand = cmd;            //da選擇資料來源，由cmd載入進來
            da.Fill(ds, "Action_product");            //da把資料填入ds裡面
            ds.Tables[0].Columns.Add("handling_fee", typeof(int)); // 建立一個欄位放手續費

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                // 找出每筆下標的商品是哪個
                string bit_product_ID;

                if (ds.Tables[0].Rows[i]["action_product_ID"] != null)
                {
                    bit_product_ID = (ds.Tables[0].Rows[i]["action_product_ID"]).ToString();
                }
                else
                {
                    bit_product_ID = "0";
                }
                string handlingfee_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;
                SqlConnection connection = new SqlConnection(handlingfee_data);
                
                string sql_statement = $"select count(*) as co from Action_product " +
                    "where bidder_ID='" + Session["member_ID"] + "' AND bit_product_ID='" + bit_product_ID + "'";

                SqlCommand Command = new SqlCommand(sql_statement, connection);
                connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();
                int intCount;
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        intCount = Int32.Parse(Reader["co"].ToString());
                        intCount *= 10;
                        ds.Tables[0].Rows[i]["handling_fee"] = intCount;
                    }// while (Reader.Read())
                }
                connection.Close();

            }
        }// protected void SQL_readActionProduct()

        // 前往確認收件人資料網頁
        protected void checkdata_Click(object sender, EventArgs e)
        {
            // if user hasn't logged, redirect to memberLoginForm
            if (Session["member_ID"] == null)
            {
                Session["logged_to_page"] = "sale_chickout_product.aspx";
                Response.Redirect("memberLoginForm.aspx");
            }
            else
            {
                Session["chickout_product"] = "chicked";
                Response.Redirect("sale_chickout_member.aspx");
            }
        }

        protected void backbid_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }

        // 計算總共價錢
        private void fetchProductInfo(string connString)
        {
            // SQL DB
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;
            SqlConnection connection = new SqlConnection(s_data);
            string sql_statement = $"SELECT pic_pathname,product,total_number,low_price,bid_price " +
                $"from Action_product " +
                $"where status = 'getbid' AND bid_winner_ID ='" + Session["member_ID"] + "'";
            SqlCommand Command = new SqlCommand(sql_statement, connection);
            connection.Open();
            SqlDataReader Reader = Command.ExecuteReader();
            if (Reader.HasRows)
            {
                while (Reader.Read())
                {
                    total_low_price += int.Parse(Reader["low_price"].ToString());
                }// while (Reader.Read())

            }// if (Reader.HasRows) login name match
            else
            {
                Response.Write("<script>alert('還未有得標商品~要再去逛逛嗎?！');</script>");
                Server.Transfer("Home.aspx");
            }// if (Reader.HasRows) login name mismatch
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
            Response.Redirect("contactus_mail.aspx");
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

    }
}