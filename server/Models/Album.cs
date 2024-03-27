namespace post_it_csharp.Models;

public class Album : RepoItem // Album class inherits all members from RepoItem class
{
  // NOTE commented out properties are being brought in through inheritance
  // public int Id { get; set; }
  // public DateTime CreatedAt { get; set; }
  // public DateTime UpdatedAt { get; set; }
  public string Title { get; set; }
  public string Category { get; set; }
  public bool Archived { get; set; }
  public string CoverImg { get; set; }
  public string CreatorId { get; set; }
  public Account Creator { get; set; }
}