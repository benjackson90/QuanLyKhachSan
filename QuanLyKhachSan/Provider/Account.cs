using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace QuanLyKhachSan.Provider
{
    class Account
    {
        
        public string fullname { set; get; }
        public string username { set; get; }
        public string password { set; get; }
        public double salary { set; get; }
        public int type { set; get; }

        public Account(string fullname, string username, string password, double salary, int type)
        {
            this.fullname = fullname;
            this.username = username;
            this.password = password;
            this.salary = salary;
            this.type = type;
        }

        static public List<Account> getAllAccounts()
        {
            DataTable dt = DataAccess.getAllAccount();
            List<Account> accoutnList = new List<Account>();
            foreach (DataRow dr in dt.Rows)
                accoutnList.Add(new Account(dr["FullName"].ToString(),
                    dr["UserName"].ToString(),
                    dr["Password"].ToString(),
                               Convert.ToDouble(dr["Salary"]),
                               Convert.ToInt32(dr["Type"])
                               )
                            );
            return accoutnList;
        }
    }
}
