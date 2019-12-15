using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Data
{
    public interface IMongoSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }

    //property dependancy (vanuit het API project invullen)
    public class MongoSettings : IMongoSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

    }
}
