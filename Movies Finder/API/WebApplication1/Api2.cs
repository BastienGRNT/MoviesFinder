using Microsoft.AspNetCore.Mvc;
using WebApplication2;
using WebApplication1;

namespace WebApplication1;

[ApiController]
[Route("api/[controller]")]
public class AddFilmApi : ControllerBase
{
    [HttpPost]
    public IActionResult AddFilm([FromBody] Film data)
    {
        if (data == null)
        {
            return BadRequest("Impossible d'ajouter une film");
        }

        var ajouterFilm = FonctionMetier2.addFilm(data);
        return Ok(ajouterFilm);
    }
}