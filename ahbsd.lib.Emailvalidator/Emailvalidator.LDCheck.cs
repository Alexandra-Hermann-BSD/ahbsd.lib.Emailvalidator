using System;
using System.Collections.Generic;
using RestSharp;

namespace ahbsd.lib.Emailvalidator
{
    /// <summary>
    /// Class for an Email-Adress.
    /// </summary>
    public partial class EMailAdress
    {
        /// <summary>
        /// Gets or sets a List of Top Level Domains.
        /// </summary>
        protected static List<string> TLDs { get; private set; }

        static EMailAdress()
        {
            TLDs = new List<string>();
            GetTLDs();
        }

        private static void GetTLDs()
        {
            IRestClient client = new RestClient("https://data.iana.org/TLD/");
            IRestRequest request = new RestRequest("tlds-alpha-by-domain.txt");
            IRestResponse response = client.Get(request);
            string[] tmp;

            if (response.IsSuccessful)
            {
                tmp = response.Content.Split('\n');

                TLDs = new List<string>(tmp.Length - 1);

                for (int i = 1; i < tmp.Length; i++)
                {
                    TLDs.Add(tmp[i]);
                }
            }
        }
    }
}