using Application.DTOs.Common;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Common
{
    public abstract class BaseController : Controller
    {

        //Não utilizar este método.
        [NonAction]
        public override JsonResult Json(object? data)
        {
            throw new NotImplementedException();
        }

        protected IActionResult JsonError(Validation validation)
        {
            string errorMessage = string.Join(", ", validation.ValidationErrors);

            return BadRequest(new { Message =  errorMessage});
        }

        protected IActionResult JsonError(string errorMessage)
        {
            return BadRequest(new { Message =  errorMessage});
        }

        protected IActionResult JsonSuccess()
        {
            return Ok(new { Message = "Operação realizada com sucesso!" });
        }

        protected IActionResult JsonSuccess(string message)
        {
            return Ok(new { Message = message });
        }
    }
}