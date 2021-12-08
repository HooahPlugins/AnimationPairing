using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using KKAPI.Studio;
using Studio;
using UnityEngine;

namespace IL_AnimationPair
{
    public class GameHooks
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(AnimeList), "OnSelect")]
        public static bool OnSelect(AnimeList __instance, int _no)
        {
            var chars = StudioAPI.GetSelectedCharacters();
            var group = (int)RefUtil.GetInstanceField(__instance, "group");
            var category = (int)RefUtil.GetInstanceField(__instance, "category");
            var ociChars = chars as OCIChar[] ?? chars.ToArray();
            if (ociChars.Length < 2)
            {
                foreach (var ociChar in ociChars) ociChar.LoadAnime(group, category, _no);
            }
            else
            {
                if (!PairedAnimations.TryGetAnimationPair(@group, category, out var pair))
                    foreach (var ociChar in ociChars)
                        ociChar.LoadAnime(group, category, _no);
                else
                {
                    pair.MapAnimations(ociChars, _no);
                    return false;
                }
            }

            return true;
        }
    }
}