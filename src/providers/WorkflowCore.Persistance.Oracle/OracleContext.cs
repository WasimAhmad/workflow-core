using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oracle.EntityFrameworkCore.Infrastructure;
using System;
using WorkflowCore.Persistence.EntityFramework.Models;
using WorkflowCore.Persistence.EntityFramework.Services;

namespace WorkflowCore.Persistance.Oracle
{
    public class OracleContext : WorkflowDbContext
    {
        private string _connectionString;
        private Action<OracleDbContextOptionsBuilder> _oracleOptionsAction;

        public OracleContext(string connectionString, Action<OracleDbContextOptionsBuilder> oracleOptionsAction = null)
        {
            this._connectionString = connectionString;
            this._oracleOptionsAction = oracleOptionsAction;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseOracle(_connectionString, _oracleOptionsAction);
        }

        protected override void ConfigureEventStorage(EntityTypeBuilder<PersistedEvent> builder)
        {
            builder.ToTable("Event");
            builder.Property(x => x.PersistenceId).ValueGeneratedOnAdd();
        }

        protected override void ConfigureExecutionErrorStorage(EntityTypeBuilder<PersistedExecutionError> builder)
        {
            builder.ToTable("ExecutionError");
            builder.Property(x => x.PersistenceId).ValueGeneratedOnAdd();
        }

        protected override void ConfigureExecutionPointerStorage(EntityTypeBuilder<PersistedExecutionPointer> builder)
        {
            builder.ToTable("ExecutionPointer");
            builder.Property(x => x.PersistenceId).ValueGeneratedOnAdd();
        }

        protected override void ConfigureExetensionAttributeStorage(EntityTypeBuilder<PersistedExtensionAttribute> builder)
        {
            builder.ToTable("ExtensionAttribute");
            builder.Property(x => x.PersistenceId).ValueGeneratedOnAdd();
        }

        protected override void ConfigureSubscriptionStorage(EntityTypeBuilder<PersistedSubscription> builder)
        {
            builder.ToTable("Subscription");
            builder.Property(x => x.PersistenceId).ValueGeneratedOnAdd();
        }

        protected override void ConfigureWorkflowStorage(EntityTypeBuilder<PersistedWorkflow> builder)
        {
            builder.ToTable("Workflow");
            builder.Property(x => x.PersistenceId).ValueGeneratedOnAdd();
        }
    }
}
