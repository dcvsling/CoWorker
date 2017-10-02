
namespace System.Reflection
{
    public static class TypeHelper
    {
        private const object NULL = null;
        public static bool IsDelegate(this Type type)
            => typeof(Delegate).IsAssignableFrom(type);

        public static bool IsNull(this object obj)
            => ReferenceEquals(obj, NULL);
		
		public static Type GetGenericDefinitionIfGeneric(this Type type)
			=> type.IsGenericType ? type.GetGenericTypeDefinition() : type;

        public static string GetPrefixName(this Type type)
            => type.Name.GetFirstPart(x => Char.IsUpper(x));

        public static string GetPostfixName(this Type type)
            => type.Name.GetLastPart(x => Char.IsUpper(x));
        
        public static string RemovePrefixName(this Type type)
            => type.Name.RemoveFirstPart(x => Char.IsUpper(x));
        public static string RemovePostfixName(this Type type)
            => type.Name.RemoveLastPart(x => Char.IsUpper(x));
        public static bool IsAssignablePOCOFrom<TType>(this Type type)
            => typeof(TType).IsAssignableFrom(type)
                && type != typeof(TType)
                && !type.IsAbstract
                && !type.IsInterface;
    }
}
