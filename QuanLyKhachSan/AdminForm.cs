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
    public partial class AdminForm : Form
    {
        private AddRoomForm add;
        private DetailForm detail;
        public static Boolean isSearch;
        public AdminForm()
        {
            InitializeComponent();
            Load += new EventHandler(AdminForm_Load);
            
        }

        public void GetServicesToDgv()
        {
            dgvListService.Columns.Clear();
            if (dgvListService.Rows.Count > 0)
                dgvListService.Rows.Clear();

            dgvListService.AutoGenerateColumns = false;
            dgvListService.Columns.Add("idcol", "ID");
            dgvListService.Columns["idcol"].DataPropertyName = "id";



            dgvListService.Columns.Add("namecol", "Name");
            dgvListService.Columns["namecol"].DataPropertyName = "name";

            dgvListService.Columns.Add("pricecol", "Price");
            dgvListService.Columns["pricecol"].DataPropertyName = "price";
      
        
            List<Service> mylist = null;
         
                mylist = Service.getAllServices();
       


            dgvListService.DataSource = mylist;
        }

        public void GetRoomsToDgv()
        {
            dgvListRoom.Columns.Clear();
            if (dgvListRoom.Rows.Count > 0)
                dgvListRoom.Rows.Clear();

            dgvListRoom.AutoGenerateColumns = false;
            dgvListRoom.Columns.Add("idroomcol", "ID");
            dgvListRoom.Columns["idroomcol"].DataPropertyName = "id";



            dgvListRoom.Columns.Add("nameroomcol", "Name");
            dgvListRoom.Columns["nameroomcol"].DataPropertyName = "name";

            dgvListRoom.Columns.Add("statuscol", "Status");
            dgvListRoom.Columns["statuscol"].DataPropertyName = "status";

            dgvListRoom.Columns.Add("roomCategorycol", "RoomCategory");
            dgvListRoom.Columns["roomCategorycol"].DataPropertyName = "roomCategory";

            dgvListRoom.Columns.Add("bedTypecol", "BedType");
            dgvListRoom.Columns["bedTypecol"].DataPropertyName = "bedType";


            List<Room> mylist = null;

            mylist = Room.getAllRooms();



            dgvListRoom.DataSource = mylist;
        }

        public void GetAccountToDgv()
        {
            dgvListAccount.Columns.Clear();
            if (dgvListAccount.Rows.Count > 0)
                dgvListAccount.Rows.Clear();

            dgvListAccount.AutoGenerateColumns = false;
            dgvListAccount.Columns.Add("fullcol", "Full Name");
            dgvListAccount.Columns["fullcol"].DataPropertyName = "fullname";



            dgvListAccount.Columns.Add("usernamecol", "User Name");
            dgvListAccount.Columns["usernamecol"].DataPropertyName = "username";

            dgvListAccount.Columns.Add("salarycol", "Salary");
            dgvListAccount.Columns["salarycol"].DataPropertyName = "salary";

        


            List<Account> mylist = null;

            mylist = Account.getAllAccounts();



            dgvListAccount.DataSource = mylist;
        }

        public void GetBillToDgv(Boolean search)
        {
            dgvBill.Columns.Clear();
            if (dgvBill.Rows.Count > 0)
                dgvBill.Rows.Clear();

            dgvBill.AutoGenerateColumns = false;
            dgvBill.Columns.Add("idcol", "ID");
            dgvBill.Columns["idcol"].DataPropertyName = "id";



            dgvBill.Columns.Add("checkincol", "Check In");
            dgvBill.Columns["checkincol"].DataPropertyName = "checkIn";

            dgvBill.Columns.Add("checkoutcol", "Check out");
            dgvBill.Columns["checkoutcol"].DataPropertyName = "checkOut";

            dgvBill.Columns.Add("namecol", "Name Room");
            dgvBill.Columns["namecol"].DataPropertyName = "roomName";

            dgvBill.Columns.Add("pricecol", "Price");
            dgvBill.Columns["pricecol"].DataPropertyName = "price";

            DataGridViewButtonColumn  btnDetail = new DataGridViewButtonColumn();
            btnDetail.Name = "Detail";
            btnDetail.Text = "Detail";
            btnDetail.UseColumnTextForButtonValue = true;
            dgvBill.Columns.Add(btnDetail);


            List<Bill> mylist = null;

            if (!search)
            {
                mylist = Bill.getAllBills();
            }
            else
            {

            

                string fromDate = dtpFrom.Value.ToString("MM/dd/yyyy");
                string toDate = dtpTo.Value.ToString("MM/dd/yyyy");

                mylist = Bill.getBillBy(fromDate, toDate);
            }
          



            dgvBill.DataSource = mylist;
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {

            GetServicesToDgv();
            GetRoomsToDgv();
            GetAccountToDgv();
            GetBillToDgv(false);
            List<RoomCategory> listRoomCa = RoomCategory.getAllRoomCategory();
            cbRC.DisplayMember = "name";
            cbRC.ValueMember = "id";
            cbRC.DataSource = listRoomCa;
            cbRC.SelectedIndex = -1;

            List<BedType> listBedType = BedType.getAllBedType();
            cbBt.DisplayMember = "name";
            cbBt.ValueMember = "id";
            cbBt.DataSource = listBedType;
            cbBt.SelectedIndex = -1;
        }

        private void dgvListService_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    

        private void dgvListRoom_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvBill_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabControl_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabControl.TabPages["Dịch vụ"])
            {
                MessageBox.Show("tabPage1");
            }
        }

        private void dgvListService_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private void dgvListService_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
           

            if (e.RowIndex >= 0)
            {
                
                DataGridViewRow row = dgvListService.Rows[e.RowIndex];
                txtServiceID.Text = row.Cells["idcol"].Value.ToString();
                txtServiceName.Text = row.Cells["namecol"].Value.ToString();
                ServicePrice.Text = row.Cells["pricecol"].Value.ToString();
            }
        }

        private void dgvListRoom_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;


            if (e.RowIndex >= 0)
            {

                DataGridViewRow row = dgvListRoom.Rows[e.RowIndex];
                txtRoomID.Text = row.Cells["idroomcol"].Value.ToString();
                txtRoomName.Text = row.Cells["nameroomcol"].Value.ToString();
                txtStatus.Text = row.Cells["statuscol"].Value.ToString();
                cbRC.SelectedValue = row.Cells["roomCategorycol"].Value.ToString();
              
            }
        }

        private void btnServiceEdit_Click(object sender, EventArgs e)
        {
            string id = txtServiceID.Text;
            string name = txtServiceName.Text;
            string price = ServicePrice.Text.ToString();
            bool check = DataAccess.UpdateServices(name, price, id);
            if (check == true)
            {
                DialogResult dr = MessageBox.Show("Update Service is successful!!!");
                GetServicesToDgv();

            }
            else
            {
                DialogResult dr = MessageBox.Show("Update Service is fail!!!");
            }
        }

    

        private void btnServiceRemove_Click(object sender, EventArgs e)
        {
            DialogResult result;

            string message = "Do you want to delete this record? YES or NO ?";
            string caption = "Notify";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            result = MessageBox.Show(this, message, caption, buttons,
                             MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,
                             MessageBoxOptions.RightAlign);


            if (result == DialogResult.Yes)
            {
                bool check = DataAccess.DeleteService(txtServiceID.Text);
                if (check == true)
                {
                    DialogResult dr = MessageBox.Show("Delete Service is successful!!!");
                    GetServicesToDgv();
                    txtServiceID.Text = "";
                    txtServiceName.Text = "";
                    ServicePrice.Text = "";

                }
                else
                {
                    DialogResult dr = MessageBox.Show("Delete Service is fail!!!");
                }


            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtServiceID.Text = "";
            txtServiceName.Text = "";
            ServicePrice.Text = "";
        }

        private void btnServiceAdd_Click(object sender, EventArgs e)

        {
            string id = txtServiceID.Text;
            string name = txtServiceName.Text;
            string price = ServicePrice.Text.ToString();
            if (id == "")
            {
                bool check = DataAccess.AddService(name, price);
                if (check == true)
                {
                    DialogResult dr = MessageBox.Show("Add Service is successful!!!");
                    GetServicesToDgv();

                }
                else
                {
                    DialogResult dr = MessageBox.Show("Add Service is fail!!!");
                }
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void txtRoomID_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel14_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnRoomAdd_Click(object sender, EventArgs e)
        {
            add = new AddRoomForm();
            add.Show();
        }

        private void btnRoomEdit_Click(object sender, EventArgs e)
        {

            string id = txtRoomID.Text;
            string name = txtRoomName.Text;
            string status = txtStatus.Text;
            string idRoomCa = "";
            string idBedType = cbBt.SelectedValue.ToString();
            if (cbRC.SelectedIndex == -1)
            {
                idRoomCa = "";
            }
            else idRoomCa = cbRC.SelectedValue.ToString();
         
            bool check = DataAccess.UpdateRoom(name, status, id, idRoomCa, idBedType);
            if (check == true)
            {
                DialogResult dr = MessageBox.Show("Update Room is successful!!!");
                GetRoomsToDgv();

            }
            else
            {
                DialogResult dr = MessageBox.Show("Update Room is fail!!!");
            }
        }

        private void AdminForm_Load_1(object sender, EventArgs e)
        {

        }

        private void dgvListAccount_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnTkRemove_Click(object sender, EventArgs e)
        {
            DialogResult result;

            string message = "Do you want to delete this record? YES or NO ?";
            string caption = "Notify";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            result = MessageBox.Show(this, message, caption, buttons,
                             MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,
                             MessageBoxOptions.RightAlign);


            if (result == DialogResult.Yes)
            {
                bool check = DataAccess.DeleteAccount(txtUserName.Text);
                if (check == true)
                {
                    DialogResult dr = MessageBox.Show("Delete Account is successful!!!");
                    GetAccountToDgv();
                    txtUserName.Text = "";
                    txtFullName.Text = "";
                    txtSalary.Text = "";
                    txtPass.Text = "";

                }
                else
                {
                    DialogResult dr = MessageBox.Show("Delete Account is fail!!!");
                }


            }
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void dgvListAccount_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;


            if (e.RowIndex >= 0)
            {

                DataGridViewRow row = dgvListAccount.Rows[e.RowIndex];
                txtFullName.Text = row.Cells["fullcol"].Value.ToString();
                txtUserName.Text = row.Cells["usernamecol"].Value.ToString();
                txtSalary.Text = row.Cells["salarycol"].Value.ToString();

            }
        }

        private void btnTkEdit_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text;
            string salary = txtSalary.Text;
           
       

            bool check = DataAccess.UpdateSalary(username, salary);
            if (check == true)
            {
                DialogResult dr = MessageBox.Show("Update Salary is successful!!!");
                GetAccountToDgv();

            }
            else
            {
                DialogResult dr = MessageBox.Show("Update Salary is fail!!!");
            }
        }

        private void btnC_Click(object sender, EventArgs e)
        {
            txtUserName.Text = "";
            txtFullName.Text = "";
            txtSalary.Text = "";
            txtPass.Text = "";
        }

        private void btnTkAdd_Click(object sender, EventArgs e)
        {
            List<Account> mylist = null;

            mylist = Account.getAllAccounts();
            
            string username = txtUserName.Text;
            string fullname = txtFullName.Text;
            string pass = txtPass.Text.ToString();
            string salary = txtSalary.Text.ToString();
            foreach (Account a in mylist)
            {
                if(username == a.username)
                {
                    DialogResult dr = MessageBox.Show("User is exist");
                    return;
                    
                }
            }
          
                bool check = DataAccess.AddAcount(fullname, username, pass,salary);
                if (check == true)
                {
                    DialogResult dr = MessageBox.Show("Add Account is successful!!!");
                    GetAccountToDgv();

                }
                else
                {
                    DialogResult dr = MessageBox.Show("Add Account is fail!!!");
                }
            
        }

        private void dgvBill_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            if (senderGrid.Columns["Detail"] is DataGridViewButtonColumn && e.ColumnIndex == dgvBill.Columns["Detail"].Index && e.RowIndex >= 0)
            {
                detail = new DetailForm();
                detail.Show();
              
                

                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dgvBill.Rows[e.RowIndex];
                   
                    detail.txtNameRoom.Text = row.Cells["namecol"].Value.ToString();
                    detail.txtBillId.Text = row.Cells["idcol"].Value.ToString();
                    detail.dgvBillInfor.Columns.Clear();
                    if (detail.dgvBillInfor.Rows.Count > 0)
                        detail.dgvBillInfor.Rows.Clear();

                    detail.dgvBillInfor.AutoGenerateColumns = false;
                    detail.dgvBillInfor.Columns.Add("idroomcol", "ID");
                    detail.dgvBillInfor.Columns["idroomcol"].DataPropertyName = "id";



                    detail.dgvBillInfor.Columns.Add("nameroomcol", "Service Name");
                    detail.dgvBillInfor.Columns["nameroomcol"].DataPropertyName = "serviceName";

                    detail.dgvBillInfor.Columns.Add("quantitycol", "Quantity");
                    detail.dgvBillInfor.Columns["quantitycol"].DataPropertyName = "quantity";

                    detail.dgvBillInfor.Columns.Add("pricecol", "Price");
                    detail.dgvBillInfor.Columns["pricecol"].DataPropertyName = "price";

                


                    List<BillInfo> mylist = null;

                    mylist = BillInfo.getBillInfoById(row.Cells["idcol"].Value.ToString());



                    detail.dgvBillInfor.DataSource = mylist;



                }
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            isSearch = true;
            GetBillToDgv(isSearch);
        }

        private void txtRoomName_Validating(object sender, CancelEventArgs e)
        {
            int n;
            bool isNumeric = int.TryParse(txtRoomName.Text.ToString(), out n);
            if (txtRoomName.Text == "")
            {
                e.Cancel = true;
            }
        }

        private void txtServiceName_Validating(object sender, CancelEventArgs e)
        {
            int n;
            bool isNumeric = int.TryParse(txtServiceName.Text.ToString(), out n);
            if (txtServiceName.Text == "" || isNumeric)
            {
                e.Cancel = true;
            }
        }

        private void txtFullName_Validating(object sender, CancelEventArgs e)
        {
            int n;
            bool isNumeric = int.TryParse(txtFullName.Text.ToString(), out n);
            if (isNumeric)
            {
                e.Cancel = true;
            }
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            int n;
            bool isNumeric = int.TryParse(txtUserName.Text.ToString(), out n);
            if (isNumeric)
            {
                e.Cancel = true;
            }
        }

        private void btnRoomRemove_Click(object sender, EventArgs e)
        {

        }
    }
}
