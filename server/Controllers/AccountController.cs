namespace post_it_csharp.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class AccountController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly Auth0Provider _auth0Provider;

  private readonly CollaboratorsService _collaborationsService;

  public AccountController(AccountService accountService, Auth0Provider auth0Provider, CollaboratorsService collaborationsService)
  {
    _accountService = accountService;
    _auth0Provider = auth0Provider;
    _collaborationsService = collaborationsService;
  }

  [HttpGet]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_accountService.GetOrCreateProfile(userInfo));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("collaborators")]
  public async Task<ActionResult<List<CollaborationAlbum>>> GetMyAlbumCollaborations()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<CollaborationAlbum> collaborationAlbums = _collaborationsService.GetMyAlbumCollaborations(userInfo.Id);
      return Ok(collaborationAlbums);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}
