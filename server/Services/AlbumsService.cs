namespace post_it_csharp.Services;

public class AlbumsService
{

  private readonly AlbumsRepository _repository;

  public AlbumsService(AlbumsRepository repository)
  {
    _repository = repository;
  }

  internal Album ArchiveAlbum(int albumId, string userId)
  {
    Album albumToArchive = GetAlbumById(albumId);


    if (albumToArchive.CreatorId != userId)
    {
      throw new Exception("NOT YOUR ALBUM");
    }

    // flips bool
    albumToArchive.Archived = !albumToArchive.Archived;

    Album updatedAlbum = _repository.ArchiveAlbum(albumToArchive);

    return updatedAlbum;

  }

  internal Album CreateAlbum(Album albumData)
  {
    Album album = _repository.CreateAlbum(albumData);
    return album;
  }

  internal Album GetAlbumById(int albumId)
  {
    Album album = _repository.GetAlbumById(albumId);

    if (album == null)
    {
      throw new Exception($"Invalid Id: {albumId}");
    }

    return album;
  }

  internal List<Album> GetAlbums()
  {
    List<Album> albums = _repository.GetAlbums();
    return albums;
  }
}