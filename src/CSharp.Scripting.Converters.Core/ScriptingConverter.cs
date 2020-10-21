using System;
using System.Reflection;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace CSharp.Scripting.Converters.Core
{
    public class ScriptingConverter
    {
        public static object Convert(string Script, params object?[] values)
        {
            var Options = ScriptOptions.Default
                .WithImports(nameof(System))
                .WithReferences(typeof(Microsoft.CSharp.RuntimeBinder.Binder).Assembly);
            var (Target, Method) = (Delegate)(values.Length switch
            {
                0 => CSharpScript.EvaluateAsync<Func<dynamic>>($"(Func<dynamic>)({Script})", Options).Result,
                1 => CSharpScript.EvaluateAsync<Func<dynamic, dynamic>>($"(Func<dynamic, dynamic>)({Script})", Options).Result,
                2 => CSharpScript.EvaluateAsync<Func<dynamic, dynamic, dynamic>>($"(Func<dynamic, dynamic, dynamic>)({Script})", Options).Result,
                3 => CSharpScript.EvaluateAsync<Func<dynamic, dynamic, dynamic, dynamic>>($"(Func<dynamic, dynamic, dynamic, dynamic>)({Script})", Options).Result,
                4 => CSharpScript.EvaluateAsync<Func<dynamic, dynamic, dynamic, dynamic, dynamic>>($"(Func<dynamic, dynamic, dynamic, dynamic, dynamic>)({Script})", Options).Result,
                5 => CSharpScript.EvaluateAsync<Func<dynamic, dynamic, dynamic, dynamic, dynamic, dynamic>>($"(Func<dynamic, dynamic, dynamic, dynamic, dynamic, dynamic>)({Script})", Options).Result,
                6 => CSharpScript.EvaluateAsync<Func<dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic>>($"(Func<dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic>)({Script})", Options).Result,
                7 => CSharpScript.EvaluateAsync<Func<dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic>>($"(Func<dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic>)({Script})", Options).Result,
                8 => CSharpScript.EvaluateAsync<Func<dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic>>($"(Func<dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic>)({Script})", Options).Result,
                9 => CSharpScript.EvaluateAsync<Func<dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic>>($"(Func<dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic>)({Script})", Options).Result,
                10 => CSharpScript.EvaluateAsync<Func<dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic>>($"(Func<dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic>)({Script})", Options).Result,
                11 => CSharpScript.EvaluateAsync<Func<dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic>>($"(Func<dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic>)({Script})", Options).Result,
                12 => CSharpScript.EvaluateAsync<Func<dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic>>($"(Func<dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic>)({Script})", Options).Result,
                13 => CSharpScript.EvaluateAsync<Func<dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic>>($"(Func<dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic>)({Script})", Options).Result,
                14 => CSharpScript.EvaluateAsync<Func<dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic>>($"(Func<dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic>)({Script})", Options).Result,
                15 => CSharpScript.EvaluateAsync<Func<dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic>>($"(Func<dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic>)({Script})", Options).Result,
                16 => CSharpScript.EvaluateAsync<Func<dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic>>($"(Func<dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic>)({Script})", Options).Result,
                _ => throw new ArgumentOutOfRangeException(nameof(values), $"support values size max 16. size:{values.Length}"),
            });
            return Method.Invoke(Target, values);
        }
    }
    internal static class DelegateExtensions
    {
        public static void Deconstruct(this Delegate Delegate, out object Target, out MethodInfo Method)
            => (Target, Method) = (Delegate.Target, Delegate.Method);
    }
}
