using System.Reflection;
using JetBrains.Annotations;
using UnityEngine;

namespace IL_AnimationPair
{
    public static class RefUtil
    {
        // public static T CopyComponent<T>(this GameObject destination, T original) where T : Component
        // {
        //     var type = original.GetType();
        //     var dst = destination.GetComponent(type) as T;
        //     if (!dst) dst = destination.AddComponent(type) as T;
        //     var fields = type.GetFields();
        //     foreach (var field in fields)
        //     {
        //         if (field.IsStatic) continue;
        //         field.SetValue(dst, field.GetValue(original));
        //     }
        //
        //     var props = type.GetProperties();
        //     foreach (var prop in props)
        //     {
        //         if (!prop.CanWrite || !prop.CanWrite || prop.Name == "name") continue;
        //         prop.SetValue(dst, prop.GetValue(original, null), null);
        //     }
        //
        //     return dst;
        // }

        public static object GetInstanceField<T>(T instance, string fieldName)
        {
            var bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
            var field = typeof(T).GetField(fieldName, bindFlags);
            return field.GetValue(instance);
        }

        public static AnimationSet? GetAnimationSet(int category)
        {
            // Yes I know it's bad
            // but i really didn't want to think anything.
            // Maybe going to change it to some sort of universal pair structure. idk maybe you do it if it's really bothersome.
            int[] result;
            if (PairedAnimations.MFPairs.TryGetValue(category, out result)) return new AnimationSet(result, AnimationType.MFPairs);
            if (PairedAnimations.FFPairs.TryGetValue(category, out result)) return new AnimationSet(result, AnimationType.FFPairs);
            if (PairedAnimations.FFMPairs.TryGetValue(category, out result)) return new AnimationSet(result, AnimationType.FFMPairs);
            if (PairedAnimations.MMFPairs.TryGetValue(category, out result)) return new AnimationSet(result, AnimationType.MMFPairs);
            return null;
        }

        public enum AnimationType
        {
            MFPairs,
            FFPairs,
            FFMPairs,
            MMFPairs
        }

        public struct AnimationSet
        {
            public int[] info;
            public AnimationType type;
            public int infoCount;

            public AnimationSet(int[] info, AnimationType type)
            {
                this.info = info;
                this.type = type;
                infoCount = info.Length;
            }
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