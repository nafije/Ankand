using Ankand.Data;
using Ankand.Data.Services;
//using eblog.Migrations;
using Ankand.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace Ankand.Controllers
{
    public class ProduktiController : Controller
    {
        private readonly IProduktService _service;

       

        public ProduktiController(IProduktService service)
        {
            _service = service;
        }
       
        public IActionResult Index()
        {
            var postdata = _service.GetAll();
            return View(postdata);
        }
        //Get:Poste/Details.id
        public IActionResult Details(int id)
        {
            var postDetails = _service.GetById(id);
            var commentdata = _service.GetAll_oferts(id);
            if (postDetails == null)
            {
                return View("NotFound");
            }
            else
            {
               
                //var model = new Poste
                //{
                //    //Post  = postDetails,
                //    Coments = (List<Coments>)commentdata
                //};

                return View(postDetails);
            }
        }
        //Get: Poste/Add
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create([Bind("Name,Description,ImageURL,StartDate,EndDate")]Produkti produkti)
        {
            if(ModelState.IsValid)
            {
                return View(produkti);
            }
            else
            {
                produkti.BiderId = 3;
                _service.Add(produkti);
                return RedirectToAction(nameof(Index));
            }
        }
       
        //Get: Poste/Details/AddComent
        //public IActionResult AddComet()
        //{
        //    return View();
        //}
        [HttpPost]
        public IActionResult AddComet(Oferta oferta)
        {
            if (ModelState.IsValid)
            {
                return View(oferta);
            }
            else
            {
                //Add CommentedOn
                oferta.CreatedOn = DateTime.Now;
                oferta.BiderId = 3;
                oferta.FullName = "Nafije";
                _service.AddComents(oferta);
                return RedirectToAction(nameof(Index));
            }
        }
        //Delte
        public async Task<IActionResult> Delete(int id)
        {
            var postDetails = _service.GetById(id);
            if (postDetails == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(postDetails);
            }

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult DeleteConfirmed1(int id)
        {
            _service.DeleteOferts(id);
            return RedirectToAction("Index", "Produkti");
        }

    }
}
