using Microsoft.AspNetCore.Mvc;
using entities_library.movie;

namespace web_api.Controllers;

[ApiController] // Anotaciobn,Indica que es un controlador de API(manejan solicitudes y generan respuestas).
[Route("[controller]")] // Anotacion, Define la ruta base para las rutas de este controlador.

public class MovieController : ControllerBase
{
    private readonly IMovie <MovieController> _movie;

    public MovieController(IMovie <MovieController> movie)
    {
        _movie = movie;
    }

    // Devuelve una lista de películas por Genero
    [HttpGet(Name = "Genero")]
    public async Task<IActionResult> GetMoviesByGenero(string genero)
    {
        var movies = await _movie.GetMoviesByCategoryAsync(genero);
        if (!movies.Any())
            return NotFound();

        return Ok(movies);
    }

    // Devuelve los detalles de una película
    [HttpGet("{id}")]
    public async Task<IActionResult> GetMovieDetails(int id)
    {
        var movie = await _movie.GetMovieDetailsAsync(id);
        if (movie == null)
            return NotFound();

        return Ok(movie);
    }
}

