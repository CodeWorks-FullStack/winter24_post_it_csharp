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