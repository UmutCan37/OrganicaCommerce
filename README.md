# 🌿 OrganicaCommerce

<div align="center">

**Clean Architecture ile geliştirilmiş, ERP tarzı organik gıda e-ticaret platformu**

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white)
![Bootstrap](https://img.shields.io/badge/Bootstrap-7952B3?style=for-the-badge&logo=bootstrap&logoColor=white)

![Clean Architecture](https://img.shields.io/badge/Architecture-Clean-brightgreen?style=flat-square)
![Design Patterns](https://img.shields.io/badge/Design%20Patterns-7-blue?style=flat-square)
![Status](https://img.shields.io/badge/Status-Completed-success?style=flat-square)

</div>

---

## 📖 Proje Hakkında

**OrganicaCommerce**, **Clean Architecture** prensiplerine sadık, gerçek dünya senaryolarını (sepet, sipariş, stok yönetimi, admin paneli) simüle eden bir e-ticaret uygulamasıdır. Proje boyunca 7 farklı tasarım deseni bilinçli olarak, birbirini tamamlayacak şekilde bir araya getirilmiştir.

---

## 🏗️ Mimari

Solution, bağımlılıkların **her zaman içe doğru** aktığı 6 katmandan oluşur:

📦 OrganicaCommerce
┃
┣ 📁 Core
┃ ┣ 🧠 OrganicaCommerce.Domain → Entity'ler, iş kuralları — hiçbir dış bağımlılık yok
┃ ┗ ⚙️ OrganicaCommerce.Application → CQRS, MediatR, validasyon, iş akışları
┃
┣ 📁 Infrastructure
┃ ┗ 🗄️ OrganicaCommerce.Infrastructure → EF Core, repository implementasyonları, migration'lar
┃
┗ 📁 Presentation
┣ 🔗 OrganicaCommerce.Contracts → Web ↔ WebApi arası paylaşılan DTO sözleşmeleri
┣ 🌐 OrganicaCommerce.WebApi → REST API (Swagger destekli)
┗ 🖥️ OrganicaCommerce.Web → MVC arayüzü (mağaza + admin panel)


> 💡 **Kural:** Domain hiçbir katmana bağımlı değil. Web, Domain'i ve Application'ı hiç tanımaz — sadece Contracts'ı bilir ve WebApi'ye `HttpClient` ile konuşur.

---

## 🎯 Kullanılan Tasarım Desenleri

<table>
<tr><th>Desen</th><th>Konum</th><th>Ne İşe Yarıyor</th></tr>

<tr>
<td>🗃️ <b>Repository</b></td>
<td><code>Infrastructure/Persistence/Repositories/</code></td>
<td>Veri erişimini soyutlar — <code>IGenericRepository&lt;T&gt;</code> + entity'ye özel repository'ler</td>
</tr>

<tr>
<td>🔄 <b>Unit of Work</b></td>
<td><code>Infrastructure/Persistence/UnitOfWork.cs</code></td>
<td>Birden fazla repository işlemini tek transaction'da toplar — ya hep ya hiç</td>
</tr>

<tr>
<td>📬 <b>CQRS</b></td>
<td><code>Application/CQRS/</code></td>
<td>Command (yazma) ve Query (okuma) işlemleri tamamen ayrı sınıflar</td>
</tr>

<tr>
<td>📮 <b>Mediator</b> <sub>(MediatR)</sub></td>
<td>Tüm Controller'lar</td>
<td>Controller, hangi Handler'ın çalışacağını bilmeden isteği gönderir</td>
</tr>

<tr>
<td>⛓️ <b>Chain of Responsibility</b></td>
<td><code>Common/Behaviors/</code> + <code>Common/Validators/</code></td>
<td>İstek, Handler'a ulaşmadan önce loglama → validasyon zincirinden geçer</td>
</tr>

<tr>
<td>👁️ <b>Observer</b></td>
<td><code>Domain/Events/</code> + <code>Application/EventHandlers/</code></td>
<td>Sipariş oluşumu ve stok tükenmesi olayları, dinleyicilere yayınlanır</td>
</tr>

<tr>
<td>🎭 <b>Facade</b></td>
<td><code>CQRS/Admin/Facades/DashboardFacade.cs</code></td>
<td>Admin dashboard için birden fazla sorguyu tek sonuçta birleştirir</td>
</tr>

</table>

---

## 🛠️ Teknoloji Yığını

| Katman | Teknolojiler |
|---|---|
| **Backend** | .NET 9, ASP.NET Core Web API, Entity Framework Core |
| **Veritabanı** | Microsoft SQL Server |
| **Mimari Araçlar** | MediatR, FluentValidation, AutoMapper |
| **Frontend** | ASP.NET Core MVC, Bootstrap 5, jQuery |
| **API Dokümantasyonu** | Swagger / Swashbuckle |
| **Görsel Tema** | Organic-html (mağaza), özgün tasarım (admin panel) |

---

## ✨ Özellikler

- 🛍️ **Mağaza:** Ana sayfa, kategori filtreli ürün listesi, ürün detay sayfası
- 🛒 **Sepet:** Ürün ekleme/çıkarma, canlı sepet özeti (View Component ile)
- 📦 **Sipariş:** Sepetten tek tıkla sipariş oluşturma, stok kontrolü ile
- 🔐 **Stok Güvenliği:** Yetersiz stok durumunda `InsufficientStockException` ile güvenli hata yönetimi
- 📊 **Admin Paneli:** Dashboard (toplam sipariş, gelir, düşük stok uyarısı), ürün/kategori CRUD, sipariş durumu yönetimi
- 🎨 **İki Ayrı Tasarım Dili:** Mağaza (organic-html teması) ve Admin Panel (özgün, modern tasarım) birbirinden bağımsız

---

## 🚀 Kurulum

### 1️⃣ Veritabanı bağlantısını ayarla
`OrganicaCommerce.WebApi/appsettings.json` içindeki connection string'i güncelle:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=OrganicaCommerceDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### 2️⃣ Migration'ı çalıştır
Package Manager Console'da (Default Project: `Infrastructure`):
```powershell
Update-Database -StartupProject OrganicaCommerce.WebApi
```

### 3️⃣ Projeleri birlikte başlat
Solution → sağ tık → **Configure Startup Projects** → **Multiple startup projects** → `WebApi` ve `Web` için **Start** seç.

### 4️⃣ Çalıştır
- 📘 **API / Swagger:** `https://localhost:<port>/swagger`
- 🏠 **Mağaza:** `https://localhost:<port>/`
- ⚙️ **Admin Panel:** `https://localhost:<port>/Admin/Dashboard`

---

## 📝 Notlar

> ⚠️ Bilinçli sadeleştirmeler:
> - 🔓 Authentication/login akışı yok — sabit bir demo kullanıcı (`CurrentUserContext`) kullanılıyor
> - 📍 Checkout/adres akışı yok — sepetten doğrudan sipariş oluşturuluyor
> - 🖼️ Ürünler tek görsel taşıyor (çoklu galeri yok)

---

<div align="center">

**Made with 🌱 and Clean Architecture**

</div>
