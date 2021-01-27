using System;
using ahbsd.lib.Emailvalidator;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            IEMailAdress[] adresses = new IEMailAdress[3];

            adresses[0] = EMailAdress.GetEmailAdress("alex.hermann@icloud.com");
            adresses[1] = EMailAdress.GetEmailAdress("alex.hermann@test1.hermann-bsd.de");
            adresses[2] = EMailAdress.GetEmailAdress("alex.hermann@test2.test1.hermann-bsd.de");

            string adr = adresses[0].GetJSon();
            adr = adresses[1].GetJSon();
            adr = adresses[2].GetJSon();
        }
    }
}
