using NoSQLProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new ServiceHost(typeof(Service));
            host.Open();

            Console.WriteLine("Server started. The address is "
                + host.Description.Endpoints.First().Address.Uri.ToString() + "."
                + Environment.NewLine + "Press any key to close.");
            Console.ReadKey(true);

            host.Close();
        }
    }
}
