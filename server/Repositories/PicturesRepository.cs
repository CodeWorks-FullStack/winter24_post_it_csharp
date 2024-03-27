
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

    Picture picture = _db.Query<Picture, Account, Picture>(sql, (picture, account) =>
    {
      picture.Creator = account;
      return picture;
    }, pictureData).FirstOrDefault();

    return picture;
  }
}