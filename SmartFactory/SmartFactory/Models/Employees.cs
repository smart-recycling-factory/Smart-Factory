using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFactory.Models
{
    public class Employees
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
        public string Address { get; set; }
        public string Position { get; set; }
        //public Image Profile { get; set; }
        public int LoginIdx { get; set; }
        public string LoginId { get; set; }
        public string LoginPw { get; set; }

        public static readonly string EMPLOYEE_SELECT_QUERY = @"SELECT [employeeId]
                                                                     , [name]
                                                                     , [gender]
                                                                     , [email]
                                                                     , [phone]
                                                                     , [address]
                                                                     , [position]
                                                                     , [profile]
                                                                     , [LoginIdx]
                                                                     , [LoginId]
                                                                     , [LoginPw]
                                                                  FROM [dbo].[employee]";

        public static readonly string EMPLOYEE_INSERT_QUERY = @"INSERT INTO [dbo].[employee]
                                                                          ( [name]
                                                                          , [gender]
                                                                          , [email]
                                                                          , [phone]
                                                                          , [address]
                                                                          , [position]
                                                                          , [profile]
                                                                          , [LoginIdx]
                                                                          , [LoginId]
                                                                          , [LoginPw])
                                                                     VALUES
                                                                          ( @name
                                                                          , @gender
                                                                          , @email
                                                                          , @phone
                                                                          , @address
                                                                          , @position
                                                                          , @profile
                                                                          , @LoginIdx
                                                                          , @LoginId
                                                                          , @LoginPw)";

        public static readonly string EMPLOYEE_DELETE_QUERY = @"DELETE FROM [dbo].[employee]
                                                                      WHERE [employeeId] = @employeeId";

        public static readonly string EMPLOYEE_UPDATE_QUERY = @"UPDATE [dbo].[employee]
                                                                   SET [name] = @name
                                                                      ,[gender] = @gender
                                                                      ,[email] = @email
                                                                      ,[phone] = @phone
                                                                      ,[address] = @address
                                                                      ,[position] = @position
                                                                      ,[LoginIdx] = @LoginIdx
                                                                      ,[LoginId] = @LoginId
                                                                      ,[LoginPw] = @LoginPw
                                                                 WHERE [employeeId] = @employeeId";

        public static readonly string EMPLOYEE_SELECT_DETAIL_QUERY = @"SELECT [employeeId]
                                                                     , [name]
                                                                     , [gender]
                                                                     , [email]
                                                                     , [phone]
                                                                     , [address]
                                                                     , [position]
                                                                     , [profile]
                                                                     , [LoginIdx]
                                                                     , [LoginId]
                                                                     , [LoginPw]
                                                                  FROM [dbo].[employee]
                                                                WHERE [employeeId] = @employeeId";
    }
}
