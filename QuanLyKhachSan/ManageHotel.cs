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
        void showBillInfo(int roomId)
        {
            lvBillInfo.Items.Clear();
            double totalPrice = 0;
            List<BillInfo> list = BillInfo.getBillInfo(roomId);
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
            int roomId = ((sender as Button).Tag as Room).RoomID;
            lvBillInfo.Tag = (sender as Button).Tag;
            showBillInfo(roomId);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Room rum = lvBillInfo.Tag as Room;
            //Console.WriteLine(rum.RoomID);
            //Console.WriteLine(rum.RoomName);
            int billIsExisted = DataAccess.getCountBill(rum.RoomID);
            int idService = Convert.ToInt32(cbService.SelectedValue);
            int count = Convert.ToInt32(Amount.Value);
            if (billIsExisted == 0)
            {
                Console.WriteLine(0);
                int x = DataAccess.insertBill(rum.RoomID);
                if (x == 1)
                {
                    Console.WriteLine(1);
                    int maxId = DataAccess.getMaxIdBill();
                    //Console.WriteLine(maxId);
                    //Console.WriteLine(rum.RoomID);
                    //Console.WriteLine(idService);
                    //Console.WriteLine(count);
                    int ok = DataAccess.insertBillInfo(maxId, idService, count);
                    showBillInfo(rum.RoomID);
                }
            }
            else if (billIsExisted == 1)
            {
                int idBill = DataAccess.getBillIdByRoomId(rum.RoomID);
                int billInfoIsExisted = DataAccess.getBillInfoColumn("COUNT(*)", idBill, idService);
                if ( billInfoIsExisted == 0 )
                {
                    Console.WriteLine(2);
                    int insertSuccess = DataAccess.insertBillInfo(idBill, idService, count);
                    if (insertSuccess == 1)
                    {
                        Console.WriteLine(3);
                        showBillInfo(rum.RoomID);
                    }
                } else if (billInfoIsExisted == 1)
                {
                    int billInfoId = DataAccess.getBillInfoColumn("id", idBill, idService);
                    int oldAmount = DataAccess.getCurrentAmount(billInfoId);
                    int updateSuccess = DataAccess.updateBillInfo(billInfoId, oldAmount+count);
                    Console.WriteLine(4);
                    if (updateSuccess == 1)
                    {
                        Console.WriteLine(5);
                        showBillInfo(rum.RoomID);
                    }
                }
            }
        }
    }
}
