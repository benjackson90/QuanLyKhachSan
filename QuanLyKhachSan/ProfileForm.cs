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
    public partial class ProfileForm : Form
    {
        public ProfileForm()
        {
            InitializeComponent();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string fullname = txtFullName.Text;
            string username = txtUsername.Text;
            string pass = txtNewPass.Text;


            Boolean check = DataAccess.UpdateAccount(fullname, username, pass);
            if (check)
            {
                DialogResult dr = MessageBox.Show("Update Account is success!!!");
                this.Close();
                
            }
            else
            {
                DialogResult dr = MessageBox.Show("Update Account is fail!!!");
            }
        }
    }
}
