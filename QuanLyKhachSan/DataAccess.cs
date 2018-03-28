using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace QuanLyKhachSan
{
    class DataAccess
    {
        public static SqlConnection getConnection()
        {
            SqlConnection myconnect = new SqlConnection(ConfigurationManager.ConnectionStrings["QuanLyKhachSan"].ToString());
            return myconnect;
        }

        //ket noi csdl lay du lieu tuong ung cua cau lenh sql
        public static DataTable getDataBySQL(string sql)
        {
            SqlCommand command = new SqlCommand(sql, getConnection());
            SqlDataAdapter adapt = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            adapt.Fill(ds);
            return ds.Tables[0];
        }

        public static DataTable getAllRooms()
        {
            string sql = "SELECT r.id, r.name, r.status "
                           + ", rc.name as roomCategory "
                           + ", bt.name as bedType FROM Room r "
                           + "INNER JOIN RoomCategory rc on r.idRoomCategory = rc.id "
                           + "INNER JOIN BedType bt on r.idBedType = bt.id";
            return getDataBySQL(sql);
        }
        public static DataTable getAllRoomsEmpty()
        {
            string sql = "SELECT r.id, r.name, r.status "
                           + ", rc.name as roomCategory "
                           + ", bt.name as bedType FROM Room r "
                           + "INNER JOIN RoomCategory rc on r.idRoomCategory = rc.id "
                           + "INNER JOIN BedType bt on r.idBedType = bt.id "
                           + "WHERE r.status like 'Empty'";
            return getDataBySQL(sql);
        }
        public static DataTable getAllRoomsOccupied()
        {
            string sql = "SELECT r.id, r.name, r.status "
                           + ", rc.name as roomCategory "
                           + ", bt.name as bedType FROM Room r "
                           + "INNER JOIN RoomCategory rc on r.idRoomCategory = rc.id "
                           + "INNER JOIN BedType bt on r.idBedType = bt.id "
                           + "WHERE r.status like 'Occupied'";
            return getDataBySQL(sql);
        }
        public static string getStatus(int id)
        {
            string sql = "SELECT status FROM Room WHERE id = " + id;
            DataTable dt = DataAccess.getDataBySQL(sql);
            object o = dt.Rows[0][0];
            return Convert.ToString(o);
        }
        public static DataTable getAllServices()
        {
            string sql = "SELECT * FROM Service";
            return getDataBySQL(sql);
        }
        public static double getRoomPrice(int idRoom)
        {
            string sql = "select rc.price + bt.price "
                          + "from Room r INNER JOIN RoomCategory rc on r.idRoomCategory = rc.id "
                          + "INNER JOIN BedType bt on r.idBedType = bt.id "
                          + "where r.id = " + idRoom;
            DataTable dt = DataAccess.getDataBySQL(sql);
            object o = dt.Rows[0][0];
            return Convert.ToDouble(o);
        }
        public static DataTable getListBillInfo(int roomId)
        {
            string sql = "SELECT bi.id," 
                        + " s.name AS serviceName,"
	                    + " s.price AS servicePrice,"
	                    + " bi.count"
                        + " FROM BillInfo bi"
                        + " INNER JOIN Service s ON bi.idService = s.id"
                        + " WHERE bi.idBill = (SELECT id FROM Bill WHERE idRoom = " + roomId +" AND status = 0) AND bi.status = 0";
            return getDataBySQL(sql);
        }
        public static int getBillInfoColumn (String what, int idRoom, int idService)
        {
            string sql = "select "+what+" from BillInfo where idBill = " +idRoom+ " and idService = "+idService+" and  status = 0";
            DataTable dt = DataAccess.getDataBySQL(sql);
            object o = dt.Rows[0][0];
            return Convert.ToInt32(o);
        }
        public static int getCurrentAmount(int idBillInfo)
        {
            string sql = "SELECT count FROM BillInfo WHERE id = "+idBillInfo;
            DataTable dt = DataAccess.getDataBySQL(sql);
            object o = dt.Rows[0][0];
            return Convert.ToInt32(o);
        }
        public static string getUsername(string username)
        {
            string sql = "select UserName from Account where UserName like '" + username + "'";
            DataTable dt = DataAccess.getDataBySQL(sql);
            object o = dt.Rows[0][0];
            return Convert.ToString(o);
        }
        public static int getBillIdByRoomId(int idRoom)
        {
            string sql = "SELECT id FROM Bill WHERE idRoom = " + idRoom;
            DataTable dt = DataAccess.getDataBySQL(sql);
            object o = dt.Rows[0][0];
            return Convert.ToInt32(o);
        }
        public static int getBillIdByRoomIdAndStatus(int idRoom)
        {
            string sql = "SELECT id FROM Bill WHERE idRoom = " + idRoom + " AND status = 0";
            DataTable dt = DataAccess.getDataBySQL(sql);
            object o = dt.Rows[0][0];
            return Convert.ToInt32(o);
        }

        public static int getCountBill (int idRoom)
        {
            string sql = "select COUNT(*) from Bill where idRoom = " + idRoom + "AND status = 0";
            DataTable dt = DataAccess.getDataBySQL(sql);
            object o = dt.Rows[0][0];
            return Convert.ToInt32(o);
        }
        public static int getMaxIdBill ()
        {
            string sql = "SELECT MAX(id) FROM Bill";
            DataTable dt = DataAccess.getDataBySQL(sql);
            object o = dt.Rows[0][0];
            return Convert.ToInt32(o);
        }

        public static int insertBill(int idRoom)
        {
            string sql = @"INSERT INTO Bill VALUES ( "
                          + "GETDATE(), "  
                          + "NULL, "
                          + "@idRoom, "
                          + "-1, "
                          + "0 "
                          + ")";
            SqlParameter param1 = new SqlParameter("@idRoom", SqlDbType.Int);
            param1.Value = idRoom;
            SqlCommand command = new SqlCommand(sql, getConnection());
            command.Parameters.Add(param1);
            command.Connection.Open();
            int i = command.ExecuteNonQuery();
            command.Connection.Close();
            return i;
        }
        public static int insertBillInfo(int idBill, int idService, int count)
        {
            string sql = @"INSERT INTO BillInfo
                         VALUES (@idBill, @idService, @count, 0)";
            SqlParameter param1 = new SqlParameter("@idBill", SqlDbType.Int);
            param1.Value = idBill;
            SqlParameter param2 = new SqlParameter("@idService", SqlDbType.Int);
            param2.Value = idService;
            SqlParameter param3 = new SqlParameter("@count", SqlDbType.Int);
            param3.Value = count;
            SqlCommand command = new SqlCommand(sql, getConnection());
            command.Parameters.Add(param1);
            command.Parameters.Add(param2);
            command.Parameters.Add(param3);
            command.Connection.Open();
            int i = command.ExecuteNonQuery();
            command.Connection.Close();
            return i;
        }

        public static int updateBillInfo(int idBillInfo, int newcount)
        {
            string sql = @"UPDATE BillInfo SET count = @count  WHERE id = @idBillInfo";
            SqlParameter param1 = new SqlParameter("@count", SqlDbType.Int);
            param1.Value = newcount;
            SqlParameter param2 = new SqlParameter("@idBillInfo", SqlDbType.Int);
            param2.Value = idBillInfo;
            SqlCommand command = new SqlCommand(sql, getConnection());
            command.Parameters.Add(param1);
            command.Parameters.Add(param2);
            command.Connection.Open();
            int i = command.ExecuteNonQuery();
            command.Connection.Close();
            return i;
        }
        public static int updateRoomStatus(string status, int idRoom)
        {
            string sql = @"UPDATE Room SET status = '"+status+"'  WHERE id = @idRoom";
            SqlParameter param1 = new SqlParameter("@idRoom", SqlDbType.Int);
            param1.Value = idRoom;
            SqlCommand command = new SqlCommand(sql, getConnection());
            command.Parameters.Add(param1);
            command.Connection.Open();
            int i = command.ExecuteNonQuery();
            command.Connection.Close();
            return i;
        }
        // thanh toán
        public static int updateStatusBillInfo(int idBill)
        {
            string sql = @"UPDATE BillInfo SET status = 1 WHERE idBill = @idBill";
            SqlParameter param1 = new SqlParameter("@idBill", SqlDbType.Int);
            param1.Value = idBill;
            SqlCommand command = new SqlCommand(sql, getConnection());
            command.Parameters.Add(param1);
            command.Connection.Open();
            int i = command.ExecuteNonQuery();
            command.Connection.Close();
            return i;
        }
        public static int updateStatusBill(int idBill, double total)
        {
            DateTime today = DateTime.Now;
            string sql = @"UPDATE Bill SET checkOut = '"+ today+"', status = 1, total = @total WHERE id = @idBill";
            SqlParameter param1 = new SqlParameter("@idBill", SqlDbType.Int);
            param1.Value = idBill;
            SqlParameter param2 = new SqlParameter("@total", SqlDbType.Int);
            param2.Value = total;
            SqlCommand command = new SqlCommand(sql, getConnection());
            command.Parameters.Add(param1);
            command.Parameters.Add(param2);
            command.Connection.Open();
            int i = command.ExecuteNonQuery();
            command.Connection.Close();
            return i;
        }
        public static DataTable checkUsername(string username, string password)
        {
            string sql = "SELECT type from Account where UserName = '"+username+"' AND Password = '"+password+"'";
            DataTable dt = getDataBySQL(sql);
            return dt;
        }
    }
}
