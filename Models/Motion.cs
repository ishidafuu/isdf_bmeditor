namespace isdf_bmeditor.Models;

public class Motion
{
    public string Name { get; set; } = string.Empty;
    public List<Koma> Komas { get; set; } = new();
    public int Loop { get; set; }
} 