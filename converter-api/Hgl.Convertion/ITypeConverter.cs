using System;


namespace Hgl.Convertion 
{
    public interface ITypeConverter 
    {
        object Convert(object source);
    }

    public interface ITypeConverter<in TSource, out TDest>
        : ITypeConverter
    {
        TDest Convert(TSource source);
        object ITypeConverter.Convert(object source) {
            if(source is TSource) {
                return this.Convert((TSource)source);
            } else {
                throw new InvalidCastException();
            }
        }
    }
}