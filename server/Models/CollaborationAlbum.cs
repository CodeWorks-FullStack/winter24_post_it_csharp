namespace post_it_csharp.Models;

public class CollaborationAlbum : Album
{
  public int CollaborationId { get; set; }
  public string AccountId { get; set; }
}