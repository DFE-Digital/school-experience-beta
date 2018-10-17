using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SchoolExperienceCandidateDataServices.Dto;

namespace SchoolExperienceCandidateDataServices.Implementation
{
    internal class GitisServices : ICandidateDataServices
    {
        private readonly GitisServicesOptions _options;

        public GitisServices(IOptions<GitisServicesOptions> options)
        {
            _options = options.Value;
        }

        public Task<SignInResult> SignIn(string credentials)
        {
            var connection = new DemoService(_options.ServiceUri);
            var products = connection.Products.SingleOrDefault(x => x.Name == credentials);

            return Task.FromResult(new SignInResult());
        }
    }
}
