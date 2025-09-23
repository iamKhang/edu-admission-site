using System;

namespace EduAdmissionSite.Models
{
    [Flags]
    public enum ArticleCategory
    {
        None         = 0,
        TinTucNoiBat = 1 << 0,
        TinTuyenSinh = 1 << 1,
        TinSinhVien  = 1 << 2,
        TinGiangVien = 1 << 3,
        TanSinhVien  = 1 << 4,
        Video        = 1 << 5,
        HinhAnh      = 1 << 6,
        SuKien       = 1 << 7,
        ThongBao     = 1 << 8
    }

    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Content { get; set; }
        public string ThumbnailUrl { get; set; } = string.Empty;
        public string? VideoUrl { get; set; }
        public ArticleCategory Category { get; set; }
        public DateTime PublishedAt { get; set; }
    }
}


