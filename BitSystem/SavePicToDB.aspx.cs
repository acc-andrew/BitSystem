using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BitSystem
{
    public partial class SavePicToDB : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void HTML_msgbox(string msg)
        {
            string msgBody = $"<script>alert('{msg}');</script>";
            Response.Write(msgBody);
        }
        protected void SQLDB_write(string connString, int nGoodID)
        {
            string s_data = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[connString].ConnectionString;

            //new一個SqlConnection物件，是與資料庫連結的通道(其名為Connection)，以s_data內的連接字串連接所對應的資料庫。
            SqlConnection connection = new SqlConnection(s_data);

            // bug1: SQL content
            string sql_newPicID_cmd = $"insert into [GoodPicTable](GoodID) values(1)";

            // bug2: sqlText
            //new一個SqlCommand告訴這個物件準備要執行什麼SQL指令
            SqlCommand Command = new SqlCommand(sql_newPicID_cmd, connection);

            //與資料庫連接的通道開啟
            connection.Open();
            Command.ExecuteNonQuery();

            // _File
            SqlCommand sql_setPixel_cmd = new SqlCommand("insert into [GoodPicTable](GoodID,Pixel) values(@GoodID,@Pixel);", connection); //SQL語句
            sql_setPixel_cmd.Parameters.Add("@GoodID", SqlDbType.Int);
            sql_setPixel_cmd.Parameters["@GoodID"].Value = nGoodID;

            sql_setPixel_cmd.Parameters.Add("@Pixel", SqlDbType.Image);
            sql_setPixel_cmd.Parameters["@Pixel"].Value = _FileUpload1.FileBytes;
            sql_setPixel_cmd.ExecuteNonQuery();

            //關閉與資料庫連接的通道
            connection.Close();

            Response.Write($"<script>alert('Write to GoodPicTable fine.');</script>");
            // HTML_msgbox('Write to GoodPictable fine.');

        }// protected void SQLDB_write()


        protected void _SaveBtn_Click(object sender, EventArgs e)
        {
            SQLDB_write("Sale_net_Jun22_2021ConnectionString4", 2);//呼叫（自己寫的一個方法）
        }// protected void _SaveBtn_Click(object sender, EventArgs e)

    }
}