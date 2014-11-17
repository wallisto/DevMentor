using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;

namespace WF45
{

    class Program
    {
        static void Main(string[] args)
        {
            Activity workflow1 = new Workflow1();
            var invoker = new WorkflowInvoker(workflow1);

            invoker.Extensions.Add(new StartsWithFilter("s"));
            invoker.Invoke();
        }
    }
}
