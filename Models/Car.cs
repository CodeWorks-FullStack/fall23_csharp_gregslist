namespace csharp_gregslist.Models;

public class Car
{
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  public string Make { get; set; }
  public string Model { get; set; }
  // NOTE the ? after our type allows this to be a null value instead of defaulting to 0
  public int? Year { get; set; }
  public int? Price { get; set; }
  public string ImgUrl { get; set; }
  // NOTE the ? after our type allows this to be a null value instead of defaulting to false
  public bool? Runs { get; set; }
  public int? Mileage { get; set; }
}