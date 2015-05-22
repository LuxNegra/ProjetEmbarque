using NoSQLProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var channelFactory = new ChannelFactory<IService>("ServerBinding");
            var service = channelFactory.CreateChannel();

            var results = service.GetJsonDocumentById("test", "8da4c108fcf9fc6a1b8c4f6792000450");
            //var results = service.GetJsonDocumentsByTitle("test", "huhuhu");

            if (!String.IsNullOrEmpty(results))
                Console.WriteLine(results);

            //if (results != null)
            //    foreach (var element in results)
            //        Console.WriteLine(element);

            Console.WriteLine("Test over. Press any key to close.");

            Console.ReadKey(true);
        }
    }
}
