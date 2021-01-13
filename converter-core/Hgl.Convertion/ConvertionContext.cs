using System;
using System.Collections.Generic;


namespace Hgl.Convertion
{
    public class ConvertionContext : IConvertionContext<TypeConverterKey>
    {
        private IDictionary<TypeConverterKey, ITypeConverter> dictionary;

        public ConvertionContext() : this(new Dictionary<TypeConverterKey, ITypeConverter>()) {
        }

        public ConvertionContext(IDictionary<TypeConverterKey, ITypeConverter> dictionary) {
            this.dictionary = dictionary;
        }

        public IConvertionContext AddConverter<TSource, TDest>(ITypeConverter<TSource, TDest> converter)
        {
            var key = new TypeConverterKey(typeof(TSource), typeof(TDest));

            if(!dictionary.ContainsKey(key)) {
                dictionary.Add(key, converter);
            } else {
                throw new InvalidOperationException(String.Format("Cannot register converter of type {0}. Convertion of type {1} to type {2} is already defined.",
                        converter.GetType(), key.SourceType, key.TargetType));
            }

            return this;
        }

        public ITypeConverter<TSource, TDest> ResolveConverter<TSource, TDest>()
        {
            var key = TypeConverterKey.Of<TSource, TDest>();
            var converter = default(ITypeConverter<TSource, TDest>);

            if(dictionary.ContainsKey(key)) {
                var rawConverter = dictionary[key];

                converter = rawConverter is ITypeConverter<TSource, TDest> ?
                        rawConverter as ITypeConverter<TSource, TDest> : converter;
            } else {
                throw new ConverterNotFoundException(String.Format("Could not found converter for type {0} to {1}.", key.SourceType, key.TargetType));
            }

            return converter;
        }
    }

    public class ConverterNotFoundException : Exception {
        public ConverterNotFoundException(String message) : base(message) { }
    }
}