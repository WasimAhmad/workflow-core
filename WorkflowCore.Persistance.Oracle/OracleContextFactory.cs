using Oracle.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowCore.Persistence.EntityFramework.Interfaces;
using WorkflowCore.Persistence.EntityFramework.Services;

namespace WorkflowCore.Persistance.Oracle
{
    public class OracleContextFactory : IWorkflowDbContextFactory
    {
        private readonly string _connectionString;
        private readonly Action<OracleDbContextOptionsBuilder> _oracleOptionsAction;

        public OracleContextFactory(string connectionString, Action<OracleDbContextOptionsBuilder> oracleOptionsAction = null)
        {
            _connectionString = connectionString;
            _oracleOptionsAction = oracleOptionsAction;
        }
        public WorkflowDbContext Build()
        {
            return new OracleContext(_connectionString, _oracleOptionsAction);
        }
    }
}
