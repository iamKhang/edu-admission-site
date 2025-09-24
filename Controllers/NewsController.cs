using Microsoft.AspNetCore.Mvc;
using EduAdmissionSite.Models;

namespace EduAdmissionSite.Controllers
{
    public class NewsController : Controller
    {
        private readonly IArticleRepository _articleRepository;
        private readonly ILogger<NewsController> _logger;

        public NewsController(IArticleRepository articleRepository, ILogger<NewsController> logger)
        {
            _articleRepository = articleRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index(ArticleCategory category = ArticleCategory.TinTuyenSinh, int page = 1, int pageSize = 12)
        {
            try
            {
                // Get articles with pagination for the specified category
                var (articles, total) = await _articleRepository.GetPagedByCategoryAsync(
                    category, 
                    page, 
                    pageSize
                );

                // Get latest 5 news items for sidebar (filtered by the same category)
                var latestNews = await _articleRepository.GetByCategoryAsync(category, 5);

                // Calculate pagination info
                var totalPages = (int)Math.Ceiling((double)total / pageSize);
                
                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = totalPages;
                ViewBag.TotalItems = total;
                ViewBag.PageSize = pageSize;
                ViewBag.LatestNews = latestNews;
                ViewBag.Category = category;
                ViewBag.CategoryName = GetCategoryName(category);

                return View(articles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading news for category: {Category}", category);
                return View(new List<Article>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var article = await _articleRepository.GetByIdAsync(id);
                if (article == null)
                {
                    return NotFound();
                }

                // Get latest 5 news items for sidebar (from the same category)
                var latestNews = await _articleRepository.GetByCategoryAsync(article.Category, 5);

                ViewBag.LatestNews = latestNews;
                ViewBag.Category = article.Category;
                ViewBag.CategoryName = GetCategoryName(article.Category);

                return View(article);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading article details for ID: {ArticleId}", id);
                return NotFound();
            }
        }

        private string GetCategoryName(ArticleCategory category)
        {
            // Check for specific categories first (exact matches)
            if (category == ArticleCategory.TinTuyenSinh) return "TIN TUYỂN SINH";
            if (category == ArticleCategory.TinSinhVien) return "SINH VIÊN";
            if (category == ArticleCategory.TinGiangVien) return "GIẢNG VIÊN";
            if (category == ArticleCategory.TanSinhVien) return "TÂN SINH VIÊN";
            if (category == ArticleCategory.ThongBao) return "THÔNG BÁO";
            if (category == ArticleCategory.TinTucNoiBat) return "TIN TỨC NỔI BẬT";
            if (category == ArticleCategory.SuKien) return "SỰ KIỆN";
            
            // Check for flags (multiple categories) - prioritize TinTucNoiBat
            if ((category & ArticleCategory.TinTucNoiBat) != 0) return "TIN TỨC NỔI BẬT";
            if ((category & ArticleCategory.TinTuyenSinh) != 0) return "TIN TUYỂN SINH";
            if ((category & ArticleCategory.TinSinhVien) != 0) return "SINH VIÊN";
            if ((category & ArticleCategory.TinGiangVien) != 0) return "GIẢNG VIÊN";
            if ((category & ArticleCategory.TanSinhVien) != 0) return "TÂN SINH VIÊN";
            if ((category & ArticleCategory.ThongBao) != 0) return "THÔNG BÁO";
            if ((category & ArticleCategory.SuKien) != 0) return "SỰ KIỆN";
            
            return "TIN TỨC";
        }
    }
}
