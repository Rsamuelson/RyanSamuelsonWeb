using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IRsDbContext
    {
        DbSet<Email> Emails { get; set; }
    }
}
