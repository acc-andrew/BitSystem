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
    public partial class MemberProfile : System.Web.UI.Page
    {

        SqlDataAdapter da = new SqlDataAdapter();       //SQL 資料庫的連接與執行命令
        SqlConnection conn = new SqlConnection();
        DataSet ds_getbid = new DataSet();
        SqlCommand cmd = new SqlCommand();

        //設定資料庫資訊
        string connString = "Sale_net_Jun22_2021ConnectionString";
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
                Session["logged_to_page"] = "memberProfile.aspx";
                Response.Redirect("memberLoginForm.aspx");

            }
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


                //創一個變數存放從config內的資訊，其實也可不用創立這變數，直接放進SqlConnection內即可。
                string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;
                //new一個SqlConnection物件，是與資料庫連結的通道(其名為Connection)，以s_data內的連接字串連接所對應的資料庫。
                SqlConnection connection = new SqlConnection(s_data);
                string spltest = "select * from Member where member_ID=" + Session["member_ID"];
                //new一個SqlCommand告訴這個物件準備要執行什麼SQL指令
                SqlCommand Command = new SqlCommand(spltest, connection);
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
                        //DataReader讀出欄位內資料的方式，通常也可寫Reader[0]、[1]...[N]代表第一個欄位到N個欄位。
                        _user_name.Text = Reader["user_name"].ToString();
                        _memberPassword.Text = Reader["password"].ToString();
                        _name.Text = Reader["name"].ToString();
                        _email.Text = Reader["mail"].ToString();
                        _cellphoneNo.Text = Reader["mobile_phone"].ToString();
                        _birthYear_list.Items.Add(Reader["year"].ToString());
                        _birthMonth_list.Items.Add(Reader["month"].ToString());
                        _birthDate_list.Items.Add(Reader["date"].ToString());
                        _address.Text = Reader["address"].ToString();
                        _status.Text = Reader["status"].ToString();
                        balance.Text = Reader["balance"].ToString();
                        _memberID.Text = Reader["member_ID"].ToString();

                    }
                }
                //關閉與資料庫連接的通道
                connection.Close();

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

        protected void ModifyBtn_Click(object sender, EventArgs e)
        {
            
                _memberPassword.Enabled = true;
                _name.Enabled = true;
                _email.Enabled = true;
                _cellphoneNo.Enabled = true;
                _birthYear_list.Enabled = true;
                _birthMonth_list.Enabled = true;
                _birthDate_list.Enabled = true;
                _address.Enabled = true;
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            if (_memberPassword.Text != "")
            {
                if (_name.Text != "")
                {
                    if (_email.Text != "")
                    {
                        if (_cellphoneNo.Text != "")
                        {
                            if (_birthYear_list.Text != "")
                            {
                                if (_birthMonth_list.Text != "")
                                {
                                    if (_birthDate_list.Text != "")
                                    {
                                        if (_address.Text != "")
                                        {
                                            if (_cellphoneNo.Text.Length == 10)
                                            {
                                                string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;

                                                SqlConnection connection = new SqlConnection(s_data);

                                                string splupdate = $"UPDATE member " +
                                                    $"SET password='{_memberPassword.Text}'," +
                                                    $"name='{_name.Text}'," +
                                                    $"mail='{_email.Text}'," +
                                                    $"mobile_phone='{_cellphoneNo.Text}'," +
                                                    $"year='{_birthYear_list.Text}'," +
                                                    $"month='{_birthMonth_list.Text}'," +
                                                    $"date='{_birthDate_list.Text}'," +
                                                    $"address='{_address.Text}' " +
                                                    $"WHERE member_ID='{_memberID.Text}'";


                                                SqlCommand Command = new SqlCommand(splupdate, connection); //SQL語句

                                                //與資料庫連接的通道開啟
                                                connection.Open();

                                                Command.ExecuteNonQuery();



                                                //關閉與資料庫連接的通道
                                                connection.Close();

                                                _memberPassword.Enabled = false;
                                                _name.Enabled = false;
                                                _email.Enabled = false;
                                                _cellphoneNo.Enabled = false;
                                                _birthYear_list.Enabled = false;
                                                _birthMonth_list.Enabled = false;
                                                _birthDate_list.Enabled = false;
                                                _address.Enabled = false;
                                            }
                                            else
                                                Response.Write($"<script>alert('行動電話長度錯誤，請填入完整10碼');</script>");
                                        }
                                        else
                                            Response.Write($"<script>alert('請輸入地址');</script>");
                                    }
                                    else
                                        Response.Write($"<script>alert('請輸入出生日期');</script>");
                                }
                                else
                                    Response.Write($"<script>alert('請輸入出生月份');</script>");
                            }
                            else
                                Response.Write($"<script>alert('請輸入出生年份');</script>");
                        }
                        else
                            Response.Write($"<script>alert('請輸入手機號碼');</script>");
                    }
                    else
                        Response.Write($"<script>alert('請輸入電子信箱');</script>");
                }
                else
                    Response.Write($"<script>alert('請輸入姓名');</script>");
            }
            else
                Response.Write($"<script>alert('請輸入密碼');</script>");

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
    
       
       


        
    
