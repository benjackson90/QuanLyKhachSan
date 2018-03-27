using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace QuanLyKhachSan.Provider
{
    class Bill
    {
        public int id { get; set; }
        public DateTime checkIn { get; set; }
        public DateTime checkOut { get; set; }   
        public string roomName { get; set; }
        public double price { get; set; }
   
        public Bill(int id, DateTime checkIn, DateTime checkOut, string roomName, double price)
        {
            this.id = id;
            this.checkIn = checkIn;
            this.checkOut = checkOut;
            this.roomName = roomName;
            this.price = price;
        }

        static public List<Bill> getAllBills()
        {
            DataTable dt = DataAccess.getBillInfoById();
            List<Bill> billList = new List<Bill>();
            foreach (DataRow dr in dt.Rows)
            {
                try
                {
                    billList.Add(new Bill(Convert.ToInt32(dr["id"]),
                             Convert.ToDateTime(dr["checkIn"]),
                              Convert.ToDateTime(dr["checkOut"]),
                              dr["roomName"].ToString(),
                              Convert.ToDouble(dr["price"].ToString())
                            )
                          );
                } catch(Exception e)
                {

                }
            }
              
            return billList;
        }

        public static List<Bill> getBillBy(string from, string to)
        {
            DataTable dt = DataAccess.getBillBy(from, to);
            List<Bill> billList = new List<Bill>();
            foreach (DataRow dr in dt.Rows)
            {
                try
                {
                    billList.Add(new Bill(Convert.ToInt32(dr["id"]),
                             Convert.ToDateTime(dr["checkIn"]),
                              Convert.ToDateTime(dr["checkOut"]),
                              dr["roomName"].ToString(),
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
