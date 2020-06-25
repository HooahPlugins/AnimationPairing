using System.Reflection;

namespace IL_AnimationPair
{
    public static class RefUtil
    {
        public static object GetInstanceField<T>(T instance, string fieldName)
        {
            var bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
            var field = typeof(T).GetField(fieldName, bindFlags);
            return field.GetValue(instance);
        }
    }
}


// I don't care io cpde
//       var key = select;
// if (SetStruct<int>(ref select, _no))
// {
// 	Studio.Anime.ListNode listNode = null;
// 	if (dicNode.TryGetValue(key, out listNode) && listNode != null)
// 	{
// 		listNode.Select = false;
// 	}
// 	listNode = null;
// 	if (dicNode.TryGetValue(select, out listNode) && listNode != null)
// 	{
// 		listNode.Select = true;
// 	}
// }