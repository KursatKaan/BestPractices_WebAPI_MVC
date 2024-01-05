# AspNetCore_WebAPI_NLayer

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

![Parameter Controller](https://github.com/KursatKaan/Asp.NetCore_Web_Api_NLayer/assets/140398297/912c2bff-c032-49c9-b06c-8fdf22053716)

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
