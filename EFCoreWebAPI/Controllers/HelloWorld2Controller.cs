using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

[ApiVersion( "2.0" )]
[ApiVersion( "3.0" )]
[Route( "api/v{version:apiVersion}/helloworld" )]
public class HelloWorld2Controller : Controller {
    [HttpGet]
    public string Get() {
        return "Hello world ver 2.0 !";
    }

    [HttpGet, MapToApiVersion( "3.0" )]
    public string GetV3() {
        return "Hello world ver 3.0 !";
    }
}