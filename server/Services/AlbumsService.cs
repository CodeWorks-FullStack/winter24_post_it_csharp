namespace post_it_csharp.Services;

public class AlbumsService
{

  private readonly AlbumsRepository _repository;

  public AlbumsService(AlbumsRepository repository)
  {
    _repository = repository;
  }
}