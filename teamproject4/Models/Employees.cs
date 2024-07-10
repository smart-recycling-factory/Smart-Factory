using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace teamproject4.Models
{
    class Employees
    {
        public class Member
        {
<<<<<<< Updated upstream
            public string Character { get; set; }
            public string Number { get; set; }
            public string Name { get; set; }
            public string Position { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            //public Brush BgColor { get; set; }

            //public static readonly string SELECT_QUERY = 
=======
            public int employeeId { get; set; }
            public string name { get; set; }
            public string gender { get; set; }
            public string email { get; set; }
            public int phone { get; set; }
            public string address { get; set; }
            public string position { get; set; }
            //public image? profile { get; set; }
            public int LoginIdx { get; set; }
            public string LoginId { get; set; }
            public string LoginPw { get; set; }

            public static readonly string EMPLOYEE_INSERT_QUERY = @"INSERT INTO [dbo].[employee]
                                                                               ([employeeId]
                                                                               ,[name]
                                                                               ,[gender]
                                                                               ,[email]
                                                                               ,[phone]
                                                                               ,[address]
                                                                               ,[position]
                                                                               ,[profile]
                                                                               ,[LoginIdx]
                                                                               ,[LoginId]
                                                                               ,[LoginPw])
                                                                          VALUES
                                                                               (@employeeId
                                                                               ,@name
                                                                               ,@gender
                                                                               ,@email
                                                                               ,@phone
                                                                               ,@address
                                                                               ,@position
                                                                               ,@profile
                                                                               ,@LoginIdx
                                                                               ,@LoginId
                                                                               ,@LoginPw)";

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
                                                                     FROM  [dbo].[employee]";

            public static readonly string DELETE_QUERY = @"DELETE FROM [dbo].[employee]";


        }

        public class result
        {
            public int plastic { get; set; }
            public int paper { get; set; }
            public int can { get; set; }

            public static readonly string RESULT_INSERT_QUERY = @"INSERT INTO [dbo].[result]
                                                                             ([plastic]
                                                                            ,[paper]
                                                                            ,[can])
                                                                        VALUES
                                                                             (plastic, int
                                                                            ,paper, int
                                                                            ,can, int,)";

            public static readonly string EMPLOYEE_SELECT_QUERY = @"SELECT [plastic]
                                                                         , [paper]
                                                                         , [can]
                                                                      FROM [dbo].[result]";

            public static readonly string DELETE_QUERY = @"DELETE FROM [dbo].[result]";
>>>>>>> Stashed changes

        }
    }
}
