using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.Users;

namespace WebAPi.Repositories
{
    public class AdminRepository
    {
        private readonly Container _container;

        public AdminRepository(CosmosClient cosmosClient, string databaseName, string containerName)
        {
            _container = cosmosClient.GetContainer(databaseName, containerName);
        }

        public async Task<IEnumerable<AdminDTO>> GetAdminUsersAsync()
        {
            var query = _container.GetItemQueryIterator<AdminDTO>(new QueryDefinition("SELECT * FROM c"));
            var results = new List<AdminDTO>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task<AdminDTO> GetAdminUsersAsyncByID(string id)
        {
            try
            {
                var response = await _container.ReadItemAsync<AdminDTO>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<AdminDTO> GetAdminUsersAsyncByEmail(string email)
        {
            try
            {
                var query = new QueryDefinition("SELECT * FROM c WHERE c.email = @Email")
                    .WithParameter("@Email", email);

                var iterator = _container.GetItemQueryIterator<AdminDTO>(query);

                var response = new List<AdminDTO>();

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

        public async Task<AdminDTO> CreateAdminUsersAsync(AdminDTO admin)
        {

            try
            {
                var response = await _container.CreateItemAsync(admin, null);

                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                return null;
            }
        }

        public async Task<AdminDTO> UpdateAdminUsersAsync(string id, AdminDTO admin)
        {
            var response = await _container.UpsertItemAsync(admin, new PartitionKey(id));
            return response.Resource;
        }

        public async Task DeleteAdminDTOAsync(string id)
        {
            await _container.DeleteItemAsync<AdminDTO>(id, new PartitionKey(id));
        }
    }
}