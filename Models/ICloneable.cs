using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public interface ICloneable<T>
    {
        T Clone();
    }
}
