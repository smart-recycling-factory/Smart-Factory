using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace teamproject4.Models
{
    class Result
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

        public static readonly string RESULT_SELECT_PLASTIC = @"SELECT [plastic]
                                                                      FROM [dbo].[result]";
        public static readonly string RESULT_SELECT_PAPER = @"SELECT [paper]
                                                                      FROM [dbo].[result]";
        public static readonly string RESULT_SELECT_CAN = @"SELECT [can]
                                                                      FROM [dbo].[result]";

        public static readonly string DELETE_QUERY = @"DELETE FROM [dbo].[result]";

    }
}
