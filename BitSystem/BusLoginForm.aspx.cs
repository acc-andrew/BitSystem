using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BitSystem
{
    public partial class BusLoginForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SQLDB_verify(string connString, string _guiName)
        {
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;

            //new一個SqlConnection物件，是與資料庫連結的通道(其名為Connection)，以s_data內的連接字串連接所對應的資料庫。
            SqlConnection connection = new SqlConnection(s_data);

            // bug1: SQL content
            string sql_statement = $"select * from BusAccountTable where BusName='{Request.Form[_guiName]}'";

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
                if (Reader.Read())
                {
                    
                    if (Reader["Password"].ToString() == Request.Form["_BusPassword"])
                    {
                        //DataReader讀出欄位內資料的方式，通常也可寫Reader[0]、[1]...[N]代表第一個欄位到N個欄位。
                        Session["BusName"] = Request.Form[_guiName];  //"_BusName"
                        Session["BusLogined"] = "Yes";
                        Response.Write("<script>alert('login successful');</script>");
                        // MessageBox.show("Login fine");
                        // Server.Transfer("test2.aspx");
                    }
                    else
                    {
                        Response.Write("<script>alert('password mismatch');</script>");
                    }
                
                }// if (Reader.Read())

            }// if (Reader.HasRows) login name match
            else
            {
                Response.Write("<script>alert('login name mismatch');</script>");
            }// if (Reader.HasRows) login name mismatch
            //關閉與資料庫連接的通道
            connection.Close();
            
    }// protected void SQLDB_verify()

        protected void _BusLoginBtn_Click(object sender, EventArgs e)
        {
            if (Session["BusLogined"] != null && Session["BusLogined"].ToString() == "Yes")
            {
                Server.Transfer("test2.aspx"); // to List all goods
            }
            else
            {
                // if BusName and BusPassword input something
                if (Request.Form["_BusName"] != null && Request.Form["_BusPassword"] != null)
                {
                    // SQLDB_("BitSystem_DBConnectionString")
                    SQLDB_verify("BitSystem_DBConnectionString", "_BusName");

                }// // if BusName and BusPassword input something
            }// if (Session["BusLogined"] != null && Session["BusLogined"].ToString() == "Yes")

        }// protected void _BusLoginBtn_Click(object sender, EventArgs e)
    }
}