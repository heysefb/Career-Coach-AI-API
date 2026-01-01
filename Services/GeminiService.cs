using System.Text;
using System.Text.Json;

namespace KariyerKocuAPI.Services
{
    public class GeminiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public GeminiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["GeminiSettings:ApiKey"];
        }

        public async Task<string> AnalyzeCvAsync(string cvText)
        {
            // API Key kontrolü
            if (string.IsNullOrEmpty(_apiKey))
            {
                return "HATA: API Key appsettings.json dosyasında bulunamadı!";
            }

            // DÜZELTİLEN SATIR BURASI: Listeden bulduğumuz 'gemini-2.0-flash' modelini kullanıyoruz.
            var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-flash-latest:generateContent?key={_apiKey}";
            var prompt = $"Sen profesyonel bir İnsan Kaynakları uzmanı ve kariyer koçusun. " +
                                                 $"Aşağıdaki CV metnini analiz et. " +
                                                 $"Adayın güçlü yanlarını, eksik yanlarını ve geliştirmesi gereken teknolojileri maddeler halinde Türkçe olarak yaz. " +
                                                 $"Maaş beklentisi ne olmalı tahmin et.\n\nCV İÇERİĞİ:\n{cvText}";

            // JSON Paketi hazırlığı
            var requestBody = new
            {
                contents = new[]
                {
                    new { parts = new[] { new { text = prompt } } }
                }
            };

            var jsonContent = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            try
            {
                // POST isteği atıyoruz (Analiz et!)
                var response = await _httpClient.PostAsync(url, jsonContent);
                var resultJson = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("GOOGLE HATASI: " + resultJson);
                    return $"Google API Hatası: {response.StatusCode}.";
                }

                // Gelen cevabı parçalayıp metni alıyoruz
                using (JsonDocument doc = JsonDocument.Parse(resultJson))
                {
                    var text = doc.RootElement
                        .GetProperty("candidates")[0]
                        .GetProperty("content")
                        .GetProperty("parts")[0]
                        .GetProperty("text")
                        .GetString();

                    return text;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("KRİTİK HATA: " + ex.Message);
                return "Analiz sırasında hata: " + ex.Message;
            }
        }
    }
}