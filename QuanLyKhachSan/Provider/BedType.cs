using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace QuanLyKhachSan.Provider
{
    class BedType
    {
        public int id { set; get; }
        public string name { set; get; }
        public double price { set; get; }

        public BedType(int id, string name, double price)
        {
            this.id = id;
            this.name = name;
            this.price = price;
        }

        static public List<BedType> getAllBedType()
        {
            DataTable dt = DataAccess.getAllBedType();
            List<BedType> bedList = new List<BedType>();
            foreach (DataRow dr in dt.Rows)
                bedList.Add(new BedType(Convert.ToInt32(dr["id"]),
                                dr["name"].ToString(),
                               Convert.ToDouble(dr["price"]))
                            );
            return bedList;
        }
    }
}
