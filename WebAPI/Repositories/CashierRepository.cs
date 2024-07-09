using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Models.Users;

namespace WebAPi.Repositories
{
    public class CashierRepository
    {
        private readonly Container _container;

        public CashierRepository(CosmosClient cosmosClient, string databaseName, string containerName)
        {
            _container = cosmosClient.GetContainer(databaseName, containerName);
        }

        public async Task<IEnumerable<CashierDTO>> GetCashierUsersAsync()
        {
            var query = _container.GetItemQueryIterator<CashierDTO>(new QueryDefinition("SELECT * FROM c"));
            var results = new List<CashierDTO>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task<CashierDTO> GetCashierUsersAsyncByID(string id)
        {
            try
            {
                var response = await _container.ReadItemAsync<CashierDTO>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<CashierDTO> GetCashierUsersAsyncByEmail(string email)
        {
            try
            {
                var query = new QueryDefinition("SELECT * FROM c WHERE c.email = @Email")
                    .WithParameter("@Email", email);

                var iterator = _container.GetItemQueryIterator<CashierDTO>(query);

                var response = new List<CashierDTO>();

                while (iterator.HasMoreResults)
                {
                    response.AddRange(await iterator.ReadNextAsync());
                }

                if (response.Count == 0)
                {
                    return null;
                }
                else
                {
                    return response.First();
                }
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<CashierDTO> CreateCashierUsersAsync(CashierDTO cashier)
        {

            try
            {
                var response = await _container.CreateItemAsync(cashier, null);

                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                return null;
            }
        }

        public async Task<CashierDTO> UpdateCashierUsersAsync(string id, CashierDTO cashier)
        {
            var response = await _container.UpsertItemAsync(cashier, new PartitionKey(id));
            return response.Resource;
        }

        public async Task DeleteCashierDTOAsync(string id)
        {
            await _container.DeleteItemAsync<CashierDTO>(id, new PartitionKey(id));
        }
    }
}