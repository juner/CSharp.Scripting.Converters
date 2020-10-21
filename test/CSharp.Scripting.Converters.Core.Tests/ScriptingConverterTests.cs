using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharp.Scripting.Converters.Core.Tests
{
    [TestClass()]
    public class ScriptingConverterTests
    {
        static IEnumerable<object?[]> ConvertTestData
        {
            get
            {
                // 0:Func<>
                yield return ConvertTest("() => \"hello\"", Array.Empty<object?>(), "hello");
                // 1:Func<,>
                yield return ConvertTest("a => a + 1", new object?[] { 1 }, 2);
                // 2:Func<,,>
                yield return ConvertTest("(a,b) => (a + b) / 2", new object?[] { 1, 3 }, 2);
                // 3:Func<,,,>
                yield return ConvertTest("(a,b,c) => a*100 + (b + c)/2", new object?[] { 5, 2, 8 }, 505);
                // string Func<,>
                yield return ConvertTest("a => a + \"test\"", new object?[] { "hoge" }, "hogetest");
                // string Func<,,>
                yield return ConvertTest("(a,b) => a + b + \"hoge\"", new object?[] { "foo", null }, "foohoge");
                // 4:Func<,,,,>
                yield return ConvertTest("(a,b,c,d) => a * b + c * d", new object?[] { 1, 2, 3, 4 }, 1 * 2 + 3 * 4);
                // 5:Func<,,,,,>
                yield return ConvertTest("(a,b,c,d,e) => a + b + c + d + e", new object?[] { 1, 2, 3, 4, 5 }, 1 + 2 + 3 + 4 + 5);
                // 6:Func<,,,,,,>
                yield return ConvertTest("(a,b,c,d,e,f) => Math.Max(a*c,b*d) + e * f", new object?[] { 1, 2, 3, 4, 5, 6 }, Math.Max(1 * 3, 2 * 4) + 5 * 6);
                // 7:Func<,,,,,,,>
                yield return ConvertTest("(a,b,c,d,e,f,g) => a + b + c + d + e + f + g", new object?[] { 1, 2, 3, 4, 5, 6, 7 }, 1 + 2 + 3 + 4 + 5 + 6 + 7);
                // 8:Func<,,,,,,,,>
                yield return ConvertTest("(a,b,c,d,e,f,g,h) => a + b + c + d + e + f + g + h", new object?[] { 1, 2, 3, 4, 5, 6, 7, 8 }, 1 + 2 + 3 + 4 + 5 + 6 + 7 + 8);
                // 9:Func<,,,,,,,,,>
                yield return ConvertTest("(a,b,c,d,e,f,g,h,i) => a + b + c + d + e + f + g + h + i", new object?[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 1 + 2 + 3 + 4 + 5 + 6 + 7 + 8 + 9);
                // 10:Func<,,,,,,,,,,>
                yield return ConvertTest("(a,b,c,d,e,f,g,h,i,j) => a + b + c + d + e + f + g + h + i + j", new object?[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 1 + 2 + 3 + 4 + 5 + 6 + 7 + 8 + 9 + 10);
                // 11:Func<,,,,,,,,,,,>
                yield return ConvertTest("(a,b,c,d,e,f,g,h,i,j,k) => a + b + c + d + e + f + g + h + i + j + k", new object?[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }, 1 + 2 + 3 + 4 + 5 + 6 + 7 + 8 + 9 + 10 + 11);
                // 12:Func<,,,,,,,,,,,,>
                yield return ConvertTest("(a,b,c,d,e,f,g,h,i,j,k,l) => a + b + c + d + e + f + g + h + i + j + k + l", new object?[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 }, 1 + 2 + 3 + 4 + 5 + 6 + 7 + 8 + 9 + 10 + 11 + 12);
                // 13:Func<,,,,,,,,,,,,,>
                yield return ConvertTest("(a,b,c,d,e,f,g,h,i,j,k,l,m) => a + b + c + d + e + f + g + h + i + j + k + l + m", new object?[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 }, 1 + 2 + 3 + 4 + 5 + 6 + 7 + 8 + 9 + 10 + 11 + 12 + 13);
                // 14:Func<,,,,,,,,,,,,,,>
                yield return ConvertTest("(a,b,c,d,e,f,g,h,i,j,k,l,m,n) => a + b + c + d + e + f + g + h + i + j + k + l + m + n", new object?[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 }, 1 + 2 + 3 + 4 + 5 + 6 + 7 + 8 + 9 + 10 + 11 + 12 + 13 + 14);
                // 15:Func<,,,,,,,,,,,,,,>
                yield return ConvertTest("(a,b,c,d,e,f,g,h,i,j,k,l,m,n,o) => a + b + c + d + e + f + g + h + i + j + k + l + m + n + o", new object?[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }, 1 + 2 + 3 + 4 + 5 + 6 + 7 + 8 + 9 + 10 + 11 + 12 + 13 + 14 + 15);
                // 16:Func<,,,,,,,,,,,,,,>
                yield return ConvertTest("(a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p) => a + b + c + d + e + f + g + h + i + j + k + l + m + n + o + p", new object?[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 }, 1 + 2 + 3 + 4 + 5 + 6 + 7 + 8 + 9 + 10 + 11 + 12 + 13 + 14 + 15 + 16);

                static object?[] ConvertTest(string Parameter, object?[] Values, object? Expected)
                    => new object?[] { Parameter, Values, Expected };
            }
        }
        [TestMethod, DynamicData(nameof(ConvertTestData))]
        public void ConvertTest(string Parameter, object?[] Values, object? Expected)
            => Assert.AreEqual(Expected, ScriptingConverter.Convert(Parameter, Values));
        [TestMethod]
        public void ConvertTest_17_Argument_Error()
            => Assert.ThrowsException<ArgumentOutOfRangeException>(() => ScriptingConverter.Convert("(a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q) => a + b + c + d + e + f + g + h + i + j + k + l + m + o + p + q", new object?[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 }));
    }
}
