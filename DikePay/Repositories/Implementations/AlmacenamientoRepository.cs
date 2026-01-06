using DikePay.Entities;
using DikePay.Repositories.Interfaces;
using DikePay.Services.Interfaces;

namespace DikePay.Repositories.Implementations
{
    public class AlmacenamientoRepository : IAlmacenamientoRepository
    {

        private readonly IDataBaseContext _context;

        public AlmacenamientoRepository(IDataBaseContext context)
        {
            _context = context;
        }

       
    }
}
