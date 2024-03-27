namespace post_it_csharp.Models;

// NOTE abstract class denotes that a class can never be constructed on its own, only inherited by other classes
public abstract class RepoItem
{
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
}


// public abstract class RepoItemWithCreator : RepoItem
// {
//   public string CreatorId { get; set; }
// }