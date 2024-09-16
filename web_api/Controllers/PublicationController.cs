/*using Microsoft.AspNetCore.Mvc;
using web_api.mock;
using web_api.dto.common;


namespace web_api.Controllers;

[ApiController]
[Route("[controller]")]

public class PublicationController : ControllerBase
    {
        [HttpPost(Name = "CreatePost")]
        
    public IActionResult CreatePost([FromBody] CreatePostDTO createPostDto)
    {
        if (createPostDto == null || string.IsNullOrEmpty(createPostDto.Text))
        {
            return BadRequest("El texto es un campo obligatorio.");
        }

        long id = PostMock.Instance.CreatePost( 
            Id = id,
            Text = text,
            Comments = comments,
            Reactions = reactions,
            DateTime = dateTime,
            User = user,
            PostStatus = postStatus,
            ReportStatus = reportStatus);
            // aqui cambiar para Postresponsedto 
            return Ok(new UserPostResponseDTO
        {
            id = id,
            name = userPostRequestDTO.name,
            lastName = userPostRequestDTO.lastName,
            mail = userPostRequestDTO.mail
        });

        // Lógica para crear la publicación
        var nuevaPublicacion = new Post
        {
            Text = CreatePostDTO.Text,
            User = CreatePostDTO.User,
            DateTime = DateTime.Now,
            Comments = new List<Comment>(),
            Reactions = new List<Reaction>(),
            ReportStatus = Enabled
        };

        // Guardar la nueva publicación en la base de datos
        // ...

        return CreatedAtAction(nameof(CreatePost), new { id = nuevaPublicacion.Id }, nuevaPublicacion);
    }

        } */