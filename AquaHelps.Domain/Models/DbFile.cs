namespace AquaHelps.Domain.Models;
public class DbFile : DbEntity
{
    public string FileName { get; set; }
    public byte[] Data { get; set; }
}
