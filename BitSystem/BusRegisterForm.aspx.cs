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
    public partial class BusRegisterForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SQLDB_write(string connString)
        {
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;

            //new一個SqlConnection物件，是與資料庫連結的通道(其名為Connection)，以s_data內的連接字串連接所對應的資料庫。
            SqlConnection connection = new SqlConnection(s_data);

            //與資料庫連接的通道開啟
            connection.Open();

            // _File
            SqlCommand sql_insert_cmd = new SqlCommand("insert into [BusAccountTable](Password,BusName,BankAccount) values(@Password,@BusName,@BankAccount);", connection); //SQL語句
            sql_insert_cmd.Parameters.Add("@Password", SqlDbType.Text);
            sql_insert_cmd.Parameters["@Password"].Value = _BusPassword.Text;

            sql_insert_cmd.Parameters.Add("@BusName", SqlDbType.Text);
            sql_insert_cmd.Parameters["@BusName"].Value = _BusName.Text;

            sql_insert_cmd.Parameters.Add("@BankAccount", SqlDbType.Text);
            sql_insert_cmd.Parameters["@BankAccount"].Value = _BusBankAccount.Text;

            sql_insert_cmd.ExecuteNonQuery();

            //關閉與資料庫連接的通道
            connection.Close();

            Response.Write($"<script>alert('Write to BusAccountTable fine.');</script>");


        }// protected void SQLDB_write()

        protected void SQLDB_verify(string connString, string _guiName)
        {
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;

            //new一個SqlConnection物件，是與資料庫連結的通道(其名為Connection)，以s_data內的連接字串連接所對應的資料庫。
            SqlConnection connection = new SqlConnection(s_data);

            // bug1: SQL content
            string sql_statement = $"select * from BusAccountTable where BusName='{_guiName}'";

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
                        string sbusID = Reader["BusID"].ToString();
                        Session["BusID"] = sbusID;
                        _BusID.Text = sbusID;

                        Session["BusLogined"] = "Yes";
                        Response.Write("<script>alert('login successful');</script>");

                        // Server.Transfer("test2.aspx");
                    }
                    else
                    {
                        Response.Write("<script>alert('password mismatch');</script>");
                        //HTML_msgbox('password mismatch');
                    }

                }// if (Reader.Read())

            }// if (Reader.HasRows) login name match
            else
            {
                Response.Write("<script>alert('login name mismatch');</script>");
                //HTML_msgbox('login name mismatch');
            }// if (Reader.HasRows) login name mismatch
            //關閉與資料庫連接的通道
            connection.Close();

        }// protected void SQLDB_verify()

        protected void RegisterBtn_Click(object sender, EventArgs e)
        {
            if (_BusPassword.Text != _ConfirmPassword.Text)
            {
                Response.Write("<script>alert('密碼不正確！請重新輸入');</script>");
            }
            else
            {
                SQLDB_write("BitSystem_DBConnectionString");
                // to get BusID from BusAccountTable
                SQLDB_verify("BitSystem_DBConnectionString", _BusName.Text);
            }// password matches

        }// 
    }
}