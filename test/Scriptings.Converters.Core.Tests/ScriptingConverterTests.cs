using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Scriptings.Converters.Core.Tests
{
    [TestClass()]
    public class ScriptingConverterTests
    {
        static IEnumerable<object?[]> ConvertTestData
        {
            get
            {
                yield return ConvertTest("a => a + 1", new object?[] { 1 }, 2);
                yield return ConvertTest("(a,b) => (a + b) / 2", new object?[] { 1, 3 }, 2);
                yield return ConvertTest("(a,b,c) => a*100 + (b + c)/2", new object?[] { 5, 2, 8 }, 505);
                yield return ConvertTest("a => a + \"test\"", new object?[] { "hoge" }, "hogetest");
                yield return ConvertTest("(a,b) => a + b + \"hoge\"", new object?[] { "foo", null }, "foohoge");
                object?[] ConvertTest(string Parameter, object?[] Values, object? Expected)
                    => new object?[] { Parameter, Values, Expected };
            }
        }
        [TestMethod, DynamicData(nameof(ConvertTestData))]
        public void ConvertTest(string Parameter, object?[] Values, object? Expected)
            => Assert.AreEqual(Expected, ScriptingConverter.Convert(Parameter, Values));
    }
}
