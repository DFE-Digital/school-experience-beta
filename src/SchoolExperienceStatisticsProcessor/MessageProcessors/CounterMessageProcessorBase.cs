using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolExperienceProcessorShared;
using SchoolExperienceStatisticsData;
using SchoolExperienceStatisticsData.Entities;

namespace SchoolExperienceStatisticsProcessor.MessageProcessors
{
    internal abstract class CounterMessageProcessorBase<T> : MessageProcessorBase<T>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger _logger;

        protected CounterMessageProcessorBase(ApplicationDbContext dbContext, ILogger logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        protected async Task AddCounter(string counterName)
        {
            var counter = await _dbContext.Counters.FirstOrDefaultAsync(x => x.Name == counterName);
            if (counter == null)
            {
                counter = new Counter
                {
                    Name = counterName,
                };
                _dbContext.Add(counter);
            }

            var saved = false;
            while (!saved)
            {
                try
                {
                    counter.Count++;
                    counter.LastUpdated = DateTime.UtcNow;
                    await _dbContext.SaveChangesAsync();
                    saved = true;
                    _logger.LogTrace($"{counterName}={counter.Count}");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    _logger.LogError(ex, "Concurrency error, retrying.");
                    foreach (var entry in ex.Entries)
                    {
                        if (entry.Entity is Counter)
                        {
                            var databaseValues = entry.GetDatabaseValues();

                            // Refresh original values to bypass next concurrency check
                            entry.OriginalValues.SetValues(databaseValues);
                        }
                        else
                        {
                            throw new NotSupportedException($"Don't know how to handle concurrency conflicts for {entry.Metadata.Name}");
                        }
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}
