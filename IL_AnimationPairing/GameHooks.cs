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
            var counts = chars.Count();
            if (counts >= 2)
            {
                var group = (int) RefUtil.GetInstanceField(__instance, "group");
                var category = (int) RefUtil.GetInstanceField(__instance, "category");
                var select = (int) RefUtil.GetInstanceField(__instance, "select");
                var sex = (AnimeGroupList.SEX) RefUtil.GetInstanceField(__instance, "sex");
                // var dicNode = (Dictionary<int, ListNode>) RefUtil.GetInstanceField(__instance, "dicNode");

                var femIndex = 1;
                foreach (var ociChar in chars)
                {
                    // if sex or afute is on
                    // fuck your shit i'm never going to play female animation on males.
                    var animsetGrp =
                        PairedAnimations.MFPairs.ContainsKey(category) ? PairedAnimations.MFPairs[category] :
                        PairedAnimations.FFPairs.ContainsKey(category) ? PairedAnimations.FFPairs[category] : null;

                    var animSetType = PairedAnimations.MFPairs.ContainsKey(category) ? 0 :
                        PairedAnimations.FFPairs.ContainsKey(category) ? 1 : -1;

                    if (animsetGrp != null)
                        switch (animsetGrp.Length)
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
                            case 3:
                                if (counts >= 3)
                                {
                                    if (ociChar.sex == 0)
                                    {
                                        ociChar.LoadAnime(@group, animsetGrp.ElementAtOrDefault(0), _no);
                                    }
                                    else
                                    {
                                        ociChar.LoadAnime(@group, animsetGrp.ElementAtOrDefault(Math.Min(2, femIndex)), _no); // fuck your null reference.
                                        femIndex++;
                                    }
                                }

                                break;
                            default:
                                ociChar.LoadAnime(@group, category, _no);
                                break;
                        }
                    else
                        ociChar.LoadAnime(@group, category, _no);
                }

                return false;
            }

            return true;
        }
    }
}