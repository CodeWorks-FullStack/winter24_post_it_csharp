


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

  internal Picture GetPictureById(int id)
  {
    Picture picture = _repository.GetPictureById(id);

    if (picture == null)
    {
      throw new Exception($"Invalid id: {id}");
    }

    return picture;
  }

  internal string DestroyPicture(int pictureId, string userId)
  {
    Picture pictureToDestroy = GetPictureById(pictureId);

    if (pictureToDestroy.CreatorId != userId)
    {
      throw new Exception("NOT YOUR PICTURE, BUDDY 🙅‍♀️");
    }

    _repository.DestroyPicture(pictureId);

    return "Picture was deleted! 🖼️🚮";
  }

  internal List<Picture> GetPicturesByAlbumId(int albumId)
  {
    List<Picture> pictures = _repository.GetPicturesByAlbumId(albumId);
    return pictures;
  }
}