using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.IO;
using MongoDBBloggerPost.Model;
using StackExchange.Redis;
using Newtonsoft.Json;

namespace MongoDBBloggerPost.Services
{
    public class Client
    {
        private readonly string _hostname;
        private readonly int _port;
        private readonly string _password;
        private ConnectionMultiplexer _redis;

        public Client(string hostname, int port, string password)
        {
            _hostname = hostname;
            _port = port;
            _password = password;
        }

        public void Connect()
        {
            _redis = ConnectionMultiplexer.Connect($"{_hostname}:{_port},password={_password}");
        }

        public IDatabase GetDatabase()
        {
            return _redis.GetDatabase();
        }

        public List<PostsModel> GetPosts(string id)
        {
            var posts = GetDatabase().StringGet(id);
            if(posts.HasValue)
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<PostsModel>>(posts.ToString());
            }
            return new List<PostsModel>();
        }

        public void SavePosts(string id, List<PostsModel> post)
        {
            GetDatabase().StringSet(id, Newtonsoft.Json.JsonConvert.SerializeObject(post));
        }
    }
}