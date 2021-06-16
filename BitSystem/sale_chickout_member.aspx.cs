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
            Session["logined"] = "logined";
            Session["member_ID"] = "1";
            if (Convert.ToString(Session["logined"]) != "logined")
            {
                //Response.Redirect("memberLoginForm.aspx");
                Server.Transfer("memberLoginForm.aspx");
            }

            //創一個變數存放從config內的資訊，其實也可不用創立這變數，直接放進SqlConnection內即可。
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Sale_net_Jun10_2021ConnectionString2"].ConnectionString;
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
            if (Session["logined"] == null)
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
                    string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["sale_netConnectionString4"].ConnectionString;

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
    }
}