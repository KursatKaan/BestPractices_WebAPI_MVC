# AspNetCore_WebAPI_NLayer

---

# Global Exception Handler Middleware (Hata Tutucu)
Bu Middleware, merkezi olarak exceptionlarımızı yönetebilmemizi sağlar. Her bir methodu try catch ve throw blockları ile exceptionları handle etmeye çalışmak yerine, 
her bir request ve response'yi handle eden ve tüm exceptionları tek bir yerden yöneten bir yapıdır.
Örneğin, uygulama içinde herhangi bir kodda exception throw ettiğimizde tüm methodların catch blocklarında log methodunu çağırmak yerine, Middlewarede bir kez log mekanizmamızı oluşturuyoruz.

Service katmanında **Exceptions** klasörü oluşturuyoruz.

![ExceptionsFolder](https://github.com/KursatKaan/Asp.NetCore_Web_Api_NLayer/assets/140398297/866072fb-5fc3-4799-a84f-cf7ab3444e34)

Bu klasör içerisine istediğimiz hata tiplerine göre isim vererek **özel exception sınıfları** oluşturuyoruz ve bu oluşturduğumuz sınıflara **Exception** sınıfını miras alıyoruz.

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
