# SOA ve REUSABILITY
Öncelikle Web API mantığını kavramamız için SOA ve Reusability kavramlarını bilmemiz gerekir. 

## Reusability *(Yazılımın tekrar kullanımı)*
Reusability, bir yazılım sisteminin geliştirilmesinde kullanılan bilginin ve ürünlerin, aynı şekilde veya ufak değişikliklerle yeni yazılımda kullanılması olarak tanımlanabilir. Yazılımlarda yeniden kullanılabilirlik artırıldığında düşük iş gücüyle daha büyük işler yapmak ve dolayısıyla hızlı geliştirilen, düşük maliyetli yazılımlar elde etmek mümkün olacaktır.

Yazılımcılar Reusibilty'i daha iyi bir seviyeye getirebilmek için sürekli olarak yeni yöntemler geliştirmişlerdir. 
#### 1- Unstructured Programming (Yapılandırılmamış Programlama)
#### 2- Procedural Programming (Prosedürel Programlama)
#### 3- [OOP] Object Oriented Programming (Nesne yönelimli programlama)
#### 4- Layered Architecture (Katmanlı Mimari)
#### 5- [SOA] Service Oriented Architecture (Servis Odaklı Mimari)
Geldiğimiz dönemde son olarak da SOA reusibilty'i en yüksek seviyede uygulamamızı sağlamaktadır.

## SOA *(Servis Odaklı Mimari)*
SOA, sistemlerin tekonoloji bağımsız bir şekilde ve tek noktadan sürdürülebilir bir sistem inşa edilmesine odaklanır. Daha iyi anlayabilmemiz için örneklendirerek anlatacağım.

Yöneticinizin bir Web projesi istediğini düşünelim, yani sadece Web'de çalışacak bir proje. Biz de bu isteğe uygun bir Web Projesi yaptık diyelim.

