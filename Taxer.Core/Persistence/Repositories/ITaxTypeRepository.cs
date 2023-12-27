using Taxer.Core.Entities;

namespace Taxer.Core.Persistence.Repositories;

public interface ITaxTypeRepository
{
    Task<TaxType?> GetByPostalCodeAsync(string postalCode);
}