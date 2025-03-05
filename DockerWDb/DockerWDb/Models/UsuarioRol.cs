namespace DockerWDb.Models
{
    public class UsuarioRol
    {
        public long UsuarioId { get; set; }
        public UserModel? Usuario { get; set; }
        public long RolId { get; set; }
        public Rol? Rol { get; set; }
    }
}
