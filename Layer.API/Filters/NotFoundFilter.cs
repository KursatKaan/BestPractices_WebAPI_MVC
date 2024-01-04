using Layer.Core.Abstract.DTO_s;
using Layer.Core.Abstract.Entities;
using Layer.Core.Abstract.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics.Metrics;
using System.Reflection.Metadata;

namespace Layer.API.Filters
{
    public class NotFoundFilter<T> : IAsyncActionFilter where T : BaseEntity //AnyAsync metodunda Id'ye erişebilmek için BaseEntity sınıfından miras aldık.
    {
        private readonly IService<T> _service;

        public NotFoundFilter(IService<T> service)
        {
            _service = service;
        }

        // Action filtresi çalıştırıldığında bu metot devreye girer.
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var idValue = context.ActionArguments.Values.FirstOrDefault(); // İstek içindeki parametre değerlerini (Id) içeren koleksiyondan ilk değeri al.

            if (idValue == null)
            {
                await next.Invoke(); // Eğer parametre değeri (Id) bulunamazsa, bir sonraki adıma geç.
                return;
            }

            var id = (int)idValue; // Parametre değeri (Id) varsa gelen bu parametre değerini integar'a dönüştürüp "id" değişkenine ata.
            var anyEntity = await _service.AnyAsync(x => x.Id == id); // Parametre (Id) ile ilgili varlık (entity) mevcut mu diye servis üzerinden kontrol yap ve "anyEntity" değişkenine ata.

            if (anyEntity == null)
            {
                await next.Invoke(); // Eğer entity bulunamazsa, bir sonraki adıma geç.
                return;
            }

            context.Result = new NotFoundObjectResult(CustomResponseDto<NoContentDto>.Fail(404, $"{typeof(T).Name}({id}) not found")); // Entity bulunamama durumunda bu hata mesajını döndür.
        }
    }
}

