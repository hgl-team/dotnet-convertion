using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hgl.Convertion.Test
{
    public class IntToDoubleConverter : ITypeConverter<int, double>
    {
        public double Convert(int source)
        {
            return source;
        }
    }

    [TestClass]
    public class ConvertionContextTest
    {
        Dictionary<TypeConverterKey, ITypeConverter> dictionary;
        ConvertionContext context;
        ITypeConverter<int, double> converter;
        ITypeConverter resolvedConverter;

        [TestInitialize]
        public void Initialize() {
            dictionary = new Dictionary<TypeConverterKey, ITypeConverter>();
            context = new ConvertionContext(dictionary);
        }

        [TestMethod]
        public void TestRegistration()
        {
            GivenANewConverter();
            WhenConverterGetsRegistered();
            ThenConverterIsAddedToTheDictionary();
        }

        [TestMethod]
        public void TestDuplicatedRegistrationFailure()
        {
            GivenANewConverterWithinTheContext();
            GivenANewConverter();
            Assert.ThrowsException<InvalidOperationException>(WhenConverterGetsRegistered);
        }

        [TestMethod]
        public void TestResolution()
        {
            GivenANewConverterWithinTheContext();
            WhenConverterIsRequestedUsinTypes();
            ThenConverterIsResolved();
        }

        [TestMethod]
        public void TestResolutionFailure()
        {
            GivenANewConverterWithinTheContext();
            Assert.ThrowsException<ConverterNotFoundException>(WhenUnknownConverterIsRequestedUsinTypes);
        }

        private void WhenUnknownConverterIsRequestedUsinTypes()
        {
            this.resolvedConverter = context.ResolveConverter<double, int>();
        }

        private void GivenANewConverterWithinTheContext()
        {
            this.converter = new IntToDoubleConverter();
            context.AddConverter(converter);
        }

        private void WhenConverterIsRequestedUsinTypes()
        {
            this.resolvedConverter = context.ResolveConverter<int, double>();
        }

        private void ThenConverterIsResolved()
        {
            Assert.IsNotNull(this.resolvedConverter);
            Assert.AreEqual(this.converter, this.resolvedConverter);
        }

        private void GivenANewConverter()
        {
            this.converter = new IntToDoubleConverter();
        }

        private void WhenConverterGetsRegistered()
        {
            this.context.AddConverter(converter);
        }

        private void ThenConverterIsAddedToTheDictionary()
        {
            Assert.IsTrue(dictionary.Count == 1);
            Assert.AreEqual((
                from c in dictionary.Values
                where converter == c
                select c            
            ).Count(), 1);
        }
    }
}
