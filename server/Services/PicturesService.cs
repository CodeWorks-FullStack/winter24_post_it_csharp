


namespace post_it_csharp.Services;

public class PicturesService
{
  private readonly PicturesRepository _repository;

  public PicturesService(PicturesRepository repository)
  {
    _repository = repository;
  }

  internal Picture CreatePicture(Picture pictureData)
  {
    Picture picture = _repository.CreatePicture(pictureData);

    return picture;
  }

  internal string DestroyPicture(int pictureId, string userId)
  {
    throw new NotImplementedException();
  }

  internal List<Picture> GetPicturesByAlbumId(int albumId)
  {
    List<Picture> pictures = _repository.GetPicturesByAlbumId(albumId);
    return pictures;
  }
}