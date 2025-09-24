using EduAdmissionSite.Models;
using Microsoft.EntityFrameworkCore;

namespace EduAdmissionSite.Data.Seed;

public class DatabaseSeeder
{
    private readonly AppDbContext _context;

    public DatabaseSeeder(AppDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        await SeedArticlesAsync();
        // Add other seed methods here
    }

    private async Task SeedArticlesAsync()
    {
        if (await _context.Articles.AnyAsync())
            return; // Data already seeded

        // Đường dẫn ảnh (bắt buộc tồn tại trong wwwroot)
        var img1 = "/images/posts/post_01/img_01.png";
        var img2 = "/images/posts/post_01/img_02.png";
        var video = "/videos/video_demo.mp4";

        // Content HTML dùng chung (có cả 2 ảnh)
        var contentHtml = $@"
<div class=""post-content"">
  <p><em>Trong không khí phấn khởi đầu xuân 2025, các em sinh viên quay trở lại trường sau một tuần học Online và kỳ nghỉ Tết an toàn. 
  Những việc cần thực hiện bao gồm: chào cờ đầu tuần; không hút thuốc; không tụ tập đông; 
  không nói tục chửi bậy; không vứt rác bừa bãi; giao tiếp ứng xử văn minh; văn hóa chào hỏi; 
  giữ vệ sinh chung; nghiêm túc trong giờ học; trang phục gọn gàng khi đi học.</em></p>

  <p>Sáng ngày 17/02/2025, sinh viên UNETI có mặt trực tiếp tại các cơ sở đào tạo để tham gia học tập sau một tuần học Online và kỳ nghỉ Tết Ất Tỵ 2025 an toàn. 
  Các em tiếp tục thực hiện tốt công tác Văn hóa học đường; phong trào thể dục thể thao; học tập nghiêm túc; 
  không hút thuốc; không nói tục; không vứt rác bừa bãi… góp phần xây dựng môi trường học tập thân thiện, văn minh.</p>

  <figure style=""margin:16px 0;text-align:center"">
    <img src=""{img1}"" alt=""Sinh viên tựu trường đầu xuân"" style=""max-width:100%;height:auto;border-radius:8px"" />
    <figcaption style=""font-size:0.95rem;color:#666;margin-top:6px"">Sinh viên tựu trường đầu xuân</figcaption>
  </figure>

  <p>Tiếp tục theo thông báo 928/TB-ĐHKTKCN (21/10/2024) về thực hiện Văn hóa học đường đối với Cán bộ, Giảng viên và Sinh viên theo mô hình quản lý 5S 
  tại các khối giảng đường, phòng thí nghiệm, xưởng thực hành. Sáng nay Phòng Chính trị & Công tác Sinh viên phối hợp cùng Đoàn Thanh niên, 
  Hội Sinh viên tổ chức kiểm tra, tuyên truyền, nhắc nhở, nhằm đảm bảo nề nếp học tập, trật tự, vệ sinh toàn trường.</p>

  <figure style=""margin:16px 0;text-align:center"">
    <img src=""{img2}"" alt=""Không khí học tập đầu xuân"" style=""max-width:100%;height:auto;border-radius:8px"" />
    <figcaption style=""font-size:0.95rem;color:#666;margin-top:6px"">Không khí học tập nghiêm túc, văn minh đầu xuân</figcaption>
  </figure>

  <p>Nhà trường kính chúc các em sức khỏe, học tập tốt và có một học kỳ mới thành công!</p>
</div>";

        var articles = new List<Article>
        {
            // 1–10
            new Article { Title = "Khởi động học kỳ mới: Tựu trường đầu xuân an toàn",               Content = contentHtml, ThumbnailUrl = img1, VideoUrl = video, Category = ArticleCategory.TinTucNoiBat | ArticleCategory.SuKien, PublishedAt = new DateTime(2025,1,1) },
            new Article { Title = "Thông báo chỉ tiêu tuyển sinh năm 2025",                           Content = contentHtml, ThumbnailUrl = img1, VideoUrl = video, Category = ArticleCategory.TinTuyenSinh | ArticleCategory.ThongBao, PublishedAt = new DateTime(2025,1,2) },
            
            // Video samples
            new Article { Title = "Giới thiệu Trường Cao Đẳng Nghề Hà Nội - HNIVC", Content = "Video giới thiệu tổng quan về trường HNIVC", ThumbnailUrl = "/images/image_thumbnail_video.jpg", VideoUrl = "/videos/video_demo.mp4", Category = ArticleCategory.Video, PublishedAt = new DateTime(2025,1,15) },
            new Article { Title = "Campus Tour HNIVC 2025", Content = "Video tour khuôn viên trường năm 2025", ThumbnailUrl = "https://hnivc.edu.vn/upload/images/2025/tuyensinh/banner-web-9-.jpg", VideoUrl = "/videos/video_demo.mp4", Category = ArticleCategory.Video, PublishedAt = new DateTime(2025,1,10) },
            new Article { Title = "Sinh viên chia sẻ trải nghiệm học tập", Content = "Video chia sẻ của sinh viên về trải nghiệm học tập tại HNIVC", ThumbnailUrl = "https://hnivc.edu.vn/upload/images/2025/tuyensinh/stande-cd-banner.png", VideoUrl = "/videos/video_demo.mp4", Category = ArticleCategory.Video, PublishedAt = new DateTime(2025,1,8) },
            new Article { Title = "Hoạt động ngoại khóa tại HNIVC", Content = "Video về các hoạt động ngoại khóa của sinh viên", ThumbnailUrl = "/images/image_thumbnail_video.jpg", VideoUrl = "/videos/video_demo.mp4", Category = ArticleCategory.Video, PublishedAt = new DateTime(2025,1,5) },
            new Article { Title = "Lễ tốt nghiệp sinh viên khóa 2024", Content = "Video ghi lại lễ tốt nghiệp của sinh viên khóa 2024", ThumbnailUrl = "/images/image_thumbnail_video.jpg", VideoUrl = "/videos/video_demo.mp4", Category = ArticleCategory.Video, PublishedAt = new DateTime(2024,12,20) },
            new Article { Title = "Workshop Công nghệ thông tin 2025", Content = "Video ghi lại workshop về công nghệ thông tin", ThumbnailUrl = "/images/image_thumbnail_video.jpg", VideoUrl = "/videos/video_demo.mp4", Category = ArticleCategory.Video, PublishedAt = new DateTime(2024,12,15) },
            new Article { Title = "Ngày hội việc làm HNIVC", Content = "Video về ngày hội việc làm dành cho sinh viên", ThumbnailUrl = "/images/image_thumbnail_video.jpg", VideoUrl = "/videos/video_demo.mp4", Category = ArticleCategory.Video, PublishedAt = new DateTime(2024,12,10) },
            new Article { Title = "Lễ khai giảng năm học mới", Content = "Video lễ khai giảng năm học 2024-2025", ThumbnailUrl = "/images/image_thumbnail_video.jpg", VideoUrl = "/videos/video_demo.mp4", Category = ArticleCategory.Video, PublishedAt = new DateTime(2024,9,5) },
            new Article { Title = "Sinh viên trở lại giảng đường: Lưu ý nề nếp và văn hóa học đường", Content = contentHtml, ThumbnailUrl = img1, VideoUrl = video, Category = ArticleCategory.TinSinhVien, PublishedAt = new DateTime(2025,1,3) },
            new Article { Title = "Giảng viên triển khai kế hoạch giảng dạy sau kỳ nghỉ Tết",         Content = contentHtml, ThumbnailUrl = img1, VideoUrl = video, Category = ArticleCategory.TinGiangVien, PublishedAt = new DateTime(2025,1,4) },
            new Article { Title = "Hướng dẫn nhập học cho Tân sinh viên đợt đầu năm",                 Content = contentHtml, ThumbnailUrl = "https://hnivc.edu.vn/upload/images/2024/thang-2-2024/tscd.jpeg", VideoUrl = video, Category = ArticleCategory.TanSinhVien | ArticleCategory.TinTuyenSinh, PublishedAt = new DateTime(2025,1,5) },
            new Article { Title = "Điểm nhấn đầu xuân: Hoạt động chào mừng ngày tựu trường",         Content = contentHtml, ThumbnailUrl = img1, VideoUrl = video, Category = ArticleCategory.Video | ArticleCategory.TinTucNoiBat, PublishedAt = new DateTime(2025,1,6) },
            new Article { Title = "Khoảnh khắc sinh viên ngày đầu tựu trường",                        Content = contentHtml, ThumbnailUrl = img1, VideoUrl = video, Category = ArticleCategory.HinhAnh | ArticleCategory.TinSinhVien, PublishedAt = new DateTime(2025,1,7) },
            new Article { Title = "Mở cổng đăng ký xét tuyển trực tuyến",                             Content = contentHtml, ThumbnailUrl = "https://hnivc.edu.vn/upload/images/2023-thang-2/z4100226565490-27eaf56f46d2086b455a33fa32bd0547.jpg", VideoUrl = video, Category = ArticleCategory.TinTuyenSinh, PublishedAt = new DateTime(2025,1,8) },
            new Article { Title = "Thông báo về lịch học tập trung tuần đầu tiên",                    Content = contentHtml, ThumbnailUrl = img1, VideoUrl = video, Category = ArticleCategory.ThongBao, PublishedAt = new DateTime(2025,1,9) },
            new Article { Title = "Sự kiện: Chào mừng năm học mới 2025",                              Content = contentHtml, ThumbnailUrl = img1, VideoUrl = video, Category = ArticleCategory.SuKien, PublishedAt = new DateTime(2025,1,10) },

            // 11–20
            new Article { Title = "Ký sự đầu xuân của sinh viên khoa Điện–Điện tử",                   Content = contentHtml, ThumbnailUrl = "https://hnivc.edu.vn/upload/images/2025/t9/image.jpg", VideoUrl = video, Category = ArticleCategory.Video | ArticleCategory.TinSinhVien, PublishedAt = new DateTime(2025,1,11) },
            new Article { Title = "Góc nhìn giảng viên: Tổ chức lớp học hiệu quả",                    Content = contentHtml, ThumbnailUrl = "https://hnivc.edu.vn/upload/images/2025/t9/image.jpg", VideoUrl = video, Category = ArticleCategory.HinhAnh | ArticleCategory.TinGiangVien, PublishedAt = new DateTime(2025,1,12) },
            new Article { Title = "Nổi bật: Văn hóa học đường và tinh thần 5S",                       Content = contentHtml, ThumbnailUrl = "https://hnivc.edu.vn/upload/images/2025/t9/image.jpg", VideoUrl = video, Category = ArticleCategory.TinTucNoiBat, PublishedAt = new DateTime(2025,1,13) },
            new Article { Title = "Tân sinh viên: Những điều cần biết trong tuần định hướng",        Content = contentHtml, ThumbnailUrl = "https://hnivc.edu.vn/upload/images/2025/t9/image.jpg", VideoUrl = video, Category = ArticleCategory.TanSinhVien, PublishedAt = new DateTime(2025,1,14) },
            new Article { Title = "Thông báo tới giảng viên: Lịch họp chuyên môn đầu kỳ",            Content = contentHtml, ThumbnailUrl = img1, VideoUrl = video, Category = ArticleCategory.TinGiangVien | ArticleCategory.ThongBao, PublishedAt = new DateTime(2025,1,15) },
            new Article { Title = "Sinh viên tham gia hoạt động rèn luyện nề nếp 5S",                Content = contentHtml, ThumbnailUrl = img1, VideoUrl = video, Category = ArticleCategory.TinSinhVien | ArticleCategory.SuKien, PublishedAt = new DateTime(2025,1,16) },
            new Article { Title = "Tuyển sinh 2025: Hình thức xét tuyển & tư vấn trực tuyến",         Content = contentHtml, ThumbnailUrl = "https://hnivc.edu.vn/upload/images/2025/t8/vin/z6946821043374-17ec75842ad0e6dc4704c6c69aa61934.jpg", VideoUrl = video, Category = ArticleCategory.TinTuyenSinh | ArticleCategory.Video, PublishedAt = new DateTime(2025,1,17) },
            new Article { Title = "Album: Những hình ảnh ấn tượng ngày trở lại trường",               Content = contentHtml, ThumbnailUrl = img1, VideoUrl = video, Category = ArticleCategory.HinhAnh | ArticleCategory.TinTucNoiBat, PublishedAt = new DateTime(2025,1,18) },
            new Article { Title = "Nổi bật trong tuần: Nề nếp & vệ sinh trường lớp",                  Content = contentHtml, ThumbnailUrl = img1, VideoUrl = video, Category = ArticleCategory.TinTucNoiBat | ArticleCategory.ThongBao, PublishedAt = new DateTime(2025,1,19) },
            new Article { Title = "Sự kiện tư vấn tuyển sinh trực tiếp cuối tuần",                    Content = contentHtml, ThumbnailUrl = "https://hnivc.edu.vn/upload/images/2025/t8/vin/z6946821043374-17ec75842ad0e6dc4704c6c69aa61934.jpg", VideoUrl = video, Category = ArticleCategory.TinTuyenSinh | ArticleCategory.SuKien, PublishedAt = new DateTime(2025,1,20) },

            // 21–30
            new Article { Title = "Sinh viên chủ động học tập: Mẹo ghi chép hiệu quả",                Content = contentHtml, ThumbnailUrl = "https://hnivc.edu.vn/upload/images/2025/t9/4a6427b2-f3bb-4810-9c01-24e595ebfb9c.png", VideoUrl = video, Category = ArticleCategory.TinSinhVien, PublishedAt = new DateTime(2025,1,21) },
            new Article { Title = "Giảng viên cập nhật tài liệu học phần học kỳ II",                  Content = contentHtml, ThumbnailUrl = "https://hnivc.edu.vn/upload/images/2025/t9/4a6427b2-f3bb-4810-9c01-24e595ebfb9c.png", VideoUrl = video, Category = ArticleCategory.TinGiangVien, PublishedAt = new DateTime(2025,1,22) },
            new Article { Title = "Tân sinh viên: Quy định sử dụng phòng học & xưởng",               Content = contentHtml, ThumbnailUrl = "https://hnivc.edu.vn/upload/images/2025/t9/4a6427b2-f3bb-4810-9c01-24e595ebfb9c.png", VideoUrl = video, Category = ArticleCategory.TanSinhVien | ArticleCategory.ThongBao, PublishedAt = new DateTime(2025,1,23) },
            new Article { Title = "Một ngày ở trường: Góc nhìn chuyển động",                         Content = contentHtml, ThumbnailUrl = "https://hnivc.edu.vn/upload/images/2025/t9/4a6427b2-f3bb-4810-9c01-24e595ebfb9c.png", VideoUrl = video, Category = ArticleCategory.Video, PublishedAt = new DateTime(2025,1,24) },
            new Article { Title = "Photo story: Sắc xuân trong khuôn viên trường",                   Content = contentHtml, ThumbnailUrl = "https://hnivc.edu.vn/upload/images/2025/t9/4a6427b2-f3bb-4810-9c01-24e595ebfb9c.png", VideoUrl = video, Category = ArticleCategory.HinhAnh, PublishedAt = new DateTime(2025,1,25) },
            new Article { Title = "Tuyển sinh 2025: Các mốc thời gian quan trọng",                   Content = contentHtml, ThumbnailUrl = "https://hnivc.edu.vn/upload/images/2025/t9/4a6427b2-f3bb-4810-9c01-24e595ebfb9c.png", VideoUrl = video, Category = ArticleCategory.TinTucNoiBat | ArticleCategory.TinTuyenSinh, PublishedAt = new DateTime(2025,1,26) },
            new Article { Title = "Sinh viên hỏi – Nhà trường trả lời về xét tuyển",                  Content = contentHtml, ThumbnailUrl = "https://hnivc.edu.vn/upload/images/2025/t9/4a6427b2-f3bb-4810-9c01-24e595ebfb9c.png", VideoUrl = video, Category = ArticleCategory.TinSinhVien | ArticleCategory.TinTuyenSinh, PublishedAt = new DateTime(2025,1,27) },
            new Article { Title = "Giảng viên đồng hành cùng hoạt động 5S toàn trường",              Content = contentHtml, ThumbnailUrl = "https://hnivc.edu.vn/upload/images/2025/t9/4a6427b2-f3bb-4810-9c01-24e595ebfb9c.png", VideoUrl = video, Category = ArticleCategory.TinGiangVien | ArticleCategory.SuKien, PublishedAt = new DateTime(2025,1,28) },
            new Article { Title = "Tân sinh viên check-in ngày đầu đến trường",                      Content = contentHtml, ThumbnailUrl = "https://hnivc.edu.vn/upload/images/2025/t9/4a6427b2-f3bb-4810-9c01-24e595ebfb9c.png", VideoUrl = video, Category = ArticleCategory.TanSinhVien | ArticleCategory.HinhAnh, PublishedAt = new DateTime(2025,1,29) },
            new Article { Title = "Thông báo: Quy định an toàn – vệ sinh – trật tự đầu kỳ",          Content = contentHtml, ThumbnailUrl = "https://hnivc.edu.vn/upload/images/2025/t9/4a6427b2-f3bb-4810-9c01-24e595ebfb9c.png", VideoUrl = video, Category = ArticleCategory.ThongBao, PublishedAt = new DateTime(2025,1,30) },

            // 31–40
            new Article { Title = "Chia sẻ chuyên môn: Kinh nghiệm giảng dạy học kỳ mới",            Content = contentHtml, ThumbnailUrl = "https://hnivc.edu.vn/upload/images/2025/t8/vin/z6946821043374-17ec75842ad0e6dc4704c6c69aa61934.jpg", VideoUrl = video, Category = ArticleCategory.Video | ArticleCategory.TinGiangVien, PublishedAt = new DateTime(2025,1,31) },
            new Article { Title = "Kho hình ảnh: Lớp học đầu tiên sau Tết",                          Content = contentHtml, ThumbnailUrl = img1, VideoUrl = video, Category = ArticleCategory.HinhAnh | ArticleCategory.TinSinhVien, PublishedAt = new DateTime(2025,2,1) },
            new Article { Title = "Điểm tựa tân sinh viên: Kỹ năng học đại học hiệu quả",            Content = contentHtml, ThumbnailUrl = img1, VideoUrl = video, Category = ArticleCategory.TinTucNoiBat | ArticleCategory.TanSinhVien, PublishedAt = new DateTime(2025,2,2) },
            new Article { Title = "Tuyển sinh 2025: Hỏi đáp trực tuyến buổi tối",                     Content = contentHtml, ThumbnailUrl = img1, VideoUrl = video, Category = ArticleCategory.TinTuyenSinh | ArticleCategory.ThongBao, PublishedAt = new DateTime(2025,2,3) },
            new Article { Title = "Sự kiện: Ngày hội Câu lạc bộ sinh viên",                           Content = contentHtml, ThumbnailUrl = img1, VideoUrl = video, Category = ArticleCategory.SuKien, PublishedAt = new DateTime(2025,2,4) },
            new Article { Title = "Thông báo tới sinh viên: Lịch đăng ký tín chỉ học kỳ II",          Content = contentHtml, ThumbnailUrl = "https://hnivc.edu.vn/upload/images/2025/t8/vin/z6946821043374-17ec75842ad0e6dc4704c6c69aa61934.jpg", VideoUrl = video, Category = ArticleCategory.TinSinhVien | ArticleCategory.ThongBao, PublishedAt = new DateTime(2025,2,5) },
            new Article { Title = "Nổi bật: Chuyên đề đổi mới phương pháp giảng dạy",                Content = contentHtml, ThumbnailUrl = img1, VideoUrl = video, Category = ArticleCategory.TinGiangVien | ArticleCategory.TinTucNoiBat, PublishedAt = new DateTime(2025,2,6) },
            new Article { Title = "Video recap: Không khí trở lại trường sau Tết",                   Content = contentHtml, ThumbnailUrl = img1, VideoUrl = video, Category = ArticleCategory.Video | ArticleCategory.SuKien, PublishedAt = new DateTime(2025,2,7) },
            new Article { Title = "Bộ ảnh: Hành lang, lớp học và sân trường đầu xuân",               Content = contentHtml, ThumbnailUrl = "https://hnivc.edu.vn/upload/images/2025/t8/vin/z6946821043374-17ec75842ad0e6dc4704c6c69aa61934.jpg", VideoUrl = video, Category = ArticleCategory.HinhAnh | ArticleCategory.TinTuyenSinh, PublishedAt = new DateTime(2025,2,8) },
            new Article { Title = "Tổng hợp thông tin đầu kỳ: Nề nếp – Sự kiện – Thông báo",         Content = contentHtml, ThumbnailUrl = "https://hnivc.edu.vn/upload/images/2025/t8/vin/z6946821043374-17ec75842ad0e6dc4704c6c69aa61934.jpg", VideoUrl = video, Category = ArticleCategory.TinTucNoiBat | ArticleCategory.SuKien | ArticleCategory.ThongBao, PublishedAt = new DateTime(2025,2,9) },
        };

        await _context.Articles.AddRangeAsync(articles);
        await _context.SaveChangesAsync();
    }
}
