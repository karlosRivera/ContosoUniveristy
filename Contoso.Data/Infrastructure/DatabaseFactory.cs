using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Contoso.Data.Infrastructure
{
public class DatabaseFactory : Disposable, IDatabaseFactory
{
    private SchoolContext dataContext;

    public SchoolContext Get()
    {
        return dataContext ?? (dataContext = new SchoolContext());
    }

    protected override void DisposeCore()
    {
        if (dataContext != null)
            dataContext.Dispose();
    }
}
}
