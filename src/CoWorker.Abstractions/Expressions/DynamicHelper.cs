namespace System.Dynamic
{
    using System.Linq.Expressions;
    public static class DynamicHelper
    {
        public static DynamicMetaObject ToMetaObject(this Expression expression)
            => new DynamicMetaObject(expression, BindingRestrictions.Empty);
        public static DynamicMetaObject ToMetaObject(this Expression expression, BindingRestrictions restrictions)
            => new DynamicMetaObject(expression, restrictions);
        public static DynamicMetaObject ToMetaObject(this Expression expression, BindingRestrictions restrictions, object value)
            => new DynamicMetaObject(expression, restrictions, value);

        public static DynamicMetaObject ToMetaObject(this Expression expression, object value)
            => new DynamicMetaObject(expression, BindingRestrictions.GetInstanceRestriction(expression, value));

        public static DynamicMetaObject ToMetaObject(this Expression expression, Type type)
            => new DynamicMetaObject(expression, BindingRestrictions.GetTypeRestriction(expression,type));

        public static DynamicMetaObject ToMetaObject(this Expression expression, Expression exp)
            => new DynamicMetaObject(expression, BindingRestrictions.GetExpressionRestriction(exp));

        public static DynamicMetaObject ToMetaObjectWithValue(this Expression expression, object instance,object value)
            => new DynamicMetaObject(expression, BindingRestrictions.GetInstanceRestriction(expression, instance),value);

        public static DynamicMetaObject ToMetaObjectWithValue(this Expression expression, Type type,object value)
            => new DynamicMetaObject(expression, BindingRestrictions.GetTypeRestriction(expression, type),value);

        public static DynamicMetaObject ToMetaObjectWithValue(this Expression expression, Expression exp,object value)
            => new DynamicMetaObject(expression, BindingRestrictions.GetExpressionRestriction(exp),value);
    }
}
