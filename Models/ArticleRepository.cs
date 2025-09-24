using Microsoft.EntityFrameworkCore;

namespace EduAdmissionSite.Models
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly AppDbContext _db;

        public ArticleRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Article?> GetByIdAsync(int id)
        {
            return await _db.Articles.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IReadOnlyList<Article>> GetTopAsync(int count)
        {
            return await _db.Articles
                .OrderByDescending(a => a.PublishedAt)
                .Take(count)
                .ToListAsync();
        }

        public async Task<(IReadOnlyList<Article> Items, int Total)> GetPagedAsync(int page, int pageSize)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            var query = _db.Articles.AsQueryable();
            var total = await query.CountAsync();
            var items = await query
                .OrderByDescending(a => a.PublishedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, total);
        }

        public async Task<(IReadOnlyList<Article> Items, int Total)> GetPagedByCategoryAsync(ArticleCategory category, int page, int pageSize)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            var query = _db.Articles
                .Where(a => (a.Category & category) != 0);
            
            var total = await query.CountAsync();
            var items = await query
                .OrderByDescending(a => a.PublishedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, total);
        }

        public async Task<IReadOnlyList<Article>> GetByCategoryAsync(ArticleCategory category, int top = 0)
        {
            IQueryable<Article> query = _db.Articles
                .Where(a => (a.Category & category) != 0)
                .OrderByDescending(a => a.PublishedAt);

            if (top > 0)
            {
                query = query.Take(top);
            }

            return await query.ToListAsync();
        }

        public async Task<Article> AddAsync(Article article)
        {
            _db.Articles.Add(article);
            await _db.SaveChangesAsync();
            return article;
        }

        public async Task<Article> UpdateAsync(Article article)
        {
            _db.Articles.Update(article);
            await _db.SaveChangesAsync();
            return article;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _db.Articles.FirstOrDefaultAsync(a => a.Id == id);
            if (entity != null)
            {
                _db.Articles.Remove(entity);
                await _db.SaveChangesAsync();
            }
        }
    }
}


