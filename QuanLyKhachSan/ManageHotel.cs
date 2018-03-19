using QuanLyKhachSan.Provider;
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
    public partial class ManageHotel : Form
    {
        public ManageHotel()
        {
            InitializeComponent();
            loadData();
        }
        private void loadData ()
        {

            List<Room> listRoom = Room.getAllRooms();
            foreach (Room item in listRoom)
            {
                Button btn = new Button() { Width = 142, Height = 88 };
                btn.Text = item.RoomName + Environment.NewLine + item.Status + Environment.NewLine
                            + item.RoomType + Environment.NewLine + item.BedType;
                btn.Tag = item;
                // add event
                btn.Click += btn_Click;
                switch (item.Status)
                {
                    case "Empty":
                        btn.BackColor = Color.Gray;
                        btn.ForeColor = Color.White;
                        break;
                    default:
                        btn.BackColor = Color.LightCoral;
                        break;
                }
                flpRoom.Controls.Add(btn);
            }

            // list service
            cbService.DisplayMember = "ServiceName";
            cbService.ValueMember = "ServiceID";
            List<Service> listEmp = Service.getAllService();
            cbService.DataSource = listEmp;
            cbService.SelectedIndex = 0;
        }

        private void thôngTinChiTiếtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProfileForm pf = new ProfileForm();
            pf.ShowDialog();
        }

        private void adminToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AdminForm af = new AdminForm();
            af.ShowDialog();
        }
        void showBillInfo(int tableId)
        {
            lvBillInfo.Items.Clear();
            double totalPrice = 0;
            List<BillInfo> list = BillInfo.getBillInfo(tableId);
            foreach (BillInfo item in list)
            {
                ListViewItem lvBillInfor = new ListViewItem(item.ServiceName);
                lvBillInfor.SubItems.Add(item.ServicePrice.ToString());
                lvBillInfor.SubItems.Add(item.amount.ToString());
                lvBillInfor.SubItems.Add(item.Total.ToString());
                lvBillInfo.Items.Add(lvBillInfor);
                totalPrice += item.Total;
            }
            txtPrice.Text = totalPrice.ToString();
        }
        void btn_Click (object sender, EventArgs e)
        {
            int tableId = ((sender as Button).Tag as Room).RoomID;
            showBillInfo(tableId);
        }
    }
}
