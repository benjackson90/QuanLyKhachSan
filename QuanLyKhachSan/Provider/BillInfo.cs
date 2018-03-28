using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace QuanLyKhachSan.Provider
{
    class BillInfo
    {
        public int id { get; set; }
        public int quantity { get; set; }
     
        public string serviceName { get; set; }
        public double price { get; set; }

        public BillInfo(int id, int quantity, string serviceName, double price)
        {
            this.id = id;
            this.quantity = quantity;
            this.serviceName = serviceName;
         
            this.price = price;
        }

        static public List<BillInfo> getBillInfoById(string id)
        {
            DataTable dt = DataAccess.getBillInfoById(id);
            List<BillInfo> billList = new List<BillInfo>();
            foreach (DataRow dr in dt.Rows)
            {
                try
                {
                    billList.Add(new BillInfo(Convert.ToInt32(dr["id"]),
                            
                              Convert.ToInt32(dr["count"]),
                              dr["name"].ToString(),
                              Convert.ToDouble(dr["price"].ToString())
                            )
                          );
                }
                catch (Exception e)
                {

                }
            }

            return billList;
        }
    }
}
