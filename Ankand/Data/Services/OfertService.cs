using Ankand.Data;
using Ankand.Models;
using Ankand.Models;
using Microsoft.EntityFrameworkCore;

namespace Ankand.Data.Services
{
    public class OfertService : IOfertService
    {
        private readonly AppDbContext _context;

        public void DeleteComent(int id)
        {
            var result = _context.Poste.FirstOrDefault(n => n.ID == id);
            _context.Poste.Remove(result);
            _context.SaveChanges();
        }

        public void DeleteOferts(int id)
        {
            throw new NotImplementedException();
        }


        //public IEnumerable<Coments> GetAll(int id)
        //{
        //    var result = _context.Coments.Where(m =>m.PostID ==id ).ToList();
        //    return result;
        //}


        public IEnumerable<Oferta> GetAll_oferts(int id)
        {
            return _context.Oferta.Where(m => m.ProduktID == id).ToList();
        }
    }
}
