namespace post_it_csharp.Services;

public class PicturesService
{
  private readonly PicturesRepository _repository;

  public PicturesService(PicturesRepository repository)
  {
    _repository = repository;
  }
}