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
    public partial class GoodListForm : System.Web.UI.Page
    {
        SqlDataAdapter _da = new SqlDataAdapter();       //SQL 資料庫的連接與執行命令
        DataSet        _ds = new DataSet();
        SqlCommand    _cmd = new SqlCommand();
        SqlConnection _Conn = new SqlConnection();
        //private ImageList _imageList = new ImageList();

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

            _GoodsGridView.RowDataBound += new GridViewRowEventHandler(this.CustomersGridView_RowDataBound);

            bSQLDB_verify("Sale_netConnectionString");
            _GoodsGridView.DataSource = _ds; //將DataSet的資料載入到GridView1內
            _GoodsGridView.DataBind();

            /*
            _ListView.View = this;
            _ListView.Columns.Add();
            */

            /*
            _ListView.DataSource = _ds;
            _ListView.DataBind();
            */
        }//protected void Page_Load(object sender, EventArgs e)

        // onrowdatabound="CustomersGridView_RowDataBound" 
        void CustomersGridView_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            System.Web.UI.WebControls.Image imgFile;// = CType(e.Row.FindControl("img0"), System.Web.UI.WebControls.Image);

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Display the company name in italics.
                e.Row.Cells[1].Text = "<i>" + e.Row.Cells[1].Text + "</i>";

            }

        }// void CustomersGridView_RowDataBound(Object sender, GridViewRowEventArgs e)
        protected void bSQLDB_verify(string connString)
        {
            _cmd.Connection = _Conn;   //將SQL執行的命令語法程式CMD與CONN與SQL連接

            //設定連線IP位置、資料表，帳戶，密碼
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;
            _Conn.ConnectionString = s_data; //"Data Source=127.0.0.1;Initial Catalog=NorthwindChinese;Persist Security Info=True";
            //這一行可依連線的字串不同而去定義它該連線到哪個資料庫!!

            _cmd.CommandText = "SELECT pic_pathname,product_name,total_number,seller_ID  from Market_product2";   //執行SQL語法進行查詢
            _da.SelectCommand = _cmd;            //da選擇資料來源，由cmd載入進來
            _da.Fill(_ds, "Market_product2");            //da把資料填入ds裡面

            /*
            //_ds.Tables["Products"].Clear();

            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;

            //new一個SqlConnection物件，是與資料庫連結的通道(其名為Connection)，以s_data內的連接字串連接所對應的資料庫。
            SqlConnection connection = new SqlConnection(s_data);

            // bug1: SQL content
            string sql_statement = $"select product,total_number,seller_ID  from Market_product";

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
                    //DataReader讀出欄位內資料的方式，通常也可寫Reader[0]、[1]...[N]代表第一個欄位到N個欄位。
                    
                    string strProduct = Reader["product"].ToString();
                    _

                    Session["memberLogged"] = "Yes";
                    Response.Write("<script>alert('會員登入成功');</script>");
                    
                    // Server.Transfer("test2.aspx");

                }// if (Reader.Read())

            }// if (Reader.HasRows) login name match
            else
            {
                Response.Write("<script>alert('會員資料庫無此帳號！');</script>");

            }// if (Reader.HasRows) login name mismatch
            //關閉與資料庫連接的通道
            connection.Close();
            */
        }// protected void SQLDB_verify()

    }// public partial class GoodListForm : System.Web.UI.Page

}// namespace BitSystem