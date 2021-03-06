using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebGrease.Css;

namespace BitSystem
{
    public partial class PutGoodOnShelfForm : System.Web.UI.Page
    {
        SqlDataAdapter da = new SqlDataAdapter();       //SQL 資料庫的連接與執行命令
        SqlConnection conn = new SqlConnection();
        DataSet ds_getbid = new DataSet();
        SqlCommand cmd = new SqlCommand();

        //設定資料庫資訊
        string connString = "Sale_net_Jun22_2021ConnectionString";


        private string[] aClassfyTitle = new string[]
        {
            "衣服/飾品", "書籍/文創", "居家/生活",
            "包包/精品","男女鞋款", "汽機車/零件百貨",
            "娛樂/收藏", "寵物/用品", "其他類別"
        };

        DateTime  _closedDateTime = new DateTime();

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
                Session["logged_to_page"] = "PutGoodOnShelfForm.aspx";
                Response.Redirect("memberLoginForm.aspx");
            }
            


            // if the page loaded first time
            if (IsPostBack == false){
                // 1. preset _Classfy
                for (int index = 0; index < aClassfyTitle.Length; index++)
                {
                    string item = aClassfyTitle[index];
                    _Classfy.Items.Add(item);
                }

                // 2. preset _ClosedHr_list
                for (int index = 0; index < 24; index++)
                    _ClosedHr_list.Items.Add(index.ToString());

                // 3. preset _ClosedMMin_list
                for (int index = 0; index < 60; index++)
                    _ClosedMin_list.Items.Add(index.ToString());

                //給下拉框預設顯示當前小時、分鐘
                _ClosedHr_list.Text = (DateTime.Now.Hour).ToString();
                _ClosedMin_list.Text = (DateTime.Now.Minute).ToString();

                if (Session["Preset_PutGoodOnShelf"] != null)
                {
                    presetGUI();
                }

                // 載入已結標熱門
                SQL_readActionProduct_getbid(connString);
                getbid_view.DataSource = ds_getbid; //將DataSet的資料載入到datalist內
                getbid_view.DataBind();

            }// first load
            else{ // postback

            }// postback

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


        private bool IfStringsOverLength(string uiText,int nLen)
        {
            if(Encoding.Default.GetByteCount(uiText) >= nLen)
                return true;
            else
                return false;
        }

        private void setPresetGUI()
        {
            // 1. set preset
            Session["Preset_PutGoodOnShelf"] = "Yes";

            DateTime selectedDate = _CalendarClosedDate.SelectedDate;
            _closedDateTime = _CalendarClosedDate.SelectedDate;

            int nYear = selectedDate.Year;
            Session["Preset_CalendarClosedDate_Year"] = nYear.ToString();

            int nMonth = selectedDate.Month;
            Session["Preset_CalendarClosedDate_Month"] = nMonth.ToString();

            int nDay = selectedDate.Day;
            Session["Preset_CalendarClosedDate_Day"] = nDay.ToString();

            Session["Preset_ClosedHr_list"] = _ClosedHr_list.Text;
            Session["Preset_ClosedMin_list"] = _ClosedMin_list.Text;

            Session["Preset_PutGoodOnShelf"] = "Yes";
            Session["Preset_Classfy"] = _Classfy.Text;
            Session["Preset_ProductName"] = _ProductName.Text;
            Session["Preset_GoodDesc"] = _GoodDesc.Text;
            Session["Preset_OfficialPrice"] = _OfficialPrice.Text;
            Session["Preset_TotalLots"] = _TotalLots.Text;

            Session["Preset_ImgGood_Pathname"] = _ImgGood.ImageUrl;
        }// private void setPresetGUI()

