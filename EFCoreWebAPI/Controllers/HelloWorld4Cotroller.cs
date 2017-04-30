using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

[ApiVersion( "4.0" )]
[Route( "api/v{version:apiVersion}/helloworld" )]
public class HelloWorld4Controller : Controller {
    [HttpGet]
    public string Get() => "Hello world ver 4.0 !";
}