using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace QuanLyKhachSan.Provider
{
    class Room
    {
        public int RoomID { get; set; }
        public string RoomName { get; set; }
        public string Status { get; set; }
        public string RoomType { get; set; }
        public string BedType { get; set; }

        public Room(int id, string name, string status, string roomType, string bedType)
        {
            RoomID = id;
            RoomName = name;
            Status = status;
            RoomType = roomType;
            BedType = bedType;
        }

        static public List<Room> getAllRooms()
        {
            DataTable dt = DataAccess.getAllRooms();
            List<Room> proList = new List<Room>();
            foreach (DataRow dr in dt.Rows)
                proList.Add(
                    new Room(Convert.ToInt32(dr["id"]),
                                dr["name"].ToString(),
                                dr["status"].ToString(),
                                dr["roomCategory"].ToString(),
                                dr["bedType"].ToString()));
            return proList;
        }
        static public List<Room> getAllRoomsEmpty()
        {
            DataTable dt = DataAccess.getAllRoomsEmpty();
            List<Room> proList = new List<Room>();
            foreach (DataRow dr in dt.Rows)
                proList.Add(
                    new Room(Convert.ToInt32(dr["id"]),
                                dr["name"].ToString(),
                                dr["status"].ToString(),
                                dr["roomCategory"].ToString(),
                                dr["bedType"].ToString()));
            return proList;
        }
        static public List<Room> getAllRoomsOccupied()
        {
            DataTable dt = DataAccess.getAllRoomsOccupied();
            List<Room> proList = new List<Room>();
            foreach (DataRow dr in dt.Rows)
                proList.Add(
                    new Room(Convert.ToInt32(dr["id"]),
                                dr["name"].ToString(),
                                dr["status"].ToString(),
                                dr["roomCategory"].ToString(),
                                dr["bedType"].ToString()));
            return proList;
        }
    }
}
