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
        public static DataTable getAllServices()
        {
            string sql = "SELECT * FROM Service";
            return getDataBySQL(sql);
        }

        public static DataTable getListBillInfo(int tableId)
        {
            string sql = "SELECT bi.id," 
                        + " s.name AS serviceName,"
	                    + " s.price AS servicePrice,"
	                    + " bi.count"
                        + " FROM BillInfo bi"
                        + " INNER JOIN Service s ON bi.idService = s.id"
                        + " WHERE bi.idBill = (SELECT id FROM Bill WHERE idRoom = " + tableId +" AND status = 0)";
            return getDataBySQL(sql);
        }
        public static DataTable searchOrder(DateTime startDate, DateTime lastDate, string CustomerID, int EmployeeID)
        {
            string sql = @"select * from Orders
            where OrderDate between '" + startDate + "' and '" + lastDate +
            "' and CustomerID = '" + CustomerID +
            "' and EmployeeID = " + EmployeeID; 
            return getDataBySQL(sql);
        }
        public static int RemoveOrder (int OrderId)
        {
            string query1 = @"delete from [Order Details] where OrderID = @orderid"+
                " \n delete from [Orders] where OrderID = @orderid1";
            SqlParameter param1 = new SqlParameter("@orderid", SqlDbType.Int);
            param1.Value = OrderId;
            SqlParameter param2 = new SqlParameter("@orderid1", SqlDbType.Int);
            param2.Value = OrderId;
            SqlCommand command = new SqlCommand(query1, getConnection());
            command.Parameters.Add(param1);
            command.Parameters.Add(param2);
            command.Connection.Open();
            int i2 = command.ExecuteNonQuery();
            command.Connection.Close();
            return i2;
        }
      
        public static DataTable getAllProductsByCatID(int catID)
        {
            string sql = @"select P.*, C.CategoryName, S.CompanyName
from Products P, Categories C, Suppliers S
where P.CategoryID = C.CategoryID and P.SupplierID = S.SupplierID
and C.CategoryID = " + catID.ToString();
            return getDataBySQL(sql);
        }
        public static int addOrder(string CustomerID, int EmployeeID, DateTime OrderDate, string ShipName, string shipCountry, double freight)
        {
            string sql = @"insert into Orders (CustomerID, EmployeeID, OrderDate, ShipName, ShipCountry, Freight) values (@customerId, @employeeId, @orderDate, @shipName, @shipCountry, @freight)";
            SqlParameter param1 = new SqlParameter("@customerId", SqlDbType.NVarChar);
            param1.Value = CustomerID;
            SqlParameter param2 = new SqlParameter("@employeeId", SqlDbType.Int);
            param2.Value = EmployeeID;
            SqlParameter param3 = new SqlParameter("@orderDate", SqlDbType.DateTime);
            param3.Value = OrderDate;
            SqlParameter param4 = new SqlParameter("@shipName", SqlDbType.NVarChar);
            param4.Value = ShipName;
            SqlParameter param5 = new SqlParameter("@shipCountry", SqlDbType.NVarChar);
            param5.Value = shipCountry;
            SqlParameter param6 = new SqlParameter("@freight", SqlDbType.Money);
            param6.Value = freight;
            SqlCommand command = new SqlCommand(sql, getConnection());
            command.Parameters.Add(param1);
            command.Parameters.Add(param2);
            command.Parameters.Add(param3);
            command.Parameters.Add(param4);
            command.Parameters.Add(param5);
            command.Parameters.Add(param6);
            command.Connection.Open();
            int i = command.ExecuteNonQuery();
            command.Connection.Close();
            return i;
        }
        public static int addOrderDetails(int OrderID, int ProductID, double UnitPrice, int Quantity, double Discount)
        {
            string sql = @"INSERT INTO dbo.[Order Details]
                            ( OrderID ,
                              ProductID ,
                              UnitPrice ,
                              Quantity ,
                              Discount
                            ) VALUES (@orderId, @productId, @unitPrice, @quantity, @discount)";
            SqlParameter param1 = new SqlParameter("@orderId", SqlDbType.Int);
            param1.Value = OrderID;
            SqlParameter param2 = new SqlParameter("@productId", SqlDbType.Int);
            param2.Value = ProductID;
            SqlParameter param3 = new SqlParameter("@unitPrice", SqlDbType.Money);
            param3.Value = UnitPrice;
            SqlParameter param4 = new SqlParameter("@quantity", SqlDbType.SmallInt);
            param4.Value = Quantity;
            SqlParameter param5 = new SqlParameter("@discount", SqlDbType.Real);
            param5.Value = Discount;
            SqlCommand command = new SqlCommand(sql, getConnection());
            command.Parameters.Add(param1);
            command.Parameters.Add(param2);
            command.Parameters.Add(param3);
            command.Parameters.Add(param4);
            command.Parameters.Add(param5);
            command.Connection.Open();
            int i = command.ExecuteNonQuery();
            command.Connection.Close();
            return i;
        }
        public static int editOrder(int orderID, string shipName, string shipCountry, DateTime orderDate, double freight)
        {
            string sql = @"UPDATE dbo.Orders
                          SET OrderDate = @orderDate, Freight = @freight, ShipName = @shipName, ShipCountry = @shipCountry
                          WHERE OrderID = @orderID";
            SqlParameter param1 = new SqlParameter("@orderDate", SqlDbType.Date);
            param1.Value = orderDate;
            SqlParameter param2 = new SqlParameter("@freight", SqlDbType.Money);
            param2.Value = freight;
            SqlParameter param3 = new SqlParameter("@shipName", SqlDbType.NVarChar);
            param3.Value = shipName;
            SqlParameter param4 = new SqlParameter("shipCountry", SqlDbType.NVarChar);
            param4.Value = shipCountry;
            SqlParameter param5 = new SqlParameter("orderID", SqlDbType.Int);
            param5.Value = orderID;
            SqlCommand command = new SqlCommand(sql, getConnection());
            command.Parameters.Add(param1);
            command.Parameters.Add(param2);
            command.Parameters.Add(param3);
            command.Parameters.Add(param4);
            command.Parameters.Add(param5);
            command.Connection.Open();
            int i = command.ExecuteNonQuery();
            command.Connection.Close();
            return i;
        }
        public static int addProduct(string proName, int catID)
        {
            string sql = @"insert into Products (ProductName, CategoryID, Discontinued, SupplierID) values (@pname, @catid, @disc, @supid)";
            SqlParameter param1 = new SqlParameter("@pname", SqlDbType.NVarChar);
            param1.Value = proName;
            SqlParameter param2 = new SqlParameter("@catid", SqlDbType.Int);
            param2.Value = catID;
            SqlParameter param3 = new SqlParameter("@disc", SqlDbType.Bit);
            param3.Value = 1;
            SqlParameter param4 = new SqlParameter("@supid", SqlDbType.Int);
            param4.Value = 1;
            SqlCommand command = new SqlCommand(sql, getConnection());
            command.Parameters.Add(param1);
            command.Parameters.Add(param2);
            command.Parameters.Add(param3);
            command.Parameters.Add(param4);
            command.Connection.Open();
            int i = command.ExecuteNonQuery();
            command.Connection.Close();
            return i;
        }
    }
}
