using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BitSystem
{
    public partial class sale_chickout_member : System.Web.UI.Page
    {
        SqlDataAdapter da = new SqlDataAdapter();       //SQL 資料庫的連接與執行命令
        SqlConnection conn = new SqlConnection();
        DataSet ds_getbid = new DataSet();
        DataSet ds = new DataSet();
        SqlCommand cmd = new SqlCommand();
        DataList getproduct_list = new DataList();

        //設定資料庫資訊
        string connString = "Sale_net_Jun22_2021ConnectionString";
        protected void Page_Load(object sender, EventArgs e)
        {
          
            if (IsPostBack == false)
            {
                //設定會員登入與否顯現標示不同
                //Session["Login"] = "logged";
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
                    Session["logged_to_page"] = "sale_chickout_member.aspx";
                    Response.Redirect("memberLoginForm.aspx");
                }

                if (Convert.ToString(Session["chickout_product"]) != "chicked")
                {
                    Response.Redirect("sale_chickout_product.aspx");
                }

                // 抓取會員資料
                SQL_read_member(connString);

                // 載入已結標熱門
                SQL_readActionProduct_getbid(connString);
                getbid_view.DataSource = ds_getbid; //將DataSet的資料載入到datalist內
                getbid_view.DataBind();
            }
        }

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

        // 抓取會員資料
        protected void SQL_read_member(string connString)
        {
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;
            SqlConnection connection = new SqlConnection(s_data);

            string sqlcode = $"SELECT user_name,name,mail " +
                $"FROM [Member] " +
                $"where member_ID = " + Session["member_ID"];

            SqlCommand Command = new SqlCommand(sqlcode, connection);
            connection.Open();
            SqlDataReader Reader = Command.ExecuteReader();

            if (Reader.HasRows)
            {
                if (Reader.Read())
                {
                    _user_name.Text = (String)Reader["user_name"];
                    _name.Text = (String)Reader["name"];
                    _email.Text = (String)Reader["mail"];
                    Session["email"] = (String)Reader["mail"];
                }
            }
            connection.Close();
        }

        // 點擊付款行為寄信改資料庫
        protected void pay_Click(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {

                Response.Redirect("memberLoginForm.aspx");
            }
            else
            {
                if (receiver.Text == "")
                {
                    Response.Write($"<script>alert('請寫入受收件者');</script>");
                }
                else if (_cellphoneNo.Text == "")
                {
                    Response.Write($"<script>alert('請寫入連絡電話');</script>");
                }
                else if (_address.Text == "")
                {
                    Response.Write($"<script>alert('請寫入寄送地址');</script>");
                }

                else
                {
                    // 讀取資料庫找到寄送資料 放到dataset
                    SQL_readActionProduct(connString);

                    // 寄email
                    sendmail("letsbidding2021@gmail.com", (string)Session["email"]);

                    //
                    string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;
                    // 設定寫入收件人
                    SqlConnection connection = new SqlConnection(s_data);

                    string splupdate = $"UPDATE Action_product " +
                        $"SET receiver_name='{receiver.Text}'," +
                        $"receiver_phone='{_cellphoneNo.Text}'," +
                        $"receiver_address='{_address.Text}' " +
                        $"WHERE status = 'getbid' AND bid_winner_ID='{Session["member_ID"]}'";

                    SqlCommand Command = new SqlCommand(splupdate, connection); //SQL語句
                    connection.Open();
                    Command.ExecuteNonQuery();

                    //設定更改商品狀態
                    SqlConnection connection_status = new SqlConnection(s_data);
                    string splupdate_status = $"UPDATE Action_product " +
                        $"SET status='checkedout'" +
                        $"WHERE status = 'getbid' AND bid_winner_ID='{Session["member_ID"]}'";

                    SqlCommand Command_status = new SqlCommand(splupdate_status, connection); //SQL語句
                    connection_status.Open();
                    Command_status.ExecuteNonQuery();
                    connection.Close();

                    //
                    Response.Write($"<script>alert('完成付款,請前往信箱查詢明細!');</script>");
                    Server.Transfer("Home.aspx");

                }
            }
        }

        //寄email
        public void sendmail(string from_mail, string to_mail)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from_mail);
            mail.To.Add(to_mail);
            mail.Subject = "<我們拍賣吧>商品結帳資訊確認單";

            mail.BodyEncoding = Encoding.UTF8;
            mail.IsBodyHtml = true;
            // 寫入mailbody
            mail.AlternateViews.Add(Mail_Body());

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
            System.Net.NetworkCredential basicCredential1 = new
            // 官方管理寄送帳號 帳號:"letsbidding2021@gmail.com" 密碼:"bidding666"
            System.Net.NetworkCredential("letsbidding2021@gmail.com", "bidding666");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;
            try
            {
                client.Send(mail);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        private AlternateView Mail_Body()
        {
            string str = $@"<h3>以下是您的得標結帳資訊,請參考! </h3><br/><br/>";
            str += "<table style='text - align:'center';text-size = '3'; border='1'>";
            str += "<tr>" +
                        "<td>商品名稱</td>" +
                        "<td>商品市價</td>" +
                        "<td>得標價錢</td>" +
                        "<td>總下標手續費</td>" +
                   "</tr>";

            // 把dataset資料塞到BODY
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                str += "<tr>" +
                            "<td>" + ds.Tables[0].Rows[i]["product"] + "</td>" +
                            "<td>" + ds.Tables[0].Rows[i]["official_price"] + "</td>" +
                            "<td>" + ds.Tables[0].Rows[i]["low_price"] + "</td>" +
                            "<td>" + ds.Tables[0].Rows[i]["handling_fee"] + "</td>" +
                        "</tr>";
            }
            str += "</table>";
            str += "<h2 align='center'>總共金額 : " + Session["total_price"] + "</h2>";

            AlternateView AV = AlternateView.CreateAlternateViewFromString(str, null, MediaTypeNames.Text.Html);
            return AV;
        }

        // // 讀取資料庫找到寄送資料 放到dataset
        protected void SQL_readActionProduct(string connString)
        {
            cmd.Connection = conn;
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;
            conn.ConnectionString = s_data; //"Data Source=127.0.0.1;Initial Catalog=NorthwindChinese;Persist Security Info=True";
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

        // 回去前一頁
        protected void back_Click(object sender, EventArgs e)
        {
            Response.Redirect("sale_chickout_product.aspx");
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