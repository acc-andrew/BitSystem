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
    public partial class product_onsale : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["logined"] = "logined";
            Session["user"] = "Austyn";
            if (Convert.ToString(Session["logined"]) != "logined")
            {
                //Response.Redirect("memberLoginForm.aspx");
                Server.Transfer("memberLoginForm.aspx");
            }

            //創一個變數存放從config內的資訊，其實也可不用創立這變數，直接放進SqlConnection內即可。
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["sale_netConnectionString2"].ConnectionString;
            //new一個SqlConnection物件，是與資料庫連結的通道(其名為Connection)，以s_data內的連接字串連接所對應的資料庫。
            SqlConnection connection = new SqlConnection(s_data);
            string sqlcode = $"SELECT * FROM [Member] where user_name =" + "'Austyn'";
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
                    //memberID.Text = (String)Reader["member_ID"];
                    user_name.Text = (String)Reader["user_name"];
                }
            }
            else
            {
               
            }
            //關閉與資料庫連接的通道
            connection.Close();


        }

        protected void onsale_Click(object sender, EventArgs e)
        {
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["sale_netConnectionString1"].ConnectionString;

            SqlConnection connection = new SqlConnection(s_data);

            //string sqlcode = $"insert into [account](account,password,nickname) values('"+user.Text+"','"+passwd.Text+"','"+nick.Text+"')";
            string sqlcode = $"insert into [Action_product](product,classify,total_number,product_status,public_price,description) values(@product,@classify,@total_number,@product_status,@public_price,@description)";

            //與資料庫連接的通道開啟
            connection.Open();
            //new一個SqlCommand告訴這個物件準備要執行什麼SQL指令
            SqlCommand Command = new SqlCommand(sqlcode, connection);

            Command.Parameters.Add("@product", SqlDbType.NVarChar);
            Command.Parameters["@product"].Value = product.Text;
            Command.Parameters.Add("@classify", SqlDbType.NVarChar);
            Command.Parameters["@classify"].Value = classify.Text;
            Command.Parameters.Add("@total_number", SqlDbType.NVarChar);
            Command.Parameters["@total_number"].Value = total_number.Text;
            Command.Parameters.Add("@product_status", SqlDbType.NVarChar);
            Command.Parameters["@product_status"].Value = product_status.Text;
            Command.Parameters.Add("@description", SqlDbType.NVarChar);
            Command.Parameters["@description"].Value = description.Text;
            Command.Parameters.Add("@public_price", SqlDbType.NVarChar);
            Command.Parameters["@public_price"].Value = public_price.Text;
            //new一個DataReader接取Execute所回傳的資料。
            Command.ExecuteNonQuery();
            connection.Close();
            Label7.Text = "上架成功";
        }


    }
}