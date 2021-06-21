using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace BitSystem
{
    public partial class Home : System.Web.UI.Page
    {
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

       
        protected void more_hot_Click(object sender, EventArgs e)
        {
                Response.Redirect("GoodListForm.aspx");
        }
    }
}