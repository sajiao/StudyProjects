﻿using System;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [EnableCors("AllowAnyOrigin")]
    [Produces("application/json")]
    public class BaseController : Controller
    {

    }
}