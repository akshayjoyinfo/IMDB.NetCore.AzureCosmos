using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApp.Domain.Interfaces;
using WebApp.Helpers;

namespace WebApp.Domain.Modules
{
    public class CosmosService<T> : ICosmosService<T>
    {
        private readonly IConfiguration _configuration;
        private readonly string COSMOS_ACCOUNT;
        private readonly string COSMOS_PRIMARY_KEY;
        private readonly string COSMOS_DB;
        private Container collectionContainer;
        private string PartitionKey;

        public CosmosService(IConfiguration configuration)
        {
            _configuration = configuration;
            var cosmosConfigSection = configuration.GetSection("CosmosDb");

            COSMOS_ACCOUNT = cosmosConfigSection.GetSection("Account").Value;
            COSMOS_PRIMARY_KEY = cosmosConfigSection.GetSection("Key").Value;
            COSMOS_DB = cosmosConfigSection.GetSection("DatabaseName").Value;

            InitializeCosmos();
        }

        private void InitializeCosmos()
        {
            CosmosClientBuilder clientBuilder = new CosmosClientBuilder(COSMOS_ACCOUNT, COSMOS_PRIMARY_KEY);
            CosmosClient dbClient = clientBuilder
                                .WithConnectionModeDirect()
                                .Build();
            DatabaseResponse database = dbClient.CreateDatabaseIfNotExistsAsync(COSMOS_DB).GetAwaiter().GetResult();

            var collectionName = typeof(T).GetAttributeValue((CosmosCollectionAttribute attr) => attr.Container);

            var partitionKey = typeof(T).GetProperties().Where(p => p.GetCustomAttributes(false).Where(a => a.GetType() == typeof(KeyAttribute)).Count() > 0).FirstOrDefault();
            database.Database.CreateContainerIfNotExistsAsync(collectionName, $"/{partitionKey.Name.ToLower()}").GetAwaiter().GetResult();
            PartitionKey = partitionKey.Name;

            collectionContainer = dbClient.GetContainer(COSMOS_DB, collectionName);

        }

        public async Task<T> AddAsync(T item)
        {
           // var nameOfProperty = PartitionKey.ToUpper();
            var propertyInfo = item.GetType().GetProperty(PartitionKey);
            var partitionKeyValue = propertyInfo.GetValue(item, null);

            var response = await collectionContainer.CreateItemAsync<T>(item, new PartitionKey(partitionKeyValue.ToString()));
            return response.Resource;
        }

        public async Task DeleteAsync(string id)
        {
            await collectionContainer.DeleteItemAsync<T>(id, new PartitionKey(PartitionKey.ToString()));
        }

        public Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            try
            {
                ItemResponse<T> response = await collectionContainer.ReadItemAsync<T>(id, new PartitionKey(PartitionKey));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return default;
            }
        }

        public async Task<IEnumerable<T>> GetQueryAsync(string queryString)
        {
            var query = collectionContainer.GetItemQueryIterator<T>(new QueryDefinition(queryString));
            List<T> results = new List<T>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task UpdateAsync(string id, T item)
        {
            await collectionContainer.UpsertItemAsync<T>(item, new PartitionKey(PartitionKey));
        }
    }
}
