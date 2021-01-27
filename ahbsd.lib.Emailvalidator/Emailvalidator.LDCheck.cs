using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using RestSharp;
using Whois;
using Whois.Servers;
using Walter.Net;
using Walter.Net.Networking;
using Walter.Net.Networking.DNS;
using Walter.Net.Networking.IANA;
using System.Globalization;
using System.IO;
using Newtonsoft.Json;
using RestSharp.Serialization.Json;

namespace ahbsd.lib.Emailvalidator
{
    /// <summary>
    /// Class for an Email-Adress.
    /// </summary>
    public partial class EMailAdress
    {

        readonly static string PathToCultureList; 
        /// <summary>
        /// Gets a List of Top Level Domains.
        /// </summary>
        protected static List<string> TLDs { get; private set; }
        /// <summary>
        /// Gets a Dictionary of available Cultures for globalized Messanges.
        /// </summary>
        /// <value>A Dictionary of available Cultures.</value>
        protected static Dictionary<CultureTypes, IDictionary<string,string>> Cultures { get; private set; }

        /// <summary>
        /// Static constructor.
        /// </summary>
        [Globalizable(true)]
        static EMailAdress()
        {
            string fullPath;
            PathToCultureList = Path.GetFullPath("../coltureLists/");

            fullPath = TestPath();

            Cultures = new Dictionary<CultureTypes, IDictionary<string,string>>();
            TLDs = new List<string>();

            GetCultures(fullPath);
            GetTLDs();
        }

        private static string TestPath()
        {
            string result = PathToCultureList;
            DirectoryInfo tmp;

            if (!Directory.Exists(PathToCultureList))
            {
                try
                {
                    tmp = Directory.CreateDirectory(PathToCultureList);

                    result = tmp.FullName;
                }
                catch (Exception)
                { }
            }

            return result;
        }

        /// <summary>
        /// Gets all known Topleveldomains from IANA.
        /// </summary>
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

        private static void GetCultures(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            IEnumerable<FileInfo> files = di.EnumerateFiles();
            StreamReader reader;
            string tmpString;
            object tmpObject;
            CultureInfo tmpCulture;

            foreach (FileInfo file in files)
            {
                if (file.Extension.ToLower().Equals(".json"))
                {
                    reader = file.OpenText();

                    tmpString = reader.ReadToEnd();
                    reader.Close();
                    tmpObject = JsonConvert.DeserializeObject(tmpString);

                    if (tmpObject != null)
                    {

                    }

                }
            }
        }

        public static bool CheckDomain(string fullDomain, out Exception exception)
        {
            bool result = false;
            string dnsTmp;
#if DEBUG
            // not really needed but nice for debugging and testing.
            IList<string> servers, stati;

            IWhoisServerLookup wsl;
            WhoisResponse response;
#endif
            WhoisLookup lookup = new WhoisLookup();
            lookup.Options.FollowReferrer = true;
            lookup.Options.TimeoutSeconds = 20;

            exception = null;

            try
            {
                response = lookup.Lookup(fullDomain);
                result = response.Status == Whois.WhoisStatus.Found;
#if DEBUG
                wsl = lookup.ServerLookup;

                dnsTmp = response.DnsSecStatus;
                servers = response.NameServers;
                stati = response.DomainStatus;
#endif
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            return result;
        }

        public static bool CheckDomain(string[] domainParts, out Exception exception)
        {
            string tmp = string.Join('.', domainParts);

            return CheckDomain(tmp, out exception);
        }
    }
}