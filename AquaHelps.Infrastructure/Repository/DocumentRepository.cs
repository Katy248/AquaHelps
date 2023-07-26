using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AquaHelps.Domain.Models;
using AquaHelps.Infrastructure.Repositories;

namespace AquaHelps.Infrastructure.Repository;
public class DocumentRepository : Repository<Document>
{
    public DocumentRepository(ApplicationDbContext context) : base(context)
    {
        AddIncludeStatement(document => document.File);
    }
}
