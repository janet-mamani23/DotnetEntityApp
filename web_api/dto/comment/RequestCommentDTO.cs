using Microsoft.AspNetCore.Authorization.Infrastructure;
using entities_library.login;
using entities_library.movie;

public class RequestCommentDTO
{
    public string FileAvatar {get; set;}
    public User User { get; set; } //Utilizariamos el nombre y apellido de quien hace el comentario
    public Movie Movie { get; set; } //Utilizariamos el id y nombre de la pelicula que estamos situados
    public required string Text {get; set; }
    public DateTime CreatedAt { get; set; }
}
