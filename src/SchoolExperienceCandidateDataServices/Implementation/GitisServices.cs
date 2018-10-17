using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
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

        public Task<SignInResult> SignIn(string credentials)
        {
            var connection = CreateServiceConnection();
            var candidateDetails = connection.Persons
                .Select(a =>
                    new SignInResult
                    {
                        Name = a.Name,
                        Id = a.ID.ToString()
                    })
                .SingleOrDefault(x => x.Name == credentials);

            return Task.FromResult(candidateDetails);
        }

        public async Task<UpdateResult> UpdateName(string id, string name)
        {
            var connection = CreateServiceConnection();
            var candidateDetails = connection.Persons
                .SingleOrDefault(x => x.ID == int.Parse(id));

            if (candidateDetails != null)
            {
                candidateDetails.Name = name;
                await connection.SaveChangesAsync();
            }

            return new UpdateResult();
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
