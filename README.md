# CourseSalesAPI

CourseSalesAPI, kullanıcıların kursları gözden geçirebileceği, sepetlerine ekleyebileceği, ödeme yapabileceği ve satın aldıkları kurslara erişebileceği bir kurs satış platformu sunar. Bu proje, **Onion Architecture** prensipleri üzerine inşa edilmiştir ve **ASP.NET Core**, **React**, ve **Redux Toolkit** gibi teknolojileri kullanır.

---

## Proje Yapısı

Proje **Onion Architecture** tabanlı olarak organize edilmiştir. Bu mimari, katmanlar arasındaki bağımlılığı minimuma indirerek test edilebilir, esnek ve modüler bir yapı sunar.

### Katmanlar:
1. **Domain**
   - **Entities**: Uygulamanın temel veri modellerini içerir.
   - **Interfaces**: Repository ve servis arayüzleri tanımlanır.

2. **Application**
   - **Features**: CQRS (Command-Query Responsibility Segregation) mimarisine uygun şekilde komutlar (örn. `CreateCourseCommand`) ve sorgular (örn. `GetCoursesQuery`) bulunur.
   - **Repositories**: Uygulamanın veri kaynağıyla olan işlem mantığı tanımlanır.

3. **Infrastructure**
   - **Persistence**: Veritabanı bağlantısı, repository implementasyonları gibi işlevler burada yer alır.
   - **Third-party integrations**: Ödeme entegrasyonu gibi harici sistemlerle entegrasyonları barındırır.

4. **API**
   - Kullanıcı, kurs, sepet, ödeme gibi alanlara yönelik **Controller** API'ları sunar.
   - **Authentication**: Kullanıcı kimlik doğrulama ve yetkilendirme için JWT kullanılır.

---

## Kullanılan Teknolojiler

### Backend
- **ASP.NET Core 7**: API ve servislerin oluşturulmasında kullanıldı.
- **MediatR**: CQRS mimarisini desteklemek için kullanıldı.
- **Entity Framework Core**: Veritabanı işlemleri için kullanıldı.
- **Stripe**: Ödeme entegrasyonu sağlamak için kullanıldı.
- **JWT**: Kullanıcı kimlik doğrulama için kullanıldı.

### Frontend
- **React**: Kullanıcı arayüzü için kullanıldı.
- **Redux Toolkit**: Global state yönetimi.
- **React Router**: Sayfa yönlendirmesi için kullanıldı.
- **CSS Modules**: Özelleştirilmiş ve modüler CSS kullanıldı.

---

## API Endpointleri

### **Authentication & User Management**
| Endpoint                  | Yöntem   | Açıklama                            |
|---------------------------|-----------|----------------------------------------|
| `/api/Auth/Login`         | POST      | Kullanıcı girişi yapar.            |
| `/api/Auth/RefreshToken`  | POST      | Token yenileme işlemi yapar.           |
| `/api/Users/CreateUser`   | POST      | Yeni kullanıcı oluşturur.           |
| `/api/UserEdit/update-user` | PUT     | Kullanıcı bilgilerini günceller.   |
| `/api/UserEdit/delete-user/{id}` | DELETE | Kullanıcıyı siler.            |

### **Catalog (Kurslar)**
| Endpoint                  | Yöntem   | Açıklama                            |
|---------------------------|-----------|----------------------------------------|
| `/api/Catalog`            | GET       | Tüm kursları getirir.                |
| `/api/Catalog/{Id}`       | GET       | Belirli bir kursun detaylarını getirir. |
| `/api/Catalog/AddCourseWithImage` | POST | Yeni bir kurs ekler.                   |
| `/api/Catalog/{Id}`       | DELETE    | Bir kursu siler.                       |

### **Basket (Sepet)**
| Endpoint                  | Yöntem   | Açıklama                            |
|---------------------------|-----------|----------------------------------------|
| `/api/Baskets`            | GET       | Sepet içeriğini getirir.             |
| `/api/Baskets`            | POST      | Sepete yeni bir kurs ekler.           |
| `/api/Baskets/{BasketItemId}` | DELETE | Sepetten bir kursu kaldırır.         |

### **Payment (Ödeme)**
| Endpoint                  | Yöntem   | Açıklama                            |
|---------------------------|-----------|----------------------------------------|
| `/api/Payment/Pay`        | POST      | Stripe entegrasyonu ile ödeme yapar. |
| `/api/Payment/PurchasedCourses` | GET | Satın alınan kursları listeler.     |

---

## React Modülleri

### Sayfalar
- **CourseList**: Mevcut kursları listeler, arama ve filtreleme yapar.
- **CourseDetail**: Seçili kursun detaylarını gösterir.
- **Basket**: Kullanıcının sepetini listeler ve sepetteki işlemleri yönetir.
- **Payment**: Kullanıcıdan kart bilgilerini alıp ödeme yapar.
- **Orders**: Kullanıcının geçmişte yaptığı satın alma işlemlerini listeler.

---

## Çalıştırma Talimatları

### Backend
1. Projeyi klonlayın:
   ```bash
   git clone https://github.com/kullanici/CourseSalesAPI.git
   ```
2. Gerekli bağımlılıkları yükleyin:
   ```bash
   cd CourseSalesAPI
   dotnet restore
   ```
3. Veritabanını oluşturun ve migrate edin:
   ```bash
   dotnet ef database update
   ```
4. Projeyi çalıştırın:
   ```bash
   dotnet run
   ```

### Frontend
1. React projesini klonlayın ve gerekli bağımlılıkları yükleyin:
   ```bash
   cd frontend
   npm install
   ```
2. Projeyi çalıştırın:
   ```bash
   npm start
   ```

---

## Stripe Entegrasyonu
**Backend** tarafında **Stripe** entegrasyonu yapıldı. Bunun için:
1. Stripe hesabınızın **Secret Key**'ini `appsettings.json` dosyasında tanımlayın:
   ```json
   "StripeSettings": {
       "SecretKey": "sk_test_...",
       "PublishableKey": "pk_test_..."
   }
   ```

---

## Proje Ekran Görünn\u00fmleri
- **Kurs Listesi**: Kullanıcılar mevcut kursları listeleyebilir.
- **Kurs Detayları**: Her kursun detay sayfası bulunur.
- **Sepet**: Kullanıcının sepetine eklediği kurslar listelenir.
- **Ödeme**: Kullanıcılar kart bilgilerini girerek ödeme yapabilir.
- **Satın Alınan Kurslar**: Kullanıcıların satın aldıkları kursları listeleme ekranı bulunur.

---

Not:Rol ile ilgili işlemlerin yapısına devam edilmektedir.

---




