﻿using Ankand.Models;
using Ankand.Models;
using System.Linq.Expressions;

namespace Ankand.Data.Services
{
    public interface SessionEndMiddleware
    {

        IEnumerable<Produkti> GetAll();
        IEnumerable<Produkti> GetAllBids(string id);
        Produkti GetById(int id);
        void Add(Produkti Produkti);
        Produkti Update(int id, Produkti newprodukt);
        void Delete(int id);
        IEnumerable<Oferta> GetAll_oferts(int id);
        void AddComents(Oferta oferta);
        void DeleteOferts(int id);
    }
}
