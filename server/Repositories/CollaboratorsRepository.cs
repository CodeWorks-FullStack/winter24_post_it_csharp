
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
}