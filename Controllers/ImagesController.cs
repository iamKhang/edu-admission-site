using Microsoft.AspNetCore.Mvc;
using EduAdmissionSite.Models;

namespace EduAdmissionSite.Controllers
{
    public class ImagesController : Controller
    {
        private readonly IArticleRepository _articleRepository;
        private readonly ILogger<ImagesController> _logger;

        public ImagesController(IArticleRepository articleRepository, ILogger<ImagesController> logger)
        {
            _articleRepository = articleRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 12)
        {
            try
            {
                var (items, total) = await _articleRepository.GetPagedByCategoryAsync(ArticleCategory.HinhAnh, page, pageSize);

                var totalPages = (int)Math.Ceiling((double)total / pageSize);

                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = totalPages;
                ViewBag.TotalItems = total;
                ViewBag.PageSize = pageSize;

                // For potential sidebar usage (latest/related)
                ViewBag.LatestNews = await _articleRepository.GetByCategoryAsync(ArticleCategory.HinhAnh, 5);

                return View(items.ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading images page");
                return View(new List<Article>());
            }
        }
    }
}


