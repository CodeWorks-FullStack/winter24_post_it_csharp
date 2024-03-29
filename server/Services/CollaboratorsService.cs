



namespace post_it_csharp.Services;

public class CollaboratorsService
{
  private readonly CollaboratorsRepository _repository;

  public CollaboratorsService(CollaboratorsRepository repository)
  {
    _repository = repository;
  }

  internal CollaborationProfile CreateCollaborator(Collaborator collaboratorData)
  {
    CollaborationProfile collaborationProfile = _repository.CreateCollaborator(collaboratorData);
    return collaborationProfile;
  }

  internal void DestroyCollaborator(int collaboratorId, string userId)
  {
    Collaborator collaboratorToDestroy = GetCollaboratorById(collaboratorId);

    if (collaboratorToDestroy.AccountId != userId)
    {
      throw new Exception("NOT YOUR COLLABORATOR 🙅‍♀️🖼️🤝");
    }

    _repository.DestroyCollaborator(collaboratorId);
  }

  internal Collaborator GetCollaboratorById(int collaboratorId)
  {
    Collaborator collaborator = _repository.GetCollaboratorById(collaboratorId);

    if (collaborator == null)
    {
      throw new Exception($"Invalid id: {collaboratorId}");
    }

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