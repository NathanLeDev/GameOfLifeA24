using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayerGameOfLife.Models;

public class InitialState
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string State { get; set; } = string.Empty;

    // The database can only store plain text not a List<(int x, int y)> thats why we need to convert in both directions 
    [NotMapped]
    public List<(int x, int y)> Values
    {
        get
        {
            if (string.IsNullOrEmpty(State))
                return new List<(int x, int y)>();

            return State
                .Split(';', StringSplitOptions.RemoveEmptyEntries)
                .Select(pair =>
                {
                    var parts = pair.Split(',');
                    return (int.Parse(parts[0]), int.Parse(parts[1]));
                })
                .ToList();
        }
        set
        {
            State = string.Concat(value.Select(v => $"{v.x},{v.y};"));
        }
    }
}
