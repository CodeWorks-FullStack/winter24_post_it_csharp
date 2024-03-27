
namespace post_it_csharp.Repositories;

public class AlbumsRepository
{
  private readonly IDbConnection _db;

  public AlbumsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Album CreateAlbum(Album albumData)
  {
    string sql = @"
    INSERT INTO
    albums(title, coverImg, category, creatorId)
    VALUES(@Title, @CoverImg, @Category, @CreatorId);
    
    SELECT * FROM albums WHERE id = LAST_INSERT_ID();";

    Album album = _db.Query<Album>(sql, albumData).FirstOrDefault();
    return album;
  }
}