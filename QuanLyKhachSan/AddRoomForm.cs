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
    public partial class AddRoomForm : Form
    {
        public AddRoomForm()
        {
            InitializeComponent();
        }

        private void AddRoomForm_Load(object sender, EventArgs e)
        {
            List<RoomCategory> listRoomCa = RoomCategory.getAllRoomCategory();
            cbRC.DisplayMember = "name";
            cbRC.ValueMember = "id";
            cbRC.DataSource = listRoomCa;
            cbRC.SelectedIndex = 0;

            List<BedType> listBedType = BedType.getAllBedType();
            cbBt.DisplayMember = "name";
            cbBt.ValueMember = "id";
            cbBt.DataSource = listBedType;
            cbBt.SelectedIndex = 0;
        }

        private void btnAddRoom_Click(object sender, EventArgs e)
        {
            string name = txtRoomName.Text;
            string status = "Empty";
            string idRoomCa = cbRC.SelectedValue.ToString();
            string idBedType = cbBt.SelectedValue.ToString();

            Boolean check = DataAccess.AddRoom(name, status, idRoomCa, idBedType);
            if (check)
            {
                DialogResult dr = MessageBox.Show("Add Room is success!!!");
                AdminForm obj = (AdminForm)Application.OpenForms["AdminForm"];
                obj.GetRoomsToDgv();
                this.Close();
            } else
            {
                DialogResult dr = MessageBox.Show("Add Room is fail!!!");
            }

        }
    }
}
