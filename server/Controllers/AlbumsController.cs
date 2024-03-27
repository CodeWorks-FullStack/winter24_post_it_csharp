[ApiController]
[Route("api/[controller]")]
public class AlbumsController : ControllerBase
{
  private readonly AlbumsService _albumsService;

  private readonly Auth0Provider _auth0Provider;

  public AlbumsController(AlbumsService albumsService, Auth0Provider auth0Provider)
  {
    _albumsService = albumsService;
    _auth0Provider = auth0Provider;
  }


  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Album>> CreateAlbum([FromBody] Album albumData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      albumData.CreatorId = userInfo.Id;
      Album album = _albumsService.CreateAlbum(albumData);
      return Ok(album);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpGet]
  public ActionResult<List<Album>> GetAlbums()
  {
    try
    {
      List<Album> albums = _albumsService.GetAlbums();
      return Ok(albums);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpGet("{albumId}")]
  public ActionResult<Album> GetAlbumById(int albumId)
  {
    try
    {
      Album album = _albumsService.GetAlbumById(albumId);
      return Ok(album);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}