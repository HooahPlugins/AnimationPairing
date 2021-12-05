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
        private static void ChangeMotionIKTarget(OCIChar[] ociChars)
        {
            /*if (ociChars.Length < 2) return;

            var femaleIndex = 0;
            var maleIndex = 0;
            ListMotionIK.Clear();
            OciIK.Clear();

            foreach (var ociChar in ociChars)
            {
                switch (ociChar)
                {
                    case OCICharMale charMale:
                        if (OciIK.ContainsKey(charMale)) continue;
                        if (maleIndex++ > 1) continue;
                        var maleIK = new MotionIK(charMale.charInfo);
                        ListMotionIK.Add(maleIK);
                        OciIK[charMale] = maleIK;
                        break;
                    case OCICharFemale charFemale:
                        if (OciIK.ContainsKey(charFemale)) continue;
                        if (femaleIndex++ > 1) continue;
                        var femaleIK = new MotionIK(charFemale.charInfo);
                        ListMotionIK.Add(femaleIK);
                        OciIK[charFemale] = femaleIK;
                        break;
                }
            }

            if (femaleIndex != 0 && maleIndex != 0) return;
            ListMotionIK.Clear();
            OciIK.Clear();*/
        }

        private static void InitializeMotionIKTarget()
        {
            /*
            foreach (var keyValuePair in OciIK)
            {
                var iks = keyValuePair.Value;
                var ociChar = keyValuePair.Key;
                var name = ociChar.charInfo.animBody.GetCurrentAnimatorStateInfo(0);
                var intHash = name.fullPathHash;

#if HS2
                iks.SetPartners(ListMotionIK);
#elif AI
                iks.SetPartners(ListMotionIK);
#endif
                try
                {
                    iks.Calc(intHash);
                }
                catch (Exception)
                {
                    // ignored
                }
                finally
                {
                }
            }
            */
        }


        public static Action Delegat()
        {
            return () => { };
        }

        public static bool SetupIKHook(OCIChar ikSource, OCIChar ikTarget, bool enable = false)
        {
            var updateDelegate = ikSource.finalIK.solver.OnPreUpdate;
            if (enable)
            {
                Delegate.Combine(updateDelegate, Delegat());
            }
            else
            {
                Delegate.Remove(updateDelegate, Delegat());
            }

            return enable;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(AnimeList), "OnSelect")]
        public static bool OnSelect(AnimeList __instance, int _no)
        {
            var chars = StudioAPI.GetSelectedCharacters();
            var ociChars = chars as OCIChar[] ?? chars.ToArray();

            var charCounts = ociChars.Count();
            if (charCounts >= 2)
            {
                // TODO: Link MotionIK

                var group = (int)RefUtil.GetInstanceField(__instance, "group");
                var category = (int)RefUtil.GetInstanceField(__instance, "category");

                var select = (int)RefUtil.GetInstanceField(__instance, "select");
                var sex = (AnimeGroupList.SEX)RefUtil.GetInstanceField(__instance, "sex");
                // var dicNode = (Dictionary<int, ListNode>) RefUtil.GetInstanceField(__instance, "dicNode");

                ChangeMotionIKTarget(ociChars);

                var femIndex = 1;
                foreach (var ociChar in ociChars)
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
                                        if (animSetType == RefUtil.AnimationType.FFMPairs && ociChar is OCICharMale ||
                                            animSetType == RefUtil.AnimationType.MMFPairs && ociChar is OCICharFemale)
                                        {
                                            ociChar.LoadAnime(
                                                animsetGrp.Length > 3 ? animsetGrp.ElementAtOrDefault(3) : @group,
                                                animsetGrp.ElementAtOrDefault(0),
                                                _no
                                            );
                                        }
                                        else
                                        {
                                            ociChar.LoadAnime(
                                                animsetGrp.Length > 4 ? animsetGrp.ElementAtOrDefault(4) : @group,
                                                animsetGrp.ElementAtOrDefault(Math.Min(2, femIndex)),
                                                _no); // fuck your null reference.
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
                                                ociChar.LoadAnime(@group, ociChar.sex != 0 ? swpCat > 0 ? category :
                                                    Math.Abs(swpCat) :
                                                    swpCat > 0 ? swpCat : category, _no);
                                            }
                                            else
                                            {
                                                ociChar.LoadAnime(@group, femIndex == 1 ? category : swpCat, _no);
                                                femIndex++;
                                            }

                                            break;
                                        case 2:
                                            ociChar.LoadAnime(animsetGrp.ElementAtOrDefault(ociChar.sex), category,
                                                _no); // fuck your null reference.
                                            break;
                                        default:
                                            ociChar.LoadAnime(@group, category, _no);
                                            break;
                                    }

                                    break;
                            }
                        else
                            ociChar.LoadAnime(@group, category, _no);
                    }
                    else
                        ociChar.LoadAnime(@group, category, _no);
                }

                InitializeMotionIKTarget();

                return false;
            }
            else
            {
                // TODO: Unlink MotionIK
            }

            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(AddObjectAssist), "AddIKTarget", typeof(OCIChar), typeof(IKCtrl), typeof(int),
            typeof(Transform), typeof(bool), typeof(Transform), typeof(bool))]
        private static void OnRegisterIKTarget(OCIChar.IKInfo __result, OCIChar _ociChar, IKCtrl _ikCtrl, int _no,
            Transform _target, bool _usedRot, Transform _bone, bool _isRed)
        {
            /*var baseObject = __result.baseObject;
            if (baseObject == null) return;
#if HS2
            var baseData = baseObject.GetComponent<Illusion.Component.Correct.BaseData>();
            var baseCorrectData = baseObject.GetComponent<Illusion.Component.Correct.Process.BaseProcess>();
#elif AI
            var baseData = baseObject.GetComponent<Correct.BaseData>();
            var baseCorrectData = baseObject.GetComponent<Correct.Process.BaseProcess>();
#endif
            if (baseData == null || baseCorrectData == null) return;
            var targetObject = __result.targetObject;
            if (targetObject == null) return;
            targetObject.gameObject.CopyComponent(baseData);
            targetObject.gameObject.CopyComponent(baseCorrectData);*/
        }


        //
        // [HarmonyPostfix]
        // [HarmonyPatch(typeof(OCIChar), "ActiveIKGroup")]
        // public static void OnIKToggled(OCIChar __instance, OIBoneInfo.BoneGroup boneGroup, bool _active)
        // {
        //     if (_active)
        //     {
        //         // remove motion data if anything went active.
        //     }
        // }
    }
}