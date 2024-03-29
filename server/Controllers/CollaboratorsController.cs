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
}