using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hgl.Convertion.Test
{
    [TestClass]
    public class TypeConverterKeyTest
    {
        TypeConverterKey key1;
        TypeConverterKey key2;
        int hashCode1;
        int hashCode2;

        [TestMethod]
        public void testHashCodeEquality()
        {
            GivenTwoKeyInstancesWithTheSameTypes();
            WhenGetTheKeyHashCode();
            ThenBothHashCodeAreEquals();
        }

        void GivenTwoKeyInstancesWithTheSameTypes() {
            this.key1 = TypeConverterKey.Of<int, double>();
            this.key2 = TypeConverterKey.Of<int, double>();
        }

        void WhenGetTheKeyHashCode() {
            this.hashCode1 = key1.GetHashCode();
            this.hashCode2 = key2.GetHashCode();
        }

        void ThenBothHashCodeAreEquals() {
            Assert.AreEqual(hashCode1, hashCode2);
        }
    }
}
