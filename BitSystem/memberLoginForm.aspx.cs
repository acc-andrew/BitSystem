using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BitSystem
{
    public partial class memberLoginForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected bool bSQLDB_verify(string connString, string _guiName)
        {
            bool bFound = false;
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;

            //new一個SqlConnection物件，是與資料庫連結的通道(其名為Connection)，以s_data內的連接字串連接所對應的資料庫。
            SqlConnection connection = new SqlConnection(s_data);

            // bug1: SQL content
            string sql_statement = $"select * from memberAccountTable where Name='{_guiName}'";

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

                    if (Reader["Password"].ToString() == Request.Form["_loginPassword"])
                    {
                        //DataReader讀出欄位內資料的方式，通常也可寫Reader[0]、[1]...[N]代表第一個欄位到N個欄位。
                        Session["Name"] = Request.Form[_guiName];  //"_BusName"
                        string smemberID = Reader["memberID"].ToString();
                        Session["memberID"] = smemberID;

                        Session["memberLogged"] = "Yes";
                        Response.Write("<script>alert('會員登入成功');</script>");
                        bFound = true;
                        // Server.Transfer("test2.aspx");
                    }
                    else
                    {
                        // Response.Write("<script>alert('password mismatch');</script>");
                        //HTML_msgbox('password mismatch');
                        bFound = true;
                    }

                }// if (Reader.Read())

            }// if (Reader.HasRows) login name match
            else
            {
                //Response.Write("<script>alert('login name mismatch');</script>");
                //HTML_msgbox('login name mismatch');
                bFound = false;
            }// if (Reader.HasRows) login name mismatch
            //關閉與資料庫連接的通道
            connection.Close();
            return bFound;
        }// protected void SQLDB_verify()


        protected void LoginBtn_Click(object sender, EventArgs e)
        {
            if (bSQLDB_verify("BitSystem_DBConnectionString", _loginName.Text)
                == false)
            {
                Response.Write("<script>alert('會員資料庫無此帳號！');</script>");
                Response.Redirect("memberRegisterForm.aspx");
            }
            else
            {
                Response.Redirect("Default.aspx");
            }

        }// protected void LoginBtn_Click(object sender, EventArgs e)
    }
}