![WebProject](https://github.com/KursatKaan/AspNetCore_NTier_WebAPI_BestPractices/assets/140398297/cfecfc80-ae25-4a14-a49a-f7d4ab396483)

Sonrasında yöneticiniz gelip müşterinin bir de Android ve IOS ortamları içinde istediğini iletti. Bu durumda bizim **Business** ve **DataAccess** katmanlarını tekrar Mobil Proje için de yazmamız gerekir. Ayrıca Mobil veya Web projemizin herhangi birinde bir değişiklik yapmamız gerektiğinde sürekli olarak bu değişikliği diğer projeyede taşımamız gerekecek. Ancak bu hiç de sürdürülebilir ve mantıklı bir durum değildir.

![Web-Mobile Project](https://github.com/KursatKaan/AspNetCore_NTier_WebAPI_BestPractices/assets/140398297/361414f7-8122-4e50-93e3-57eb5aaf7cbb)

İşte bu durumda biz tek bir ana servis oluşturup Mobil ve Web projelerinin arayüzlerine tek bir noktadan servis sağlıyoruz. Bu sayede herhangi bir değişiklik sadece tek bir noktadan olacak ve bütün arayüzlere yansıyacak. ayrıca servisimizi kodladığımız dil ile arayüzlerin dilleri birbirinden tamamen bağımsız olabilir.

![Service Project](https://github.com/KursatKaan/AspNetCore_NTier_WebAPI_BestPractices/assets/140398297/7fe70fba-ddcf-4f15-95bd-60f4c38bac2e)

## SOA Technologies
#### 1- .Net Remoting
#### 2- Web Services (xml)
#### 3- WCF (Windows Communication Foundation)
#### 4- RESTful Services (Web APIs)

---

# REST (Representational State Transfer)
#### Türkçe açılımı Temsili Durum Aktarımı.
#### Arayüzü olan her cihaz için servis sağlar.
#### Implementasyonu kolaydır.
#### JSON formatında küçük boyutlu veri döndürür.
#### HTTP'nin tüm metotlarını kullanabilir. (GET, POST, PUT, DELETE)
#### Web API'ler RESTful servisleridir.

![RESTful API](https://github.com/KursatKaan/AspNetCore_NTier_WebAPI_BestPractices/assets/140398297/9c2ed142-5834-4db2-88dd-2157327bbdc3)

---

# Fluent Validation
Fluent Validation, İş nesnelerimiz için doğrulama kuralları oluşturmak amacıyla akıcı bir arabirim ve lambda ifadeleri kullanan küçük bir .NET doğrulama kütüphanesidir
FluentValidation ve benzeri ürünlerin kullanılması, verilerin doğru şekilde yani verilerin oluştururken konulmuş kısıtlamaları sağlayarak kurallara uyumlu halde olmasını ve kullanıcı ya da sistem kaynaklı hataların oluşmasını engeller.

Service katmanında **Validations** klasörü oluşturup içerisine istediğimiz modelin DTO'suna göre **ModelDto Validator** sınıfı oluşturuyoruz.

![Validator Folder](https://github.com/KursatKaan/Asp.NetCore_Web_Api_NLayer/assets/140398297/0e065edf-c3c6-44fc-bd4b-d36ca695a04f)

Oluşturduğumuz **ModelDto Validator** sınıfına **AbstractValidator<model>** sınıfını miras alıyoruz ve sınıfta oluşturduğumuz constructor içerisine istediğimiz validasyon kurallarını yazıyoruz.

![Validation Rules](https://github.com/KursatKaan/Asp.NetCore_Web_Api_NLayer/assets/140398297/45368a9d-cda7-4acb-85dd-bc2868d360af)

Belirttiğimiz kuralların uygulanacağı uygulamanın katmanında (Api, MVC vb.) **Filters** Klosörü oluşturup içerisinde **ValidateFilterAttribute** sınıfı oluşturuyoruz.

![ValidateFilterAttribute Class](https://github.com/KursatKaan/Asp.NetCore_Web_Api_NLayer/assets/140398297/f9e1d1ea-911c-4f34-bf4b-eedfa0fb57d3)

Oluşturduğumuz **ValidateFilterAttribute** sınıfına **ActionFilterAttribute** sınıfını miras alıyoruz.

![ActionFilterAttribute Kalıtım](https://github.com/KursatKaan/Asp.NetCore_Web_Api_NLayer/assets/140398297/e4f9f39b-094c-4a79-ab6c-303f782bf04d)

Oluşturduğumuz **ValidateFilterAttribute** sınıfı içerisine **OnActionExecuting** metodunu override ediyoruz ve metot içerisinde model durumunu kontrol edip hata olması durumunda istediğimiz hata modelini ve hata kodunu döndürüyoruz.

![OnActionExcetuting Method](https://github.com/KursatKaan/Asp.NetCore_Web_Api_NLayer/assets/140398297/7ac7e1c5-e565-45ef-ad28-eb8ad7cdfe59)

Bu validasyon kurallarını her controllerde tek tek belirtmek yerine tüm projede geçerli olması için Program.cs'de belirtiyoruz.

![Program cs](https://github.com/KursatKaan/Asp.NetCore_Web_Api_NLayer/assets/140398297/51af7922-d0cb-47a0-b89e-a4f8486cce14)

API katmanında bizim kendi oluşturduğumuz özel Validasyon kontrolünü geçerli kılabilmek için **Default** olarak geçerli olan validasyon kontrolünü bastırarak etkisiz kılıyoruz.

![Remove API Default Validation](https://github.com/KursatKaan/Asp.NetCore_Web_Api_NLayer/assets/140398297/0cfa3147-e4a1-4922-8366-d2551885f390)

---

# Global Exception Handler Middleware (Hata Tutucu)
Bu Middleware, merkezi olarak exceptionlarımızı yönetebilmemizi sağlar. Her bir methodu try catch ve throw blockları ile exceptionları handle etmeye çalışmak yerine, 
her bir request ve response'yi handle eden ve tüm exceptionları tek bir yerden yöneten bir yapıdır.
Örneğin, uygulama içinde herhangi bir kodda exception throw ettiğimizde tüm methodların catch blocklarında log methodunu çağırmak yerine, Middlewarede bir kez log mekanizmamızı oluşturuyoruz.

Service katmanında **Exceptions** klasörü oluşturuyoruz.

![ExceptionsFolder](https://github.com/KursatKaan/Asp.NetCore_Web_Api_NLayer/assets/140398297/866072fb-5fc3-4799-a84f-cf7ab3444e34)

Oluşturduğumuz **Exceptions** klasörü içerisine istediğimiz hata tiplerine göre isim vererek **Özel Exception Sınıfları** oluşturuyoruz ve bu oluşturduğumuz sınıflara **Exception** sınıfını miras alıyoruz.

#### NotFound (404) hatası döndürmesi için özel exception sınıfı:
![NotFoundException](https://github.com/KursatKaan/Asp.NetCore_Web_Api_NLayer/assets/140398297/dbcd797d-2489-46b2-ba08-1d2bc0941e33)

#### BadRequest (400) hatası döndürmesi için özel exception sınıfı:
![ClientSideException](https://github.com/KursatKaan/Asp.NetCore_Web_Api_NLayer/assets/140398297/8c61305c-7ccb-42a7-a04f-a6c35192d834)


Uygulama katmanında **Middlewares** klasörü oluşturup içerisine kendi özel middleware'mizi tanımlamak için **UseCustomExceptionHandler** isminde sınıf oluşturuyoruz. *(BestPractice açısından sınıf ismini "Use" ile başlatıyoruz, çünkü program.cs dosyasında metotlar "Use" ile başlar.)*

![MiddlewaresFolder](https://github.com/KursatKaan/Asp.NetCore_Web_Api_NLayer/assets/140398297/cacd9f8b-e18c-4851-907e-3c3188e6ab17)

Servis katmanında **Exceptions** klasörü içerisinde oluşturduğumuz özel exception sınıflarını, uygulama katmanında oluşturduğumuz **UseCustomExceptionHandle** middleware'mizde hata kodları ile belirtiyoruz.

![UseCustomExceptionHandler](https://github.com/KursatKaan/Asp.NetCore_Web_Api_NLayer/assets/140398297/bf9796c2-8b38-42c5-a617-600ef2da25b6)

Eğer middlewareinize gelen exception type ClientSideException ise, response Status codeu 400 yani BadRequest, NotFoundException ise 404 status codeu ile NotFound olacaktır. Aksi halde response içinde 500 yani InternalServerError status code dönecektir. Middlewareinizi istediğiniz şekilde customize edebilir, yeni Exception typelar oluşturup kullanabilirsiniz.

Oluşturduğumuz **UseCustomExceptionHandler** middleware metodumuzu uygulamanın program.cs'de tanımlayıp kullanıma hazır hale getiriyoruz.

![UseCustomExceptionMethod](https://github.com/KursatKaan/Asp.NetCore_Web_Api_NLayer/assets/140398297/75dec456-fff5-4752-8863-fa00d7107952)

### Servis Katmanında Hata Fırlatmak:
Servis katmanı içerisinde istediğimiz şarta bağlı kontrolü gerçekleştirip, oluşturduğumuz **ExceptionHandler** sınıfımızda bulunan hataları fırlatabiliriz.

![HataFirlatma](https://github.com/KursatKaan/Asp.NetCore_Web_Api_NLayer/assets/140398297/ccaabc82-a578-4b62-8689-75c266bed2cd)

---

# Not Found Filter
Bu filtre, bir HTTP isteği işlenirken, belirli bir varlık (entity) ID'sinin mevcut olup olmadığını kontrol eder. Eğer belirli bir varlık bulunamazsa, "HTTP 404 Not Found" durumuyla birlikte özel bir hata mesajı döndürür. Bu filtre, genellikle varlık (entity) bulunamadığında uygulama davranışını ele almak için kullanılır.

Uygulama katmanında **Filters** klasörü oluşturup içerisine tüm entity'ler için kullanılabilecek **NotFoundFilter** isminde Dinamik bir sınıf oluşturuyoruz.

![NotFoundFilter Folder](https://github.com/KursatKaan/Asp.NetCore_Web_Api_NLayer/assets/140398297/da3099eb-9e97-423d-ab0d-5c94ded428fb)

Bu sınıfa **IAsyncActionFilter** Interface'sini miras alıyoruz ve bu Interface'nin metodunu implemente ediyoruz.

![IAsyncActionFilter Method Implemente](https://github.com/KursatKaan/Asp.NetCore_Web_Api_NLayer/assets/140398297/5acc00f3-7037-4ee6-9be7-fa458096937a)

**NotFoundFilter<T>** sınıfı içerisinde service katmanındaki **IService(*IGenericService*)** Interface'sini çağırıp constructor oluşturuyoruz

![IService Constructor](https://github.com/KursatKaan/Asp.NetCore_Web_Api_NLayer/assets/140398297/45a0b070-ea19-4459-9766-027b759a8924)

**OnActionExecutionAsync** metodu içerisinde, gelen parametreye göre kontrolleri sağlayıp istediğimiz hata durumunu ve mesajını döndürüyoruz.

![Parameter Control](https://github.com/KursatKaan/Asp.NetCore_Web_Api_NLayer/assets/140398297/c5ce9ae6-6110-45d2-b203-39534e8a24e1)

Bu oluşturduğumuz **NotFoundFilter** içerisinde Constructor ile parametre aldığından dolayı Program.cs'ye bildiriyoruz.

![NotFoundFilter Program cs](https://github.com/KursatKaan/Asp.NetCore_Web_Api_NLayer/assets/140398297/a48c3d3e-ce30-438f-9288-53ac87bbf036)

Ve yine aynı sebepten dolayı kullanırken de **[Service Filter]** Attributesi ile kullanıyoruz.

![ServiceFilter Use](https://github.com/KursatKaan/Asp.NetCore_Web_Api_NLayer/assets/140398297/d696969a-8b69-4867-808d-8224a44bd40f)

---

# AutoFac (IoC Container)
**AutoFac**, .NET tabanlı framework için geliştirilmiş **IOC** *(Inversion Of Control)* container’dır. Hem bağımlılıklarımızı yöneten hem de **AOP** *(Aspect-Oriented Programming)* destekleyen ve bu işleri çok efektif bir şekilde yapabilen bir kütüphanedir.

### IoC Container nedir ?
Oluşturulacak olan nesnelerin yaşam döngüsünün yönetilmesidir. Yani belirlenen koşullarda, herbir request için **Singelton** *(Tekil)* şekilde ilgili nesne örneğinin bizim adımıza üretilmesidir. Bu bize kolaylık ve kodda gözle görülür bir sadelik getirir.

### AOP nedir ?
Business katmanında sürekli olarak tekrar eden Logging , Authorization , Exception Handling , Cache gibi her aşamada kontrol ettiğimiz durumları modüler yapıya kavuşturup tek yerden yönetilmesini amaçlar.

### Varsayılan Built-in DI Container'den farkları;

**Built-in DI Container** kullanarak Constructor ve Method enjekte edebiliyoruz.

**AutoFac** ile Constructor, Method ve ekstra olarak Property enjekte edebilirken ayrıca **Dinamik** olarak servis enjekte edebiliriz.
*(Örn: Sonu X ile biten 10 adet servisimiz olsun, Biz Sonu X ile biten bütün Interfaceleri tek satırda enjekte edebiliyoruz.)*


Uygulama katmanında program.cs'de **UseServiceProviderFactory** Metodunu **AutofacServiceProviderFactory()** sınıfı ile birlikte ekliyoruz.

![Use Service Provider Factory](https://github.com/KursatKaan/Asp.NetCore_Web_Api_NLayer/assets/140398297/82952bf0-5783-44c0-a8fd-533411f24924)

Uygulama katmanında **Modules** klasörü oluşturup içerisine IoC Containerimiz olacak **RepoServiceModule** isminde sınıf oluşturuyoruz.

![Create Modules Folder](https://github.com/KursatKaan/Asp.NetCore_Web_Api_NLayer/assets/140398297/9a72b74d-b732-4975-9354-e6f5420f566a)

Oluşturduğumuz **RepoServiceModule** sınıfına **Module** sınıfını miras alıp **Load** metodunu override ediyoruz.

![RepoServiceModule Class](https://github.com/KursatKaan/Asp.NetCore_Web_Api_NLayer/assets/140398297/7f383174-4acd-4b66-b1e9-7412cf4f46b9)

Load metodu içerisine bağımlılıkları ekliyoruz.

![Save Dependencies](https://github.com/KursatKaan/Asp.NetCore_Web_Api_NLayer/assets/140398297/d5d10857-da68-45fb-a415-33c88710e9c4)

IoC Container'e eklediğimiz bağımlılıkları Program.cs'den kaldırıyoruz.

![Remove Program cs](https://github.com/KursatKaan/Asp.NetCore_Web_Api_NLayer/assets/140398297/2844faf8-8e49-4788-976f-ef7ff036fba5)

Son olarak Program.cs'ye oluşturduğumuz **IoC Container** *(RepoServiceModule)* ekliyoruz.

![Save ContainerModul](https://github.com/KursatKaan/Asp.NetCore_Web_Api_NLayer/assets/140398297/3b872e6e-3d61-4d90-a307-9a202c3d56be)

---

# In-Memory-Caching

ASP.NET Core'da in-memory cache kullanarak verileri geçici bir süre boyunca bellekte saklamak oldukça yaygındır. Son kullanıcıya nadiren güncellenen veya geniş aralıklarla tazelenen verileri her seferinde veritabanı üzerinden elde edetmek yerine önbelleğe alınmış veriyi sunarız.

Biz bu projede sadece Product için in-memory-cache işlemi kullanacağız.

In-Memory Cache özelliğini kullanabilmek için **MemoryCache** modülünü Program.cs'ye service olarak ekliyoruz.

![Add Cache Module Program cs](https://github.com/KursatKaan/Asp.NetCore_Web_Api_NLayer/assets/140398297/a79ab143-2dd4-486a-8ee4-dd05971af4ab)

Caching katmanında Product modeli için **ProductServiceCaching** sınıfı oluşturuyoruz ve **IProductService** interface'sini miras alıyoruz.

![ProductServiceWithCaching Class](https://github.com/KursatKaan/Asp.NetCore_Web_Api_NLayer/assets/140398297/d1501e33-7166-4679-a252-ff2bf434d81b)

Tüm productları tutacağımız bir **CacheKey** belirliyoruz ve kullanacağımız *(IMapper, IMemoryCache, IProductRepository, IUnitOfWork)* interfacelerini ekliyoruz.

![Add Interfaces](https://github.com/KursatKaan/Asp.NetCore_Web_Api_NLayer/assets/140398297/a089d556-e2ae-45ca-9945-4a3d1f314a5d)

Constructor oluşturuyoruz ve Interface'lerimizi bu constructorda tanımlıyoruz.

![Add Constructor](https://github.com/KursatKaan/Asp.NetCore_Web_Api_NLayer/assets/140398297/12dd2db7-a9e3-4b57-a4e6-38d16ae3b5c1)

Aynı constructor içerisinde eğer önbellekte ürünler yoksa, veritabanından çekip önbelleğe alacağı **if koşulunu** ekliyoruz.

![Caching](https://github.com/KursatKaan/Asp.NetCore_Web_Api_NLayer/assets/140398297/99c4fd1f-dc36-4f44-9808-978d7a6d5aad)

İşlemlerimizi kolaylaştırmak için tüm ürünleri önnbelleğe alma işlemini yapacak **CacheAllProduct** isminde metot oluşturuyoruz. 

![CacheAllProduct](https://github.com/KursatKaan/Asp.NetCore_Web_Api_NLayer/assets/140398297/caaddf31-92ef-4902-b340-a0cf2f2d8b80)

Önbelleğe alma işlemi uygulayacağımız metotlarda *(Ekleme, Silme, Güncelleme)* oluşturduğumuz **CacheAllProducts** metodunu kullanıyoruz. 

![Save Cache](https://github.com/KursatKaan/Asp.NetCore_Web_Api_NLayer/assets/140398297/6e072e91-0c7f-46fa-b6af-8d11b56204c8)

Tüm ürünleri önbellekten alma işlemini **(_memoryCache.Get<List<Model>>(CacheProductKey)** ile sağlıyoruz.

![Get From Cache](https://github.com/KursatKaan/Asp.NetCore_Web_Api_NLayer/assets/140398297/9f886ab6-b217-4db7-8962-bb19c7625506)

---
