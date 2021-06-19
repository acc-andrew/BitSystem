﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BitSystem
{
    public partial class ProductDetailForm : System.Web.UI.Page
    {
        private int _sellerID = 0;
        private int _ProductID = 0;
        private DateTime _ProductClosedDateTime = new DateTime();
        private int _official_price = 0;
        private int _bidder_ID = 0;
        private const int nCharge = 10;
        System.Timers.Timer _timer;

        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            //設定會員登入與否顯現標示不同
            Session["Login"] = null;

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
            */

            // if the page loaded first time
            if (IsPostBack == false)
            {
                if(Session["ProductName"] != null){
                    string strProductName = (string) Session["ProductName"];
                    _ProductName.Text = strProductName;
                }

                if (Session["ProductDesc"] != null)
                {
                    string strProductDesc = (string) Session["ProductDesc"];
                    _ProductDesc.Text = strProductDesc;
                }

                if(Session["SellerID"] != null){
                    string strSellerID = (string) Session["SellerID"];
                    _sellerID = int.Parse(strSellerID);
                }

                if(Session["ImageUrl"] != null){
                    string strImageUrl = (string) Session["ImageUrl"];
                    _ProductImage.ImageUrl = strImageUrl;
                }

                if(Session["ProductID"] != null){
                    string strProductID = (string) Session["ProductID"];
                    _ProductID = int.Parse(strProductID);

                    getSQLDB_FindProduct_closedDateTime_officialPrice("Sale_netConnectionString6", _ProductID);

                    // to display least time
                    showProductLastTime();

                    // to display the bit winner until now
                    ShowNowBitWinner();
                }

                 // Call this procedure when the application starts.  
                // Set to 1 minute.  
                int interval = 1000;  
                _timer = new System.Timers.Timer(interval);
                //設定重複計時
                _timer.AutoReset = true;
                //設定執行System.Timers.Timer.Elapsed事件
                
                _timer.Elapsed += new System.Timers.ElapsedEventHandler(Mytimer_tick);
                _timer.Start();
            
            } // if the page loaded first time
        }// protected void Page_Load(object sender, EventArgs e)

        private void Mytimer_tick(object sender, System.Timers.ElapsedEventArgs e)
        {  
            showProductLastTime();
            // Response.Redirect(Page.Request.Url.ToString(), false);// System.Web.HttpException: '在此內容中無法使用回應。'
        }  

        public class BidderData
        {
            public int   _nActionBidderID { get; set; }
            public int   _nBidderID { get; set; }
            public int   _nPrice { get; set; }
        }

        private int getProductIDfromSession()
        {
            string strProductID = (string) Session["ProductID"];
            return(int.Parse(strProductID));
        }

        private void ShowNowBitWinner()
        {
            List<BidderData> bidderData = new List<BidderData>();
            BidderData bidderDataWinner = new BidderData();
            int             nWinnerID = 0;
            string          strWinnerName = "";

            // 1. to open Action_bidder
            SQLDB_getBidderPrice("Sale_netConnectionString6", ref bidderData);
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
                        if(bidderDataWinner._nPrice > bidderData[i]._nPrice)
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
                    SQLDB_getBidderNameFromMember("Sale_netConnectionString6", nWinnerID, ref strWinnerName);
                    _NowBitWinner.Text = strWinnerName;

                    string strPrice = bidderDataWinner._nPrice.ToString();
                    _NowBitPrice.Text = strPrice;

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
                        SQLDB_getBidderNameFromMember("Sale_netConnectionString6", nWinnerID, ref strWinnerName);
                        _NowBitWinner.Text = strWinnerName;

                        string strPrice = bidderDataWinner._nPrice.ToString();
                        _NowBitPrice.Text = strPrice;

                    }// 1 person diff bids, he/she is winner
                }// // 1 person multiple bids

            }// multiple bids

        }// private void ShowNowBitWinner()

        private void UpdateBitWinner()
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
            SQLDB_getBidderPrice("Sale_netConnectionString6", ref bidderData);
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
                        if(bidderDataWinner._nPrice > bidderData[i]._nPrice)
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
                    SQLDB_getBidderNameFromMember("Sale_netConnectionString6", nWinnerID, ref strWinnerName);
                    _NowBitWinner.Text = strWinnerName;

                    string strPrice = bidderDataWinner._nPrice.ToString();
                    _NowBitPrice.Text = strPrice;

                    // save winner to Action_product
                    SQLDB_saveProductBitWinner("Sale_netConnectionString6", nWinnerID, getProductIDfromSession());

                    // save winner to Action_bidder
                    SQLDB_updateBidderWinner(bidderDataWinner._nActionBidderID, bidderDataWinner._nPrice);

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
                        SQLDB_getBidderNameFromMember("Sale_netConnectionString6", nWinnerID, ref strWinnerName);
                        _NowBitWinner.Text = strWinnerName;

                        string strPrice = bidderDataWinner._nPrice.ToString();
                        _NowBitPrice.Text = strPrice;

                        // save winner to Action_product
                        SQLDB_saveProductBitWinner("Sale_netConnectionString6", nWinnerID, getProductIDfromSession());

                        // save winner to Action_bidder
                        SQLDB_updateBidderWinner(bidderDataWinner._nActionBidderID, bidderDataWinner._nPrice);
                    }// 1 person diff bids, he/she is winner
                }// // 1 person multiple bids

            }// multiple bids

        }// private void UpdateBitWinner()

        private bool ifAllPriceAreSame(List<BidderData> fetchData)
        {
            bool nRet = true;
            int  nPrice = 0;
            for (int i = 0; i < fetchData.Count; i++)
            {
                if(i == 0)
                    nPrice = fetchData[i]._nPrice;
                else{
                    if(nPrice != fetchData[i]._nPrice)
                        nRet = false;
                        break;
                }// i=1..
            }// for
            return nRet;
        }// private bool ifAllPriceAreSame(List<BidderData> fetchData)

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

        // to get data from Action_bidder
        private void SQLDB_getBidderPrice(string connString, ref List<BidderData> bidderData)
        {
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;

            //new一個SqlConnection物件，是與資料庫連結的通道(其名為Connection)，以s_data內的連接字串連接所對應的資料庫。
            SqlConnection connection = new SqlConnection(s_data);

            // bug1: SQL content
            string sql_statement = $"select action_bidder_ID,bidder_ID,price from Action_bidder";

            // bug2: sqlText
            //new一個SqlCommand告訴這個物件準備要執行什麼SQL指令
            SqlCommand Command = new SqlCommand(sql_statement, connection);

            //與資料庫連接的通道開啟
            connection.Open();

            //new一個DataReader接取Execute所回傳的資料。
            SqlDataReader Reader = Command.ExecuteReader();

            //檢查是否有資料列
            while(Reader.Read())
            {
                //使用Read方法把資料讀進Reader，讓Reader一筆一筆順向指向資料列，並回傳是否成功。
                BidderData data = new BidderData();
                string tmp;

                tmp = Reader["action_bidder_ID"].ToString();
                data._nActionBidderID = int.Parse(tmp);

                tmp = Reader["bidder_ID"].ToString();
                data._nBidderID = int.Parse(tmp);

                tmp = Reader["price"].ToString();
                data._nPrice   = int.Parse(tmp);
                bidderData.Add(data);
            }// while(Reader.Read())

            //關閉與資料庫連接的通道
            connection.Close();
        }// private void SQLDB_getBidderPrice(string connString, ref List<BidderData> bidderData)

        // if the someDateTime is passed, ret true
        private bool ifClosed(DateTime someDateTime, ref TimeSpan ts)
        {
            DateTime nowDateTime = DateTime.Now;
            ts = someDateTime - nowDateTime;
            double days = ts.TotalDays;
            double mins = ts.TotalMinutes;
            if((days < 0.0) && (mins < 0.0))
                return true;
            else
                return false;
        }// private bool ifClosed()

        private void showProductLastTime()
        {
            TimeSpan ts = new TimeSpan();
            if(ifClosed(_ProductClosedDateTime, ref ts) == false){
                double nDays = 0;
                double nHour = 0;
                double nOriMin = 0;
                double nMin = 0;
                double nSec = 0;
                string strSec = "";

                nOriMin = ts.TotalMinutes;
                nDays = (int)Math.Floor(ts.TotalDays);
                if(nOriMin > 1440.0)// day
                    nMin -= (nDays * 1440.0); 
                else if(nOriMin > 60.0){ // hour
                    nHour = nOriMin / 60.0;
                    nHour = (int)Math.Floor(nHour);
                    nMin = nOriMin - (nHour*60.0);
                    nMin = (int)Math.Floor(nMin);
                    strSec = ts.Seconds.ToString();
                }
                else{
                    nMin = (int)Math.Floor(nOriMin);
                    strSec = ts.Seconds.ToString();
                }
                _LeftTime.Text = "差距 " + Convert.ToInt32(nDays).ToString() + " 天 "
                                         + nHour.ToString() + " 小時 "
                                         + nMin.ToString() + " 分鐘 "
                                         + strSec + " 秒" ;

                // Response.Redirect(Request.RawUrl);
            }// if closedDateTime is later than now

        }// private void showProductLastTime()

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
                    _official_price = (int) Reader["official_price"];
                    _GUI_official_price.Text = _official_price.ToString();
                }// if (Reader.Read())

            }// if (Reader.HasRows) login name match
            else
            {

            }// if (Reader.HasRows) login name mismatch
            //關閉與資料庫連接的通道
            connection.Close();
        }// protected void getSQLDB_FindProduct_closedDateTime_officialPrice()

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

        protected int SQLDB_getBidderID_fromBitWinnerMark(string connString)
        {
            int nBidderID = 0;
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;

            //new一個SqlConnection物件，是與資料庫連結的通道(其名為Connection)，以s_data內的連接字串連接所對應的資料庫。
            SqlConnection connection = new SqlConnection(s_data);

            // bug1: SQL content
            string sql_statement = "select action_bidder_ID from Action_bidder where bit_winner='Yes'";

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
                    nBidderID = (int)Reader["action_bidder_ID"];
                }// if (Reader.Read())

            }// if (Reader.HasRows) login name match
            //關閉與資料庫連接的通道
            connection.Close();
            return nBidderID;
        }// protected void SQLDB_getBidderID_fromBitWinnerMark()

        protected void SQLDB_updateBidderWinner(int nActionBidID, int nUpdatedPrice)
        {
            /*
            1. to remove last bit_winner "Yes" to 0
            2. to set bit_winner to "Yes" by bitWinner ID */
            int nLastBidderID = 0;
            nLastBidderID = SQLDB_getBidderID_fromBitWinnerMark("Sale_netConnectionString6");
            if(nLastBidderID != 0){
                SQLDB_setBitWinnerMark("Sale_netConnectionString6", nLastBidderID, "0");
                SQLDB_setBidderLowPrice("Sale_netConnectionString6",nLastBidderID, 0);
            }
            SQLDB_setBitWinnerMark("Sale_netConnectionString6", nActionBidID, "Yes");
            SQLDB_setBidderLowPrice("Sale_netConnectionString6",nActionBidID, nUpdatedPrice);
        }// protected void SQLDB_updateBidderWinner()

        protected void SQLDB_setBidderLowPrice(string connString, int nActionBidID, int nNewPrice)
        {
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;

            //new一個SqlConnection物件，是與資料庫連結的通道(其名為Connection)，以s_data內的連接字串連接所對應的資料庫。
            SqlConnection connection = new SqlConnection(s_data);

            //與資料庫連接的通道開啟
            connection.Open();
            // string sql_update = string.Format("update Member SET balance={0} where member_ID='{1}'", nBalance, bidder_ID);
            string sql_update = string.Format("update Action_bidder SET low_price={0} where action_bidder_ID='{1}'", nNewPrice, nActionBidID);
            SqlCommand sql_update_cmd = new SqlCommand(sql_update, connection); //SQL語句

            sql_update_cmd.ExecuteNonQuery();

            //關閉與資料庫連接的通道
            connection.Close();

            Response.Write($"<script>alert('修改 Action_bidder 最低價成功');</script>");

        }//protected void SQLDB_setBitWinnerMark("Sale_netConnectionString", nBidderID)

        protected void SQLDB_setBitWinnerMark(string connString, int nActionBidID, string strMark)
        {
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;

            //new一個SqlConnection物件，是與資料庫連結的通道(其名為Connection)，以s_data內的連接字串連接所對應的資料庫。
            SqlConnection connection = new SqlConnection(s_data);

            //與資料庫連接的通道開啟
            connection.Open();
            // string sql_update = string.Format("update Member SET balance={0} where member_ID='{1}'", nBalance, bidder_ID);
            string sql_update = string.Format("update Action_bidder SET bit_winner='{0}' where action_bidder_ID='{1}'", strMark, nActionBidID);
            SqlCommand sql_update_cmd = new SqlCommand(sql_update, connection); //SQL語句

            sql_update_cmd.ExecuteNonQuery();

            //關閉與資料庫連接的通道
            connection.Close();

            Response.Write($"<script>alert('修改 Action_bidder 得標者成功');</script>");

        }//protected void SQLDB_setBitWinnerMark("Sale_netConnectionString", nBidderID)

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

        protected void SQLDB_saveBidder(string connString, int nBidder_ID, int nProductID, int nPrice)
        {
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;

            //new一個SqlConnection物件，是與資料庫連結的通道(其名為Connection)，以s_data內的連接字串連接所對應的資料庫。
            SqlConnection connection = new SqlConnection(s_data);

            //與資料庫連接的通道開啟
            connection.Open();

            // _File
            SqlCommand sql_insert_cmd = new SqlCommand("insert into Action_bidder(number,price,status,action_product_ID,bidder_ID) values(@number,@price,@status,@action_product_ID,@bidder_ID);", connection); //SQL語句
            sql_insert_cmd.Parameters.Add("@number", SqlDbType.Text);
            sql_insert_cmd.Parameters["@number"].Value = "0";

            sql_insert_cmd.Parameters.Add("@price", SqlDbType.Text);
            sql_insert_cmd.Parameters["@price"].Value = nPrice.ToString();

            sql_insert_cmd.Parameters.Add("@status", SqlDbType.Text);
            sql_insert_cmd.Parameters["@status"].Value = "競標中";

            sql_insert_cmd.Parameters.Add("@action_product_ID", SqlDbType.Int);
            sql_insert_cmd.Parameters["@action_product_ID"].Value = nProductID;

            sql_insert_cmd.Parameters.Add("@bidder_ID", SqlDbType.Int);
            sql_insert_cmd.Parameters["@bidder_ID"].Value = nBidder_ID;
            
            sql_insert_cmd.ExecuteNonQuery();

            //關閉與資料庫連接的通道
            connection.Close();

            Response.Write($"<script>alert('寫入 Action_bidder 資料庫成功');</script>");

        }// protected void SQLDB_saveBidder("Sale_netConnectionString", _bidder_ID, _ProductID, nPrice)

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

            _bidder_ID = (int)Session["member_ID"];
            int bidderBalance = getSQLDB_FindMember_balance("Sale_netConnectionString6", _bidder_ID);

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
                SQLDB_saveMemberBalance("Sale_netConnectionString6", _bidder_ID, bidderBalance);
            }// balance is enough

            // 3. to save the bidder record
            string strProductID = (string) Session["ProductID"];
            int nProductID = int.Parse(strProductID);
            SQLDB_saveBidder("Sale_netConnectionString6", _bidder_ID, nProductID, nPrice);
            
            // 4. to show bit winner until now
            UpdateBitWinner();

            // 5. to clean input price after all jobs
            _BitPrice.Text = "";

        }// protected void _BitBtn_Click(object sender, EventArgs e)

        
        //linkbutton 點擊連接網址
        protected void home_Click(object sender, EventArgs e)
        {
            Server.Transfer("Home.aspx");
        }

        protected void member_info_Click(object sender, EventArgs e)
        {
            Server.Transfer("memberProfile.aspx");
        }

        protected void order_info_Click(object sender, EventArgs e)
        {
            Server.Transfer("memberOrder.aspx");
        }

        protected void my_info_Click(object sender, EventArgs e)
        {
            Server.Transfer("memberLoginForm.aspx");
        }

        protected void register_Click(object sender, EventArgs e)
        {
            Server.Transfer("memberRegisterForm.aspx");
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
            Server.Transfer("Home.aspx");
        }
        
    }// public partial class ProductDetailForm : System.Web.UI.Page
}// namespace BitSystem