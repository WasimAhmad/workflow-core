using Oracle.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using WorkflowCore.Persistance.Oracle;
using WorkflowCore.Persistence.EntityFramework.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static WorkflowOptions UseOracle(this WorkflowOptions options, string connectionString, bool canCreateDB, bool canMigrateDB, Action<OracleDbContextOptionsBuilder> oracleOptionsAction = null)
        {
            options.UsePersistence(sp => new EntityFrameworkPersistenceProvider(new OracleContextFactory(connectionString, oracleOptionsAction), canCreateDB, canMigrateDB));
            options.Services.AddTransient<IWorkflowPurger>(sp => new WorkflowPurger(new OracleContextFactory(connectionString, oracleOptionsAction)));
            return options;
        }
    }
}
