using KariyerKocuAPI.Data;
using KariyerKocuAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using UglyToad.PdfPig;
using KariyerKocuAPI.Services;

namespace KariyerKocuAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidatesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly GeminiService _geminiService;

        public CandidatesController(AppDbContext context, GeminiService geminiService)
        {
            _context = context;
            _geminiService = geminiService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadCV([FromForm] string fullName, [FromForm] string email, IFormFile file, [FromForm] string phoneNumber)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Lütfen bir CV dosyası yükleyin.");
            }
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "UploadedCVs");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            string extractedText = "";
            try
            {
                using (var pdf = PdfDocument.Open(filePath))
                {
                    foreach (var page in pdf.GetPages())
                    {
                        extractedText += page.Text + " ";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("PDF metin çıkarma hatası: " + ex.Message);
                extractedText = "PDF metni çıkarılamadı.";
            }
            string aiResponse = await _geminiService.AnalyzeCvAsync(extractedText);
            var candidate = new Candidate
            {
                FullName = fullName,
                Email = email,
                CvFilePath = filePath,
                CreatedAt = DateTime.Now,
                AiComment = aiResponse,
                PhoneNumber = phoneNumber
            };
            _context.Candidates.Add(candidate);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                Message = "Analiz Tamamlandı!",
                CandidateId = candidate.Id,
                YapayZekaYorumu = aiResponse
            });
        }
    }
}