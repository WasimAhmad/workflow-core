using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading;
using WorkflowCore.Interface;

namespace WorkflowCore.SampleOracle
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceProvider serviceProvider = (Microsoft.Extensions.DependencyInjection.ServiceProvider)ConfigureServices();

            //start the workflow host
            var host = serviceProvider.GetService<IWorkflowHost>();
            host.RegisterWorkflow<HumanWorkflow>();
            host.Start();


            Console.WriteLine("Starting workflow...");
            string workflowId = host.StartWorkflow("HumanWorkflow").Result;

            var timer = new Timer(new TimerCallback((state) => { PrintOptions(host, workflowId); }), null, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(10));

            Thread.Sleep(1000);
            Console.WriteLine();
            Console.WriteLine("Open user actions are");
            var openItems = host.GetOpenUserActions(workflowId);
            foreach (var item in openItems)
            {
                Console.WriteLine(item.Prompt + ", Assigned to " + item.AssignedPrincipal);
                Console.WriteLine("Options are ");
                foreach (var option in item.Options)
                {
                    Console.WriteLine(" - " + option.Key + " : " + option.Value + ", ");
                }

                //Thread.Sleep(500);

                var input = Console.ReadLine();
                Console.WriteLine();

                string key = item.Key;
                string value = item.Options.Single(x => x.Value == input).Value;

                Console.WriteLine("Choosing key:" + key + " value:" + value);

                host.PublishUserAction(key, @"domain\john", value).Wait();
            }
            Thread.Sleep(1000);
            Console.WriteLine("Open user actions left:" + host.GetOpenUserActions(workflowId).Count().ToString());
            timer.Dispose();
            timer = null;
            Console.WriteLine("Workflow ended.");
            Console.ReadLine();
            host.Stop();
        }
        private static void PrintOptions(IWorkflowHost host, string workflowId)
        {
            var openItems = host.GetOpenUserActions(workflowId);
            foreach (var item in openItems)
            {
                Console.WriteLine(item.Prompt + ", Assigned to " + item.AssignedPrincipal);
                Console.WriteLine("Options are ");
                foreach (var option in item.Options)
                {
                    Console.WriteLine(" - " + option.Key + " : " + option.Value + ", ");
                }
            }
        }
        private static IServiceProvider ConfigureServices()
        {
            //setup dependency injection
            IServiceCollection services = new ServiceCollection();
            services.AddLogging();
            //services.AddWorkflow();
            services.AddWorkflow(x => x.UseOracle(@"Data Source=localhost:1522/db19c;User Id=WorkFlowEngine;Password=WorkFlowEngine", false, true));
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }
    }
}
