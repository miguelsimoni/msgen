using System;
using System.Collections.Generic;
using System.Text;

namespace msgen.data
{
    public class DataAccessFactory
    {
        public static IDataAccess CreateDataAccess(DataAccessType type, string connectionString)
        {
            switch (type)
            {
                case DataAccessType.OleDb:
                    return new OleDbDataAccess(connectionString);
                case DataAccessType.SqlServer:
                    return new SqlDataAccess(connectionString);
                default:
                    throw new ArgumentException("Invalid DataAccess Type.");
            }
        }

    }
}
