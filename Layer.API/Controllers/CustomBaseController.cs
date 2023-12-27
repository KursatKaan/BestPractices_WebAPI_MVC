using Layer.Core.Abstract.DTO_s;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Layer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        [NonAction] //Endpoint olmadığını bildiriyoruz.
        public IActionResult CreateActionResult<T>(CustomResponseDto<T> response)
        {
            if (response.StatusCode == 204) //204 no content demektir.
                return new ObjectResult(null)
                {
                    StatusCode = response.StatusCode,
                };

            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
    }
}
