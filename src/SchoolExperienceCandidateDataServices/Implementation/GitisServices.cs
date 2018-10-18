using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.OData.Client;
using SchoolExperienceCandidateDataServices.Dto;

namespace SchoolExperienceCandidateDataServices.Implementation
{
    internal class GitisServices : ICandidateDataServices
    {
        private readonly GitisServicesOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="GitisServices"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public GitisServices(IOptions<GitisServicesOptions> options)
        {
            _options = options.Value;
        }

        public async Task<SignInResult> SignIn(string credentials)
        {
            var connection = CreateServiceConnection();
            var candidateDetails = connection.Persons
                .Where(x => x.Name == credentials)
                .Select(a =>
                    new SignInResult
                    {
                        Name = a.Name,
                        Id = a.ID.ToString()
                    })
            ;

            var result = await ExecuteQueryAsync(candidateDetails);
            return result.SingleOrDefault();
        }

        private async Task<IEnumerable<T>> ExecuteQueryAsync<T>(IQueryable<T> query2)
        {
            var query = (DataServiceQuery<T>)query2;

            var taskFactory = new TaskFactory<IEnumerable<T>>();
            var result = await taskFactory.FromAsync(query.BeginExecute(null, null), iar => query.EndExecute(iar));
            return result.ToList();
        }

        public async Task<UpdateResult> UpdateName(string id, string name)
        {
            var connection = CreateServiceConnection();
            var query = connection.Persons
                .Where(x => x.ID == int.Parse(id))
                .Select(p => p);
            var queryResult = await ExecuteQueryAsync(query);
            var candidate = queryResult.SingleOrDefault();

            if (candidate != null)
            {
                candidate.Name = name;
                connection.UpdateObject(candidate);
                await connection.SaveChangesAsync();
            }

            return new UpdateResult { IsSuccessful = candidate != null };
        }

        private DemoService CreateServiceConnection()
        {
            var connection = new DemoService(_options.ServiceUri)
            {
                Credentials = new NetworkCredential
                {
                    UserName = _options.UserName,
                    Password = _options.Password,
                }
            };

            return connection;
        }
    }
}
