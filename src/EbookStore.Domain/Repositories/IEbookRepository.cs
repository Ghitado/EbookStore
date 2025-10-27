using EbookStore.Domain.Common;
using EbookStore.Domain.Entities;

namespace EbookStore.Domain.Repositories;

public interface IEbookRepository
{
	Task AddAsync(Ebook ebook);
	Task<Ebook?> GetByIdAsync(Guid id);
	Task<PagedResult<Ebook>> GetPagedAsync(int page, int pageSize);
	Task UpdateAsync(Ebook ebook); 
	Task<bool> DeleteAsync(Guid id);
}
