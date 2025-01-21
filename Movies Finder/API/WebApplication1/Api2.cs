using Microsoft.AspNetCore.Mvc;

namespace WebApplication1;

[ApiController]
[Route("api/[controller]")]
public class AddFilmApi : ControllerBase
{
    [HttpPost]
    public IActionResult AddFilm([FromBody] AddFilmApi data)
    {
        if (data == null)
        {
            return BadRequest("Impossible d'ajouter une film");
        }

        var ajouterFilm = FonctionMetier2.addFilm(data);
        return Ok(ajouterFilm);
    }
}