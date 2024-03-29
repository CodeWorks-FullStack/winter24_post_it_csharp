




namespace post_it_csharp.Repositories;
public class CollaboratorsRepository
{
  private readonly IDbConnection _db;

  public CollaboratorsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal CollaborationProfile CreateCollaborator(Collaborator collaboratorData)
  {
    string sql = @"
    INSERT INTO 
    collaborators(albumId, accountId)
    VALUES(@AlbumId, @AccountId);

    SELECT 
    collab.*,
    account.* 
    FROM collaborators collab
    JOIN accounts account ON account.id = collab.accountId
    WHERE collab.id = LAST_INSERT_ID();";

    CollaborationProfile collaborationProfile = _db.Query<Collaborator, CollaborationProfile, CollaborationProfile>(sql, (collaborator, profile) =>
    {
      profile.CollaborationId = collaborator.Id;
      profile.AlbumId = collaborator.AlbumId;
      return profile;
    }, collaboratorData).FirstOrDefault();
    return collaborationProfile;
  }

  internal void DestroyCollaborator(int collaboratorId)
  {
    string sql = "DELETE FROM collaborators WHERE id = @collaboratorId LIMIT 1;";

    _db.Execute(sql, new { collaboratorId });
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

  internal Collaborator GetCollaboratorById(int collaboratorId)
  {
    string sql = "SELECT * FROM collaborators WHERE id = @collaboratorId;";

    Collaborator collaborator = _db.Query<Collaborator>(sql, new { collaboratorId }).FirstOrDefault();
    return collaborator;
  }

  internal List<CollaborationAlbum> GetMyAlbumCollaborations(string userId)
  {
    string sql = @"
    SELECT
    collab.*,
    album.*,
    account.*
    FROM collaborators collab
    JOIN albums album ON album.id = collab.albumId
    JOIN accounts account ON account.id = album.creatorId
    WHERE collab.accountId = @userId;";


    List<CollaborationAlbum> collaborationAlbums = _db.Query<Collaborator, CollaborationAlbum, Account, CollaborationAlbum>(sql, (collaborator, album, account) =>
    {
      album.CollaborationId = collaborator.Id;
      album.AccountId = collaborator.AccountId;
      album.Creator = account;
      return album;
    }, new { userId }).ToList();

    return collaborationAlbums;
  }
}