        private void presetGUI()
        {
            string strYear =(string) Session["Preset_CalendarClosedDate_Year"];
            int nYear = (int.Parse(strYear));

            string strMonth =(string) Session["Preset_CalendarClosedDate_Month"];
            int nMonth = (int.Parse(strMonth));

            string strDay =(string) Session["Preset_CalendarClosedDate_Day"];
            int nDay = (int.Parse(strDay));

            DateTime closedDateTime = new DateTime(nYear, nMonth, nDay);
            

            string strHour =(string) Session["Preset_ClosedHr_list"];
            int nHour = (int.Parse(strHour));

            string strMin =(string) Session["Preset_ClosedMin_list"];
            int nMin = (int.Parse(strMin));
            TimeSpan timespan = new TimeSpan(nHour, nMin, 0);
            closedDateTime += timespan;

            // keep session info of closed dateTime to class member
             _closedDateTime = closedDateTime;

            _ClosedHr_list.Text = (string) Session["Preset_ClosedHr_list"];
            _ClosedMin_list.Text = (string)Session["Preset_ClosedMin_list"];

            _Classfy.Text       = (string) Session["Preset_Classfy"];
            _ProductName.Text   = (string) Session["Preset_ProductName"];
            _GoodDesc.Text      = (string) Session["Preset_GoodDesc"] ;
            _OfficialPrice.Text = (string) Session["Preset_OfficialPrice"];
            _TotalLots.Text     = (string) Session["Preset_TotalLots"];


            _ClosedHr_list.Text  = (string)Session["Preset_ClosedHr_list"];
            _ClosedMin_list.Text = (string) Session["Preset_ClosedMin_list"];
            _ImgGood.ImageUrl = (string) Session["Preset_ImgGood_Pathname"];

        }// private void presetGUI()

        protected void _SetGoodPicBtn_Click(object sender, EventArgs e)
        {
            if (_FileUpload.FileName == "")
            {
                Response.Write($"<script>alert('請選取商品圖片');</script>");
                return;
            }
            else
            {
                // to load picture on server directory: ~/GoodPics/
                string path = Server.MapPath("~/GoodPics/");
                _FileUpload.SaveAs(path + _FileUpload.FileName);
                _ImgGood.ImageUrl = "~/GoodPics/" + _FileUpload.FileName;
            }

        }// protected void _GoodOnShelfBtn_Click(object sender, EventArgs e)

        protected void SQLDB_writeAutionProduct(string connString, DateTime closedDateTime)
        {
         

            int seller_ID = (int)(Session["member_ID"]);

            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;

            //new一個SqlConnection物件，是與資料庫連結的通道(其名為Connection)，以s_data內的連接字串連接所對應的資料庫。
            SqlConnection connection = new SqlConnection(s_data);

            //與資料庫連接的通道開啟
            connection.Open();
            // _File
            SqlCommand sql_insert_cmd = new SqlCommand("insert into Action_product" +
                "(classify,product,total_number,low_price,status,description,official_price,seller_ID,closedDateTime,pic_pathname,high_price) " +
                "values(@classify,@product,@total_number,@low_price,@status,@description,@official_price,@seller_ID,@closedDateTime,@pic_pathname,@high_price);", connection); //SQL語句

            // run-time error type mismatch
            sql_insert_cmd.Parameters.Add("@closedDateTime", SqlDbType.DateTime);
            sql_insert_cmd.Parameters["@closedDateTime"].Value = closedDateTime;

            //設定classify 中文轉成英文 提供azure 雲端語法辨認
            string classify_temp = "";

            if (_Classfy.Text == "衣服/飾品"){
                classify_temp = "cloth";
            }
            else if(_Classfy.Text == "書籍/文創")
            {
                classify_temp = "book";
            }
            else if (_Classfy.Text == "居家/生活")
            {
                classify_temp = "life";
            }
            else if (_Classfy.Text == "包包/精品")
            {
                classify_temp = "bag";
            }
            else if (_Classfy.Text == "男女鞋款")
            {
                classify_temp = "shoes";
            }
            else if (_Classfy.Text == "汽機車/零件百貨")
            {
                classify_temp = "car";
            }
            else if (_Classfy.Text == "娛樂/收藏")
            {
                classify_temp = "habbit";
            }
            else if (_Classfy.Text == "寵物/用品")
            {
                classify_temp = "pet";
            }
            else if (_Classfy.Text == "其他類別")
            {
                classify_temp = "other";
            }

            sql_insert_cmd.Parameters.Add("@classify", SqlDbType.NVarChar);
            sql_insert_cmd.Parameters["@classify"].Value = classify_temp;

            sql_insert_cmd.Parameters.Add("@product", SqlDbType.NVarChar);
            sql_insert_cmd.Parameters["@product"].Value = _ProductName.Text;
            
            sql_insert_cmd.Parameters.Add("@total_number", SqlDbType.NVarChar);
            sql_insert_cmd.Parameters["@total_number"].Value = _TotalLots.Text;

            sql_insert_cmd.Parameters.Add("@low_price", SqlDbType.NVarChar);
            sql_insert_cmd.Parameters["@low_price"].Value = "0";

            sql_insert_cmd.Parameters.Add("@high_price", SqlDbType.NVarChar);
            sql_insert_cmd.Parameters["@high_price"].Value = "0";

            sql_insert_cmd.Parameters.Add("@status", SqlDbType.NVarChar);
            sql_insert_cmd.Parameters["@status"].Value = "onsale";

            sql_insert_cmd.Parameters.Add("@official_price", SqlDbType.Int);
            sql_insert_cmd.Parameters["@official_price"].Value = int.Parse(_OfficialPrice.Text);

            sql_insert_cmd.Parameters.Add("@seller_ID", SqlDbType.Int);
            sql_insert_cmd.Parameters["@seller_ID"].Value = seller_ID;

            sql_insert_cmd.Parameters.Add("@pic_pathname", SqlDbType.NVarChar);
            sql_insert_cmd.Parameters["@pic_pathname"].Value = _ImgGood.ImageUrl;

            sql_insert_cmd.Parameters.Add("@description", SqlDbType.NVarChar);
            sql_insert_cmd.Parameters["@description"].Value = _GoodDesc.Text;
            
            sql_insert_cmd.ExecuteNonQuery();

            //關閉與資料庫連接的通道
            connection.Close();

            Response.Write($"<script>alert('寫入拍賣商品資料庫成功');</script>");


        }// protected void SQLDB_write()

