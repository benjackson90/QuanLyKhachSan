using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace QuanLyKhachSan.Provider
{
    class Room
    {
        public int id { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public string roomCategory { get; set; }
        public string bedType { get; set; }
        public Room(int id,string name, string status, string roomCategory, string bedType)
        {
            this.id = id;
            this.name = name;
            this.status = status;
            this.roomCategory = roomCategory;
            this.bedType = bedType;
        }

        static public List<Room> getAllRooms()
        {
            DataTable dt = DataAccess.getAllRoom();
            List<Room> roomList = new List<Room>();
            foreach (DataRow dr in dt.Rows)
                roomList.Add(new Room(Convert.ToInt32(dr["id"]),
                                dr["name"].ToString(),
                                dr["status"].ToString(),
                                dr["roomCategory"].ToString(),
                                dr["bedType"].ToString()
                              )
                            );
            return roomList;
        }

    }
}
