using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace BitSystem
{

    public partial class GoodListForm : System.Web.UI.Page
    {
        SqlDataAdapter da = new SqlDataAdapter();       //SQL 資料庫的連接與執行命令
        DataSet ds_check = new DataSet();
        DataSet ds_page = new DataSet();
        SqlCommand cmd_check = new SqlCommand();
        SqlCommand cmd_page = new SqlCommand();
        SqlConnection conn = new SqlConnection();
        DataSet ds_getbid = new DataSet();
        SqlCommand cmd = new SqlCommand();


        //設定分頁項目
        int PageSize, RecordCount, PageCount, CurrentPage;
        //設定資料庫資訊
        string connString = "Sale_net_Jun22_2021ConnectionString";
        protected void Page_Load(object sender, EventArgs e)
        {
            //設定PageSize
            PageSize = 7;

            if (IsPostBack == false)
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

                //設定/商品列表/分類顯示
                if (Convert.ToString(Session["classify"]) == "cloth")
                {
                    classify_label.Visible = true;
                    classify_label.Text = "衣服飾品";
                }
                else if (Convert.ToString(Session["classify"]) == "book")
                {
                    classify_label.Visible = true;
                    classify_label.Text = "書籍文創";
                }
                else if (Convert.ToString(Session["classify"]) == "life")
                {
                    classify_label.Visible = true;
                    classify_label.Text = "居家生活";
                }
                else if (Convert.ToString(Session["classify"]) == "bag")
                {
                    classify_label.Visible = true;
                    classify_label.Text = "包包精品";
                }
                else if (Convert.ToString(Session["classify"]) == "shoes")
                {
                    classify_label.Visible = true;
                    classify_label.Text = "男女鞋款";
                }
                else if (Convert.ToString(Session["classify"]) == "car")
                {
                    classify_label.Visible = true;
                    classify_label.Text = "汽機車零件百貨";
                }
                else if (Convert.ToString(Session["classify"]) == "habbit")
                {
                    classify_label.Visible = true;
                    classify_label.Text = "娛樂收藏";
                }
                else if (Convert.ToString(Session["classify"]) == "pet")
                {
                    classify_label.Visible = true;
                    classify_label.Text = "寵物用品";
                }
                else if (Convert.ToString(Session["classify"]) == "other")
                {
                    classify_label.Visible = true;
                    classify_label.Text = "其他類別";
                }
                else
                {
                    classify_label.Visible = false;
                }



                // 判斷有沒有資料
                SQL_readActionProduct_byClosest(connString);

                if (ds_check.Tables[0].Rows.Count > 0)
                {
                    //初始設定剛進頁面 匯入資料                       
                    ListBind(connString);
                    CurrentPage = 0;
                    ViewState["PageIndex"] = 0;

                    //計算總共有多少記錄(onsale)             
                    RecordCount = CalculateRecord(connString);
                    lblRecordCount.Text = RecordCount.ToString();

                    //計算總共有多少頁(onsale)
                    PageCount = (RecordCount / PageSize) + 1;
                    lblPageCount.Text = PageCount.ToString();
                    ViewState["PageCount"] = PageCount;
                }
                else
                {
                    Response.Write("<script>alert('目前還沒有此分類商品哦~請再看看別的!');</script>");
                    product_view.Visible = false;
                }

                // 載入以截標熱門
                SQL_readActionProduct_getbid(connString);
                getbid_view.DataSource = ds_getbid; //將DataSet的資料載入到datalist內
                getbid_view.DataBind();

            }
        }// Page_Load

        // 左側以截標商品展示
        protected void SQL_readActionProduct_getbid(string connString)
        {
            cmd.Connection = conn;   //將SQL執行的命令語法程式CMD與CONN與SQL連接
            //設定連線IP位置、資料表，帳戶，密碼
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;
            conn.ConnectionString = s_data; //"Data Source=127.0.0.1;Initial Catalog=NorthwindChinese;Persist Security Info=True";
                                            //這一行可依連線的字串不同而去定義它該連線到哪個資料庫!!

            cmd.CommandText = $"SELECT Top 3 pic_pathname,official_price,low_price,Member.name " +
                            $"FROM Action_product " +
                $"INNER JOIN Member " +
                $"ON Action_product.bid_winner_ID = Member.member_ID " +
                $"ORDER BY Action_product.closedDateTime Desc";   //執行SQL語法進行查詢

            da.SelectCommand = cmd;            //da選擇資料來源，由cmd載入進來

            da.Fill(ds_getbid, "Action_product"); //da把資料填入ds裡面

        }// protected void SQL_readActionProduct()


        //計算總共有多少條記錄
        public int CalculateRecord(string connectiion)
        {
            // 抓取今日資訊
            string strToday = getTodayString();

            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connectiion].ConnectionString;
            SqlConnection connection = new SqlConnection(s_data);
            string sql_statement_no_classify = $"select count(*) as co from Action_product " +
                "where status='onsale'" + $"and closedDateTime >= '{strToday}'";

            // bug1: SQL content with session classify
            string sql_statement1_classify = $"select count(*) as co from Action_product " +
                "where status='onsale' and classify ='" + Session["classify"] + "'" + $"and closedDateTime >= '{strToday}'";
            
            string sql_statement1;
            // bug2: sqlText
            //new一個SqlCommand告訴這個物件準備要執行什麼SQL指令
            if (Session["classify"] == null)
            {
                sql_statement1 = sql_statement_no_classify;
            }
            else
            {
                sql_statement1 = sql_statement1_classify;
            }
            SqlCommand Command = new SqlCommand(sql_statement1, connection);
            connection.Open();
            SqlDataReader Reader = Command.ExecuteReader();

            int intCount;

            if (Reader.HasRows)
            {
                Reader.Read();
                intCount = Int32.Parse(Reader["co"].ToString());
            }
            else
            {
                intCount = 0;
            }
            Reader.Close();
            return intCount;
        }

        // 匯入資料
        public void ListBind(string connString)
        {
            // 抓取今日資訊
            string strToday = getTodayString();

            //設定匯入的起終地址
            int StartIndex = CurrentPage * PageSize;
            cmd_page.Connection = conn;
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;
            conn.ConnectionString = s_data;

            string sql_statement_no_classify = $"SELECT pic_pathname,product,description,total_number,seller_ID,action_product_ID,official_price " +
                $"from Action_product where status='onsale'" + $"and closedDateTime >= '{strToday}' order by closedDateTime";

            // bug1: SQL content with session classify
            string sql_statement1_classify = $"SELECT pic_pathname,product,description,total_number,seller_ID,action_product_ID,official_price " +
                $"from Action_product where status='onsale' and classify ='" + Session["classify"] + "'" + $"and closedDateTime >= '{strToday}' order by closedDateTime";

            // bug2: sqlText
            //new一個SqlCommand告訴這個物件準備要執行什麼SQL指令
            //da選擇資料來源，由cmd載入進來  
            if (Session["classify"] == null)
            {
                cmd_page.CommandText = sql_statement_no_classify;
            }
            else
            {
                cmd_page.CommandText = sql_statement1_classify;
            }

            da.SelectCommand = cmd_page;
            da.Fill(ds_page, StartIndex, PageSize, "Action_product");//da把資料填入ds裡面

            product_view.DataSource = ds_page.Tables["Action_product"].DefaultView;
            product_view.DataBind();

            //設定頁數與按鈕顯示
            lbnNextPage.Enabled = true;
            lbnPrevPage.Enabled = true;
            if (CurrentPage == (PageCount - 1)) lbnNextPage.Enabled = false;
            if (CurrentPage == 0) lbnPrevPage.Enabled = false;
            lblCurrentPage.Text = (CurrentPage + 1).ToString();

        }

        //按下更換分頁
        public void Page_OnClick(Object sender, CommandEventArgs e)
        {
            CurrentPage = (int)ViewState["PageIndex"];
            PageCount = (int)ViewState["PageCount"];

            string cmd = e.CommandName;
            //判斷cmd,以判定翻頁方向
            switch (cmd)
            {
                case "next":
                    if (CurrentPage < (PageCount - 1)) CurrentPage++;
                    break;
                case "prev":
                    if (CurrentPage > 0) CurrentPage--;
                    break;
            }

            ViewState["PageIndex"] = CurrentPage;

            ListBind(connString);
            
        }

        // 紀錄商品資訊 提供點擊連結至商品頁
        protected void product_view_ItemCommand(object source, DataListCommandEventArgs e)
        {
            DataListItem currentItem = e.Item;

            if (e.CommandName == "click")
            {

                product_view.SelectedIndex = currentItem.ItemIndex;

                string pic_pathname = ((ImageButton)currentItem.FindControl("pic_pathname")).ImageUrl;
                string product = ((Label)currentItem.FindControl("product")).Text;
                string official_price = ((Label)currentItem.FindControl("official_price")).Text;
                string action_product_ID = ((Label)currentItem.FindControl("action_product_ID")).Text;
                string description = ((Label)currentItem.FindControl("description")).Text;
                string seller_ID = ((Label)currentItem.FindControl("seller_ID")).Text;

                Session["ProductName"] = product;
                Session["ProductDesc"] = description;
                Session["official_price"] = official_price;
                Session["ImageUrl"] = pic_pathname;
                Session["ProductID"] = action_product_ID;
                Session["SellerID"] = seller_ID;

                Response.Redirect("BitProductForm.aspx");

            }
        }
        
        // 超過字數的欄位會隱藏 變成...
        protected void product_view_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            // itemdatabound 使用e.Item.FindControl解決每個row的最後一項讀不到的問題
            if (((Label)e.Item.FindControl("description")).Text.Length > 75)
            {
                ((Label)e.Item.FindControl("description")).Text = ((Label)e.Item.FindControl("description")).Text.Substring(0, 75) + "......";
            }
        }

        // 抓取今日資訊
        protected string getTodayString()
        {
            string strYear = DateTime.Today.Year.ToString();
            string strMonth = DateTime.Today.Month.ToString("00");
            string strDay = DateTime.Today.Day.ToString("00");  
            string strTotal = strYear+strMonth+strDay;  
            return strTotal;
        }

        // 判斷有沒有資料
        protected void SQL_readActionProduct_byClosest(string connString)
        {
            // 抓取今日資訊
            string strTotal = getTodayString();
            // string today = DateTime.Now.Date.ToString("YYYYMMDD");
            cmd_check.Connection = conn;   //將SQL執行的命令語法程式CMD與CONN與SQL連接

            //設定連線IP位置、資料表，帳戶，密碼
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;
            conn.ConnectionString = s_data; //"Data Source=127.0.0.1;Initial Catalog=NorthwindChinese;Persist Security Info=True";
                                            //這一行可依連線的字串不同而去定義它該連線到哪個資料庫!!

            // bug1: SQL content without session classify
            string sql_statement_no_classify = $"SELECT pic_pathname,product,description,total_number,seller_ID,action_product_ID,official_price " +
                $"from Action_product where status='onsale' and closedDateTime >= '{strTotal}'";

            // bug1: SQL content with session classify
            string sql_statement1_classify = $"SELECT pic_pathname,product,description,total_number,seller_ID,action_product_ID,official_price " +
                $"from Action_product where status='onsale' and classify ='" + Session["classify"] + "'" + $" and closedDateTime >= '{strTotal}'";

            // bug2: sqlText
            //new一個SqlCommand告訴這個物件準備要執行什麼SQL指令
            if (Session["classify"] == null)
            {
                cmd_check.CommandText = sql_statement_no_classify;
            }
            else
            {
                cmd_check.CommandText = sql_statement1_classify;
            }

            da.SelectCommand = cmd_check;            //da選擇資料來源，由cmd載入進來
            da.Fill(ds_check, "Action_product");            //da把資料填入ds裡面
        }// protected void SQL_readActionProduct_byClosest(string connString)



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

        //左側連接分類功能
        protected void cloth_Click(object sender, EventArgs e)
        {
            Session["classify"] = "cloth";
            Response.Redirect("GoodListForm.aspx");
            /*
            SQL_readActionProductClassify("Sale_net_Jun22_2021ConnectionString2", 
                                            "action_product_ID > 19 and action_product_ID < 23");
            */
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
            /*
            SQL_readActionProductClassify("Sale_net_Jun22_2021ConnectionString2", 
                                        "action_product_ID > 25 and action_product_ID <= 28");
            */
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