        protected void _putOnShelfBtn_Click(object sender, EventArgs e)
        {
            int nOffPrice = 0, nTotalLots = 0;

            if (Session["member_ID"] == null)
            {

                setPresetGUI();
                // to set pre logged page
                Session["logged_to_page"] = "PutGoodOnShelfForm.aspx";
                
                // to the member logging page
                Response.Redirect("memberLoginForm.aspx");
            }
            else
            {
                try
                {
                    nOffPrice = int.Parse(_OfficialPrice.Text);
                }
                catch (System.FormatException)
                {
                    Response.Write($"<script>alert('官方售價 請填入 數字');</script>");
                    return;
                }

                try
                {
                    nTotalLots = int.Parse(_TotalLots.Text);
                }
                catch (System.FormatException)
                {
                    Response.Write($"<script>alert('商品數量 請填入 數字');</script>");
                    return;
                }
                // to check every field are filled
                if (_ProductName.Text == "")
                {
                    Response.Write($"<script>alert('請寫入商品名稱');</script>");
                    return;
                }
                else if(IfStringsOverLength(_ProductName.Text, 50) == true)
                {
                    Response.Write($"<script>alert('商品名稱 字串過長，請刪減');</script>");
                    return;
                }
                else if (_Classfy.Text == "")
                {
                    Response.Write($"<script>alert('請選擇商品分類');</script>");
                    return;
                }
                else if (_GoodDesc.Text == "")
                {
                    Response.Write($"<script>alert('請寫入商品描述文字');</script>");
                    return;
                }
                else if (IfStringsOverLength(_GoodDesc.Text, 500) == true)
                {
                    Response.Write($"<script>alert('商品描述 過長，請刪減文字');</script>");
                    return;
                }
                else if (_OfficialPrice.Text == "")
                {
                    Response.Write($"<script>alert('請填官方售價');</script>");
                }
                else if (int.Parse(_OfficialPrice.Text) <= 0)
                {
                    Response.Write($"<script>alert('官方售價必須大於 0');</script>");
                }
                else if (_TotalLots.Text == "")
                {
                    Response.Write($"<script>alert('請填商品數量');</script>");
                }
                else if (int.Parse(_TotalLots.Text) <= 0)
                {
                    Response.Write($"<script>alert('商品數量最少為 1');</script>");
                    return;
                }
                else if (_CalendarClosedDate.SelectedDate == DateTime.MinValue.Date)
                {
                    Response.Write($"<script>alert('請先設定結案日期與時間');</script>");
                }
                else if (_ImgGood.ImageUrl == "")
                {
                    Response.Write($"<script>alert('請上傳商品圖片');</script>");
                    return;
                }
                else
                {
                    // all input items are ready
                    DateTime closedDateTime = _CalendarClosedDate.SelectedDate;
                    TimeSpan setTimespan = new TimeSpan(int.Parse(_ClosedHr_list.Text), int.Parse(_ClosedMin_list.Text), 0);
                    closedDateTime += setTimespan;
                    /**/

                    SQLDB_writeAutionProduct(connString, closedDateTime);
                    Response.Redirect("GoodListForm.aspx");
                }

            }// member logged

        }// protected void _putOnShelfBtn_Click(object sender, EventArgs e)


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
    }//public partial class PutGoodOnShelfForm : System.Web.UI.Page
}