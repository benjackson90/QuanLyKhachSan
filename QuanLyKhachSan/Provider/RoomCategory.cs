using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace QuanLyKhachSan.Provider
{
    class RoomCategory
    {
        public int id { set; get; }
        public string name { set; get; }
        public double price { set; get; }

        public RoomCategory(int id, string name, double price)
        {
            this.id = id;
            this.name = name;
            this.price = price;
        }

        static public List<RoomCategory> getAllRoomCategory()
        {
            DataTable dt = DataAccess.getAllRoomCategory();
            List<RoomCategory> roomCaList = new List<RoomCategory>();
            foreach (DataRow dr in dt.Rows)
                roomCaList.Add(new RoomCategory(Convert.ToInt32(dr["id"]),
                                dr["name"].ToString(),
                               Convert.ToDouble(dr["price"]))
                            );
            return roomCaList;
        }
    }
}
