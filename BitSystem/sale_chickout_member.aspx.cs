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
    public partial class sale_chickout_member : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
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
                Server.Transfer("memberLoginForm.aspx");
            }

            if (Convert.ToString(Session["chickout_product"]) != "chicked") 
            {
                Server.Transfer("sale_chickout_product.aspx");
            }


            //Session["member_ID"] = "1";


            //創一個變數存放從config內的資訊，其實也可不用創立這變數，直接放進SqlConnection內即可。
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Sale_net_Jun22_2021ConnectionString"].ConnectionString;
            //new一個SqlConnection物件，是與資料庫連結的通道(其名為Connection)，以s_data內的連接字串連接所對應的資料庫。
            SqlConnection connection = new SqlConnection(s_data);
            string sqlcode = $"SELECT * FROM [Member] where member_ID = " + Session["member_ID"];
            //new一個SqlCommand告訴這個物件準備要執行什麼SQL指令
            SqlCommand Command = new SqlCommand(sqlcode, connection);
            //與資料庫連接的通道開啟
            connection.Open();
            //new一個DataReader接取Execute所回傳的資料。
            SqlDataReader Reader = Command.ExecuteReader();


            //檢查是否有資料列
            if (Reader.HasRows)
            {
                //使用Read方法把資料讀進Reader，讓Reader一筆一筆順向指向資料列，並回傳是否成功。
                if (Reader.Read())
                {
                    //memberID.Text = (Int32)((String)Reader["member_ID"]);
                    _user_name.Text = (String)Reader["user_name"];
                    _name.Text = (String)Reader["name"];
                    _email.Text = (String)Reader["mail"];
                }
            }
            else
            {

            }
            //關閉與資料庫連接的通道
            connection.Close();

        }

        protected void pay_Click(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {

                Server.Transfer("memberLoginForm.aspx");
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
                    string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Sale_net_Jun22_2021ConnectionString"].ConnectionString;

                    SqlConnection connection = new SqlConnection(s_data);

                    //string sqlcode = $"insert into [account](account,password,nickname) values('"+user.Text+"','"+passwd.Text+"','"+nick.Text+"')";
                    string sqlcode = $"insert into [Action_order](receiver_name,receiver_phone,receiver_address) values(@receiver_name,@receiver_phone,@receiver_address)";

                    //與資料庫連接的通道開啟
                    connection.Open();
                    //new一個SqlCommand告訴這個物件準備要執行什麼SQL指令
                    SqlCommand Command = new SqlCommand(sqlcode, connection);

                    Command.Parameters.Add("@receiver_name", SqlDbType.NVarChar);
                    Command.Parameters["@receiver_name"].Value = receiver.Text;

                    Command.Parameters.Add("@receiver_phone", SqlDbType.NVarChar);
                    Command.Parameters["@receiver_phone"].Value = _cellphoneNo.Text;

                    Command.Parameters.Add("@receiver_address", SqlDbType.NVarChar);
                    Command.Parameters["@receiver_address"].Value = _address.Text;

                    //new一個DataReader接取Execute所回傳的資料。
                    Command.ExecuteNonQuery();
                    connection.Close();
                    Response.Write($"<script>alert('準備進行付款');</script>");
                    Server.Transfer("Home.aspx");
                }
            }
        }

        protected void back_Click(object sender, EventArgs e)
        {
            Server.Transfer("Home.aspx");
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

        //取消classify
        protected void sale_list_Click(object sender, EventArgs e)
        {
            Session["classify"] = null;
            Response.Redirect("GoodListForm.aspx");
        }
    }
}