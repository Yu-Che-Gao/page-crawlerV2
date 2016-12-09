using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace net_page_crawlerV2
{
    public partial class Form1 : Form
    {
        config sickConfig=null;

        public Form1()
        {
            InitializeComponent();
            sickConfig = new net_page_crawlerV2.config();
            sickConfig.dbConn("localhost", "root", "", "sick-detection");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string pagePosts = fbgraph.getPagePosts("57613404340");
                dynamic decode = JsonConvert.DeserializeObject(pagePosts);
                var fbId = decode.data[0].id;
                string message = decode.data[0].message.ToString().Replace("\n", "");
                var createTime = decode.data[0].created_time;
                
                sickConfig.getCmd().CommandText= "INSERT INTO raw(`fbId`, `message`, `create_time`) VALUES ('" + fbId + "', '" + message + "', '" + createTime + "')";
                sickConfig.getCmd().ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
