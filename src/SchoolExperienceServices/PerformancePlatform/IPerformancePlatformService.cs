using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolExperienceServices.PerformancePlatform
{
    public interface IPerformancePlatformService
    {
        Task UpdateAsync(string name, IEnumerable<PerformancePlatformMetric> metrics);
        Task EmptyAsync(string name);
    }
}
