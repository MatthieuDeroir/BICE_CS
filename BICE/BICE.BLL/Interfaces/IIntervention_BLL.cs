namespace BICE.BLL.Interfaces
{
    public interface IIntervention_BLL
    {
        string Denomination { get; set; }
        string? Description { get; set; }
        DateTime StartDate { get; set; }
        DateTime? EndDate { get; set; }
    }
}
