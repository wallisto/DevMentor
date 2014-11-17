using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Services.Test
{
    [TestClass]
    public class SMSServicesTests
    {
        [TestMethod]
        public void Send_WhenInvoked_ShouldSendOnceForEachContact()
        {
            var mocks = new MockRepository();

            Contact[] contacts = new[]
                                     {
                                         new Contact{FirstName = "", CellPhone = ""},
                                         new Contact{FirstName = "", CellPhone = ""},
                                         new Contact{FirstName = "", CellPhone = ""},
                                         new Contact{FirstName = "", CellPhone = ""},
                                         new Contact{FirstName = "", CellPhone = ""},
                                     };
            var gateway = mocks.StrictMock<ISMSGateway>();

            Expect.Call(() => gateway.Send(null, null))
                .IgnoreArguments()
                .Repeat
                .Times(contacts.Length);

            var sms = new SMSService(gateway);

            mocks.ReplayAll();

            sms.Send(contacts, "foo {0}");

            mocks.VerifyAll();
        }

        [TestMethod]
        public void Send_WhenInvoked_ShouldSendToEachContact()
        {
            var mocks = new MockRepository();
            string phone = "123";
            var contact = new Contact {FirstName = "Rich", CellPhone = phone};
            string msgTemplate = "Foo {0}";
            string expectedMessage = "Foo Rich";
                                     
            var gateway = mocks.StrictMock<ISMSGateway>();

            Expect.Call(() => gateway.Send(phone, expectedMessage));

            var sms = new SMSService(gateway);

            mocks.ReplayAll();

            sms.Send(new Contact[]{contact}, msgTemplate);

            mocks.VerifyAll();
        }
    }
}
