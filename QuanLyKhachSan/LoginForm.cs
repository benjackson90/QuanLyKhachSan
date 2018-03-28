using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QuanLyKhachSan
{
    public partial class formLogin : Form
    {
        public formLogin()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát chương trình không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        public static string txtUser = "";
        public static int type = 0;
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPass.Text;
            if (checkUser(username, password))
            {
                txtUser = username;
                type = Convert.ToInt32 ( DataAccess.checkUsername(username, password).Rows[0][0] );
                ManageHotel mh = new ManageHotel();
                this.Hide();
                mh.ShowDialog();
            } else
            {
                MessageBox.Show("Username or password are incorrect.");
            }       
        }
        private Boolean checkUser(string username, string password)
        {
            if (DataAccess.checkUsername(username, password).Rows.Count == 0)
                return false;
            return true;
        }
    }
}
