using System;
using System.Collections.Generic;
using System.Text;

namespace CoWorker.Abstractions.Values
{
    public struct Email : IEquatable<Email>
    {
        public const char SPLIT_TOKEN = '@';
        public static Email New(string email) => new Email(email);
        private Email(string email)
        {
            if (string.IsNullOrEmpty(email) || email.IndexOfAny(new char[] { SPLIT_TOKEN }) < 0)
                throw new TypeInitializationException(
                    nameof(Email),
                    new ArgumentException("argument is not email format")
                );
            var items = email.Split(SPLIT_TOKEN);
            Name = items[0];
            Domain = items[1];
        }
        public string Name { get; }
        public string Domain { get; }

        public override Int32 GetHashCode() => Name.GetHashCode() + Domain.GetHashCode();

        public Boolean Equals(Email other) => other.GetHashCode().Equals(this.GetHashCode());

        public override String ToString() => $"{Name}@{Domain}";

        public static implicit operator string(Email email) => email.ToString();
    }
}
