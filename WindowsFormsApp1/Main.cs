using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Main : Form
    {
        Class_DB.Class_DB sql = new Class_DB.Class_DB();

        public Main()
        {
            InitializeComponent();
        }
        public static SqlConnection My_con;
        private void button1_Click(object sender, EventArgs e)
        {
            //执行语句
            var v = sql.GetDataSet("select * from [UFTData312682_000001]..gl_doc");
            dataGridView1.DataSource = v.Tables[0];
            dataGridView1.Columns["ts"].Visible = false;
            dataGridView1.ReadOnly = true;
        }
        /// <summary>
        /// 放到activated比load优先级更高
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_Activated(object sender, EventArgs e)
        {
            try
            {
                ///读取注册表找参数
                string add = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Sqlcon", true).GetValue("add").ToString();
                string sa = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Sqlcon", true).GetValue("sa").ToString();
                string pwd = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Sqlcon", true).GetValue("pwd").ToString();
                ///连接失败后走catch进行配置连接信息
                string M_str_sqlcon = @"Data Source =" + add + "; Database=master;User id = " + sa + "; PWD=" + pwd;
                My_con = new SqlConnection(M_str_sqlcon); //用SqlConnection对象与指定的数据库相连接
                My_con.Open(); //打开数据库连接
            }
            catch
            {
                new Admin().Show();///弹出配置界面
                Hide();///隐藏本界面
            }
        }
    }
}
