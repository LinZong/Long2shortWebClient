using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MongoEntry
{

    public class MongoConnection
    {
        static string ConnectString = null;
        public static string DatabaseName = null;
        public static string CollectionName = null;
        static MongoConnection _instance;
        private static IMongoClient _client = null;
        private MongoConnection()
        {
            if (ConnectString == null)
            {
                BuildConnectString();
            }
            try
            {
                var client = new MongoClient(ConnectString);
                if (client != null) _client = client;
            }
            catch (MongoConnectionException)
            {

                System.Console.WriteLine("Connection Failed");
            }
            catch
            {
                System.Console.WriteLine("Unknown Error.May be bad connString");
            }
        }
        public static IMongoClient GetDatabaseConnection()
        {
            if (_instance == null)
            {
                _instance = new MongoConnection();
            }
            return _client;
        }
        public static IMongoDatabase GetDatabaseContextHandler(string SpecifiedDatabaseName)
        {
            if (_instance == null)
            {
                _instance = new MongoConnection();
            }
            return _client.GetDatabase(SpecifiedDatabaseName);
        }
        public static bool BuildConnectString()
        {
            try
            {
                StreamReader sr = new StreamReader("MongoConfig.json", Encoding.Default);
                string JsonString = sr.ReadToEnd();
                JObject jObject = (JObject)JsonConvert.DeserializeObject(JsonString);
                DatabaseName = jObject["Database"].ToString();
                CollectionName = jObject["Collection"].ToString();
                //Assign 2 prop;
                //ConnnetString Sample is : mongodb://username:password@serveraddress/databaseName
                string connstring = "mongodb://{0}:{1}@{2}/{3}";
                ConnectString = string.Format(connstring, jObject["Username"].ToString(), jObject["Password"].ToString(), jObject["ServerAddress"].ToString(), jObject["Database"].ToString());
                return true;
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("The connect string can not be built due to lacking arguments");
                return false;
            }
            catch(FormatException)
            {
                Console.WriteLine("The connect string can not be built.Your MongoConfig.json may have some problems");
                return false;
            }
            catch
            {
                Console.WriteLine("Unknown error.");
                return false;
            }
        }
    }

}
       

