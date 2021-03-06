using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;
using System.Collections;

namespace BitSystem
{
    public partial class memberOrder : System.Web.UI.Page
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

            //設定會員登入與否顯現標示不同
            if (IsPostBack == false)
            {
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

                if (DropDownList1.SelectedValue == "已上架")
                {
                    product_view.Visible = true;

                    //已上架
                    string cmdText_onsale = "SELECT pic_pathname,product,description,status,total_number,seller_ID,action_product_ID,bid_price,official_price " +
                        "from Action_product where status='onsale' and seller_ID='" + Session["member_ID"] + "'";
                                        
                    // 判斷有沒有資料
                    SQL_readActionProduct(connString, cmdText_onsale);
                                        
                    if (ds_check.Tables[0].Rows.Count > 0)
                    {
                        //初始設定剛進頁面 匯入資料                       
                        ListBind(connString, cmdText_onsale);
                        CurrentPage = 0;
                        ViewState["PageIndex"] = 0;

                        //計算總共有多少記錄(onsale)             
                        string sql_statement1_onsale = $"select count(*) as co from Action_product " +
                        "where status='onsale' and seller_ID='" + Session["member_ID"] + "'";
                        RecordCount = CalculateRecord(connString, sql_statement1_onsale);
                        lblRecordCount.Text = RecordCount.ToString();

                        //計算總共有多少頁(onsale)
                        PageCount = (RecordCount / PageSize)+1;
                        lblPageCount.Text = PageCount.ToString();
                        ViewState["PageCount"] = PageCount;
                    }
                    else
                    {
                        Response.Write("<script>alert('還沒有商品喔!');</script>");
                        product_view.Visible = false;
                    }

                    // 載入已結標熱門
                    SQL_readActionProduct_getbid(connString);
                    getbid_view.DataSource = ds_getbid; //將DataSet的資料載入到datalist內
                    getbid_view.DataBind();

                }
            }
        }

        // 左測已結標商品展示
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


        //下拉式選單選擇時出現商品頁更換
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (DropDownList1.SelectedValue == "已上架")
            {
                product_view.Visible = true;

                //已上架
                string cmdText_onsale = "SELECT pic_pathname,product,description,status,total_number,seller_ID,action_product_ID,bid_price,official_price " +
                        "from Action_product where status='onsale' and seller_ID='" + Session["member_ID"] + "'";
                // 判斷有沒有資料
                SQL_readActionProduct(connString, cmdText_onsale);

                if (ds_check.Tables[0].Rows.Count > 0)
                {
                    //初始設定剛進頁面 匯入資料
                    ListBind(connString, cmdText_onsale);
                    CurrentPage = 0;
                    ViewState["PageIndex"] = 0;

                    //計算總共有多少記錄(onsale)             
                    string sql_statement1_onsale = $"select count(*) as co from Action_product " +
                    "where status='onsale' and seller_ID='" + Session["member_ID"] + "'";
                    RecordCount = CalculateRecord(connString, sql_statement1_onsale);
                    lblRecordCount.Text = RecordCount.ToString();

                    //計算總共有多少頁(onsale)
                    PageCount = (RecordCount / PageSize) + 1;
                    lblPageCount.Text = PageCount.ToString();
                    ViewState["PageCount"] = PageCount;
                }
                else
                {
                    lblRecordCount.Text = "0";
                    lblPageCount.Text = "0";
                    lblCurrentPage.Text = "0";
                    Response.Write("<script>alert('還沒有商品喔!');</script>");
                    product_view.Visible = false;
                }
            }
            else if (DropDownList1.SelectedValue == "競標中")
            {
                // 顯示表格對應欄位
                product_view.Visible = true;

                //競標中
                string cmdText_bidding = "SELECT pic_pathname,product,description,status,total_number,seller_ID,action_product_ID,bid_price,official_price " +
                    "from Action_product " +
                    "where status='bidding' and bidder_ID='" + Session["member_ID"] + "'";
                // 判斷有沒有資料
                SQL_readActionProduct(connString, cmdText_bidding);

                if (ds_check.Tables[0].Rows.Count > 0)
                {
                    //初始設定剛進頁面 匯入資料      
                    ListBind(connString, cmdText_bidding);
                    CurrentPage = 0;
                    ViewState["PageIndex"] = 0;

                    //計算總共有多少記錄(onsale)             
                    string sql_statement1_onsale = $"select count(*) as co from Action_product " +
                    "where status='bidding' and bidder_ID='" + Session["member_ID"] + "'";
                    RecordCount = CalculateRecord(connString, sql_statement1_onsale);
                    lblRecordCount.Text = RecordCount.ToString();

                    //計算總共有多少頁(onsale)
                    PageCount = (RecordCount / PageSize) + 1;
                    lblPageCount.Text = PageCount.ToString();
                    ViewState["PageCount"] = PageCount;
                }
                else
                {
                    lblRecordCount.Text = "0";
                    lblPageCount.Text = "0";
                    lblCurrentPage.Text = "0";
                    Response.Write("<script>alert('還沒有商品喔!');</script>");
                    product_view.Visible = false;
                }
            }
            else if (DropDownList1.SelectedValue == "已得標")
            {
                product_view.Visible = true;

                //已結標
                string cmdText_getbid = "SELECT pic_pathname,product,description,status,total_number,seller_ID,action_product_ID,bid_price,official_price " +
                    "from Action_product " +
                    "where (status='getbid' or status='checkedout') and bid_winner_ID='" + Session["member_ID"] + "'";
                // 判斷有沒有資料
                SQL_readActionProduct(connString, cmdText_getbid);

                if (ds_check.Tables[0].Rows.Count > 0)
                {
                    //初始設定剛進頁面 匯入資料
                    
                    ListBind(connString, cmdText_getbid);
                    CurrentPage = 0;
                    ViewState["PageIndex"] = 0;

                    //計算總共有多少記錄(onsale)
                    string sql_statement1_onsale = $"select count(*) as co from Action_product " +
                    "where (status='getbid' or status='checkedout') and bid_winner_ID='" + Session["member_ID"] + "'";
                    RecordCount = CalculateRecord(connString, sql_statement1_onsale);
                    lblRecordCount.Text = RecordCount.ToString();

                    //計算總共有多少頁(onsale)
                    PageCount = (RecordCount / PageSize) + 1;
                    lblPageCount.Text = PageCount.ToString();
                    ViewState["PageCount"] = PageCount;
                }
                else
                {
                    lblRecordCount.Text = "0";
                    lblPageCount.Text = "0";
                    lblCurrentPage.Text = "0";
                    Response.Write("<script>alert('還沒有商品喔!');</script>");
                    product_view.Visible = false;
                }
            }
        }


        //計算總共有多少條記錄
        public int CalculateRecord(string connectiion, string sql_statement1)
        {
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connectiion].ConnectionString;
            SqlConnection connection = new SqlConnection(s_data);
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
        public void ListBind(string connString, string sql_statement1_onsale)
        {
            //設定匯入的起終地址
            int StartIndex = CurrentPage * PageSize;
            cmd_page.Connection = conn;
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;
            conn.ConnectionString = s_data;

            cmd_page.CommandText = sql_statement1_onsale;   //執行SQL語法進行查詢
            da.SelectCommand = cmd_page;            //da選擇資料來源，由cmd載入進來  
            da.Fill(ds_page, StartIndex, PageSize, "Action_product");//da把資料填入ds裡面

            for (int i = 0; i < ds_page.Tables["Action_product"].Rows.Count; i++) 
            {
                if ((string)ds_page.Tables["Action_product"].Rows[i]["status"] == "onsale    ")
                {
                    ds_page.Tables["Action_product"].Rows[i]["status"] = "已上架商品";
                }
                else if ((string)ds_page.Tables["Action_product"].Rows[i]["status"] == "bidding   ")
                {
                    ds_page.Tables["Action_product"].Rows[i]["status"] = "下標過商品";
                }
                else if ((string)ds_page.Tables["Action_product"].Rows[i]["status"] == "getbid    ")
                {
                    ds_page.Tables["Action_product"].Rows[i]["status"] = "已得標未結帳";
                }
                else if ((string)ds_page.Tables["Action_product"].Rows[i]["status"] == "checkedout")
                {
                    ds_page.Tables["Action_product"].Rows[i]["status"] = "已結帳";
                }
            }

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

            if (DropDownList1.SelectedValue == "已上架")
            {
                string cmdText_onsale = "SELECT pic_pathname,product,description,status,total_number,seller_ID,action_product_ID,bid_price,official_price " +
                        "from Action_product where status='onsale' and seller_ID='" + Session["member_ID"] + "'";
                ListBind(connString, cmdText_onsale);
            }
            else if (DropDownList1.SelectedValue == "競標中")
            {
                string cmdText_bidding = "SELECT pic_pathname,product,description,status,total_number,seller_ID,action_product_ID,bid_price,official_price " +
                    "from Action_product where status='bidding' and seller_ID='" + Session["member_ID"] + "'";
                ListBind(connString, cmdText_bidding);
            }
            else if (DropDownList1.SelectedValue == "已得標")
            {
                string cmdText_getbid = "SELECT pic_pathname,product,description,status,total_number,seller_ID,action_product_ID,bid_price,official_price " +
                    "from Action_product where status='getbid' and seller_ID='" + Session["member_ID"] + "'";
                ListBind(connString, cmdText_getbid);
            }
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

            // 競標中改變欄位
            if (DropDownList1.SelectedValue == "競標中")
            {
                Label head_descript = (Label)e.Item.FindControl("head_descript");
                head_descript.Visible = false;
                Label head_bitprice = (Label)e.Item.FindControl("head_bitprice");
                head_bitprice.Visible = true;
                Label description = (Label)e.Item.FindControl("description");
                description.Visible = false;
                Label bid_price = (Label)e.Item.FindControl("bid_price");
                bid_price.Visible = true;
            }
        }
     
        // 檢查個狀態資料庫是否有資料
        protected void SQL_readActionProduct(string connString,string cmdText)
        {
            cmd_check.Connection = conn;   //將SQL執行的命令語法程式CMD與CONN與SQL連接

            //設定連線IP位置、資料表，帳戶，密碼
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;
            conn.ConnectionString = s_data; //"Data Source=127.0.0.1;Initial Catalog=NorthwindChinese;Persist Security Info=True";
            //這一行可依連線的字串不同而去定義它該連線到哪個資料庫!!

            cmd_check.CommandText = cmdText;   //執行SQL語法進行查詢
            da.SelectCommand = cmd_check;            //da選擇資料來源，由cmd載入進來
            da.Fill(ds_check, "Action_product");            //da把資料填入ds裡面

        }// protected void SQL_readActionProduct()



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