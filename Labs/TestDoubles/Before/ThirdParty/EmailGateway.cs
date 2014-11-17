using System;

namespace ThirdParty
{
    public class EmailGateway
    {
        public void Send(string from, string to, string subject, string message)
        {
            Console.WriteLine("From: {0}\r\nTo: {1}\r\nRe: {2}\r\n\r\n{3}\r\n----------------\r\n", from, to, subject, message);
        }
    }

    
}