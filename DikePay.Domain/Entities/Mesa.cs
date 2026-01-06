using DikePay.Domain.Enums;

namespace DikePay.Domain.Entities
{
    public class Mesa
    {
        public int Numero { get; set; }
        public AreaRestaurante Area { get; set; }
        public bool EstaOcupada { get; set; }
    }
}
