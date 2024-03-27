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

    // NOTE                   ⬇️     ⬇️ The two data types coming in on the same row from our join, needs to match table select order from sql
    //                                        ⬇️ The return type of the mapping function    
    Album album = _db.Query<Album, Account, Album>(sql,
    // NOTE second argument passed to dapper is our mapping function. dapper will automatically split up your data coming in on each row by the id column. You will need a parameter set up for each data type joined on your rows
    (album, account) =>
    {
      album.Creator = account; // adds account information to album as a nested object. "populate"
      return album; // this is what we return out of mapping function. Data type must match 3rd type passed to query
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