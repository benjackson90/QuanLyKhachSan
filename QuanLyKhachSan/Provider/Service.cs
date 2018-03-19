using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace QuanLyKhachSan.Provider
{
    class Service
    {
        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
        public double Price { get; set; }

        public Service (int id, string name, double price)
        {
            this.ServiceID = id;
            this.ServiceName = name;
            this.Price = price;
        }
        static public List<Service> getAllService()
        {
            DataTable dt = DataAccess.getAllServices();
            List<Service> List = new List<Service>();
            foreach (DataRow dr in dt.Rows)
                List.Add(
                    new Service(Convert.ToInt32(dr["id"]),
                                dr["name"].ToString(),
                                Convert.ToDouble(dr["price"].ToString()))
                                );
            return List;
        }
    }
}
