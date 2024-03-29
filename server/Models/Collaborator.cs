namespace post_it_csharp.Models;


public class Collaborator : RepoItem
{
  // NOTE id, updatedAt, createdAt brought in through inherited class
  public int AlbumId { get; set; }
  public string AccountId { get; set; }
}