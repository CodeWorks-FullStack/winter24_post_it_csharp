namespace post_it_csharp.Models;

public class CollaborationProfile : Account
{
  // public string Id { get; set; }
  // public string Name { get; set; }
  // public string Picture { get; set; }
  // public string Email { get; set; }

  public int AlbumId { get; set; }
  public int CollaborationId { get; set; }
}