namespace APBD_EF_Example.Models;

public class ActionDTO
{
    public int IdAction { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public bool NeedSpecialEquipment { get; set; }
    public List<int> Firetrucks { get; set; }
    
}