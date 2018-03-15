/*
Author: Lalo ¬¬
Requires having sqllocaldb installed
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Dinamitec.InstanceSqlLocalDb.Models;

namespace Dinamitec.InstanceSqlLocalDb
{
    public class BuildInstance
    {
        LocalInstance local;

        public BuildInstance()
        {
            local = new LocalInstance();
        }

        public BuildInstance SetInstance(string instance)
        {
            local.InstanceName = instance;
            return this;
        }

        public BuildInstance SetLocation(string location)
        {
            local.Location = location;
            return this;
        }

        public BuildInstance Build()
        {
            using (Process cmd = new Process())
            {
                cmd.StartInfo.RedirectStandardOutput = true;
                cmd.StartInfo.UseShellExecute = false;
                cmd.StartInfo.FileName = "sqllocaldb";
                cmd.StartInfo.Arguments = $"create {local.InstanceName}";
                cmd.Start();
            }
            return this;
        }

    }
}
