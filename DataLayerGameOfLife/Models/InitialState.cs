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

    [NotMapped]
    public List<(int x, int y)> Values
    {
        get
        {
           //Complete to return a list of points (x,y)
        }
        set
        {
            //complete
            //parse it with x1,y1;x2,y2;x3,y3; eg : "1,2;3,2;5,6;"
        }
    }
}
