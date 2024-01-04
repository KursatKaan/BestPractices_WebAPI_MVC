# AspNetCore_WebAPI_NLayer

---

## Global Exception Handler (Hata Tutucu)
Service katmanında **Exceptions** klasörü oluşturuyoruz. 

![ExceptionsFolder](https://github.com/KursatKaan/Asp.NetCore_Web_Api_NLayer/assets/140398297/866072fb-5fc3-4799-a84f-cf7ab3444e34)

Bu klasör içerisine istediğimiz hata tiplerine göre isim vererek **özel exception sınıfları** oluşturuyoruz ve bu oluşturduğumuz sınıflara, **Exception** sınıfını miras veriyoruz.

#### NotFound (404) hatası döndürmesi için özel exception sınıfı:
![NotFoundException](https://github.com/KursatKaan/Asp.NetCore_Web_Api_NLayer/assets/140398297/dbcd797d-2489-46b2-ba08-1d2bc0941e33)

#### BadRequest (400) hatası döndürmesi için özel exception sınıfı:
![ClientSideException](https://github.com/KursatKaan/Asp.NetCore_Web_Api_NLayer/assets/140398297/8c61305c-7ccb-42a7-a04f-a6c35192d834)


Uygulama katmanında **Middlewares** klasörü oluşturup içerisine kendi özel middleware'mizi tanımlamak için **UseCustomExceptionHandler** isminde sınıf oluşturuyoruz. *(BestPractice açısından sınıf ismini "Use" ile başlatıyoruz, çünkü program.cs dosyasında metotlar "Use" ile başlar.)*
 
![MiddlewaresFolder](https://github.com/KursatKaan/Asp.NetCore_Web_Api_NLayer/assets/140398297/cacd9f8b-e18c-4851-907e-3c3188e6ab17)

Servis katmanında **Exceptions** klasörü içerisinde oluşturduğumuz özel exception sınıflarını, uygulama katmanında oluşturduğumuz **UseCustomExceptionHandle** middleware'mizde hata kodları ile belirtiyoruz.
 
![UseCustomExceptionHandler](https://github.com/KursatKaan/Asp.NetCore_Web_Api_NLayer/assets/140398297/bf9796c2-8b38-42c5-a617-600ef2da25b6)

Oluşturduğumuz **UseCustomExceptionHandler** middleware metodumuzu uygulamanın program.cs sınıfında çağırıyoruz.

![UseCustomExceptionMethod](https://github.com/KursatKaan/Asp.NetCore_Web_Api_NLayer/assets/140398297/75dec456-fff5-4752-8863-fa00d7107952)

### Servis Katmanında Hata Fırlatmak:
Servis katmanı içerisinde istediğimiz şarta bağlı kontrolü gerçekleştirip, oluşturduğumuz **ExceptionHandler** sınıfımızda bulunan hataları fırlatabiliriz.
 
![HataFirlatma](https://github.com/KursatKaan/Asp.NetCore_Web_Api_NLayer/assets/140398297/ccaabc82-a578-4b62-8689-75c266bed2cd)

---

## Not Found Filter
Bu filtre, bir HTTP isteği işlenirken, belirli bir varlık (entity) ID'sinin mevcut olup olmadığını kontrol eder. Eğer belirli bir varlık bulunamazsa, "HTTP 404 Not Found" durumuyla birlikte özel bir hata mesajı döndürür. Bu filtre, genellikle varlık (entity) bulunamadığında uygulama davranışını ele almak için kullanılır.
