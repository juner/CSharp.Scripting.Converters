using System;
using System.Reflection;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace Scriptings.Converters.Core
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
                0 => throw new ArgumentException("support values size 1 or later.", nameof(values)),
                1 => CSharpScript.EvaluateAsync<Func<dynamic, dynamic>>($"(Func<dynamic, dynamic>)({Script})", Options).Result,
                2 => CSharpScript.EvaluateAsync<Func<dynamic, dynamic, dynamic>>($"(Func<dynamic, dynamic, dynamic>)({Script})", Options).Result,
                3 => CSharpScript.EvaluateAsync<Func<dynamic, dynamic, dynamic, dynamic>>($"(Func<dynamic, dynamic, dynamic, dynamic>)({Script})", Options).Result,
                4 => CSharpScript.EvaluateAsync<Func<dynamic, dynamic, dynamic, dynamic, dynamic>>($"(Func<dynamic, dynamic, dynamic, dynamic, dynamic>)({Script})", Options).Result,
                5 => CSharpScript.EvaluateAsync<Func<dynamic, dynamic, dynamic, dynamic, dynamic, dynamic>>($"(Func<dynamic, dynamic, dynamic, dynamic, dynamic, dynamic>)({Script})", Options).Result,
                _ => throw new ArgumentException($"support values size max 5. size:{values.Length}", nameof(values)),
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
