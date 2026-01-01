using KariyerKocuAPI.Data;
using Microsoft.EntityFrameworkCore;
using KariyerKocuAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// --- SERVİSLERİN EKLENDİĞİ BÖLÜM ---

// API dokümantasyonu (Swagger) için gerekli servisler
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Veritabanı bağlantı servisi
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Controller yapısını kullanacağımızı söylüyoruz
builder.Services.AddControllers();

builder.Services.AddHttpClient<GeminiService>();

var app = builder.Build();

// --- UYGULAMA AYARLARI (MIDDLEWARE) ---

// Sadece geliştirme ortamındaysak Swagger'ı aç
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization(); // İleride yetkilendirme yaparsak lazım olacak

// Controller'ları haritala (Bunu eklemezsek API endpoint'leri çalışmaz!)
app.MapControllers();

app.Run();