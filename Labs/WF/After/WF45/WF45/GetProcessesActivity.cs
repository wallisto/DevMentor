using System.Activities;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace WF45
{
    public class GetProcessesActivity : CodeActivity
    {
        public OutArgument<List<Process>> Processes { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            Processes.Set(context, Process.GetProcesses().ToList());
        }
    }
}