using System.Collections.Generic;
using System.Reflection;

namespace IL_AnimationPair
{
    public static class RefUtil
    {
        private const BindingFlags BindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic |
                                               BindingFlags.Static;

        private static readonly Dictionary<string, FieldInfo> FieldInfos = new Dictionary<string, FieldInfo>();

        public static object GetInstanceField<T>(T instance, string fieldName)
        {
            if (FieldInfos.TryGetValue(fieldName, out var fieldInfo))
                return fieldInfo != null ? fieldInfo.GetValue(instance) : null;

            fieldInfo = typeof(T).GetField(fieldName, BindFlags);
            FieldInfos[fieldName] = fieldInfo;

            return fieldInfo != null ? fieldInfo.GetValue(instance) : null;
        }
    }
}