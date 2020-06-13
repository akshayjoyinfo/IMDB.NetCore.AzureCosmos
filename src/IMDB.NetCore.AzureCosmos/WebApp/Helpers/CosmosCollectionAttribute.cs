using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Helpers
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CosmosCollectionAttribute :Attribute
    {
        public string Container { get; set; }
    }
}
