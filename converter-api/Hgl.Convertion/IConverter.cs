using System;

using Hgl.Convertion;

namespace Hgl.Convertion
{
    public interface IConverter
    {
        TDest Convert<TSource, TDest>(TSource source);
    }
}