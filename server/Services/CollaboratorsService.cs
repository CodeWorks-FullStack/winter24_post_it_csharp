


namespace post_it_csharp.Services;

public class CollaboratorsService
{
  private readonly CollaboratorsRepository _repository;

  public CollaboratorsService(CollaboratorsRepository repository)
  {
    _repository = repository;
  }

  internal Collaborator CreateCollaborator(Collaborator collaboratorData)
  {
    Collaborator collaborator = _repository.CreateCollaborator(collaboratorData);
    return collaborator;
  }

  internal List<CollaborationProfile> GetAlbumCollaborators(int albumId)
  {
    List<CollaborationProfile> collaborationProfiles = _repository.GetAlbumCollaborators(albumId);
    return collaborationProfiles;
  }

  internal List<CollaborationAlbum> GetMyAlbumCollaborations(string userId)
  {
    List<CollaborationAlbum> collaborationAlbums = _repository.GetMyAlbumCollaborations(userId);
    return collaborationAlbums;
  }
}