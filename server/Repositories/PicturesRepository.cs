

namespace post_it_csharp.Repositories;
public class PicturesRepository
{
  private readonly IDbConnection _db;

  public PicturesRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Picture CreatePicture(Picture pictureData)
  {
    string sql = @"
    INSERT INTO
    pictures(imgUrl, albumId, creatorId)
    VALUES(@ImgUrl, @AlbumId, @CreatorId);
    
    SELECT
    picture.*,
    account.*
    FROM pictures picture
    JOIN accounts account ON picture.creatorId = account.id 
    WHERE picture.id = LAST_INSERT_ID();";

    // Picture picture = _db.Query<Picture, Account, Picture>(sql, (picture, account) =>
    // {
    //   picture.Creator = account;
    //   return picture;
    // }, pictureData).FirstOrDefault();

    // NOTE mapping function abstracted out to _populateCreator method. Above code works fine
    Picture picture = _db.Query<Picture, Account, Picture>(sql, _populateCreator, pictureData).FirstOrDefault();

    return picture;
  }

  internal void DestroyPicture(int pictureId)
  {
    string sql = "DELETE FROM pictures WHERE id = @pictureId LIMIT 1;";

    int rowsAffected = _db.Execute(sql, new { pictureId });

    if (rowsAffected == 0)
    {
      throw new Exception("Nothing was deleted. You should probably check your sql query");
    }

    if (rowsAffected > 1)
    {
      throw new Exception("Tell Jake his vacation is over, we have a critical error in our database.");
    }
  }

  internal Picture GetPictureById(int id)
  {
    string sql = "SELECT * FROM pictures WHERE id = @id;";

    Picture picture = _db.Query<Picture>(sql, new { id }).FirstOrDefault();
    return picture;
  }

  internal List<Picture> GetPicturesByAlbumId(int albumId)
  {
    string sql = @"
    SELECT
    picture.*,
    account.*
    FROM pictures picture
    JOIN accounts account ON picture.creatorId = account.id
    WHERE picture.albumId = @albumId
    ;";

    List<Picture> pictures = _db.Query<Picture, Account, Picture>(sql, _populateCreator, new { albumId }).ToList();

    return pictures;
  }

  // NOTE private method is only accessible within the context of this class, no other module can call _picturesRepository._populateCreator
  private Picture _populateCreator(Picture picture, Account account)
  {
    picture.Creator = account;
    return picture;
  }
}