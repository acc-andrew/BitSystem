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
            //設定會員登入與否顯現標示不同
            

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
        }
        
        protected bool bSQLDB_verify(string connString, string _guiName)
        {
            bool bFound = false;
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;

            //new一個SqlConnection物件，是與資料庫連結的通道(其名為Connection)，以s_data內的連接字串連接所對應的資料庫。
            SqlConnection connection = new SqlConnection(s_data);

            // bug1: SQL content
            string sql_statement = $"select * from Member  where Name='{_guiName}'";

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
                        Session["name"] = Request.Form[_guiName];  //"_BusName"
                        int nMemberID = (int)(Reader["member_ID"]);
                        Session["member_ID"] = nMemberID;

                        Response.Write("<script>alert('會員登入成功');</script>");
                        Session["Login"] = "logged";

                        bFound = true;
                        // Server.Transfer("test2.aspx");
                    }
                    else
                    {
                        Response.Write("<script>alert('密碼不正確！');</script>");
                        bFound = false;
                    }

                }// if (Reader.Read())

            }// if (Reader.HasRows) login name match
            else
            {
                Response.Write("<script>alert('會員資料庫無此帳號！');</script>");
                bFound = false;
            }// if (Reader.HasRows) login name mismatch
            //關閉與資料庫連接的通道
            connection.Close();
            return bFound;
        }// protected void SQLDB_verify()

        protected bool bSQLDB_ifmatch(string connString, string _enterEmail)
        {
            bool bFound = false;
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;

            //new一個SqlConnection物件，是與資料庫連結的通道(其名為Connection)，以s_data內的連接字串連接所對應的資料庫。
            SqlConnection connection = new SqlConnection(s_data);

            // bug1: SQL content
            string sql_statement = $"select * from Member where mail='{_enterEmail}'";

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
                    bFound = true;
                }// if (Reader.Read())

            }// if (Reader.HasRows) login name match
            else
            {
                bFound = false;
            }// if (Reader.HasRows) login name mismatch
            //關閉與資料庫連接的通道
            connection.Close();
            return bFound;
        }// protected void bSQLDB_ifmatch()

        protected void LoginBtn_Click(object sender, EventArgs e)
        {
            if (bSQLDB_verify("Sale_net_Jun18_2021_betaConnectionString3", _loginName.Text)
                == true)
            {

                // goto the web page after member logging
                if(Session["logged_to_page"] != null){
                    string pageAfterLogging = (string) Session["logged_to_page"];
                    Server.Transfer(pageAfterLogging);

                }
                else
                {
                    Response.Redirect("Home.aspx");
                }
            }

        }// protected void LoginBtn_Click(object sender, EventArgs e)

        protected void _CreaateMemberBtn_Click(object sender, EventArgs e)
        {
            // if the e-mail exists, reply the account has used
            if (bSQLDB_ifmatch("Sale_net_Jun18_2021_betaConnectionString3", _memberEmail.Text)
                == true)
            {
                Response.Write("<script>alert('E-mail 已有會員登記，請改另一個 E-mail ');</script>");
            }
            else
            {
                Session["NewMemberEmail"] = _memberEmail.Text;

                Response.Redirect("memberRegisterForm.aspx");
            }

        }// protected void _CreaateMemberBtn_Click(object sender, EventArgs e)


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