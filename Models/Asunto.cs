namespace AsuntoService.Models
{
    public class Asunto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public byte Status { get; set; }

        public enum EStatus { Eliminado = 0, Activo = 1 }
    }
}