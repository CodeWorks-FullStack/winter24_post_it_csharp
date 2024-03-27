



namespace post_it_csharp.Repositories;

public class AlbumsRepository
{
  private readonly IDbConnection _db;

  public AlbumsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Album ArchiveAlbum(Album albumData)
  {
    string sql = @"
    UPDATE albums
    SET
    archived = @Archived
    WHERE id = @Id;
    
    SELECT
    album.*,
    account.*
    FROM albums album
    JOIN accounts account ON album.creatorId = account.id
    WHERE album.id = @Id;";

    Album album = _db.Query<Album, Account, Album>(sql, (album, account) =>
    {
      album.Creator = account;
      return album;
    }, albumData).FirstOrDefault();

    return album;
  }

  internal Album CreateAlbum(Album albumData)
  {
    string sql = @"
    INSERT INTO
    albums(title, coverImg, category, creatorId)
    VALUES(@Title, @CoverImg, @Category, @CreatorId);
    
    SELECT 
    album.*,
    account.*
    FROM albums album
    JOIN accounts account ON album.creatorId = account.id
    WHERE album.id = LAST_INSERT_ID();";

    Album album = _db.Query<Album, Account, Album>(sql, (album, account) =>
    {
      album.Creator = account;
      return album;
    }, albumData).FirstOrDefault();
    return album;
  }

  internal Album GetAlbumById(int albumId)
  {
    string sql = @"
      SELECT 
      album.*,
      account.* 
      FROM albums album
      JOIN accounts account ON album.creatorId = account.id 
      WHERE album.id = @albumId
      ;";

    Album album = _db.Query<Album, Account, Album>(sql, (album, account) =>
    {
      album.Creator = account;
      return album;
    }, new { albumId }).FirstOrDefault();
    return album;
  }

  internal List<Album> GetAlbums()
  {
    string sql = @"
    SELECT 
    album.*,
    account.* 
    FROM albums album
    JOIN accounts account ON album.creatorId = account.id
    ;";

    List<Album> albums = _db.Query<Album, Account, Album>(sql, (album, account) =>
    {
      album.Creator = account;
      return album;
    }).ToList();
    return albums;
  }
}