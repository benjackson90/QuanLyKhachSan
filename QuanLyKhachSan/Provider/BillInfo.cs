using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace QuanLyKhachSan.Provider
{
    class BillInfo
    {
        public int BillInfoID { get; set; }
        public string ServiceName { get; set; }
        public double ServicePrice { get; set; }
        public int amount { get; set; }
        public double Total { get; set; }

        public BillInfo(int id, string name, double price, int amount, double total)
        {
            this.BillInfoID = id;
            this.ServiceName = name;
            this.ServicePrice = price;
            this.amount = amount;
            this.Total = total;
        }

        static public List<BillInfo> getBillInfo(int billId)
        {
            DataTable dt = DataAccess.getListBillInfo(billId);
            List<BillInfo> proList = new List<BillInfo>();
            foreach (DataRow dr in dt.Rows)
            {
                double price = Convert.ToDouble(dr["servicePrice"].ToString()) * Convert.ToInt32(dr["count"].ToString());
                proList.Add(new BillInfo(Convert.ToInt32(dr["id"].ToString()),
                                dr["serviceName"].ToString(),
                                Convert.ToDouble(dr["servicePrice"].ToString()),
                                Convert.ToInt32(dr["count"].ToString()),
                                price)
                            );
            }
            return proList;
        }
    }
}
