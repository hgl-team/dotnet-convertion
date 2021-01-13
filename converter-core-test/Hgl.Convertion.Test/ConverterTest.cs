using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Hgl.Convertion
{
    public class IntToDoubleConverter : ITypeConverter<int, double>, ITypeConverter<double, int>
    {
        public double Convert(int source)
        {
            return source;
        }

        public int Convert(double source)
        {
            return (int)source;
        }

        public object Convert(object source)
        {
            if(source is int) return Convert((int)source);
            else if(source is double) return Convert((double)source);
            else throw new InvalidCastException();
        }
    }

    [TestClass]
    public class ConverterTest {

        ConvertionContext context;
        Converter converter;
        int intValue;
        double doubleValue;
        int crossIntValue;
        double crossDoubleValue;

        [TestInitialize]
        public void initialize() {
            context = new ConvertionContext();
            converter = new Converter(context);
        }

        [TestMethod]
        public void TestConvertion() {
            GivenAnIntegerToDoubleConverter();
            GivenAnIntValue();
            WhenConvertIntValueToDouble();
            ThenResultHasExpectedValue();
        }

        [TestMethod]
        public void TestMultipleConvertionWithSingleInstace() {
            GivenAnIntegerToDoubleConverter();
            GivenAnDoubleToIntConverter();
            GivenAnIntValue();
            GivenADoubleValue();
            WhenCrossConvertValues();
            ThenResultsHasExpectedValue();
        }

        private void ThenResultsHasExpectedValue()
        {
            Assert.AreEqual((double)this.intValue, this.crossDoubleValue);
            Assert.AreEqual((int)this.doubleValue, this.crossIntValue);
        }

        private void WhenCrossConvertValues()
        {
            this.crossIntValue = this.converter.Convert<double, int>(this.doubleValue);
            this.crossDoubleValue = this.converter.Convert<int, double>(this.intValue);
        }

        private void GivenADoubleValue()
        {
            this.doubleValue = 10.23d;
        }

        private void GivenAnDoubleToIntConverter()
        {
            this.context.AddConverter<double, int>(new IntToDoubleConverter());
        }

        private void GivenAnIntegerToDoubleConverter()
        {
            this.context.AddConverter<int, double>(new IntToDoubleConverter());
        }

        private void GivenAnIntValue()
        {
            this.intValue = 10;
        }

        private void WhenConvertIntValueToDouble()
        {
            this.doubleValue = this.converter.Convert<int, double>(this.intValue);
        }

        private void ThenResultHasExpectedValue()
        {
            Assert.AreEqual(10.0d, this.doubleValue);
        }
    }
}