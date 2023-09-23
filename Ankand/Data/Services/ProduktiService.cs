using Ankand.Data;
using Ankand.Models;
using Ankand.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq.Expressions;

namespace Ankand.Data.Services
{
    [Authorize]
    public class ProduktiService : IProduktService
    {
        private readonly AppDbContext _context;
        public ProduktiService(AppDbContext context)
        {
            _context = context;
        }
        public void Add(Produkti Produkti)
        {
            _context.Poste.Add(Produkti);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var result = _context.Oferta.Where(c => c.ProduktID == id).ToList();
            _context.Oferta.RemoveRange(result); 

            var result1 = _context.Poste.FirstOrDefault(p => p.ID == id);
            if (result1 != null)
            {
                _context.Poste.Remove(result1);
            }

            _context.SaveChanges();
        }

        public IEnumerable<Produkti> GetAll()
        {
            DateTime currentDate = DateTime.Now;

            var result = _context.Poste
            .AsEnumerable()
            .Select(p => new
            {
                Post = p,
                RemainingTime = (p.EndDate - currentDate).TotalSeconds 
            })
            .Where(p => p.RemainingTime > 0) 
            .OrderBy(p => p.RemainingTime) 
            .Select(p => p.Post) 
            .ToList();
            return result;
        }
        public Produkti GetById(int id)
        {
            var result = _context.Poste.FirstOrDefault(n => n.ID == id);
            return result;
        }
        public IEnumerable<Oferta> GetAll_oferts(int id)
        {
            var result = _context.Oferta.Where(m => m.ProduktID == id).ToList();
            return result;
        }

        

        public Produkti Update(int id, Produkti newprodukt)
        {
            _context.Update(newprodukt);
            _context.SaveChanges();
            return newprodukt;
        }

        public void AddComents( Oferta oferta)
        {
            _context.Oferta.Add(oferta);
            _context.SaveChanges();
        }

        public void DeleteOferts(int id)
        {
            var result = _context.Oferta.FirstOrDefault(n => n.ID == id);
            _context.Oferta.Remove(result);
            _context.SaveChanges();
        }
    }
}
