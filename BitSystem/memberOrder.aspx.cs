using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;

namespace BitSystem
{
    public partial class memberOrder : System.Web.UI.Page
    {
        SqlDataAdapter da = new SqlDataAdapter();       //SQL 資料庫的連接與執行命令
        DataSet ds = new DataSet();
        SqlCommand cmd = new SqlCommand();
        SqlConnection conn = new SqlConnection();

        protected void Page_Load(object sender, EventArgs e)
        {
            //設定會員登入與否顯現標示不同

            if (IsPostBack == false)
            {
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

                if (DropDownList1.SelectedValue == "已上架")
                {
                    product_view.Visible = true;
                    // pre-fetch picture pathname from Market_product2 DB
                    fetchProductInfo();

                    string cmdText_onsale = "SELECT pic_pathname,product,description,total_number,seller_ID,action_product_ID,official_price " +
                    "from Action_product where status='onsale' and seller_ID='" + Session["member_ID"] + "'";

                    SQL_readActionProduct("Sale_net_Jun22_2021ConnectionString", cmdText_onsale);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        product_view.DataSource = ds; //將DataSet的資料載入到GridView1內
                        product_view.DataBind();
                    }
                    else
                    {
                        Response.Write("<script>alert('還沒有商品喔!');</script>");
                        product_view.Visible = false;
                    }
                }
            }

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cmdText_onsale = "SELECT pic_pathname,product,description,total_number,seller_ID,action_product_ID,official_price " +
                "from Action_product where status='onsale' and seller_ID='" + Session["member_ID"] + "'";

            string cmdText_bidding = "SELECT pic_pathname,product,description,total_number,seller_ID,action_product_ID,official_price " +
                "from Action_product where status='bidding' and seller_ID='" + Session["member_ID"] + "'";

            string cmdText_getbid = "SELECT pic_pathname,product,description,total_number,seller_ID,action_product_ID,official_price " +
                "from Action_product where status='getbid' and seller_ID='" + Session["member_ID"] + "'";

            if (DropDownList1.SelectedValue == "已上架")
            {
                product_view.Visible = true;
                // pre-fetch picture pathname from Market_product2 DB
                fetchProductInfo();

                SQL_readActionProduct("Sale_net_Jun22_2021ConnectionString", cmdText_onsale);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    product_view.DataSource = ds; //將DataSet的資料載入到GridView1內
                    product_view.DataBind();
                }
                else
                {
                    Response.Write("<script>alert('還沒有商品喔!');</script>");
                    product_view.Visible = false;
                }
            }
            else if (DropDownList1.SelectedValue == "競標中")
            {
                product_view.Visible = true;
                // pre-fetch picture pathname from Market_product2 DB
                fetchProductInfo();

                SQL_readActionProduct("Sale_net_Jun22_2021ConnectionString", cmdText_bidding);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    product_view.DataSource = ds; //將DataSet的資料載入到GridView1內
                    product_view.DataBind();
                }
                else
                {
                    Response.Write("<script>alert('還沒有商品喔!');</script>");
                    product_view.Visible = false;
                }
            }
            else if (DropDownList1.SelectedValue == "已得標")
            {
                product_view.Visible = true;
                // pre-fetch picture pathname from Market_product2 DB
                fetchProductInfo();

                SQL_readActionProduct("Sale_net_Jun22_2021ConnectionString", cmdText_getbid);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    product_view.DataSource = ds; //將DataSet的資料載入到GridView1內
                    product_view.DataBind();
                }
                else
                {
                    Response.Write("<script>alert('還沒有商品喔!');</script>");
                    product_view.Visible = false;
                }
            }         
        }

        protected void product_view_ItemCommand(object source, DataListCommandEventArgs e)
        {
            DataListItem currentItem = e.Item;

            if (e.CommandName == "click")
            {

                product_view.SelectedIndex = currentItem.ItemIndex;

                string pic_pathname = ((ImageButton)currentItem.FindControl("pic_pathname")).ImageUrl;
                string product = ((Label)currentItem.FindControl("product")).Text;
                string official_price = ((Label)currentItem.FindControl("official_price")).Text;
                string action_product_ID = ((Label)currentItem.FindControl("action_product_ID")).Text;
                string description = ((Label)currentItem.FindControl("description")).Text;
                string seller_ID = ((Label)currentItem.FindControl("seller_ID")).Text;

                Session["ProductName"] = product;
                Session["ProductDesc"] = description;
                Session["official_price"] = official_price;
                Session["ImageUrl"] = pic_pathname;
                Session["ProductID"] = action_product_ID;
                Session["SellerID"] = seller_ID;

                Response.Redirect("BitProductForm.aspx");

            }
        }

        protected void product_view_DataBound(object sender, EventArgs e)
        {
            // 演示ToolTip，使用GridView自帶的ToolTip
            for (int i = 0; i < product_view.Items.Count; i++)
            {
                product_view.Items[i].ToolTip = ((Label)product_view.Items[i].FindControl("description")).Text;
                if (((Label)product_view.Items[i].FindControl("description")).Text.Length > 150)
                {
                    ((Label)product_view.Items[i].FindControl("description")).Text = ((Label)product_view.Items[i].FindControl("description")).Text.Substring(0, 125) + "......";
                }
            }
        }


        private void fetchProductInfo()
        {
            // SQL DB
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Sale_net_Jun22_2021ConnectionString"].ConnectionString;

            //new一個SqlConnection物件，是與資料庫連結的通道(其名為Connection)，以s_data內的連接字串連接所對應的資料庫。
            SqlConnection connection = new SqlConnection(s_data);

            // bug1: SQL content
            string sql_statement = $"select action_product_ID,product,total_number,description,seller_ID,pic_pathname from Action_product";

            // bug2: sqlText
            //new一個SqlCommand告訴這個物件準備要執行什麼SQL指令
            SqlCommand Command = new SqlCommand(sql_statement, connection);

            //與資料庫連接的通道開啟
            connection.Open();

            //new一個DataReader接取Execute所回傳的資料。
            SqlDataReader Reader = Command.ExecuteReader();

            //檢查是否有資料列
            if (Reader.HasRows)
            {
                //使用Read方法把資料讀進Reader，讓Reader一筆一筆順向指向資料列，並回傳是否成功。
                while (Reader.Read())
                {
   
                }// while (Reader.Read())

            }// if (Reader.HasRows) login name match
            else
            {
                Response.Write("<script>alert('商品資料庫 Action_product 無此帳號！');</script>");

            }// if (Reader.HasRows) login name mismatch
            //關閉與資料庫連接的通道
            connection.Close();
        }// private void fetchGoodPicsPathname()

        
        protected void SQL_readActionProduct(string connString,string cmdText)
        {
            cmd.Connection = conn;   //將SQL執行的命令語法程式CMD與CONN與SQL連接

            //設定連線IP位置、資料表，帳戶，密碼
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;
            conn.ConnectionString = s_data; //"Data Source=127.0.0.1;Initial Catalog=NorthwindChinese;Persist Security Info=True";
            //這一行可依連線的字串不同而去定義它該連線到哪個資料庫!!

            cmd.CommandText = cmdText;   //執行SQL語法進行查詢
            da.SelectCommand = cmd;            //da選擇資料來源，由cmd載入進來
            da.Fill(ds, "Action_product");            //da把資料填入ds裡面

        }// protected void SQL_readActionProduct()



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
    }
}