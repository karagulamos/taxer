using Microsoft.EntityFrameworkCore;
using Taxer.Core.Entities;
using Taxer.Core.Persistence.Repositories;

namespace Taxer.Persistence.EntityFramework.Repositories;

public class TaxTypeRepository(TaxerContext context) : ITaxTypeRepository
{
    public Task<TaxType?> GetByPostalCodeAsync(string postalCode)
    {
        return context.TaxTypes.SingleOrDefaultAsync(x => x.PostalCode == postalCode);
    }
}
