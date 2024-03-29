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
  public async Task<ActionResult<CollaborationProfile>> CreateCollaborator([FromBody] Collaborator collaboratorData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);

      collaboratorData.AccountId = userInfo.Id;

      CollaborationProfile collaborationProfile = _collaboratorsService.CreateCollaborator(collaboratorData);
      return Ok(collaborationProfile);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpDelete("{collaboratorId}")]
  [Authorize]
  public async Task<ActionResult<string>> DestroyCollaborator(int collaboratorId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      _collaboratorsService.DestroyCollaborator(collaboratorId, userInfo.Id);
      return Ok("Collaborator has been deleted!");
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}