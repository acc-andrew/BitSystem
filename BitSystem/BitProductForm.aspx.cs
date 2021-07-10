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
    public partial class BitProductForm : System.Web.UI.Page
    {
        SqlDataAdapter da = new SqlDataAdapter();       //SQL 資料庫的連接與執行命令
        DataSet ds_getbid = new DataSet();
        SqlCommand cmd = new SqlCommand();
        SqlConnection conn = new SqlConnection();

        private int _sellerID = 0;
        private int _ProductID = 0;
        private DateTime _ProductClosedDateTime = new DateTime();
        private int _official_price = 0;
        public int _bidder_ID = 0;
        private const int nCharge = 10;
        System.Timers.Timer _timer;

        //設定資料庫資訊
        string connString = "Sale_net_Jun22_2021ConnectionString";

        protected void Page_Load(object sender, EventArgs e)
        {
            // if the page loaded first time
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

                if (Session["ProductName"] != null)
                {
                    string strProductName = (string) Session["ProductName"];
                    _ProductName.Text = strProductName;
                }

                if (Session["ProductDesc"] != null)
                {
                    string strProductDesc = (string)Session["ProductDesc"];
                    _ProductDesc.Text = strProductDesc;
                }

                if (Session["official_price"] != null)
                {
                    string strofficial_price = Convert.ToInt32(Session["official_price"]).ToString();
                    _sellerID = int.Parse(strofficial_price);
                }

                if (Session["SellerID"] != null)
                {
                    string strSellerID = Convert.ToInt32(Session["SellerID"]).ToString();
                    _sellerID = int.Parse(strSellerID);
                }

                if (Session["ImageUrl"] != null)
                {
                    string strImageUrl = (string)Session["ImageUrl"];
                    _ProductImage.ImageUrl = strImageUrl;
                }

                if (Session["ProductID"] != null)
                {
                    string strProductID = Convert.ToInt32(Session["ProductID"]).ToString();
                    _ProductID = int.Parse(strProductID);

                    getSQLDB_FindProduct_closedDateTime_officialPrice(connString, _ProductID);

                    // to display least time
                    showProductLastTime();

                    // to display the bit winner until now
                    ShowNowBitWinner(_ProductID);
                }

                // Call this procedure when the application starts.  
                // Set to 1 minute.  
                int interval = 1000;
                _timer = new System.Timers.Timer(interval);
                //設定重複計時
                _timer.AutoReset = true;
                //設定執行System.Timers.Timer.Elapsed事件

                _timer.Elapsed += new System.Timers.ElapsedEventHandler(Mytimer_tick);
                //_timer.Start();

                // 載入以截標熱門
                SQL_readActionProduct_getbid(connString);
                getbid_view.DataSource = ds_getbid; //將DataSet的資料載入到datalist內
                getbid_view.DataBind();

            } // if (IsPostBack == false)
        } // protected void Page_Load(object sender, EventArgs e)

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


        private void Mytimer_tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            showProductLastTime();
            // Response.Redirect(Page.Request.Url.ToString(), false);// System.Web.HttpException: '在此內容中無法使用回應。'
        }

        protected void SQLDB_saveProductBitWinner(string connString, int bidder_ID, int nProdcutID)
        {

            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;

            //new一個SqlConnection物件，是與資料庫連結的通道(其名為Connection)，以s_data內的連接字串連接所對應的資料庫。
            SqlConnection connection = new SqlConnection(s_data);

            //與資料庫連接的通道開啟
            connection.Open();
            // string sql_update = string.Format("update Member SET balance={0} where member_ID='{1}'", nBalance, bidder_ID);
            string sql_update = string.Format("update Action_product SET bid_winner_ID={0} where action_product_ID='{1}'", bidder_ID, nProdcutID);
            SqlCommand sql_update_cmd = new SqlCommand(sql_update, connection); //SQL語句

            sql_update_cmd.ExecuteNonQuery();

            //關閉與資料庫連接的通道
            connection.Close();

            Response.Write($"<script>alert('寫入 Action_product 得標者成功');</script>");

        }// protected void SQLDB_saveMemberBalance()

        private int getProductIDfromSession()
        {
            string strProductID = (string) Session["ProductID"];
            return(int.Parse(strProductID));
        }

        protected int SQLDB_getBidderID_fromBitWinnerMark(string connString, int nProdcutID)
        {
            int nLastBidWinnerID = 0;
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;

            //new一個SqlConnection物件，是與資料庫連結的通道(其名為Connection)，以s_data內的連接字串連接所對應的資料庫。
            SqlConnection connection = new SqlConnection(s_data);

            // bug1: SQL content
            string sql_statement = $"select bid_winner_ID from Action_product where action_product_ID={nProdcutID}";

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
                    string tmp = Reader["bid_winner_ID"].ToString();
                    nLastBidWinnerID = int.Parse(tmp);
                }// if (Reader.Read())

            }// if (Reader.HasRows) login name match
            //關閉與資料庫連接的通道
            connection.Close();
            return nLastBidWinnerID;
        }// protected void SQLDB_getBidderID_fromBitWinnerMark()

        protected void SQLDB_setBitWinnerMark(string connString, int nProdcutID, int nSetValue)
        {
            // nProdcutID, nActionBidID, "Yes"
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;

            //new一個SqlConnection物件，是與資料庫連結的通道(其名為Connection)，以s_data內的連接字串連接所對應的資料庫。
            SqlConnection connection = new SqlConnection(s_data);

            //與資料庫連接的通道開啟
            connection.Open();
            // string sql_update = string.Format("update Member SET balance={0} where member_ID='{1}'", nBalance, bidder_ID);
            string sql_update = string.Format("update Action_product SET bid_winner_ID='{0}' where action_product_ID='{1}'", nSetValue, nProdcutID);
            SqlCommand sql_update_cmd = new SqlCommand(sql_update, connection); //SQL語句

            sql_update_cmd.ExecuteNonQuery();

            //關閉與資料庫連接的通道
            connection.Close();

            Response.Write($"<script>alert('修改 Action_product 得標者成功');</script>");

        }//protected void SQLDB_setBitWinnerMark("Sale_net_Jun18_2021_betaConnectionString2", nBidderID)

        protected void SQLDB_setProductBidPrice(string connString, int nProdcutID, int nNewPrice)
        {
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;

            //new一個SqlConnection物件，是與資料庫連結的通道(其名為Connection)，以s_data內的連接字串連接所對應的資料庫。
            SqlConnection connection = new SqlConnection(s_data);

            //與資料庫連接的通道開啟
            connection.Open();
            // string sql_update = string.Format("update Member SET balance={0} where member_ID='{1}'", nBalance, bidder_ID);
            string sql_update = string.Format("update Action_product SET bid_price={0} where action_product_ID={1}", nNewPrice, nProdcutID);
            SqlCommand sql_update_cmd = new SqlCommand(sql_update, connection); //SQL語句

            sql_update_cmd.ExecuteNonQuery();

            //關閉與資料庫連接的通道
            connection.Close();

            Response.Write($"<script>alert('修改 Action_product 下標價成功');</script>");

        }//protected void SQLDB_setBitWinnerMark("Sale_netConnectionString", nBidderID)

        protected void SQLDB_updateBidderWinner(int nActionBidID, int nUpdatedPrice, int nProdcutID)
        {
            /*
            1. to remove last bit_winner "Yes" to 0
            2. to set bit_winner to "Yes" by bitWinner ID */
            int nLastBidderID = 0;
            nLastBidderID = SQLDB_getBidderID_fromBitWinnerMark(connString, nProdcutID);
            if(nLastBidderID != 0){
                SQLDB_setBitWinnerMark(connString, nProdcutID, 0);
                SQLDB_setProductBidPrice(connString, nProdcutID, 0);
            }
            SQLDB_setBitWinnerMark(connString, nProdcutID, nActionBidID);
            SQLDB_setProductBidPrice(connString, nProdcutID, nUpdatedPrice);
        }// protected void SQLDB_updateBidderWinner()

        private void UpdateBitWinner(int nSelectedProdcutID)
        {
            // 1. to open Action_bidder
            // 2. to get the least price and bidder
            // 3. to get all bid records belongs the bidder
            // 4. if the price is the lease price , and the lot of it is one, he/she wins
            List<BidderData> bidderData = new List<BidderData>();
            BidderData bidderDataWinner = new BidderData();
            int             nWinnerID = 0;
            string          strWinnerName = "";

            // 1. to open Action_bidder
            SQLDB_getBidderPrice(connString, ref bidderData, nSelectedProdcutID);
            // case1 : nobody bids
            if(bidderData.Count == 0){
                _NowBitWinner.Text = "無";
                _NowBitPrice.Text =  "無";
            }
            else{
                // case2. one or multiple bids
                // to get the smallest price
                for (int i = 0; i < bidderData.Count; i++)
                {
                    if(i == 0)
                        bidderDataWinner = bidderData[i];
                    else{
                        if(bidderDataWinner._nBidPrice > bidderData[i]._nBidPrice)
                            bidderDataWinner = bidderData[i];
                    }// i=1..
                }// for

                // 3. to get all bid records belongs the bidder
                List<BidderData>  listSameBidderPrices = bidderData.FindAll(x => x._nBidderID == bidderDataWinner._nBidderID);
                // to list Count
                string strFoundTotal = listSameBidderPrices.Count.ToString();
                _NowBitWinner.Text = strFoundTotal; 

                if(listSameBidderPrices.Count == 1){
                    // he/she is winner
                    nWinnerID = bidderDataWinner._nBidderID;
                    // to get Member name from Member DB
                    SQLDB_getBidderNameFromMember(connString, nWinnerID, ref strWinnerName);
                    _NowBitWinner.Text = strWinnerName;

                    string strPrice = bidderDataWinner._nBidPrice.ToString();
                    _NowBitPrice.Text = strPrice;

                    // save winner to Action_product
                    int nProdcutID = getProductIDfromSession();
                    SQLDB_saveProductBitWinner(connString, nWinnerID, nProdcutID);

                    // save winner to Action_bidder
                    SQLDB_updateBidderWinner(bidderDataWinner._nBidderID, bidderDataWinner._nBidPrice, nProdcutID);

                }
                else{
                    // 1 person multiple bids
                    //  if each of those prices are the same: no winner
                    if(ifAllPriceAreSame(listSameBidderPrices) == true){
                        _NowBitWinner.Text = "無";
                        _NowBitPrice.Text =  "無";
                    }
                    else{
                        nWinnerID = bidderDataWinner._nBidderID;
                        // to get Member name from Member DB
                        SQLDB_getBidderNameFromMember(connString, nWinnerID, ref strWinnerName);
                        _NowBitWinner.Text = strWinnerName;

                        string strPrice = bidderDataWinner._nBidPrice.ToString();
                        _NowBitPrice.Text = strPrice;

                        int nProdcutID = getProductIDfromSession();
                        // save winner to Action_product
                        SQLDB_saveProductBitWinner(connString, nWinnerID, nProdcutID);

                        // save winner to Action_bidder
                        SQLDB_updateBidderWinner(bidderDataWinner._nBidderID, bidderDataWinner._nBidPrice, nProdcutID);
                    }// 1 person diff bids, he/she is winner
                }// // 1 person multiple bids

            }// multiple bids

        }// private void UpdateBitWinner()

        // if the someDateTime is passed, ret true
        private bool ifClosed(DateTime someDateTime, ref TimeSpan ts)
        {
            DateTime nowDateTime = DateTime.Now;
            ts = someDateTime - nowDateTime;
            double days = ts.TotalDays;
            double mins = ts.TotalMinutes;
            if ((days < 0.0) && (mins < 0.0))
                return true;
            else
                return false;
        }// private bool ifClosed()

        private void showProductLastTime()
        {
            TimeSpan ts = new TimeSpan();
            if (ifClosed(_ProductClosedDateTime, ref ts) == false)
            {
                double nDays = 0;
                double nHour = 0;
                double nOriMin = 0;
                double nMin = 0;
                double nSec = 0;
                string strSec = "";

                nOriMin = ts.TotalMinutes;
                nDays = (int)Math.Floor(ts.TotalDays);
                if (nOriMin > 1440.0)// day
                    nMin -= (nDays * 1440.0);
                else if (nOriMin > 60.0)
                { // hour
                    nHour = nOriMin / 60.0;
                    nHour = (int)Math.Floor(nHour);
                    nMin = nOriMin - (nHour * 60.0);
                    nMin = (int)Math.Floor(nMin);
                    strSec = ts.Seconds.ToString();
                }
                else
                {
                    nMin = (int)Math.Floor(nOriMin);
                    strSec = ts.Seconds.ToString();
                }
                _LeftTime.Text = "差距 " + Convert.ToInt32(nDays).ToString() + " 天 "
                                 + nHour.ToString() + " 小時 "
                                 + nMin.ToString() + " 分鐘 "
                                 + strSec + " 秒";

                // Response.Redirect(Request.RawUrl);
            }// if closedDateTime is later than now

        }// private void showProductLastTime()

        protected void getSQLDB_FindProduct_closedDateTime_officialPrice(string connString, int nProductID)
        {
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;

            //new一個SqlConnection物件，是與資料庫連結的通道(其名為Connection)，以s_data內的連接字串連接所對應的資料庫。
            SqlConnection connection = new SqlConnection(s_data);

            // bug1: SQL content
            string sql_statement = $"select closedDateTime, official_price from Action_product where action_product_ID='{nProductID}'";

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
                    _ProductClosedDateTime = (DateTime)Reader["closedDateTime"];
                    _official_price = (int)Reader["official_price"];
                    _GUI_official_price.Text = _official_price.ToString();
                }// if (Reader.Read())

            }// if (Reader.HasRows) login name match
            else
            {

            }// if (Reader.HasRows) login name mismatch
            //關閉與資料庫連接的通道
            connection.Close();
        }// protected void getSQLDB_FindProduct_closedDateTime_officialPrice()

        // to get data from Action_bidder
        private void SQLDB_getBidderPrice(string connString, ref List<BidderData> bidderData, int nSelectedProdcutID)
        {
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;

            //new一個SqlConnection物件，是與資料庫連結的通道(其名為Connection)，以s_data內的連接字串連接所對應的資料庫。
            SqlConnection connection = new SqlConnection(s_data);

            // "select bid_winner_ID,bid_price from Action_product where bid_winner_ID is not NULL";
            string sql_statement = $"select bidder_ID,bid_price from Action_product where bit_product_ID={nSelectedProdcutID}";

            // bug2: sqlText
            //new一個SqlCommand告訴這個物件準備要執行什麼SQL指令
            SqlCommand Command = new SqlCommand(sql_statement, connection);

            //與資料庫連接的通道開啟
            connection.Open();

            //new一個DataReader接取Execute所回傳的資料。
            SqlDataReader Reader = Command.ExecuteReader();

            if(Reader.HasRows){
            //檢查是否有資料列
                while (Reader.Read())
                {
                    //使用Read方法把資料讀進Reader，讓Reader一筆一筆順向指向資料列，並回傳是否成功。
                    BidderData data = new BidderData();
                    string tmp;

                    tmp = Reader["bidder_ID"].ToString();
                    data._nBidderID = int.Parse(tmp);

                    tmp = Reader["bid_price"].ToString();
                    data._nBidPrice = int.Parse(tmp);
                    bidderData.Add(data);
                }// while(Reader.Read())
            }// if(Reader.HasRows){

            //關閉與資料庫連接的通道
            connection.Close();
        }// private void SQLDB_getBidderPrice(string connString, ref List<BidderData> bidderData)

        private void SQLDB_getBidderNameFromMember(string connString,int nWinnerID, ref string strWinnerName)
        {
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;

            //new一個SqlConnection物件，是與資料庫連結的通道(其名為Connection)，以s_data內的連接字串連接所對應的資料庫。
            SqlConnection connection = new SqlConnection(s_data);

            // bug1: SQL content
            string sql_statement = $"select user_name from Member where member_ID='{nWinnerID}'";

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
                    strWinnerName = Reader["user_name"].ToString();
                }// if (Reader.Read())

            }// if (Reader.HasRows) login name match

            //關閉與資料庫連接的通道
            connection.Close();
        }// private void SQLDB_getBidderNameFromMember(string connString,int nWinnerID, ref strWinnerName)

        private bool ifAllPriceAreSame(List<BidderData> fetchData)
        {
            bool nRet = true;
            int  nPrice = 0;
            for (int i = 0; i < fetchData.Count; i++)
            {
                if(i == 0)
                    nPrice = fetchData[i]._nBidPrice;
                else{
                    if(nPrice != fetchData[i]._nBidPrice){
                        nRet = false;
                        break;
                    }
                }// i=1..
            }// for
            return nRet;
        }// private bool ifAllPriceAreSame(List<BidderData> fetchData)

        protected void SQLDB_saveMemberBalance(string connString, int bidder_ID, int nBalance)
        {
            int seller_ID = 0;

            seller_ID = (int)(Session["member_ID"]);

            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;

            //new一個SqlConnection物件，是與資料庫連結的通道(其名為Connection)，以s_data內的連接字串連接所對應的資料庫。
            SqlConnection connection = new SqlConnection(s_data);

            //與資料庫連接的通道開啟
            connection.Open();
            // _File
            string sql_update = string.Format("update Member SET balance={0} where member_ID='{1}'", nBalance, bidder_ID);
            SqlCommand sql_insert_cmd = new SqlCommand(sql_update, connection); //SQL語句

            sql_insert_cmd.ExecuteNonQuery();

            //關閉與資料庫連接的通道
            connection.Close();

            Response.Write($"<script>alert('手續費扣款成功');</script>");

        }// protected void SQLDB_saveMemberBalance()

        protected int getSQLDB_FindMember_balance(string connString, int bidder_ID)
        {
            int nRet = 0;
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;

            //new一個SqlConnection物件，是與資料庫連結的通道(其名為Connection)，以s_data內的連接字串連接所對應的資料庫。
            SqlConnection connection = new SqlConnection(s_data);

            // bug1: SQL content
            string sql_statement = $"select balance from Member where member_ID='{bidder_ID}'";

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
                    nRet = (int)Reader["balance"];

                }// if (Reader.Read())

            }// if (Reader.HasRows) login name match
            else
            {

            }// if (Reader.HasRows) login name mismatch
            //關閉與資料庫連接的通道
            connection.Close();
            return nRet;
        }// protected void getSQLDB_FindMember_balance()

        protected void SQLDB_saveBidder(string connString, int nBidder_ID, int nProductID, int nPrice)
        {
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;

            //new一個SqlConnection物件，是與資料庫連結的通道(其名為Connection)，以s_data內的連接字串連接所對應的資料庫。
            SqlConnection connection = new SqlConnection(s_data);

            //與資料庫連接的通道開啟
            connection.Open();

            
            // _File
            SqlCommand sql_insert_cmd = new SqlCommand("insert into Action_product(product,description,pic_pathname,bid_price,status,bit_product_ID,bidder_ID) values(@product,@description,@pic_pathname,@bid_price,@status,@bit_product_ID,@bidder_ID);", connection); //SQL語句
            
            sql_insert_cmd.Parameters.Add("@product", SqlDbType.Text);
            sql_insert_cmd.Parameters["@product"].Value = Session["ProductName"];

            sql_insert_cmd.Parameters.Add("@description", SqlDbType.Text);
            sql_insert_cmd.Parameters["@description"].Value = Session["ProductDesc"];

            sql_insert_cmd.Parameters.Add("@pic_pathname", SqlDbType.Text);
            sql_insert_cmd.Parameters["@pic_pathname"].Value = Session["ImageUrl"];

            sql_insert_cmd.Parameters.Add("@bid_price", SqlDbType.Int);
            sql_insert_cmd.Parameters["@bid_price"].Value = nPrice;

            sql_insert_cmd.Parameters.Add("@status", SqlDbType.Text);
            sql_insert_cmd.Parameters["@status"].Value = "bidding";

            sql_insert_cmd.Parameters.Add("@bit_product_ID", SqlDbType.Int);
            sql_insert_cmd.Parameters["@bit_product_ID"].Value = nProductID;

            sql_insert_cmd.Parameters.Add("@bidder_ID", SqlDbType.Int);
            sql_insert_cmd.Parameters["@bidder_ID"].Value = nBidder_ID;
            
            sql_insert_cmd.ExecuteNonQuery();

            //關閉與資料庫連接的通道
            connection.Close();

            Response.Write($"<script>alert('下標資料寫入 Action_product 資料庫成功');</script>");

        }// protected void SQLDB_saveBidder("Sale_netConnectionString", _bidder_ID, _ProductID, nPrice)

        public class BidderData
        {
            public int _nBidderID { get; set; }
            public int _nBidPrice { get; set; }
        }

        private void ShowNowBitWinner(int nSelectedProdcutID)
        {
            List<BidderData> bidderData = new List<BidderData>();
            BidderData bidderDataWinner = new BidderData();
            int nWinnerID = 0;
            string strWinnerName = "";

            // 1. to open Action_bidder
            SQLDB_getBidderPrice(connString, ref bidderData, nSelectedProdcutID);
            // case1 : nobody bids
            if (bidderData.Count == 0)
            {
                _NowBitWinner.Text = "無";
                _NowBitPrice.Text = "無";
            }
            else
            {
                // case2. one or multiple bids
                // to get the smallest price
                for (int i = 0; i < bidderData.Count; i++)
                {
                    if (i == 0)
                        bidderDataWinner = bidderData[i];
                    else
                    {
                        if (bidderDataWinner._nBidPrice > bidderData[i]._nBidPrice)
                            bidderDataWinner = bidderData[i];
                    }// i=1..
                }// for

                // 3. to get all bid records belongs the bidder
                List<BidderData> listSameBidderPrices = bidderData.FindAll(x => x._nBidderID == bidderDataWinner._nBidderID);
                // to list Count
                string strFoundTotal = listSameBidderPrices.Count.ToString();
                _NowBitWinner.Text = strFoundTotal;

                if (listSameBidderPrices.Count == 1)
                {
                    // he/she is winner
                    nWinnerID = bidderDataWinner._nBidderID;
                    // to get Member name from Member DB
                    SQLDB_getBidderNameFromMember(connString, nWinnerID, ref strWinnerName);
                    _NowBitWinner.Text = strWinnerName;

                    string strPrice = bidderDataWinner._nBidPrice.ToString();
                    _NowBitPrice.Text = strPrice;

                }
                else
                {
                    // 1 person multiple bids
                    //  if each of those prices are the same: no winner
                    if (ifAllPriceAreSame(listSameBidderPrices) == true)
                    {
                        _NowBitWinner.Text = "無";
                        _NowBitPrice.Text = "無";
                    }
                    else
                    {
                        nWinnerID = bidderDataWinner._nBidderID;
                        // to get Member name from Member DB
                        SQLDB_getBidderNameFromMember(connString, nWinnerID, ref strWinnerName);
                        _NowBitWinner.Text = strWinnerName;

                        string strPrice = bidderDataWinner._nBidPrice.ToString();
                        _NowBitPrice.Text = strPrice;

                    }// 1 person diff bids, he/she is winner
                }// // 1 person multiple bids

            }// multiple bids

        }// private void ShowNowBitWinner()

        protected void _BitBtn_Click(object sender, EventArgs e)
        {
            int nPrice = 0;
            if (_BitPrice.Text == "")
            {
                Response.Write("<script>alert('請先輸入出價金額！');</script>");
                return;
            }
            try
            {
                nPrice = int.Parse(_BitPrice.Text);
            }
            catch (System.FormatException)
            {
                Response.Write($"<script>alert('出價金額 請填入 數字');</script>");
                return;
            }

            // 
            if(Session["member_ID"] == null){
                Session["logged_to_page"] = "BitProductForm.aspx";
                // to the member logging page
                Response.Write($"<script>alert('請先登入會員');</script>");
                Response.Redirect("memberLoginForm.aspx");
                //Server.Transfer("memberLoginForm.aspx");
                return;
            }

            _bidder_ID = (int)Session["member_ID"];
            int bidderBalance = getSQLDB_FindMember_balance(connString, _bidder_ID);

            // 1. if balance is enough
            if (bidderBalance < nCharge)
            {
                Response.Write("<script>alert('會員手續費不足，請先儲值！');</script>");
                // ShowNowBitWinner();
                return;
            }// balance is not enough
            else
            {
                // 2. balance minues charge
                // 2.1 balance minus charge
                bidderBalance -= nCharge;
                // 2.2 store balance to DB
                SQLDB_saveMemberBalance(connString, _bidder_ID, bidderBalance);
            }// balance is enough

            // 3. to save the bidder record
            string strProductID = (string) Session["ProductID"];
            int nProductID = int.Parse(strProductID);
            SQLDB_saveBidder(connString, _bidder_ID, nProductID, nPrice);
            
            // 4. to show bit winner until now
            UpdateBitWinner(nProductID);

            // 5. to clean input price after all jobs
            _BitPrice.Text = "";
        }// protected void _BitBtn_Click(object sender, EventArgs e)

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

    } // public partial class BitProductForm : System.Web.UI.Page


}// namespace BitSystem