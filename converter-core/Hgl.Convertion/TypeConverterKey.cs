using System;
using System.Diagnostics.CodeAnalysis;

namespace Hgl.Convertion
{
    public class TypeConverterKey : IConverterKey, IComparable<TypeConverterKey>, IEquatable<TypeConverterKey>
    {
        public TypeConverterKey(Type sourceType, Type targetType) {
            this.SourceType = sourceType;
            this.TargetType = targetType;
        }

        public Type SourceType { get; private set; }
        public Type TargetType { get; private set; }

        public override int GetHashCode()
        {
            return HashCode.Combine(SourceType.GetHashCode(), TargetType.GetHashCode());
        }

        public static TypeConverterKey Of<TSource, TDest>() {
            return new TypeConverterKey(typeof(TSource), typeof(TDest));
        }

        public int CompareTo([AllowNull] TypeConverterKey other)
        {
            if(other != null) {
                return this.GetHashCode().CompareTo(other.GetHashCode());
            } else {
                return 1;
            }
        }

        public bool Equals([AllowNull] TypeConverterKey other)
        {
            return this.CompareTo(other) == 0;
        }
    }
}