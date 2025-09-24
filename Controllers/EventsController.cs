using Microsoft.AspNetCore.Mvc;
using EduAdmissionSite.Models;

namespace EduAdmissionSite.Controllers
{
    public class EventsController : Controller
    {
        private readonly IArticleRepository _articleRepository;
        private readonly ILogger<EventsController> _logger;

        public EventsController(IArticleRepository articleRepository, ILogger<EventsController> logger)
        {
            _articleRepository = articleRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 12)
        {
            try
            {
                // Get events articles with pagination
                var (articles, total) = await _articleRepository.GetPagedByCategoryAsync(
                    ArticleCategory.SuKien, 
                    page, 
                    pageSize
                );

                // Calculate pagination info
                var totalPages = (int)Math.Ceiling((double)total / pageSize);
                
                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = totalPages;
                ViewBag.TotalItems = total;
                ViewBag.PageSize = pageSize;

                return View(articles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading events");
                return View(new List<Article>());
            }
        }
    }
}
