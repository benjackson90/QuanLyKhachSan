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

        public static DataTable getAllRoomCategory()
        {
            string sql = @"select * from RoomCategory";
            return getDataBySQL(sql);
        }
    

              public static DataTable getBillInfoById()
        {
            string sql = @"SELECT b.id
    , b.checkIn
    , b.checkOut

    , b.price

    , r.name as roomName
FROM Bill b
INNER JOIN Room r
    on b.idRoom = r.id";
            return getDataBySQL(sql);
        }


        public static DataTable getAllBedType()
        {
            string sql = @"select * from BedType";
            return getDataBySQL(sql);
        }


        public static DataTable getAllAccount()
        {
            string sql = @"select * from Account";
            return getDataBySQL(sql);
        }

        public static DataTable getAllServices()
        {
            string sql = @"select * from Service";
            return getDataBySQL(sql);
        }

        public static DataTable getAllRoom()
        {
            string sql = @"SELECT r.id
    , r.name
    , r.status
	, rc.name as roomCategory
	, bt.name as bedType
FROM Room r
INNER JOIN RoomCategory rc
    on r.idRoomCategory = rc.id
INNER JOIN BedType bt
    on r.idBedType = bt.id";
            return getDataBySQL(sql);
        }

        static public bool UpdateServices(string name, string price, string id)
        {
            string sql = @"UPDATE dbo.Service SET name = @name, price = @price
                          WHERE id = @id";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["QuanLyKhachSan"].ToString());
            SqlCommand command = new SqlCommand(sql, con);
            int n = -1;

            try
            {
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@id", Convert.ToInt32(id));
                command.Parameters.AddWithValue("@price", Convert.ToDouble(price));



                con.Open();

                n = command.ExecuteNonQuery();
            }
            catch (Exception e)
            {

            }


            con.Close();

            if (n != -1) return true;
            return false;


        }

        static public bool DeleteService(string id)
        {
            string sql = @"DELETE FROM dbo.[BillInfo] 
                            WHERE idService = @id

                            DELETE FROM dbo.Service 
                            WHERE id = @id";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["QuanLyKhachSan"].ToString());
            SqlCommand command = new SqlCommand(sql, con);

            command.Parameters.AddWithValue("@id", Convert.ToInt32(id));

            con.Open();

            int n = command.ExecuteNonQuery();

            con.Close();

            if (n != -1) return true;
            return false;
        }

        public static Boolean AddService(string name , string price)
        {
            string sql = @"insert into Service values (@name, @price)";
            SqlParameter param1 = new SqlParameter("@name", SqlDbType.NVarChar);
            param1.Value = name;
            SqlParameter param2 = new SqlParameter("@price", SqlDbType.Money);
            param2.Value = Convert.ToDouble(price);
      
            SqlCommand command = new SqlCommand(sql, getConnection());
            command.Parameters.Add(param1);
            command.Parameters.Add(param2);
           
            command.Connection.Open();
            int i = command.ExecuteNonQuery();
            command.Connection.Close();
            if (i != -1) return true;
            return false;
        }

        public static Boolean AddRoom(string name, string status, string idRoomCa, string idBedType)
        {
            string sql = @"insert into Room values (@name, @status, @idRoomCa, @idBedtype)";
            SqlParameter param1 = new SqlParameter("@name", SqlDbType.NVarChar);
            param1.Value = name;
            SqlParameter param2 = new SqlParameter("@status", SqlDbType.NVarChar);
            param2.Value = status;
            SqlParameter param3 = new SqlParameter("@idRoomCa", SqlDbType.Int);
            param3.Value = Convert.ToInt32(idRoomCa);
            SqlParameter param4 = new SqlParameter("@idBedtype", SqlDbType.Int);
            param4.Value = Convert.ToInt32(idBedType);

            SqlCommand command = new SqlCommand(sql, getConnection());
            command.Parameters.Add(param1);
            command.Parameters.Add(param2);
            command.Parameters.Add(param3);
            command.Parameters.Add(param4);

            command.Connection.Open();
            int i = command.ExecuteNonQuery();
            command.Connection.Close();
            if (i != -1) return true;
            return false;
        }

        static public bool DeleteRoom(string id)
        {
            string sql = @"DELETE FROM dbo.[BillInfo] 
                            WHERE idService = @id

                            DELETE FROM dbo.Service 
                            WHERE id = @id";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["QuanLyKhachSan"].ToString());
            SqlCommand command = new SqlCommand(sql, con);

            command.Parameters.AddWithValue("@id", Convert.ToInt32(id));

            con.Open();

            int n = command.ExecuteNonQuery();

            con.Close();

            if (n != -1) return true;
            return false;
        }

        static public bool UpdateRoom(string name, string status, string id, string idRoomCa, string idBedType)
        {
            string sql = @"UPDATE dbo.Room SET name = @name, status = @status, idRoomCategory = @idRoomCa, idBedType = @idBedType  
                          WHERE id = @id";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["QuanLyKhachSan"].ToString());
            SqlCommand command = new SqlCommand(sql, con);
            int n = -1;

            try
            {
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@id", Convert.ToInt32(id));
                command.Parameters.AddWithValue("@status", status);
                command.Parameters.AddWithValue("@idRoomCa", Convert.ToInt32(idRoomCa));
                command.Parameters.AddWithValue("@idBedType", Convert.ToInt32(idBedType));




                con.Open();

                n = command.ExecuteNonQuery();
            }
            catch (Exception e)
            {

            }


            con.Close();

            if (n != -1) return true;
            return false;


        }
        public static Boolean AddAcount(string fullname, string username, string password, string salary)
        {
            string sql = @"insert into Account values (@fullname, @username, @password, @salary, @type)";
            SqlParameter param1 = new SqlParameter("@fullname", SqlDbType.NVarChar);
            param1.Value = fullname;
            SqlParameter param2 = new SqlParameter("@username", SqlDbType.NVarChar);
            param2.Value = username;
            SqlParameter param3 = new SqlParameter("@password", SqlDbType.NVarChar);
            param3.Value = password;
            SqlParameter param4 = new SqlParameter("@salary", SqlDbType.Money);
            param4.Value = Convert.ToDouble(salary);
            SqlParameter param5 = new SqlParameter("@type", SqlDbType.Int);
            param5.Value = 0;

            SqlCommand command = new SqlCommand(sql, getConnection());
            command.Parameters.Add(param1);
            command.Parameters.Add(param2);
            command.Parameters.Add(param3);
            command.Parameters.Add(param4);
            command.Parameters.Add(param5);
          

            command.Connection.Open();
            int i = command.ExecuteNonQuery();
            command.Connection.Close();
            if (i != -1) return true;
            return false;
        }

        static public bool DeleteAccount(string username)
        {
            string sql = @"DELETE FROM dbo.[Account] 
                            WHERE UserName = @username";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["QuanLyKhachSan"].ToString());
            SqlCommand command = new SqlCommand(sql, con);

            command.Parameters.AddWithValue("@username", username);

            con.Open();

            int n = command.ExecuteNonQuery();

            con.Close();

            if (n != -1) return true;
            return false;
        }

        static public bool UpdateSalary(string username ,string salary)
        {
            string sql = @"UPDATE dbo.Account SET Salary = @salary
                          WHERE UserName = @username";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["QuanLyKhachSan"].ToString());
            SqlCommand command = new SqlCommand(sql, con);
            int n = -1;

            try
            {
                command.Parameters.AddWithValue("@salary", Convert.ToDouble(salary));
                command.Parameters.AddWithValue("@username", username);
       




                con.Open();

                n = command.ExecuteNonQuery();
            }
            catch (Exception e)
            {

            }


            con.Close();

            if (n != -1) return true;
            return false;


        }

        public static DataTable getBillBy(string from, string to)
        {
            string sql = @"SELECT b.id
    , b.checkIn
    , b.checkOut
	, b.price
	, r.name as roomName
FROM Bill b
INNER JOIN Room r
    on b.idRoom = r.id
    where checkIn BETWEEN '" + from + "' and '" + to + "'";
            return getDataBySQL(sql);
        }


        public static DataTable getBillInfoById(string id)
        {
            string sql = @"SELECT bi.id
    , bi.count
	,s.name
	,s.price
FROM BillInfo bi

INNER JOIN Service s
    on bi.idService = s.id
where bi.idBill = "+id+"";
            return getDataBySQL(sql);
        }

        static public bool UpdateAccount(string username, string fullname,string password)
        {
            string sql = @"UPDATE dbo.Account SET FullName = @fullname, Password = @password
                          WHERE UserName = @username";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["QuanLyKhachSan"].ToString());
            SqlCommand command = new SqlCommand(sql, con);
            int n = -1;

            try
            {
                command.Parameters.AddWithValue("@fullname", fullname);
                command.Parameters.AddWithValue("@password", password);
                command.Parameters.AddWithValue("@username", username);





                con.Open();

                n = command.ExecuteNonQuery();
            }
            catch (Exception e)
            {

            }


            con.Close();

            if (n != -1) return true;
            return false;


        }

    }
}
