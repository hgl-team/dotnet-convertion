using System;


namespace Hgl.Convertion
{
    public interface IConverterKey
    {
        Type SourceType { get; }
        Type TargetType { get; }
    }
}