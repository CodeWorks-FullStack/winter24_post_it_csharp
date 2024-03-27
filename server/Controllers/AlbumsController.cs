[ApiController]
[Route("api/[controller]")]
public class AlbumsController : ControllerBase
{
  private readonly AlbumsService _albumsService;

  public AlbumsController(AlbumsService albumsService)
  {
    _albumsService = albumsService;
  }


  // [HttpPost]
  // [Authorize]
  // public ActionResult<Album> CreateAlbum()
  // {
  //   try
  //   {

  //   }
  //   catch (Exception exception)
  //   {
  //     return BadRequest(exception.Message);
  //   }
  // }
}