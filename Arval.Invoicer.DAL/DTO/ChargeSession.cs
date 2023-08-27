namespace Arval.Invoicer.DAL.DTO;

public sealed class ChargeSession
{
    public Guid Id { get; set; }
    public string DeviceId { get; set; } = null!;
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public double Energy { get; set; }
    public int CommitMetadata { get; set; }
    public DateTime CommitEndDateTime { get; set; }
    public string UserFullName { get; set; } = null!;
    public Guid ChargerId { get; set; }
    public string DeviceName { get; set; } = null!;
    public string UserEmail { get; set; } = null!;
    public Guid UserId { get; set; }
    public string TokenName { get; set; } = null!;
    public string ExternalId { get; set; } = null!;
    public bool ExternallyEnded { get; set; }
    public List<EnergyDetail> EnergyDetails { get; set; } = new List<EnergyDetail>();
    public FirmwareVersion ChargerFirmwareVersion { get; set; } = null!;
    public string SignedSession { get; set; } = null!;
    public Guid ReplacedBySessionId { get; set; }
}
