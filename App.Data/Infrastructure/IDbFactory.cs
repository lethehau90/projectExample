using System;

namespace App.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        AppDbContext Init();
    }
}