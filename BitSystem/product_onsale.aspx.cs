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
    public partial class product_onsale : System.Web.UI.Page
    {
        private string[] classtitle = new string[]
        {
            "美食/伴手禮", "書籍/文創商品", "居家/生活",
            "背包/精品","男女/鞋款", "汽機車零件/百貨",
            "娛樂/收藏", "寵物", "其他類別"
        };

        protected void Page_Load(object sender, EventArgs e)
        {
            for (int index = 0; index < classtitle.Length; index++)
            {
                string item = classtitle[index];
                classify.Items.Add(item);
            }

            for (int index = 1; index < 100; index++)
            {
                total_number.Items.Add(index.ToString());
            }


            Session["logined"] = "logined";
            Session["user"] = "Austyn";
            if (Convert.ToString(Session["logined"]) != "logined")
            {
                //Response.Redirect("memberLoginForm.aspx");
                Server.Transfer("memberLoginForm.aspx");
            }

            //創一個變數存放從config內的資訊，其實也可不用創立這變數，直接放進SqlConnection內即可。
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["sale_netConnectionString2"].ConnectionString;
            //new一個SqlConnection物件，是與資料庫連結的通道(其名為Connection)，以s_data內的連接字串連接所對應的資料庫。
            SqlConnection connection = new SqlConnection(s_data);
            string sqlcode = $"SELECT * FROM [Member] where user_name =" + "'Austyn'";
            //new一個SqlCommand告訴這個物件準備要執行什麼SQL指令
            SqlCommand Command = new SqlCommand(sqlcode, connection);
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
                    //memberID.Text = (Int32)((String)Reader["member_ID"]);
                    user_name.Text = (String)Reader["user_name"];
                }
            }
            else
            {
               
            }
            //關閉與資料庫連接的通道
            connection.Close();


        }

        protected void onsale_Click(object sender, EventArgs e)
        {
            if (Session["logined"] == null)
            {

                Server.Transfer("memberLoginForm.aspx");
            }
            else
            {
                if (product.Text == "")
                {
                    Response.Write($"<script>alert('請寫入商品名稱');</script>");
                }
                else if (classify.Text == "")
                {
                    Response.Write($"<script>alert('請寫入分類');</script>");
                }
                else if (total_number.Text == "")
                {
                    Response.Write($"<script>alert('請寫入商品數量');</script>");
                }
                else if (product_status.Text == "")
                {
                    Response.Write($"<script>alert('請寫入商品狀態');</script>");
                }
                else if (public_price.Text == "")
                {
                    Response.Write($"<script>alert('請寫入商品市價');</script>");
                }
                else if (description.Text == "")
                {
                    Response.Write($"<script>alert('請寫入商品描述');</script>");
                }
                else if (upload_img.ImageUrl == "")
                {
                    Response.Write($"<script>alert('請提供商品照片');</script>");
                }

                else
                {
                    string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["sale_netConnectionString1"].ConnectionString;

                    SqlConnection connection = new SqlConnection(s_data);

                    //string sqlcode = $"insert into [account](account,password,nickname) values('"+user.Text+"','"+passwd.Text+"','"+nick.Text+"')";
                    string sqlcode = $"insert into [Action_product](product,classify,total_number,product_status,public_price,description,status) values(@product,@classify,@total_number,@product_status,@public_price,@description,@status)";

                    //與資料庫連接的通道開啟
                    connection.Open();
                    //new一個SqlCommand告訴這個物件準備要執行什麼SQL指令
                    SqlCommand Command = new SqlCommand(sqlcode, connection);

                    Command.Parameters.Add("@product", SqlDbType.NVarChar);
                    Command.Parameters["@product"].Value = product.Text;

                    Command.Parameters.Add("@classify", SqlDbType.NVarChar);
                    Command.Parameters["@classify"].Value = classify.Text;

                    Command.Parameters.Add("@total_number", SqlDbType.NVarChar);
                    Command.Parameters["@total_number"].Value = total_number.Text;

                    Command.Parameters.Add("@status", SqlDbType.Text);
                    Command.Parameters["@status"].Value = "競拍中";

                    Command.Parameters.Add("@product_status", SqlDbType.NVarChar);
                    Command.Parameters["@product_status"].Value = product_status.Text;

                    Command.Parameters.Add("@description", SqlDbType.NVarChar);
                    Command.Parameters["@description"].Value = description.Text;

                    Command.Parameters.Add("@public_price", SqlDbType.NVarChar);
                    Command.Parameters["@public_price"].Value = public_price.Text;
                    //new一個DataReader接取Execute所回傳的資料。
                    Command.ExecuteNonQuery();
                    connection.Close();
                    Response.Write($"<script>alert('上架成功');</script>");

                }
            }
        }

        protected void show_upload_img_Click(object sender, EventArgs e)
        {
            if (FileUpload2.FileName == "")
            {
                Response.Write($"<script>alert('請上傳圖片');</script>");
            }
            else 
            {
                string path = Server.MapPath("~/GoodPics/");
                FileUpload2.SaveAs(path + FileUpload2.FileName);
                upload_img.ImageUrl = ("~/GoodPics/" + FileUpload2.FileName);
            }
        }
    }
}