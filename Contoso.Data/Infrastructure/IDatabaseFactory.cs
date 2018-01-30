using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Contoso.Data.Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        SchoolContext Get();
    }
}
