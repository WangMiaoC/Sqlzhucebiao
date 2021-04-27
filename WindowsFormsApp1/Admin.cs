using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(add.Text == "" || sa.Text == "" || pwd.Text == "")
            {
                MessageBox.Show(@"服务器\SA\密码不可为空");
            }
            else
            {
                try
                {
                    string constr = "server=" + add.Text + ";database=master;uid=" + sa.Text + ";pwd=" + pwd.Text;
                    SqlConnection conn = new SqlConnection(constr);
                    conn.Open();
                    RegistryKey regkeySetKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft", true).CreateSubKey("Sqlcon"); //创建了一个Sqlcon子键并有三个子健
                    regkeySetKey.SetValue("add", add.Text);
                    regkeySetKey.SetValue("sa", sa.Text);
                    regkeySetKey.SetValue("pwd", pwd.Text);
                    conn.Close();
                    new Main().Show(); //验证成功显示主窗体
                    Hide();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }
        private void Admin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
