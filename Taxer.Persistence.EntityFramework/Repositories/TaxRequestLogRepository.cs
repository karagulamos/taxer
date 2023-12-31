using Taxer.Core.Entities;
using Taxer.Core.Persistence.Repositories;

namespace Taxer.Persistence.EntityFramework.Repositories;

public class TaxRequestLogRepository(TaxerContext context) : ITaxRequestLogRepository
{
    public Task AddAsync(TaxRequestLog log)
    {
        context.TaxRequestLogs.Add(log);
        return context.SaveChangesAsync();
    }
}
