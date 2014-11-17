using System;

namespace ThirdParty
{
    public class SMSGateway
    {
        public void Send(string to, string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("{0} ... {1}", to, message);
            Console.ResetColor();
        }

    }
}