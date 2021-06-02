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
    public class ListItem
    {
        public Image _image { get; set; }
        public string _product_name { get; set; }
        public string _total_number { get; set; }
        public string _seller_ID { get; set; }

        public ListItem(string name, string description, Image image)
        {
            this._product_name = name;
            this._total_number = description;
            this._image = image;
        }

    }// public class ListItem



    public partial class GoodListForm : System.Web.UI.Page
    {
        SqlDataAdapter _da = new SqlDataAdapter();       //SQL 資料庫的連接與執行命令
        DataSet        _ds = new DataSet();
        SqlCommand    _cmd = new SqlCommand();
        SqlConnection _Conn = new SqlConnection();
        private List<string> _aPathname_Good_Picture = new List<string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            // member has logged
            if (Session["memberLogged"] == "Yes" &&
                Session["member_ID"] != null)
            {
                _memberLoginBtn.Enabled = false;
                _memberRegisterBtn.Enabled = false;

                _MyGoodsBtn.Enabled = true;
                _OnSaleWebSiteBtn.Enabled = true;
            }
            else
            {
                _memberLoginBtn.Enabled = true;
                _memberRegisterBtn.Enabled = true;

                _MyGoodsBtn.Enabled = false;
                _OnSaleWebSiteBtn.Enabled = false;
            }

            // pre-fetch picture pathname from Market_product2 DB
            fetchGoodPicsPathname();

            // to set event handler: row
            // called while each row data prepared
            _GoodsGridView.RowDataBound += new GridViewRowEventHandler(GridViewRowDataBound);

            SQL_readMarketProduct2("Sale_netConnectionString");
            _GoodsGridView.DataSource = _ds; //將DataSet的資料載入到GridView1內
            _GoodsGridView.DataBind();

        }//protected void Page_Load(object sender, EventArgs e)

        private void fetchGoodPicsPathname()
        {
            // SQL DB
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Sale_netConnectionString"].ConnectionString;

            //new一個SqlConnection物件，是與資料庫連結的通道(其名為Connection)，以s_data內的連接字串連接所對應的資料庫。
            SqlConnection connection = new SqlConnection(s_data);

            // bug1: SQL content
            string sql_statement = $"select pic_pathname from Market_product2";

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
                while (Reader.Read())
                {
                    //DataReader讀出欄位內資料的方式，通常也可寫Reader[0]、[1]...[N]代表第一個欄位到N個欄位。
                    string str_pic_pathname = Reader["pic_pathname"].ToString();
                    _aPathname_Good_Picture.Add(str_pic_pathname);

                }// while (Reader.Read())

            }// if (Reader.HasRows) login name match
            else
            {
                Response.Write("<script>alert('商品資料庫 Market_product2 無此帳號！');</script>");

            }// if (Reader.HasRows) login name mismatch
            //關閉與資料庫連接的通道
            connection.Close();
        }// private void fetchGoodPicsPathname()

        protected void GridViewRowDataBound(Object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType == DataControlRowType.DataRow)  // 
            {
                System.Web.UI.WebControls.Image imgFile = (System.Web.UI.WebControls.Image)e.Row.FindControl("img0");

                string str_pic_pathname = _aPathname_Good_Picture[e.Row.DataItemIndex];
                imgFile.ImageUrl = str_pic_pathname;

            }// if(e.Row.RowType == DataControlRowType.DataRow)

        }// void CustomersGridView_RowDataBound(Object sender, GridViewRowEventArgs e)

         protected void SQL_readMarketProduct2(string connString)
         {
            _cmd.Connection = _Conn;   //將SQL執行的命令語法程式CMD與CONN與SQL連接

            //設定連線IP位置、資料表，帳戶，密碼
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;
            _Conn.ConnectionString = s_data; //"Data Source=127.0.0.1;Initial Catalog=NorthwindChinese;Persist Security Info=True";
            //這一行可依連線的字串不同而去定義它該連線到哪個資料庫!!

            _cmd.CommandText = "SELECT pic_pathname,product_name,total_number,seller_ID  from Market_product2";   //執行SQL語法進行查詢
            _da.SelectCommand = _cmd;            //da選擇資料來源，由cmd載入進來
            _da.Fill(_ds, "Market_product2");            //da把資料填入ds裡面

        }// protected void SQL_readMarketProduct2()

    }// public partial class GoodListForm : System.Web.UI.Page

}// namespace BitSystem