
namespace Hgl.Convertion 
{
    public interface IConvertionContext
    {
        ITypeConverter<TSource, TDest> ResolveConverter<TSource, TDest>();
        IConvertionContext AddConverter<TSource, TDest>(ITypeConverter<TSource, TDest> converter);
    }

    public interface IConvertionContext<TKey> : IConvertionContext 
        where TKey : IConverterKey {
    }
}