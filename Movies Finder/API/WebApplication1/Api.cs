using Microsoft.AspNetCore.Mvc;
using WebApplication2;
using WebApplication1;

namespace WebApplication1;

[ApiController]
[Route("api/[controller]")]
public class FilmAPI : ControllerBase
{
    [HttpPost]
    public IActionResult AvoirFilm([FromBody] Film film)
    {
        
        if (film == null)
        {
            return BadRequest("Le titre recherché est obligatoire.");
        }

        var filmes = FonctionMetier.RecherFilm(film);

        return Ok(filmes);
    }
}