using System;

namespace Hgl.Convertion
{
    public class Converter : IConverter
    {
        private IConvertionContext context;

        public Converter(IConvertionContext context) {
            this.context = context;
        }

        public TDest Convert<TSource, TDest>(TSource source)
        {
            return context.ResolveConverter<TSource, TDest>()
                .Convert(source);
        }
    }
}