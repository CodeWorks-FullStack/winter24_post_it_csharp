namespace post_it_csharp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CollaboratorsController : ControllerBase
{
  private readonly CollaboratorsService _collaboratorsService;
  private readonly Auth0Provider _auth0Provider;

  public CollaboratorsController(CollaboratorsService collaboratorsService, Auth0Provider auth0Provider)
  {
    _collaboratorsService = collaboratorsService;
    _auth0Provider = auth0Provider;
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Collaborator>> CreateCollaborator([FromBody] Collaborator collaboratorData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);

      collaboratorData.AccountId = userInfo.Id;

      // FIXME return something better (that would be a good reference for AllSpice)
      Collaborator collaborator = _collaboratorsService.CreateCollaborator(collaboratorData);
      return Ok(collaborator);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}