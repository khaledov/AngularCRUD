using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Mongo
{
    public interface IIdentity<T>
    {
        T Id { get; }
    }
}
