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
        private readonly IProduktService _service;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly AppDbContext _context;

        public ProduktiController(AppDbContext context, IProduktService service,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _service = service;
            _userManager = userManager;
        }

        [Authorize] 
        public IActionResult IndexBid()
        {
            var currentUser =  _userManager.GetUserAsync(User).Result;
            var postdata = _service.GetAllBids(currentUser.Email);
            return View(postdata);
        }
        public IActionResult Index()
        {
            var postdata = _service.GetAll();
            var currentUser = _userManager.GetUserAsync(User).Result;

            var isCurrentUserCreatorList = new List<bool>();

            foreach (var product in postdata)
            {
                bool isCurrentUserCreator = product.BiderId == currentUser.Email;
                isCurrentUserCreatorList.Add(isCurrentUserCreator);
            }
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

                //marrim id e ofertes 
                int offertid = oferta.ID;
                return RedirectToAction(nameof(AddWalletBudget), new { id = offertid });
            }
        }
        //Add wallet 
        public IActionResult AddWalletBudget(int id)
        {
            var user = _userManager.GetUserAsync(User).Result;
            decimal totalBids = GetTotalBidsByBidder1(user.Email);
            decimal bilancimbetur = 1000 - totalBids;
            if (bilancimbetur < 0)
            {
                DeleteConfirmed1(id);
                return  View("Bilanci_Negativ");
            }
            var wallet = new Wallet
            {
                BidderId = user.Email,
                Amount = 1000 ,
                OfertId=id,
                Balance= bilancimbetur

            };
            _service.AddWallet(wallet);
            return RedirectToAction(nameof(Index));
        }
        
        private decimal GetTotalBidsByBidder1(string email)
        {
            decimal totalBids = _service.GetTotalBidsByBidder(email);

            return totalBids;
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
            return RedirectToAction(nameof(AddWalletBudget), new { id = id });
        }

    }
}
