using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowCore.Persistance.Oracle
{
    public class MigrationContextFactory : IDesignTimeDbContextFactory<OracleContext>
    {
        public OracleContext CreateDbContext(string[] args)
        {
            return new OracleContext(@"Data Source=localhost:1522/db19c;User Id=WorkFlowEngine;Password=WorkFlowEngine");
        }
    }
}
