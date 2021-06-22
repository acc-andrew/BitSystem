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
            //設定會員登入與否顯現標示不同

            Session["Login"] = "logged";

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

            for (int index = 0; index < classtitle.Length; index++)
            {
                string item = classtitle[index];
                classify.Items.Add(item);
            }

            for (int index = 1; index < 100; index++)
            {
                total_number.Items.Add(index.ToString());
            }


            Session["user"] = "Austyn";
            if (Convert.ToString(Session["Login"]) != "logged")
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
            Session["classify"] = "衣服/飾品";
            Response.Redirect("list_view.aspx");
        }

        protected void book_Click(object sender, EventArgs e)
        {
            Session["classify"] = "書籍/文創";
            Response.Redirect("list_view.aspx");
        }

        protected void life_Click(object sender, EventArgs e)
        {
            Session["classify"] = "居家/生活";
            Response.Redirect("list_view.aspx");
        }

        protected void bag_Click(object sender, EventArgs e)
        {
            Session["classify"] = "包包/精品";
            Response.Redirect("list_view.aspx");
        }

        protected void shoes_Click(object sender, EventArgs e)
        {
            Session["classify"] = "男女鞋款";
            Response.Redirect("list_view.aspx");
        }

        protected void car_Click(object sender, EventArgs e)
        {
            Session["classify"] = "汽機車/零件百貨";
            Response.Redirect("list_view.aspx");
        }

        protected void entertainment_Click(object sender, EventArgs e)
        {
            Session["classify"] = "娛樂/收藏";
            Response.Redirect("list_view.aspx");
        }

        protected void pet_Click(object sender, EventArgs e)
        {
            Session["classify"] = "寵物/用品";
            Response.Redirect("list_view.aspx");
        }

        protected void others_Click(object sender, EventArgs e)
        {
            Session["classify"] = "其他類別";
            Response.Redirect("list_view.aspx");
        }

        //取消classify
        protected void sale_list_Click(object sender, EventArgs e)
        {
            Session["classify"] = null;
            Response.Redirect("GoodListForm.aspx");
        }
    }
}