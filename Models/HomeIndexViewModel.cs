using System.Collections.Generic;

namespace EduAdmissionSite.Models
{
    public class HomeIndexViewModel
    {
        public IReadOnlyList<Article> TinTucNoiBat { get; set; } = new List<Article>();
        public IReadOnlyList<Article> TinTuyenSinh { get; set; } = new List<Article>();
        public IReadOnlyList<Article> TinSinhVien { get; set; } = new List<Article>();
        public IReadOnlyList<Article> TinGiangVien { get; set; } = new List<Article>();
        public IReadOnlyList<Article> TanSinhVien { get; set; } = new List<Article>();
        public IReadOnlyList<Article> ThongBao { get; set; } = new List<Article>();
        public IReadOnlyList<Article> SuKien { get; set; } = new List<Article>();
    }
}
