using Taxer.Core.Entities;

namespace Taxer.Core.Persistence.Repositories;

public interface ITaxRequestLogRepository
{
    Task AddAsync(TaxRequestLog log);
}