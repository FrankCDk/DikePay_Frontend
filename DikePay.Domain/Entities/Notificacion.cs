namespace DikePay.Domain.Entities
{
    public class Notificacion
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Titulo { get; set; } = string.Empty;
        public string Mensaje { get; set; } = string.Empty;
        public DateTime Fecha { get; set; } = DateTime.Now;
        public bool Leido { get; set; } = false;
        public TipoNotificacion Tipo { get; set; } // Error, Info, Alerta
    }

    public enum TipoNotificacion { Info, Alerta, Error, Exito }
}
