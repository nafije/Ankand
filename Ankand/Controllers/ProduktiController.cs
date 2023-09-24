using Ankand.Data;
using Ankand.Data.Services;
using Ankand.Data.ViewModels;
//using eblog.Migrations;
using Ankand.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Ankand.Controllers
{
    public class ProduktiController : Controller
    {
        private readonly SessionEndMiddleware _service;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly AppDbContext _context;

        public ProduktiController(AppDbContext context,SessionEndMiddleware service,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _service = service;
            _userManager = userManager;
        }

        [Authorize] // Ensure that the user is authenticated
        public IActionResult IndexBid()
        {
            // Get the current logged-in user
            var currentUser =  _userManager.GetUserAsync(User).Result;
            var postdata = _service.GetAllBids(currentUser.Email);
            return View(postdata);
        }
        public IActionResult Index()
        {
            var postdata = _service.GetAll();
            var currentUser = _userManager.GetUserAsync(User).Result;

            // Create a list to store boolean values indicating if the current user is the creator for each product.
            var isCurrentUserCreatorList = new List<bool>();

            // Iterate through each product and check if the current user is the creator.
            foreach (var product in postdata)
            {
                bool isCurrentUserCreator = product.BiderId == currentUser.Email;
                isCurrentUserCreatorList.Add(isCurrentUserCreator);
            }

            // Pass the products and the list of boolean values to the view.
            var viewModel = new PostViewModel
            {
                Products = postdata,
                IsCurrentUserCreatorList = isCurrentUserCreatorList
            };

            return View(viewModel);
        }
        //Get:Poste/Details.id
        public IActionResult Details(int id)
        {
            var postDetails = _service.GetById(id);
            var commentdata = _service.GetAll_oferts(id);
            var currentUser = _userManager.GetUserAsync(User).Result;
            bool isCurrentUserCreator = postDetails.BiderId == currentUser.Email;
            if (postDetails == null)
            {
                return View("NotFound");
            }
            else
            {
                var viewModel = new PostDetailsViewModel
                {
                    Produkti = postDetails,
                    IsCurrentUserCreator = isCurrentUserCreator
                };
                return View(viewModel);
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
                var user = _userManager.GetUserAsync(User).Result;
                produkti.BiderId = user.Email;
                using (var context = new AppDbContext()) // Replace YourDbContext with your actual DbContext
                {
                    context.Attach(produkti);
                    context.SaveChanges();
                }
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
                var user = _userManager.GetUserAsync(User).Result;
                oferta.BiderId = user.Email;
                oferta.FullName = user.FullNAme;
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
