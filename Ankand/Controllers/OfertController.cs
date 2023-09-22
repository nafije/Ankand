using Ankand.Data.Services;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace Ankand.Controllers
{
    public class OfertController : Controller
    {
        private readonly IOfertService _service;
        public OfertController(IOfertService service)
        {
            _service = service;
        }
        public IActionResult Index(int id)
        {
            var commentdata = _service.GetAll_oferts(id);
            return View(commentdata);
        }
 
    }
}
