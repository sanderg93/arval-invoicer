namespace Arval.Invoicer.DAL.DTO;

public class ChargeHistoryParameters
{
    public Guid? InstallationId { get; set; }
    public Guid? UserId { get; set; }
    public Guid? ChargerId { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public AggregateGroup? GroupBy { get; set; }
    public DetailLevelFlags? DetailLevel { get; set; }
    public string? SortProperty { get; set; }
    public bool? SortDescending { get; set; }
    public int? PageSize { get; set; }
    public int? PageIndex { get; set; }
    public bool? IncludeDisabled { get; set; }
    public List<Guid>? Exclude { get; set; }
}
