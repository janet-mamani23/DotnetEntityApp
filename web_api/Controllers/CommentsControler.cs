using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using web_api.mock;
using web_api.dto.comment;
using Microsoft.Extensions.Configuration.UserSecrets;
using entities_library.login;
using entities_library.comment;
using entities_library.movie;
/*
[ApiController]
[Route("api/[controller]")]


public class CommentsController : ControllerBase
{
    /*private readonly IMapper _mapper;
    private readonly List<Comment> _comments;
    private readonly List<User> _users;
    private readonly List<Movie> _movies;* ESTO ESTABA COMENTADO


    [HttpPut("{movieId}/comment")]
    public IActionResult CommentMovie(long movieId, RequestCommentDTO  requestCommentDTO )
    {
    {
        if (requestCommentDTO == null || string.IsNullOrEmpty(requestCommentDTO.Text))
    {
        return BadRequest("Invalid request data.");
    }

    // Obtener la instancia de MoviesMock para que halla pelicula registrada
    MoviesMock moviesMock = MoviesMock.Instance;
    }}}
    long commentId = CommentsMock.Instance.CreateCommentMock(      //creacion de un comentario en memoria.
            requestCommentDTO.Text,
            requestCommentDTO.User,
            requestCommentDTO. Movie,
            requestCommentDTO.CreatedAt 
    );
    
    // Obtengo el comentario creado
    Comment newComment = CommentsMock.Instance.GetCommentById(commentId);
    if (newComment == null)
    {
        return NotFound("Comment not found");
    }

    // Agregar el comentario a la película
    moviesMock.AddCommentToMovie(movieId, newComment);

    return Ok(new ResponseCommentDTO
    {
        Id = commentId,
        AvatarUser = requestCommentDTO.FileAvatar,
        UserName = $"{requestCommentDTO.User.Name} {requestCommentDTO.User.LastName}",
        Text = requestCommentDTO.Text,
        CreatedAt = requestCommentDTO.CreatedAt,
    });
    }
    }

    [HttpGet("{movieId}/comments")]
    public IActionResult GetComments(long movieId)
    {
        // Obtener la instancia de MoviesMock
        MoviesMock moviesMock = MoviesMock.Instance;

        // Buscar la película por su Id
        var movie = moviesMock.GetMovies().FirstOrDefault(m => m.Id == movieId);
        if (movie == null)
        {
            return NotFound("Movie not found");
        }

        // Devolver los comentarios de la película
        return Ok(movie.Comments);
    }
 
} */






