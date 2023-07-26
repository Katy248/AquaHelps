using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaHelps.Domain.Models;
public class DbFile : DbEntity
{
    public string FileName { get; set; }
    public byte[] Data { get; set; }
}
