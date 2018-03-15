using Dinamitec.InstanceSqlLocalDb.Models;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinamitec.InstanceSqlLocalDb
{
    public class BuildDatabase
    {
        private LocalInstance local;
        private Process process;

        public BuildDatabase()
        {
            local = new LocalInstance();
            process = new Process();
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
        }
    
        public BuildDatabase SetInstance(string instance)
        {
            local.InstanceName = instance;
            return this;
        }

        public BuildDatabase SetLocation(string location)
        {
            local.Location = location;
            return this;
        }

        public int BuildLocationScript(string urlScript)
        {
            string script = File.ReadAllText(urlScript);
            string sqlConnection = string.Format("server={0}\\{1}",local.Location,local.InstanceName);
            SqlConnection con = new SqlConnection(sqlConnection);
            Server server = new Server(new ServerConnection(con));

            int result = server.ConnectionContext.ExecuteNonQuery(script);

            return result;
        }
    }
}
