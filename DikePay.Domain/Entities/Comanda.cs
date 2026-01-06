using DikePay.Domain.Enums;
using SQLite;

namespace DikePay.Domain.Entities
{

    [Table("Comandas")]
    public class Comanda
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public TipoServicio Servicio { get; set; }
        public string MesaOReferencia { get; set; } = ""; // "Mesa 5" o "Nombre Cliente"
        public DateTime Fecha { get; set; } = DateTime.Now;
        public bool EstaPagada { get; set; } = false;
        public decimal Total { get; set; }

        // Almacenamos los items como JSON en SQLite para simplificar la persistencia 
        // de una entidad compleja en una sola tabla de móvil.
        public string ItemsJson { get; set; } = "[]";
    }
}
