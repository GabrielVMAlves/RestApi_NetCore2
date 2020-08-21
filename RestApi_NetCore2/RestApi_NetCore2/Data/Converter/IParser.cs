using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi_NetCore2.Data.Converter
{
    public interface IParser <O, D>
    {
        D Parse(O origin);
        List<D> Parse(List<O> origins);
    }
}
