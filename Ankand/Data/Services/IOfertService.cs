using Ankand.Models;
using Ankand.Models;

namespace Ankand.Data.Services
{
    public interface IOfertService
    {
        IEnumerable<Oferta> GetAll_oferts(int id);
        void DeleteOferts(int id);
    }

}
