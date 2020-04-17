﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using WorkflowCore.Users.Models;

namespace WorkflowCore.SampleWebApp
{
    public class HumanWorkflow : IWorkflow<MyData>
    {
        public string Id => "HumanWorkflow";

        public int Version => 1;

        void IWorkflow<MyData>.Build(IWorkflowBuilder<MyData> builder)
        {
            builder
                .StartWith(context => ExecutionResult.Next())
                .UserTask("Do you approve", data => @"domain\bob")
                    .WithOption("yes", "I approve").Do(then => then
                        .StartWith(context => Console.WriteLine("You approved"))
                    )
                    .WithOption("no", "I do not approve").Do(then => then
                        .StartWith(context => Console.WriteLine("You did not approve"))
                    )
                    .WithEscalation(x => TimeSpan.FromSeconds(20), x => @"domain\frank", action => action
                        .StartWith(context => Console.WriteLine("Escalated task"))
                        .Then(context => Console.WriteLine("Sending notification..."))
                        )
                .Then(context => Console.WriteLine("end"));
        }
    }
}

