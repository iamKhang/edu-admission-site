using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EduAdmissionSite.Models;

namespace EduAdmissionSite.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IArticleRepository _articleRepository;

    public HomeController(ILogger<HomeController> logger, IArticleRepository articleRepository)
    {
        _logger = logger;
        _articleRepository = articleRepository;
    }

    public async Task<IActionResult> Index()
    {
        var viewModel = new HomeIndexViewModel
        {
            TinTucNoiBat = await _articleRepository.GetByCategoryAsync(ArticleCategory.TinTucNoiBat, 4),
            TinTuyenSinh = await _articleRepository.GetByCategoryAsync(ArticleCategory.TinTuyenSinh, 5),
            TinSinhVien = await _articleRepository.GetByCategoryAsync(ArticleCategory.TinSinhVien, 5),
            TinGiangVien = await _articleRepository.GetByCategoryAsync(ArticleCategory.TinGiangVien, 5),
            TanSinhVien = await _articleRepository.GetByCategoryAsync(ArticleCategory.TanSinhVien, 5),
            ThongBao = await _articleRepository.GetByCategoryAsync(ArticleCategory.ThongBao, 6),
            SuKien = await _articleRepository.GetByCategoryAsync(ArticleCategory.SuKien, 6)
        };

        return View(viewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}