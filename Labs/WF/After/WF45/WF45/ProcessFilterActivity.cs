using System.Activities;
using System.Diagnostics;

namespace WF45
{
    public class ProcessFilterActivity : CodeActivity
    {
        public InArgument<Process> Process { get; set; }
        public OutArgument<bool> IsMatch { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var filter = context.GetExtension<ProcessFilterExtension>();

            if(filter == null)
            {
                IsMatch.Set(context, true);
            }
            else
            {
                IsMatch.Set(context, filter.IsMatchingProcess(Process.Get(context)));
            }
        }
    }
}