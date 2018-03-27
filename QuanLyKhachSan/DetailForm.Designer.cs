namespace QuanLyKhachSan
{
    partial class DetailForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.lbRoomName = new System.Windows.Forms.Label();
            this.dgvBillInfor = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.lbId = new System.Windows.Forms.Label();
            this.txtNameRoom = new System.Windows.Forms.TextBox();
            this.txtBillId = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBillInfor)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Thong tin chi tiet phong";
            // 
            // lbRoomName
            // 
            this.lbRoomName.AutoSize = true;
            this.lbRoomName.Location = new System.Drawing.Point(183, 36);
            this.lbRoomName.Name = "lbRoomName";
            this.lbRoomName.Size = new System.Drawing.Size(0, 13);
            this.lbRoomName.TabIndex = 1;
            // 
            // dgvBillInfor
            // 
            this.dgvBillInfor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBillInfor.Location = new System.Drawing.Point(13, 127);
            this.dgvBillInfor.Name = "dgvBillInfor";
            this.dgvBillInfor.Size = new System.Drawing.Size(431, 250);
            this.dgvBillInfor.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Bill ID";
            // 
            // lbId
            // 
            this.lbId.AutoSize = true;
            this.lbId.Location = new System.Drawing.Point(183, 57);
            this.lbId.Name = "lbId";
            this.lbId.Size = new System.Drawing.Size(0, 13);
            this.lbId.TabIndex = 4;
            // 
            // txtNameRoom
            // 
            this.txtNameRoom.Enabled = false;
            this.txtNameRoom.Location = new System.Drawing.Point(153, 28);
            this.txtNameRoom.Name = "txtNameRoom";
            this.txtNameRoom.Size = new System.Drawing.Size(100, 20);
            this.txtNameRoom.TabIndex = 5;
            // 
            // txtBillId
            // 
            this.txtBillId.Enabled = false;
            this.txtBillId.Location = new System.Drawing.Point(153, 57);
            this.txtBillId.Name = "txtBillId";
            this.txtBillId.Size = new System.Drawing.Size(100, 20);
            this.txtBillId.TabIndex = 6;
            // 
            // DetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 389);
            this.Controls.Add(this.txtBillId);
            this.Controls.Add(this.txtNameRoom);
            this.Controls.Add(this.lbId);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvBillInfor);
            this.Controls.Add(this.lbRoomName);
            this.Controls.Add(this.label1);
            this.Name = "DetailForm";
            this.Text = "DetailForm";
            this.Load += new System.EventHandler(this.DetailForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBillInfor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbRoomName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbId;
        public System.Windows.Forms.DataGridView dgvBillInfor;
        public System.Windows.Forms.TextBox txtNameRoom;
        public System.Windows.Forms.TextBox txtBillId;
    }
}