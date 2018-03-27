using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

namespace QuanLyKhachSan.Provider
{
    class Service
    {
        public int id { get; set; }
        public string name { get; set; }
        public double price { get; set; }

        public Service(int id, string name, double price)
        {
            this.id = id;
            this.name = name;
            this.price = price;
        }

        static public List<Service> getAllServices()
        {
            DataTable dt = DataAccess.getAllServices();
            List<Service> serviceList = new List<Service>();
            foreach (DataRow dr in dt.Rows) 
                serviceList.Add(new Service(Convert.ToInt32(dr["id"]),
                                dr["name"].ToString(),
                               Convert.ToDouble(dr["price"]))
                            );
            return serviceList;
        }

    }
}
