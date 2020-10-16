using System;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace Scriptings.Converters.Core
{
    public class ScriptingConverter
    {
        public static object Convert(string Parameter, params object?[] values)
        {
            var Options = ScriptOptions.Default
                .WithImports(nameof(System))
                .WithReferences(typeof(Microsoft.CSharp.RuntimeBinder.Binder).Assembly);
            return values.Length switch
            {
                0 => throw new ArgumentException("support values size 1 or later.", nameof(values)),
                1 => CSharpScript.EvaluateAsync<Func<dynamic, dynamic>>($"(Func<dynamic, dynamic>)({Parameter})", Options).Result.Invoke(values[0]!),
                2 => CSharpScript.EvaluateAsync<Func<dynamic, dynamic, dynamic>>($"(Func<dynamic, dynamic, dynamic>)({Parameter})", Options).Result.Invoke(values[0]!, values[1]!),
                3 => CSharpScript.EvaluateAsync<Func<dynamic, dynamic, dynamic, dynamic>>($"(Func<dynamic, dynamic, dynamic, dynamic>)({Parameter})", Options).Result.Invoke(values[0]!, values[1]!, values[2]!),
                _ => throw new ArgumentException($"support values size max 3. size:{values.Length}", nameof(values)),
            };
        }
    }
}
