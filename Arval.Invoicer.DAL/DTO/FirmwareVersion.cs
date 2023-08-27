namespace Arval.Invoicer.DAL.DTO;

public sealed class FirmwareVersion
{
    public int Major { get; set; }
    public int Minor { get; set; }
    public int Build { get; set; }
    public int Revision { get; set; }
    public int MajorRevision { get; set; }
    public int MinorRevision { get; set; }
}
