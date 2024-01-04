using Layer.Core.Abstract.DTO_s;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Layer.API.Filters
{
    public class ValidateFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid) // Eğer model durumu geçerli değilse (yani gelen veri doğrulanamazsa)
            {
                //Kullanılan modelin validation kurallarına göre bir hata varsa hataları ve o hataya bağlı bilgilendirme Mesajlarını listele.
                var errors = context.ModelState.Values.SelectMany(x => x.Errors).Select(x=>x.ErrorMessage).ToList();

                // Bir BadRequestObjectResult oluştur ve bu nesneyi, hata kodeu 400 olarak özel bir hata yanıtını temsil eden CustomResponseDto ile doldur.
                context.Result= new BadRequestObjectResult(CustomResponseDto<NoContentDto>.Fail(400,errors));
            }
        }
    }

}
