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
        double totalPrice = 0;
        Boolean roomIsSelected = false;
        int Room_ID = -1;
        public ManageHotel()
        {
            InitializeComponent();
            loadData(0);
        }
        private void loadData (int index)
        {
            flpRoom.Controls.Clear();
            txtPrice.Text = totalPrice.ToString();
            List<Room> listRoom = new List<Room>();
            switch (index)
            {
                case 1:
                    listRoom = Room.getAllRoomsOccupied();
                    break;
                case 2:
                    listRoom = Room.getAllRoomsEmpty();
                    break;
                default:
                    listRoom = Room.getAllRooms();
                    break;
            }
            
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
            if (formLogin.type == 1)
            {
                AdminForm af = new AdminForm();
                af.ShowDialog();
            } else
            {
                MessageBox.Show("Chỉ có ADMIN có quyền truy cập vào đây!");
            }
        }
        void showBillInfo(int roomId)
        {
            double servicePrice = 0;
            lvBillInfo.Items.Clear();
            List<BillInfo> list = BillInfo.getBillInfo(roomId);
            foreach (BillInfo item in list)
            {
                ListViewItem lvBillInfor = new ListViewItem(item.ServiceName);
                lvBillInfor.SubItems.Add(item.ServicePrice.ToString());
                lvBillInfor.SubItems.Add(item.amount.ToString());
                lvBillInfor.SubItems.Add(item.Total.ToString());
                lvBillInfo.Items.Add(lvBillInfor);
                totalPrice += item.Total;
                servicePrice += item.Total;
            }
            ServicePrice.Text = servicePrice.ToString();
            txtPrice.Text = totalPrice.ToString();
            int index = cbSearch.SelectedIndex;
            //loadData(index);
        }
        void btn_Click (object sender, EventArgs e)
        {
            roomIsSelected = true;
            int roomId = ((sender as Button).Tag as Room).RoomID;
            Room_ID = ((sender as Button).Tag as Room).RoomID;
            RoomPrice.Text = DataAccess.getRoomPrice(roomId).ToString();
            totalPrice = DataAccess.getRoomPrice(roomId);
            lvBillInfo.Tag = (sender as Button).Tag;
            roomName.Text = "Room: " + ((sender as Button).Tag as Room).RoomName;
            showBillInfo(roomId);  
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (roomIsSelected)
            {
                Room rum = lvBillInfo.Tag as Room;
                int billIsExisted = DataAccess.getCountBill(rum.RoomID);
                DataAccess.updateRoomStatus("Occupied", rum.RoomID);
                int idService = Convert.ToInt32(cbService.SelectedValue);
                int count = Convert.ToInt32(Amount.Value);
                // bill is not existed
                if (billIsExisted == 0)
                {
                    Console.WriteLine(0);
                    int x = DataAccess.insertBill(rum.RoomID);
                    if (x == 1)
                    {
                        Console.WriteLine(1);
                        int maxId = DataAccess.getMaxIdBill();
                        int ok = DataAccess.insertBillInfo(maxId, idService, count);
                        showBillInfo(rum.RoomID);
                        changeStatus(rum.RoomID);
                    }
                }
                else if (billIsExisted == 1)
                {
                    int idBill = DataAccess.getBillIdByRoomIdAndStatus(rum.RoomID);
                    int billInfoIsExisted = DataAccess.getBillInfoColumn("COUNT(*)", idBill, idService);
                    if (billInfoIsExisted == 0)
                    {
                        Console.WriteLine(2);
                        int insertSuccess = DataAccess.insertBillInfo(idBill, idService, count);
                        if (insertSuccess == 1)
                        {
                            Console.WriteLine(3);
                            showBillInfo(rum.RoomID);
                        }
                    }
                    else if (billInfoIsExisted == 1)
                    {
                        int billInfoId = DataAccess.getBillInfoColumn("id", idBill, idService);
                        int oldAmount = DataAccess.getCurrentAmount(billInfoId);
                        int updateSuccess = DataAccess.updateBillInfo(billInfoId, oldAmount + count);
                        Console.WriteLine(4);
                        if (updateSuccess == 1)
                        {
                            Console.WriteLine(5);
                            showBillInfo(rum.RoomID);
                        }
                    }
                }
            } else
            {
                MessageBox.Show("You have to select one room first.");
            }
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            if (roomIsSelected)
            {
                if (DataAccess.getStatus(Room_ID).Equals("Empty"))
                {
                    MessageBox.Show("Cannot pay for empty room.");
                }
                else if (DataAccess.getStatus(Room_ID).Equals("Occupied"))
                {
                    if (MessageBox.Show("Tổng hóa đơn: "+totalPrice+"$", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                    {
                        // get idRoom
                        int idRoom = Room_ID;
                        // get idBill by idRoom
                        int idBill = DataAccess.getBillIdByRoomIdAndStatus(idRoom);
                        // update status bill info
                        DataAccess.updateStatusBillInfo(idBill);
                        //update status bill
                        DataAccess.updateStatusBill(idBill, totalPrice);
                        //update status room
                        DataAccess.updateRoomStatus("Empty", idRoom);
                        int index = cbSearch.SelectedIndex;
                        loadData(index);
                        showBillInfo(Room_ID);
                    }
                }
            }
            else
            {
                MessageBox.Show("You have to select one room first.");
            }
        }

        private void btnBook_Click(object sender, EventArgs e)
        {
            if (roomIsSelected)
            {
                if (DataAccess.getStatus(Room_ID).Equals("Empty"))
                {
                    if (MessageBox.Show("Are you sure?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                    {
                        DataAccess.insertBill(Room_ID);
                        changeStatus(Room_ID);
                    }
                }
            }
            else
            {
                MessageBox.Show("You have to select one room first.");
            }
                
        }
        private void changeStatus (int roomId)
        {
            DataAccess.updateRoomStatus("Occupied", roomId);
            int index = cbSearch.SelectedIndex;
            loadData(index);
        }

        private void ManageHotel_Load(object sender, EventArgs e)
        {
            cbSearch.Items.Add("-------------------All-------------------");
            cbSearch.Items.Add("-------------------Occupied-------------------");
            cbSearch.Items.Add("-------------------Empty-------------------");
            lbUsername.Text = formLogin.txtUser;
            cbSearch.SelectedIndex = 0;
        }

        private void cbSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cbSearch.SelectedIndex;
            if (cbSearch.SelectedIndex==1)
            {
                loadData(1);
            } else if (cbSearch.SelectedIndex == 2)
            {
                loadData(2);
            } else
            {
                loadData(0);
            }
            
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Mày có thực sự muốn đăng xuất?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                this.Hide();
                formLogin lg = new formLogin();
                lg.Show();
            }
        }
    }
}
