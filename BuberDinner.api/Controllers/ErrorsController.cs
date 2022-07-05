using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.api.Controllers;

[ApiController]
public class ErrorsController: ControllerBase
{

   [Route("/error")]
   public IActionResult Error(){
      return Problem();
   }
}