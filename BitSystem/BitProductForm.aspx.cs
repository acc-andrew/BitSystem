using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BitSystem
{
    public partial class ProductDetailForm : System.Web.UI.Page
    {
        int     _sellerID = 0;
        int     _selectedProductID = 0;
        int     _ProductID = 0;
        DateTime _ProductClosedDateTime = new DateTime();

        protected void Page_Load(object sender, EventArgs e)
        {
            // class member


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

                    bSQLDB_FindProduct_closedDateTime("Sale_netConnectionString", _ProductID);
                }

            } // if the page loaded first time
        }// protected void Page_Load(object sender, EventArgs e)

        protected bool bSQLDB_FindProduct_closedDateTime(string connString, int nProductID)
        {
            bool bFound = false;
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;

            //new一個SqlConnection物件，是與資料庫連結的通道(其名為Connection)，以s_data內的連接字串連接所對應的資料庫。
            SqlConnection connection = new SqlConnection(s_data);

            // bug1: SQL content
            string sql_statement = $"select closedDateTime from Action_product where action_product_ID='{nProductID}'";

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
                    _LeftTime.Text = _ProductClosedDateTime.ToString();
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

    }// public partial class ProductDetailForm : System.Web.UI.Page
}// namespace BitSystem