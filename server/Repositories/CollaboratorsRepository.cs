


namespace post_it_csharp.Repositories;
public class CollaboratorsRepository
{
  private readonly IDbConnection _db;

  public CollaboratorsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Collaborator CreateCollaborator(Collaborator collaboratorData)
  {
    string sql = @"
    INSERT INTO 
    collaborators(albumId, accountId)
    VALUES(@AlbumId, @AccountId);

    SELECT * FROM collaborators WHERE id = LAST_INSERT_ID();";

    Collaborator collaborator = _db.Query<Collaborator>(sql, collaboratorData).FirstOrDefault();
    return collaborator;
  }

  internal List<CollaborationProfile> GetAlbumCollaborators(int albumId)
  {
    string sql = @"
    SELECT 
    collab.*,
    account.*
    FROM collaborators collab
    JOIN accounts account ON account.id = collab.accountId
    WHERE albumId = @albumId;
    ";

    List<CollaborationProfile> collaborationProfiles = _db.Query<Collaborator, CollaborationProfile, CollaborationProfile>(sql, (collaborator, profile) =>
    {
      profile.AlbumId = collaborator.AlbumId;
      profile.CollaborationId = collaborator.Id;
      return profile;
    }, new { albumId }).ToList();
    return collaborationProfiles;
  }

  internal List<CollaborationAlbum> GetMyAlbumCollaborations(string userId)
  {
    string sql = @"
    SELECT
    collab.*,
    album.*
    FROM collaborators collab
    JOIN albums album ON album.id = collab.albumId
    WHERE collab.accountId = @userId;";

    List<CollaborationAlbum> collaborationAlbums = _db
    .Query<Collaborator, CollaborationAlbum, CollaborationAlbum>(sql, (collaborator, album) =>
    {
      album.CollaborationId = collaborator.Id;
      album.AccountId = collaborator.AccountId;
      return album;
    }, new { userId }).ToList();

    return collaborationAlbums;
  }
}