using System;
using System.Linq;
using HarmonyLib;
using KKAPI.Studio;
using Studio;

namespace IL_AnimationPair
{
    public class GameHooks
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(AnimeList), "OnSelect")]
        public static bool OnSelect(AnimeList __instance, int _no)
        {
            var chars = StudioAPI.GetSelectedCharacters();
            var charCounts = chars.Count();
            if (charCounts >= 2)
            {
                var group = (int) RefUtil.GetInstanceField(__instance, "group");
                var category = (int) RefUtil.GetInstanceField(__instance, "category");
                var select = (int) RefUtil.GetInstanceField(__instance, "select");
                var sex = (AnimeGroupList.SEX) RefUtil.GetInstanceField(__instance, "sex");
                // var dicNode = (Dictionary<int, ListNode>) RefUtil.GetInstanceField(__instance, "dicNode");

                var femIndex = 1;
                foreach (var ociChar in chars)
                {
                    RefUtil.AnimationSet? animSetData = RefUtil.GetAnimationSet(category);
                    if (animSetData.HasValue)
                    {
                        // fuck your shit i'm never going to play female animation on males.
                        var animsetGrp = animSetData.Value.info;
                        var animSetType = animSetData.Value.type;
                        var infoCount = animSetData.Value.infoCount;

                        if (animsetGrp != null && infoCount > 0)
                            switch (animSetType)
                            {
                                // If the animation is related with more than 3 actors?
                                case RefUtil.AnimationType.FFMPairs:
                                case RefUtil.AnimationType.MMFPairs:
                                    if (charCounts >= 3)
                                    {
                                        if ((animSetType == RefUtil.AnimationType.FFMPairs && ociChar.sex == 0) || (animSetType == RefUtil.AnimationType.MMFPairs && ociChar.sex != 0))
                                        {
                                            ociChar.LoadAnime(@group, animsetGrp.ElementAtOrDefault(0), _no);
                                        }
                                        else
                                        {
                                            ociChar.LoadAnime(@group, animsetGrp.ElementAtOrDefault(Math.Min(2, femIndex)), _no); // fuck your null reference.
                                            femIndex++;
                                        }
                                    }
                                    else
                                        ociChar.LoadAnime(@group, category, _no);

                                    break;
                                case RefUtil.AnimationType.FFPairs:
                                case RefUtil.AnimationType.MFPairs:
                                    switch (infoCount)
                                    {
                                        case 1:
                                            // when category is getting changed, keep everything else.
                                            var swpCat = animsetGrp.ElementAtOrDefault(0);
                                            if (animSetType == 0)
                                            {
                                                ociChar.LoadAnime(@group, ociChar.sex != 0 ? swpCat > 0 ? category : Math.Abs(swpCat) :
                                                    swpCat > 0 ? swpCat : category, _no);
                                            }
                                            else
                                            {
                                                ociChar.LoadAnime(@group, femIndex == 1 ? category : swpCat, _no);
                                                femIndex++;
                                            }

                                            break;
                                        case 2:
                                            ociChar.LoadAnime(animsetGrp.ElementAtOrDefault(ociChar.sex), category, _no); // fuck your null reference.
                                            break;
                                        default:
                                            ociChar.LoadAnime(@group, category, _no);
                                            break;
                                    } 
                                    break;
                            }
                        else
                            ociChar.LoadAnime(@group, category, _no);
                    } else 
                        ociChar.LoadAnime(@group, category, _no);
                }

                return false;
            }

            return true;
        }
    }
}