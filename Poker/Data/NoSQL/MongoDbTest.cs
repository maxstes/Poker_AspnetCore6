using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Diagnostics;

namespace Poker.Data.NoSQL
{
    public static class MongoDbTest
    {
        public async static Task<bool> IsTest()
        {
            var logger = GetLogger();  // Get ILogger 
            var collection = GetCollection();// Get collection for test

            try
            { 
                List<BsonDocument> users = await collection.Find(new BsonDocument()).ToListAsync();
                logger.LogInformation("Successfully connected to MongoDB");
                return false;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error connection to MongoDb {ex.Message}");
                return true;
            }
        }
        private static ILogger GetLogger()
        {
            using ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            ILogger logger = loggerFactory.CreateLogger("MongoDbTest");

            return logger;
        }
        public static void StartServer()
        {
            string filePath = "C:\\mongodb\\bin\\mongod.exe";

            ProcessStartInfo info = new() 
            { 
                FileName= filePath,
                UseShellExecute = true,
                CreateNoWindow = false
            };

            using Process process = new Process { StartInfo = info };
            process.Start();
            
        }
        private static IMongoCollection<BsonDocument> GetCollection()
        {
            MongoClient client = new();
            var database = client.GetDatabase("test");
            var collection = database.GetCollection<BsonDocument>("cards");

            return collection;
        }


    }
}
