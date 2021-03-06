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
        SqlDataAdapter _da = new SqlDataAdapter();       //SQL 資料庫的連接與執行命令
        DataSet        _ds = new DataSet();
        SqlCommand    _cmd = new SqlCommand();
        SqlConnection _Conn = new SqlConnection();
        private List<fetchProductData> _aFetchedProduct = new List<fetchProductData>();
        public virtual System.Web.UI.WebControls.GridViewRow SelectedRow { get; }

        protected void Page_Load(object sender, EventArgs e)
        {

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


                // pre-fetch picture pathname from Market_product2 DB
                fetchProductInfo();

                // to set event handler: row
                // called while each row data prepared
                _GoodsGridView.RowDataBound += new GridViewRowEventHandler(GridViewRowDataBound);


                SQL_readActionProduct("Sale_net_Jun22_2021ConnectionString");
                _GoodsGridView.DataSource = _ds; //將DataSet的資料載入到GridView1內
                _GoodsGridView.DataBind();

            }
            

        }//protected void Page_Load(object sender, EventArgs e)

        public class fetchProductData
        {
            public int   _ActionProductID { get; set; }
            public string _imagePathname { get; set; }
            public string _product_name { get; set; }
            public string _description { get; set; }
            public string _total_number { get; set; }
            public string _seller_ID { get; set; }

            public fetchProductData()
            {

            }

            public fetchProductData(int prodID, string imagePathname, 
                            string name, string description, 
                            string total_number, string seller_ID)
            {
                this._ActionProductID = prodID;
                this._imagePathname = imagePathname;
                this._product_name = name;
                this._description = description;
                this._total_number = total_number;
                this._seller_ID = seller_ID;
            }// ctor

        }// public class ListItem
        private void fetchProductInfo()
        {
            // SQL DB
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Sale_net_Jun22_2021ConnectionString"].ConnectionString;

            //new一個SqlConnection物件，是與資料庫連結的通道(其名為Connection)，以s_data內的連接字串連接所對應的資料庫。
            SqlConnection connection = new SqlConnection(s_data);

            // bug1: SQL content
            string sql_statement_no_classify = $"select action_product_ID,product,total_number,description,seller_ID,pic_pathname from Action_product where status='onsale'";

            // bug1: SQL content
            string sql_statement1_classify = $"select action_product_ID,product,total_number,description,seller_ID,pic_pathname from Action_product where status='onsale' and classify ='" + Session["classify"] + "'";

            SqlCommand Command;
            // bug2: sqlText
            //new一個SqlCommand告訴這個物件準備要執行什麼SQL指令
            if (Session["classify"] == null)
            {
                Command = new SqlCommand(sql_statement_no_classify, connection);
            }
            else {

                Command = new SqlCommand(sql_statement1_classify, connection);
            }

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
                    fetchProductData rowData = new fetchProductData();
                    //DataReader讀出欄位內資料的方式，通常也可寫Reader[0]、[1]...[N]代表第一個欄位到N個欄位。
                    string str_pic_pathname = Reader["pic_pathname"].ToString();
                    rowData._imagePathname = str_pic_pathname;
                    _aFetchedProduct.Add(rowData);
                    

                }// while (Reader.Read())

            }// if (Reader.HasRows) login name match
            else
            {
                Response.Write("<script>alert('目前還沒有此分類商品哦~請再看看別的!');</script>");

            }// if (Reader.HasRows) login name mismatch
            //關閉與資料庫連接的通道
            connection.Close();
        }// private void fetchGoodPicsPathname()

        protected void GridViewRowDataBound(Object sender, GridViewRowEventArgs e)
        { 
            if(e.Row.RowType == DataControlRowType.DataRow)  // 
            {
                System.Web.UI.WebControls.Image imgFile = (System.Web.UI.WebControls.Image)e.Row.FindControl("img0");

                string str_pic_pathname = _aFetchedProduct[e.Row.DataItemIndex]._imagePathname;
                imgFile.ImageUrl = str_pic_pathname;

            }// if(e.Row.RowType == DataControlRowType.DataRow)

        }// void CustomersGridView_RowDataBound(Object sender, GridViewRowEventArgs e)

        private string getCtlText(string idName)
        {
            GridViewRow SelectedRow = _GoodsGridView.SelectedRow;
            Label lblName = (Label)SelectedRow.FindControl(idName);
            return lblName.Text;
        }// 
        private Image getCtlImage(string idName)
        {
            GridViewRow SelectedRow = _GoodsGridView.SelectedRow;
            Image img = (Image)SelectedRow.FindControl(idName);
            return img;
        }// 
        protected void GoodsGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            int.Parse(getCtlText("Action_product_ID")) ;
            int nSellerID = int.Parse(getCtlText("seller_ID"));
            */
            // 1. to store info to Session
            // ImageUrl, product, desc, seller_ID
            // 2. Redirect to ProductDetailForm.aspx
            Image selectedImage = getCtlImage("img0");
            Session["ImageUrl"] = selectedImage.ImageUrl;

            string strProductName = getCtlText("product_name");
            Session["ProductName"] = strProductName;

            string strProductDesc = getCtlText("product_desc");
            Session["ProductDesc"] = strProductDesc;

            string strSellerID = getCtlText("seller_ID");
            Session["SellerID"] = strSellerID; 

            string strProductID = getCtlText("Action_product_ID");
            Session["ProductID"] = strProductID; 

            Response.Redirect("BitProductForm.aspx");
            //Response.Write("<script>alert('選擇 GridView 內個別商品');</script>");
        }// 

        protected void SQL_readActionProduct(string connString)
         {
            _cmd.Connection = _Conn;   //將SQL執行的命令語法程式CMD與CONN與SQL連接

            //設定連線IP位置、資料表，帳戶，密碼
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;
            _Conn.ConnectionString = s_data; //"Data Source=127.0.0.1;Initial Catalog=NorthwindChinese;Persist Security Info=True";
            //這一行可依連線的字串不同而去定義它該連線到哪個資料庫!!

            // bug1: SQL content without session classify
            string sql_statement_no_classify = $"SELECT pic_pathname,product,description,total_number,seller_ID,Action_product_ID from Action_product where status='onsale'";

            // bug1: SQL content with session classify
            string sql_statement1_classify = $"SELECT pic_pathname,product,description,total_number,seller_ID,Action_product_ID from Action_product where status='onsale' and classify ='" + Session["classify"] + "'";

            SqlCommand Command;
            // bug2: sqlText
            //new一個SqlCommand告訴這個物件準備要執行什麼SQL指令
            if (Session["classify"] == null)
            {
                _cmd.CommandText = sql_statement_no_classify;
            }
            else
            {
                _cmd.CommandText = sql_statement1_classify;
            }


            _da.SelectCommand = _cmd;            //da選擇資料來源，由cmd載入進來
            _da.Fill(_ds, "Action_product");            //da把資料填入ds裡面

        }// protected void SQL_readActionProduct()

        protected void _GoodOnShelfBtn_Click(object sender, EventArgs e)
        {
            // if user hasn't logged, redirect to memberLoginForm
            if (Session["member_ID"] == null)
            {
                Session["logged_to_page"] = "GoodListForm.aspx";
                Response.Redirect("memberLoginForm.aspx");
            }
            else
            {
                Response.Redirect("PutGoodOnShelfForm.aspx");
            }
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

    }// public partial class GoodListForm : System.Web.UI.Page

}// namespace BitSystem