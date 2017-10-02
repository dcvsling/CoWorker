using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoWorker.Abstractions.Values
{
    public struct ContentType :IEquatable<ContentType>
    {
		public ContentType Create(string contentType) => new ContentType(contentType);
		private ContentType(ContentType other)
		{
			this.source = other.source;
			this.Type = other.Type;
			this.SubType = other.SubType;
			this.Properties = other.Properties;
		}

		private ContentType(string contentType)
		{
			source = contentType.Trim();
			var items = contentType
                .Split(';')
                .Select(x => x.Trim())
                .DefaultIfEmpty(string.Empty);
            var types = items
                .First()
                .Split('/')
                .Concat(Enumerable.Repeat(string.Empty, 2));
			this.Type = types.First();
			this.SubType = types.Skip(1).First();
			this.Properties = items.Skip(1).Select(x => x.Split('=')).ToDictionary(x => x[0], x => x[1]);
		}
		private readonly string source;
		public string Type { get; }
		public string SubType { get; }
		public IDictionary<string,string> Properties { get; }

		public Boolean Equals(ContentType other) => other.GetHashCode() == GetHashCode();

		public override String ToString() => source;
		public override Int32 GetHashCode() => source.GetHashCode();

		public static implicit operator string(ContentType content) => content.ToString();
		public static implicit operator ContentType(string content) => new ContentType(content);
	}
}
