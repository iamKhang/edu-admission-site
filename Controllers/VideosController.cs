using Microsoft.AspNetCore.Mvc;
using EduAdmissionSite.Models;

namespace EduAdmissionSite.Controllers
{
    public class VideosController : Controller
    {
        private readonly IArticleRepository _articleRepository;
        private const int PageSize = 8; // 4 items per row × 2 rows = 8 items per page

        public VideosController(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            // Lấy tất cả bài viết có category Video
            var allVideos = await _articleRepository.GetByCategoryAsync(ArticleCategory.Video, 0);
            
            // Phân trang
            var totalItems = allVideos.Count;
            var totalPages = (int)Math.Ceiling((double)totalItems / PageSize);
            var pagedVideos = allVideos.Skip((page - 1) * PageSize).Take(PageSize).ToList();
            
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            
            return View(pagedVideos);
        }
    }
}
