using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TaskBoard.Controllers
{
    [Authorize]
    public class BaseController : Controller { }
}
