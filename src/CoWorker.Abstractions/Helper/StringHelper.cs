namespace System
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Text;
    using Newtonsoft.Json;
    using Microsoft.Extensions.Logging.Abstractions.Internal;

    public static class StringHelper
    {
        public static bool IsEmpty(this string str) => string.IsNullOrWhiteSpace(str);

        public static bool Equal(this string left,
            string right,
            StringComparison comparison = StringComparison.Ordinal)
            => string.Equals(left, right, comparison);

        public static string ToJoin(this IEnumerable<string> iterator,
            string split = null)
            => string.Join(split ?? string.Empty,iterator.ToArray());

        public static string ReplaceAllToOne(this string str,
            string newstr,
            params string[] pattern)
            => pattern.Aggregate(str, (seed, ptn) => seed.Replace(ptn, newstr));

        public static string ReplaceWithRegex(
            this string str, 
            string newstr, 
            string pattern, 
            RegexOptions options = RegexOptions.None)
            => Regex.Replace(str, pattern, newstr, options);

        public static string MergeToString(this IEnumerable<char> iterator)
            => new string(iterator.ToArray());

        public static string ToUTF8(this byte[] bytes)
            => Encoding.UTF8.GetString(bytes);

        public static byte[] ToBytes(this string str)
            => Encoding.UTF8.GetBytes(str);


		public static string ToJson(this object obj)
            => JsonConvert.SerializeObject(obj);

        public static T JsonToObject<T>(this string str)
            => JsonConvert.DeserializeObject<T>(str);

        public static string ToFormatString(this Type type)
            => TypeNameHelper.GetTypeDisplayName(type);
		public static string ToShortString(this Type type)
			=> type.ToFormatString().Split('.').DefaultIfEmpty(string.Empty).Last();


		public static string ToBase64(this string source)
            => Encoding.UTF8.GetBytes(source).ToBase64();

        public static string ToBase64(this byte[] bytes)
            => Convert.ToBase64String(bytes);

        public static string Remove(this string str, string item)
            => str.Replace(item,string.Empty);
		
        //private static string ToStringWithGenericArgument(this IEnumerable<Type> types)
        //    => $"<{types.Select(x => $"{x.Name}{(x.IsGenericType ? x.GenericTypeArguments.ToStringWithGenericArgument() : string.Empty)}>";

		public static string WithFormat(this string str, string format)
			=> String.Format(format, str);
		public static string WithFormat(this IEnumerable<string> str, string format)
			=> String.Format(format, str.ToArray());

		public static (object _,Func<StringComparison, bool> By) StartWith(
			this string target,
			params string[] patterns)
			=> (null,c => patterns.Any(x => target.StartsWith(x, c)));
        public static string GetFirstPart(this string str, Func<char, bool> predicate, bool ignorehead = true)
            => str.Substring(0,str.MarkPoint(predicate,ignorehead).FirstOrDefault());
        public static string RemoveFirstPart(
            this string str,
            Func<char, bool> predicate,
            bool ignorehead = true)
            => str.Substring(str.MarkPoint(predicate, ignorehead).FirstOrDefault());

        public static string GetLastPart(this string str, Func<char, bool> predicate, bool ignorehead = true)
            => str.Substring(str.MarkPoint(predicate, ignorehead).LastOrDefault());
        public static string RemoveLastPart(
            this string str,
            Func<char, bool> predicate,
            bool ignorehead = true)
        {
            var mark = str.MarkPoint(predicate, ignorehead);
            return str.Substring(0, str.MarkPoint(predicate, ignorehead).LastOrDefault());
        }

        //public static string GetPart(this string str, Func<char, bool> predicate, bool ignorehead = true)
        //    => str.GetPart(predicate,predicate,ignorehead);

        //public static string GetPart(this string str,Func<char, bool> fromstart,Func<char,bool> fromend,bool ignorehead = true)
        //    => str.SkipWhile(fromstart.WrapIgnoreHead()(ignorehead))
        //        .Reverse()
        //        .SkipWhile(fromend.WrapIgnoreHead()(ignorehead))
        //        .Reverse()
        //        .MergeToString();
        
    }
}
