using MyCouch;
using MyCouch.Requests;
using MyCouch.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace NoSQLProject
{
    public class Service : IService
    {
        private static string _dbAddress = "http://10.145.128.97:5555/";

        public string GetJsonDocumentById(string baseName, string id)
        {
            DocumentResponse queryResult = null;
            // Connexion à une base
            using (var client = new MyCouchClient(_dbAddress + baseName))
            {
                // Récup d'un document par son ID
                queryResult = client.Documents.GetAsync(new GetDocumentRequest(id)).Result;
            }
            // Conversion du résultat en dictionnaire (hashtable)
            if (queryResult == null)
                return null;

            return queryResult.Content;
        }

        public List<string> GetJsonDocumentsByTitle(string baseName, string title)
        {
            ViewQueryResponse viewQueryResult = null;

            // Connexion à une base
            using (var client = new MyCouchClient(_dbAddress + baseName))
            {
                // Récup d'un document par son titre
                var viewQuery = new QueryViewRequest("viewtest", "test2").Configure(q => q.Key(title));
                viewQueryResult = client.Views.QueryAsync(viewQuery).Result;
            }
            // Conversion du résultat en dictionnaire (hashtable)
            if (viewQueryResult == null || viewQueryResult.Rows == null)
                return null;

            List<string> documents = new List<string>();
            foreach (var element in viewQueryResult.Rows.ToList())
                documents.Add(element.Value);

            return documents;
        }
        
        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
