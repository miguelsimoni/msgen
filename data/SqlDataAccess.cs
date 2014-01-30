using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
//using Microsoft.SqlServer;
//using Microsoft.SqlServer.Management.Common;
//using Microsoft.SqlServer.Management.Smo;

namespace msgen.data
{
    public class SqlDataAccess : DataAccess
    {

        public SqlDataAccess(string connectionString) : base(connectionString)
        {
            base.connetion = new SqlConnection(connectionString);
            base.command = new SqlCommand();
            base.dataAdapter = new SqlDataAdapter();
        }

        public bool executeScriptFile(string script)
        {
            //SqlConnection connection = new SqlConnection(this.ConnectionString);
            //Server server = new Server(new ServerConnection(connection));
            //server.ConnectionContext.ExecuteNonQuery(script);

            return false;
        }

    }
}
