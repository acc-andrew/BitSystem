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
    public partial class memberRegisterForm : System.Web.UI.Page
    {
        SqlDataAdapter da = new SqlDataAdapter();       //SQL 資料庫的連接與執行命令
        SqlConnection conn = new SqlConnection();
        DataSet ds_getbid = new DataSet();
        SqlCommand cmd = new SqlCommand();

        //設定資料庫資訊
        string connString = "Sale_net_Jun22_2021ConnectionString2";
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

            if ((string)Session["NewMemberAccount"] != null)
            {
                _user_name.Text = (string)Session["NewMemberAccount"];
            }


            // for year, month  droplist
            if (!IsPostBack) //如果第一次載入頁面
            {

                for (int y = 1949; y < 2050; y++) //給日期1下拉框新增年份資料
                {
                    _birthYear_list.Items.Add(y.ToString());
                }
                for (int m = 1; m < 13; m++) //給日期2下拉框新增月份資料
                {
                    _birthMonth_list.Items.Add(m.ToString());
                }

                // to initialize date
                MonthChanged(this, System.EventArgs.Empty);

                //給下拉框預設顯示當前日期
                _birthYear_list.Text = (DateTime.Now.Year).ToString(); 
                _birthMonth_list.Text = (DateTime.Now.Month).ToString();
                _birthDate_list.Text = (DateTime.Now.Date).ToString();


                // 載入已結標熱門
                SQL_readActionProduct_getbid(connString);
                getbid_view.DataSource = ds_getbid; //將DataSet的資料載入到datalist內
                getbid_view.DataBind();

            }// if (!IsPostBack)

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


        protected void SQLDB_write(string connString)
        {
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;

            //new一個SqlConnection物件，是與資料庫連結的通道(其名為Connection)，以s_data內的連接字串連接所對應的資料庫。
            SqlConnection connection = new SqlConnection(s_data);

            //與資料庫連接的通道開啟
            connection.Open();

            // _File
            SqlCommand sql_insert_cmd = new SqlCommand("insert into Member(user_name,password,name,mail,mobile_phone,year,month,date,address,status,balance)" +
                " values(@user_name,@password,@name,@mail,@mobile_phone,@year,@month,@date,@address,@status,@balance);", connection); //SQL語句
            
            sql_insert_cmd.Parameters.Add("@user_name", SqlDbType.Text);
            sql_insert_cmd.Parameters["@user_name"].Value = _user_name.Text;

            sql_insert_cmd.Parameters.Add("@password", SqlDbType.Text);
            sql_insert_cmd.Parameters["@password"].Value = _memberPassword.Text;

            sql_insert_cmd.Parameters.Add("@name", SqlDbType.NVarChar);
            sql_insert_cmd.Parameters["@name"].Value = _name.Text;

            sql_insert_cmd.Parameters.Add("@mail", SqlDbType.Text);
            sql_insert_cmd.Parameters["@mail"].Value = _email.Text;

            sql_insert_cmd.Parameters.Add("@mobile_phone", SqlDbType.Text);
            sql_insert_cmd.Parameters["@mobile_phone"].Value = _cellphoneNo.Text;
            
            sql_insert_cmd.Parameters.Add("@year", SqlDbType.Text);
            sql_insert_cmd.Parameters["@year"].Value = _birthYear_list.Text;

            sql_insert_cmd.Parameters.Add("@month", SqlDbType.Text);
            sql_insert_cmd.Parameters["@month"].Value = _birthMonth_list.Text;

            sql_insert_cmd.Parameters.Add("@date", SqlDbType.Text);
            sql_insert_cmd.Parameters["@date"].Value = _birthDate_list.Text;


            sql_insert_cmd.Parameters.Add("@address", SqlDbType.NVarChar);
            sql_insert_cmd.Parameters["@address"].Value = _address.Text;

            sql_insert_cmd.Parameters.Add("@status", SqlDbType.NVarChar);
            sql_insert_cmd.Parameters["@status"].Value = _status.Text;

            sql_insert_cmd.Parameters.Add("@balance", SqlDbType.Int);
            sql_insert_cmd.Parameters["@balance"].Value = 100;

            sql_insert_cmd.ExecuteNonQuery();

            //關閉與資料庫連接的通道
            connection.Close();

            Response.Write($"<script>alert('寫入會員資料庫成功');</script>");


        }// protected void SQLDB_write()

        protected void SQLDB_verify(string connString, string _guiName)
        {
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;

            //new一個SqlConnection物件，是與資料庫連結的通道(其名為Connection)，以s_data內的連接字串連接所對應的資料庫。
            SqlConnection connection = new SqlConnection(s_data);

            // bug1: SQL content
            string sql_statement = $"select * from Member where user_name='{_user_name.Text}'";

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

                    if (Reader["Password"].ToString() == Request.Form["_memberPassword"])
                    {
                        //DataReader讀出欄位內資料的方式，通常也可寫Reader[0]、[1]...[N]代表第一個欄位到N個欄位。
                        Session["name"] = Request.Form[_guiName];  //"_BusName"
                        string smemberID = Reader["member_ID"].ToString();
                        Session["member_ID"] = smemberID;
                        _memberID.Text = smemberID;

                        Session["memberLogged"] = "Yes";
                        Response.Write("<script>alert('會員註冊成功');</script>");

                        Server.Transfer("memberLoginForm.aspx");
                    }
                    else
                    {
                        Response.Write("<script>alert('密碼錯誤！');</script>");
                    }

                }// if (Reader.Read())

            }// if (Reader.HasRows) login name match
            else
            {
                Response.Write("<script>alert('會員註冊失敗');</script>");

            }// if (Reader.HasRows) login name mismatch
            //關閉與資料庫連接的通道
            connection.Close();

        }// protected void SQLDB_verify()

        protected bool bSQLDB_checkAccount(string connString, string _guiName)
        {
            bool bFound = false;
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;

            //new一個SqlConnection物件，是與資料庫連結的通道(其名為Connection)，以s_data內的連接字串連接所對應的資料庫。
            SqlConnection connection = new SqlConnection(s_data);

            // bug1: SQL content
            string sql_statement = $"select * from Member where user_name='{_guiName}'";

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
        }// protected void SQLDB_verify()


        protected void RegisterBtn_Click(object sender, EventArgs e)
        {
            if (_user_name.Text == "")
            {
                Response.Write($"<script>alert('請寫入帳號');</script>");
            }

            else if (_memberPassword.Text == "")
            {
                Response.Write($"<script>alert('請寫入密碼');</script>");
            }

            else if(_ConfirmPassword.Text == "")
            {
                Response.Write($"<script>alert('請確認密碼');</script>");
            }

            else if(_name.Text == "")
            {
                Response.Write($"<script>alert('請確認會員名稱');</script>");
            }

            else if(_email.Text == "")
            {
                Response.Write($"<script>alert('請確認會員Email');</script>");
            }

            else if(_cellphoneNo.Text == "")
            {
                Response.Write($"<script>alert('請確認會員電話');</script>");
            }

            else if(_memberPassword.Text != _ConfirmPassword.Text)
            {
                Response.Write("<script>alert('密碼不正確！請重新輸入');</script>");
            }
            else
            {
                //check memeber account not repeat
                if (bSQLDB_checkAccount(connString, _user_name.Text)
                    == true)
                {
                    Response.Write("<script>alert('帳號已有會員登記，請重新選個好聽的名稱');</script>");
                }
                else 
                { 
                    SQLDB_write(connString);
                    // to get BusID from BusAccountTable
                    SQLDB_verify(connString, _user_name.Text);
                }
                
            }// password matches
        }// protected void RegisterBtn_Click(object sender, EventArgs e)

        protected bool isLeapYear()
        {
            int nYear = int.Parse(_birthYear_list.SelectedValue);
            return (((nYear % 4) == 0) && ((nYear % 100) != 0)) || ((nYear % 400) == 0);
        }
        protected void YearSelected(object sender, EventArgs e)
        {
            // force  to refresh month and date
            MonthChanged(this, System.EventArgs.Empty);
        }

        protected void MonthChanged(object sender, EventArgs e)
        {
            int nMonth = int.Parse(_birthMonth_list.SelectedValue);
            _birthDate_list.Items.Clear();
            switch (nMonth)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    for (int d = 1; d < 32; d++)
                    {
                        _birthDate_list.Items.Add(d.ToString());
                    }
                    break;

                case 4:
                case 6:
                case 9:
                case 11:
                    for (int d = 1; d < 31; d++)
                    {
                        _birthDate_list.Items.Add(d.ToString());
                    }
                    break;
                case 2:
                    int nLastFebDate = 0;
                    bool ifLeapYear = isLeapYear();
                    if (ifLeapYear == true)
                    {
                        nLastFebDate = 30;
                    }
                    else
                    {
                        nLastFebDate = 29;
                    }
                    for (int d = 1; d < nLastFebDate; d++)
                    {
                        _birthDate_list.Items.Add(d.ToString());
                    }
                    break;
            }// switch
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