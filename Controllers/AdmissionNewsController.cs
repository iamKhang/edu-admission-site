using Microsoft.AspNetCore.Mvc;
using EduAdmissionSite.Models;

namespace EduAdmissionSite.Controllers
{
    public class AdmissionNewsController : Controller
    {
        private readonly IArticleRepository _articleRepository;
        private readonly ILogger<AdmissionNewsController> _logger;

        public AdmissionNewsController(IArticleRepository articleRepository, ILogger<AdmissionNewsController> logger)
        {
            _articleRepository = articleRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 12)
        {
            try
            {
                // Get admission news articles with pagination
                var (articles, total) = await _articleRepository.GetPagedByCategoryAsync(
                    ArticleCategory.TinTuyenSinh, 
                    page, 
                    pageSize
                );

                // Get latest 5 news items for sidebar
                var latestNews = await _articleRepository.GetTopAsync(5);

                // Calculate pagination info
                var totalPages = (int)Math.Ceiling((double)total / pageSize);
                
                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = totalPages;
                ViewBag.TotalItems = total;
                ViewBag.PageSize = pageSize;
                ViewBag.LatestNews = latestNews;

                return View(articles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading admission news");
                return View(new List<Article>());
            }
        }
    }
}
