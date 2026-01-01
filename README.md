# 🚀 AI Destekli Kariyer Koçu API (Career Coach AI API)

Bu proje, **.NET 8** ve **Google Gemini 2.0 (Generative AI)** teknolojilerini kullanarak adayların CV'lerini analiz eden, eksiklerini tespit eden ve güncel piyasa koşullarına göre maaş tahmini yapan akıllı bir RESTful API servisidir.

🔗 **Proje Linki:** [https://github.com/heysefb/Career-Coach-AI-API](https://github.com/heysefb/Career-Coach-AI-API)

---

## 🎯 Projenin Amacı
İş arama sürecindeki yazılımcıların veya profesyonellerin CV'lerini (PDF) sisteme yükleyerek;
- **Güçlü ve Zayıf Yönlerini** yapay zeka gözüyle öğrenmelerini,
- **Teknoloji Eksiklerini** tespit etmelerini,
- **Maaş Tahmini** ile sektördeki değerlerini görmelerini sağlamaktır.

## 🛠️ Kullanılan Teknolojiler ve Mimari

| Teknoloji | Açıklama |
| :--- | :--- |
| **ASP.NET Core 8.0** | Modern ve hızlı Backend API geliştirmesi. |
| **Google Gemini AI** | CV analizi ve doğal dil işleme (LLM) için kullanılan yapay zeka modeli. |
| **PdfPig** | PDF dosyalarından metin ayıklama (Text Extraction). |
| **Entity Framework Core** | Code-First yaklaşımı ile veritabanı yönetimi. |
| **MSSQL** | Verilerin saklandığı ilişkisel veritabanı. |
| **Swagger UI** | API uç noktalarını test etmek için kullanıcı arayüzü. |

## ⚙️ Özellikler

* 📂 **PDF Yükleme:** Kullanıcılar `.pdf` formatındaki CV'lerini sisteme yükler.
* 📝 **OCR / Metin Okuma:** Sistem, `PdfPig` kütüphanesi ile PDF içindeki metni ayıklar.
* 🧠 **Yapay Zeka Analizi:** Ayıklanan metin Google Gemini API'ye gönderilir. AI, bir İK Uzmanı rolüne girerek adayı analiz eder.
* 💾 **Kayıt:** Analiz sonuçları ve dosya yolu veritabanına kaydedilir.
* 📊 **Detaylı Rapor:** Adaya JSON formatında veya metin olarak geri bildirim döner.

## 🚀 Kurulum ve Çalıştırma

Projeyi yerel makinenizde çalıştırmak için aşağıdaki adımları izleyin:

### 1. Projeyi Klonlayın
Terminale şu komutu yazarak projeyi bilgisayarınıza indirin:

git clone [https://github.com/heysefb/Career-Coach-AI-API.git](https://github.com/heysefb/Career-Coach-AI-API.git)
cd Career-Coach-AI-API

### 2. API Anahtarını Ayarlayın
Google AI Studio'dan aldığınız API anahtarını appsettings.json dosyasına ekleyin:

"GeminiSettings": {
    "ApiKey": "BURAYA_GOOGLE_API_KEY_GELECEK"
}

### 3. Veritabanını Oluşturun
Proje dizininde terminali açın ve şu komutları sırasıyla çalıştırın:

dotnet restore
dotnet ef database update

### 4. Projeyi Başlatın
dotnet run

Proje ayağa kalktıktan sonra tarayıcınızda http://localhost:5xxx/swagger adresine giderek API'yi test edebilirsiniz.


### Örnek Kullanım (Endpoint)
POST /api/Candidates/upload

Parametreler:

fullName: Adayın Adı Soyadı

email: E-posta adresi

file: Yüklenecek CV dosyası (.pdf)

Örnek AI Yanıtı:

"Adayın .NET Core tecrübesi güçlü ancak Docker ve Kubernetes konusunda eksikleri var. Sektör ortalamasına göre tahmini maaş beklentisi..."

---
Developed by [heysefb](https://github.com/heysefb)