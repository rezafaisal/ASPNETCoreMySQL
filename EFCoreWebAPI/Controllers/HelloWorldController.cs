using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

[ApiVersion( "1.0" )]
[Route( "api/v{version:apiVersion}/[controller]" )]
public class HelloWorldController : Controller {
    [HttpGet]
    public string Get() {
        return "Hello world ver 1.0 !";
    }
}