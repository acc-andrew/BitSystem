using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace BitSystem
{
    public partial class memberOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["logined"] = "logined";
            Session["member_ID"] = "1";
            Session["user"] = "Alan";
            if (Convert.ToString(Session["logined"]) != "logined")
            {
                //Response.Redirect("memberLoginForm.aspx");
                Server.Transfer("memberLoginForm.aspx");
            }
            //創一個變數存放從config內的資訊，其實也可不用創立這變數，直接放進SqlConnection內即可。
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["sale_netConnectionString3"].ConnectionString;
            //new一個SqlConnection物件，是與資料庫連結的通道(其名為Connection)，以s_data內的連接字串連接所對應的資料庫。
            SqlConnection connection = new SqlConnection(s_data);
            string sqlcode = $"SELECT * FROM [member] where user_name =" + "'" + Session["user"] + "'";
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
                }
            }
            else
            {

            }
            //關閉與資料庫連接的通道
            connection.Close();
        }
    }
}