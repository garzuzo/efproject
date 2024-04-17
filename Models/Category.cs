using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace efproject.Models;

public class Category
{
    public Guid CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Points { get; set; }
    //When the data is returned it does not response with tasks collection
    [JsonIgnore]
    public virtual ICollection<Task> Tasks { get; set; }

}