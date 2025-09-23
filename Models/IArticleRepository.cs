using System.Collections.Generic;
using System.Threading.Tasks;

namespace EduAdmissionSite.Models
{
    public interface IArticleRepository
    {
        Task<Article?> GetByIdAsync(int id);
        Task<IReadOnlyList<Article>> GetTopAsync(int count);
        Task<(IReadOnlyList<Article> Items, int Total)> GetPagedAsync(int page, int pageSize);
        Task<IReadOnlyList<Article>> GetByCategoryAsync(ArticleCategory category, int top = 0);
        Task<Article> AddAsync(Article article);
        Task<Article> UpdateAsync(Article article);
        Task DeleteAsync(int id);
    }
}


