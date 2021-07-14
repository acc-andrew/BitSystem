using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebGrease.Css;

namespace BitSystem
{
    public partial class contactus_mail : System.Web.UI.Page
    {
        SqlDataAdapter da = new SqlDataAdapter();       //SQL 資料庫的連接與執行命令
        SqlConnection conn = new SqlConnection();
        DataSet ds_getbid = new DataSet();
        SqlDataAdapter da_contact = new SqlDataAdapter();
        DataSet ds_contact = new DataSet();
        SqlCommand cmd = new SqlCommand();

        //設定資料庫資訊
        string connString = "Sale_net_Jun22_2021ConnectionString5";
        protected void Page_Load(object sender, EventArgs e)
        {
            // if the page loaded first time
            if (IsPostBack == false)
            {
                //設定會員登入與否顯現標示不同
                if (Convert.ToString(Session["Login"]) == "logged")
                {
                    member_info.Visible = true;
                    order_info.Visible = true;
                    logout.Visible = true;
                    member_ID.Text = Session["member_ID"].ToString();
                }
                else
                {
                    my_info.Visible = true;
                    register.Visible = true;
                    manager.Visible = true;
                    Response.Redirect("memberLoginForm.aspx");
                }

                // 載入已結標熱門
                SQL_readActionProduct_getbid(connString);
                getbid_view.DataSource = ds_getbid; //將DataSet的資料載入到datalist內
                getbid_view.DataBind();

                // 載入曾詢問過問題
                SQL_read_contact_us(connString);
                contactus_view.DataSource = ds_contact; //將DataSet的資料載入到datalist內
                contactus_view.DataBind();
            }
            else
            { // postback

            }// postback

        }// protected void Page_Load(object sender, EventArgs e)

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

        // 寫入建議到資料庫
        protected void SQLDB_writeContactUs(string connString)
        {
         
            int seller_ID = (int)(Session["member_ID"]);

            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;

            //new一個SqlConnection物件，是與資料庫連結的通道(其名為Connection)，以s_data內的連接字串連接所對應的資料庫。
            SqlConnection connection = new SqlConnection(s_data);

            //與資料庫連接的通道開啟
            connection.Open();
            // _File
            SqlCommand sql_insert_cmd = new SqlCommand("insert into Contact_us" +
                "(member_ID,action_product_ID,target,contact_text) " +
                "values(@member_ID,@action_product_ID,@target,@contact_text);", connection); //SQL語句

            // run-time error type mismatch
            sql_insert_cmd.Parameters.Add("@member_ID", SqlDbType.Int);
            sql_insert_cmd.Parameters["@member_ID"].Value = Session["member_ID"];

            sql_insert_cmd.Parameters.Add("@action_product_ID", SqlDbType.NVarChar);
            sql_insert_cmd.Parameters["@action_product_ID"].Value = action_product_ID.Text;

            sql_insert_cmd.Parameters.Add("@target", SqlDbType.NVarChar);
            sql_insert_cmd.Parameters["@target"].Value = target.Text;
            
            sql_insert_cmd.Parameters.Add("@contact_text", SqlDbType.NVarChar);
            sql_insert_cmd.Parameters["@contact_text"].Value = contact_text.Text;
            
            sql_insert_cmd.ExecuteNonQuery();

            //關閉與資料庫連接的通道
            connection.Close();

            Response.Write($"<script>alert('送出建議成功');</script>");

        }// protected void SQLDB_write()

        // 顯示提問過問題
        protected void SQL_read_contact_us(string connString)
        {
            cmd.Connection = conn;   //將SQL執行的命令語法程式CMD與CONN與SQL連接
            //設定連線IP位置、資料表，帳戶，密碼
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;
            conn.ConnectionString = s_data; //"Data Source=127.0.0.1;Initial Catalog=NorthwindChinese;Persist Security Info=True";
                                            //這一行可依連線的字串不同而去定義它該連線到哪個資料庫!!

            cmd.CommandText = $"select *" +
                $"from Contact_us " +
                $"where member_ID ='{Session["member_ID"]}'";

            da_contact.SelectCommand = cmd;            //da選擇資料來源，由cmd載入進來
            da_contact.Fill(ds_contact, "Contact_us"); //da把資料填入ds裡面

            for (int i = 0; i < ds_contact.Tables[0].Rows.Count; i++)
            {
                // 如果客服尚未回覆為NULL
                string feedback_text;
                if ((ds_contact.Tables[0].Rows[i]["feedback"]).ToString() == "")
                {
                    feedback_text = "尚未回覆";
                }
                else
                {
                    feedback_text = (ds_contact.Tables[0].Rows[i]["feedback"]).ToString();
                }

                ds_contact.Tables[0].Rows[i]["feedback"] = feedback_text;
            }
        }// protected void SQL_readActionProduct()

        protected void contactus_Click(object sender, EventArgs e)
        {
            if (Session["member_ID"] == null)
            {
                Response.Redirect("memberLoginForm.aspx");
            }
            else
            {       
                // to check every field are filled
                if (target.Text == "")
                {
                    Response.Write($"<script>alert('請寫入問題主題');</script>");
                    return;
                }
                else if (contact_text.Text == "")
                {
                    Response.Write($"<script>alert('請寫入問題描述');</script>");
                    return;
                }
                else
                {
                    SQLDB_writeContactUs(connString);
                }

            }// member logged

        }// protected void _putOnShelfBtn_Click(object sender, EventArgs e)


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
    }//public partial class PutGoodOnShelfForm : System.Web.UI.Page
}