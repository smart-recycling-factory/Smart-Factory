using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFactory.Models
{
    public class Result
    {
        public int Plastic { get; set; }
        public int Paper { get; set; }
        public int Can { get; set; }

        public static readonly string RESULT_INSERT_QUERY = @"INSERT INTO [dbo].[result]
                                                                        ( [plastic]
                                                                        , [paper]
                                                                        , [can])
                                                                   VALUES
                                                                        ( plastic
                                                                        , paper
                                                                        , can)";

        public static readonly string RESULT_SELECT_PLASTIC = @"SELECT [plastic]
                                                                  FROM [dbo].[result]";

        public static readonly string RESULT_SELECT_PAPER = @"SELECT [paper]
                                                                FROM [dbo].[result]";

        public static readonly string RESULT_SELECT_CAN = @"SELECT [can]
                                                              FROM [dbo].[result]";

        public static readonly string DELETE_QUERY = @"DELETE FROM [dbo].[result]";
    }
}
