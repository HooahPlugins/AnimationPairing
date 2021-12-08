using System;
using System.Collections.Generic;
using System.Linq;
using Studio;

/*
 * I UNDERSTAND YOUR PAIN, THIS IS PAINFUL.
 * BUT PLEASE UNDERSTAND IM SUFFERING TOO.
 */
namespace IL_AnimationPair
{
    public struct PairTarget
    {
        public int Group; // CT_BIG
        public int Category; // CT_MID

        public PairTarget(int g, int c)
        {
            Group = g;
            Category = c;
        }
    }

    public readonly struct AnimationPair
    {
        private readonly PairTarget[] _femaleTargets;
        private readonly PairTarget[] _maleTargets;

        // Female first, male second.

        public AnimationPair(int group, int slotA, int slotB, int slotC, bool ffm)
        {
            _femaleTargets = ffm
                ? new[] { new PairTarget(group, slotA), new PairTarget(group, slotB), }
                : new[] { new PairTarget(group, slotA) };
            _maleTargets = ffm
                ? new[] { new PairTarget(group, slotC) }
                : new[] { new PairTarget(group, slotB), new PairTarget(group, slotC) };
        }

        public AnimationPair(int groupA, int groupB, int slotA, int slotB, int slotC, bool ffm)
        {
            _femaleTargets = ffm
                ? new[] { new PairTarget(groupA, slotA), new PairTarget(groupA, slotB), }
                : new[] { new PairTarget(groupA, slotA) };
            _maleTargets = ffm
                ? new[] { new PairTarget(groupB, slotC) }
                : new[] { new PairTarget(groupB, slotB), new PairTarget(groupB, slotC) };
        }

        public AnimationPair(int groupA, int groupB, int slotA, int slotB)
        {
            _femaleTargets = new[] { new PairTarget(groupA, slotA) };
            _maleTargets = new[] { new PairTarget(groupB, slotB) };
        }

        public AnimationPair(int groupA, int slotA, int slotB, bool isLes)
        {
            if (isLes)
            {
                _femaleTargets = new[] { new PairTarget(groupA, slotA) };
                _maleTargets = new[] { new PairTarget(groupA, slotB) };
            }
            else
            {
                _maleTargets = Array.Empty<PairTarget>();
                _femaleTargets = new[]
                {
                    new PairTarget(groupA, slotA),
                    new PairTarget(groupA, slotB)
                };
            }
        }


        public void MapAnimations(IEnumerable<OCIChar> characters, int no)
        {
            var femaleIndex = 0;
            var maleIndex = 0;
            foreach (var ociChar in characters)
            {
                switch (ociChar)
                {
                    case OCICharFemale _ when _femaleTargets.Length == 0:
                        continue;
                    case OCICharFemale _:
                    {
                        var animTarget =
                            _femaleTargets.ElementAtOrDefault(Math.Min(femaleIndex, _femaleTargets.Length - 1));
                        ociChar.LoadAnime(animTarget.Group, animTarget.Category, no);
                        femaleIndex++;
                        break;
                    }
                    case OCICharMale _ when _maleTargets.Length == 0:
                        continue;
                    case OCICharMale _:
                    {
                        var animTarget =
                            _maleTargets.ElementAtOrDefault(Math.Min(maleIndex, _maleTargets.Length - 1));
                        ociChar.LoadAnime(animTarget.Group, animTarget.Category, no);
                        maleIndex++;
                        break;
                    }
                }
            }
        }
    }

    // key group
    // group, category tuple

    public static class PairedAnimations
    {
        private class AnimDictionary<TKeyA, TKeyB, TValue> : Dictionary<TKeyA, Dictionary<TKeyB, TValue>>
        {
        }

        public static bool TryGetAnimationPair(int group, int category, out AnimationPair pair)
        {
            pair = default;
            return AnimationPairDictionary.TryGetValue(group, out var categoryDict) &&
                   categoryDict.TryGetValue(category, out pair);
        }

        private static readonly AnimDictionary<int, int, AnimationPair> AnimationPairDictionary =
            new AnimDictionary<int, int, AnimationPair>
            {
                {
                    1, new Dictionary<int, AnimationPair>
                    {
                        { 100, new AnimationPair(1, 2, 100, 100) },
                        { 101, new AnimationPair(1, 2, 101, 101) },
                        { 102, new AnimationPair(1, 2, 102, 102) },
                        { 103, new AnimationPair(1, 2, 103, 103) },
                        { 104, new AnimationPair(1, 2, 104, 104) },
                        { 105, new AnimationPair(1, 2, 105, 105) },
                        { 106, new AnimationPair(1, 2, 106, 106) },
                        { 107, new AnimationPair(1, 2, 107, 107) },
                        { 108, new AnimationPair(1, 2, 108, 108) },
                        { 109, new AnimationPair(1, 2, 109, 109) },
                        { 110, new AnimationPair(1, 2, 110, 110) },
                        { 111, new AnimationPair(1, 2, 111, 111) },
                        { 112, new AnimationPair(1, 2, 112, 112) },
                        { 113, new AnimationPair(1, 2, 113, 113) },
                        { 114, new AnimationPair(1, 2, 114, 114) },
                        { 115, new AnimationPair(1, 2, 115, 115) },
                        { 116, new AnimationPair(1, 2, 116, 116) },
                        { 117, new AnimationPair(1, 2, 117, 117) },
                        { 118, new AnimationPair(1, 2, 118, 118) },
                        { 119, new AnimationPair(1, 2, 119, 119) },
                        { 120, new AnimationPair(1, 2, 120, 120) },
                        { 121, new AnimationPair(1, 2, 121, 121) },
                        { 122, new AnimationPair(1, 2, 122, 122) },
                        { 123, new AnimationPair(1, 2, 123, 123) },
                        { 124, new AnimationPair(1, 2, 124, 124) },
                        { 125, new AnimationPair(1, 2, 125, 125) },
                        { 126, new AnimationPair(1, 2, 126, 126) },
                    }
                },
                {
                    2, new Dictionary<int, AnimationPair>
                    {
                        { 100, new AnimationPair(1, 2, 100, 100) },
                        { 101, new AnimationPair(1, 2, 101, 101) },
                        { 102, new AnimationPair(1, 2, 102, 102) },
                        { 103, new AnimationPair(1, 2, 103, 103) },
                        { 104, new AnimationPair(1, 2, 104, 104) },
                        { 105, new AnimationPair(1, 2, 105, 105) },
                        { 106, new AnimationPair(1, 2, 106, 106) },
                        { 107, new AnimationPair(1, 2, 107, 107) },
                        { 108, new AnimationPair(1, 2, 108, 108) },
                        { 109, new AnimationPair(1, 2, 109, 109) },
                        { 110, new AnimationPair(1, 2, 110, 110) },
                        { 111, new AnimationPair(1, 2, 111, 111) },
                        { 112, new AnimationPair(1, 2, 112, 112) },
                        { 113, new AnimationPair(1, 2, 113, 113) },
                        { 114, new AnimationPair(1, 2, 114, 114) },
                        { 115, new AnimationPair(1, 2, 115, 115) },
                        { 116, new AnimationPair(1, 2, 116, 116) },
                        { 117, new AnimationPair(1, 2, 117, 117) },
                        { 118, new AnimationPair(1, 2, 118, 118) },
                        { 119, new AnimationPair(1, 2, 119, 119) },
                        { 120, new AnimationPair(1, 2, 120, 120) },
                        { 121, new AnimationPair(1, 2, 121, 121) },
                        { 122, new AnimationPair(1, 2, 122, 122) },
                        { 123, new AnimationPair(1, 2, 123, 123) },
                        { 124, new AnimationPair(1, 2, 124, 124) },
                        { 125, new AnimationPair(1, 2, 125, 125) },
                        { 126, new AnimationPair(1, 2, 126, 126) },
                    }
                },
                {
                    3, new Dictionary<int, AnimationPair>
                    {
                        { 127, new AnimationPair(3, 4, 127, 127) },
                        { 128, new AnimationPair(3, 4, 128, 128) },
                        { 129, new AnimationPair(3, 4, 129, 129) },
                        { 130, new AnimationPair(3, 4, 130, 130) },
                        { 131, new AnimationPair(3, 4, 131, 131) },
                        { 132, new AnimationPair(3, 4, 132, 132) },
                        { 133, new AnimationPair(3, 4, 133, 133) },
                        { 134, new AnimationPair(3, 4, 134, 134) },
                        { 135, new AnimationPair(3, 4, 135, 135) },
                        { 136, new AnimationPair(3, 4, 136, 136) },
                        { 137, new AnimationPair(3, 4, 137, 137) },
                        { 138, new AnimationPair(3, 4, 138, 138) },
                        { 139, new AnimationPair(3, 4, 139, 139) },
                        { 140, new AnimationPair(3, 4, 140, 140) },
                        { 141, new AnimationPair(3, 4, 141, 141) },
                        { 142, new AnimationPair(3, 4, 142, 142) },
                        { 143, new AnimationPair(3, 4, 143, 143) },
                        { 144, new AnimationPair(3, 4, 144, 144) },
                        { 145, new AnimationPair(3, 4, 145, 145) },
                        { 146, new AnimationPair(3, 4, 146, 146) },
                        { 147, new AnimationPair(3, 4, 147, 147) },
                        { 148, new AnimationPair(3, 4, 148, 148) },
                        { 149, new AnimationPair(3, 4, 149, 149) },
                        { 150, new AnimationPair(3, 4, 150, 150) },
                        { 151, new AnimationPair(3, 4, 151, 151) },
                        { 152, new AnimationPair(3, 4, 152, 152) },
                        { 153, new AnimationPair(3, 4, 153, 153) },
                        { 154, new AnimationPair(3, 4, 154, 154) },
                        { 155, new AnimationPair(3, 4, 155, 155) },
                        { 156, new AnimationPair(3, 4, 156, 156) },
                    }
                },
                {
                    4, new Dictionary<int, AnimationPair>
                    {
                        { 127, new AnimationPair(3, 4, 127, 127) },
                        { 128, new AnimationPair(3, 4, 128, 128) },
                        { 129, new AnimationPair(3, 4, 129, 129) },
                        { 130, new AnimationPair(3, 4, 130, 130) },
                        { 131, new AnimationPair(3, 4, 131, 131) },
                        { 132, new AnimationPair(3, 4, 132, 132) },
                        { 133, new AnimationPair(3, 4, 133, 133) },
                        { 134, new AnimationPair(3, 4, 134, 134) },
                        { 135, new AnimationPair(3, 4, 135, 135) },
                        { 136, new AnimationPair(3, 4, 136, 136) },
                        { 137, new AnimationPair(3, 4, 137, 137) },
                        { 138, new AnimationPair(3, 4, 138, 138) },
                        { 139, new AnimationPair(3, 4, 139, 139) },
                        { 140, new AnimationPair(3, 4, 140, 140) },
                        { 141, new AnimationPair(3, 4, 141, 141) },
                        { 142, new AnimationPair(3, 4, 142, 142) },
                        { 143, new AnimationPair(3, 4, 143, 143) },
                        { 144, new AnimationPair(3, 4, 144, 144) },
                        { 145, new AnimationPair(3, 4, 145, 145) },
                        { 146, new AnimationPair(3, 4, 146, 146) },
                        { 147, new AnimationPair(3, 4, 147, 147) },
                        { 148, new AnimationPair(3, 4, 148, 148) },
                        { 149, new AnimationPair(3, 4, 149, 149) },
                        { 150, new AnimationPair(3, 4, 150, 150) },
                        { 151, new AnimationPair(3, 4, 151, 151) },
                        { 152, new AnimationPair(3, 4, 152, 152) },
                        { 153, new AnimationPair(3, 4, 153, 153) },
                        { 154, new AnimationPair(3, 4, 154, 154) },
                        { 155, new AnimationPair(3, 4, 155, 155) },
                        { 156, new AnimationPair(3, 4, 156, 156) },
                    }
                },
                {
                    5, new Dictionary<int, AnimationPair>
                    {
                        { 157, new AnimationPair(5, 6, 157, 157) },
                        { 158, new AnimationPair(5, 6, 158, 158) },
                        { 159, new AnimationPair(5, 6, 159, 159) },
                        { 160, new AnimationPair(5, 6, 160, 160) },
                        { 161, new AnimationPair(5, 6, 161, 161) },
                        { 162, new AnimationPair(5, 6, 162, 162) },
                        { 163, new AnimationPair(5, 6, 163, 163) },
                        { 164, new AnimationPair(5, 6, 164, 164) },
                        { 165, new AnimationPair(5, 6, 165, 165) },
                        { 166, new AnimationPair(5, 6, 166, 166) },
                        { 167, new AnimationPair(5, 6, 167, 167) },
                        { 168, new AnimationPair(5, 6, 168, 168) },
                        { 169, new AnimationPair(5, 6, 169, 169) },
                        { 170, new AnimationPair(5, 6, 170, 170) },
                        { 171, new AnimationPair(5, 6, 171, 171) },
                        { 172, new AnimationPair(5, 6, 172, 172) },
                        { 173, new AnimationPair(5, 6, 173, 173) },
                        { 174, new AnimationPair(5, 6, 174, 174) },
                        { 175, new AnimationPair(5, 6, 175, 175) },
                        { 176, new AnimationPair(5, 6, 176, 176) },
                        { 177, new AnimationPair(5, 6, 177, 177) },
                        { 178, new AnimationPair(5, 6, 178, 178) },
                        { 179, new AnimationPair(5, 6, 179, 179) },
                        { 180, new AnimationPair(5, 6, 180, 180) },
                        { 181, new AnimationPair(5, 6, 181, 181) },
                        { 182, new AnimationPair(5, 6, 182, 182) },
                        { 183, new AnimationPair(5, 6, 183, 183) },
                        { 184, new AnimationPair(5, 6, 184, 184) },
                        { 185, new AnimationPair(5, 6, 185, 185) },
                        { 186, new AnimationPair(5, 6, 186, 186) },
                        { 187, new AnimationPair(5, 6, 187, 187) },
                        { 188, new AnimationPair(5, 6, 188, 188) },
                        { 189, new AnimationPair(5, 6, 189, 189) },
                        { 190, new AnimationPair(5, 6, 190, 190) },
                        { 191, new AnimationPair(5, 6, 191, 191) },
                        { 192, new AnimationPair(5, 6, 192, 192) },
                        { 193, new AnimationPair(5, 6, 193, 193) },
                        { 194, new AnimationPair(5, 6, 194, 194) },
                        { 195, new AnimationPair(5, 6, 195, 195) },
                        { 196, new AnimationPair(5, 6, 196, 196) },
                        { 197, new AnimationPair(5, 6, 197, 197) },
                        { 198, new AnimationPair(5, 6, 198, 198) },
                        { 199, new AnimationPair(5, 6, 199, 199) },
                        { 200, new AnimationPair(5, 6, 200, 200) },
                    }
                },
                {
                    6, new Dictionary<int, AnimationPair>
                    {
                        { 157, new AnimationPair(5, 6, 157, 157) },
                        { 158, new AnimationPair(5, 6, 158, 158) },
                        { 159, new AnimationPair(5, 6, 159, 159) },
                        { 160, new AnimationPair(5, 6, 160, 160) },
                        { 161, new AnimationPair(5, 6, 161, 161) },
                        { 162, new AnimationPair(5, 6, 162, 162) },
                        { 163, new AnimationPair(5, 6, 163, 163) },
                        { 164, new AnimationPair(5, 6, 164, 164) },
                        { 165, new AnimationPair(5, 6, 165, 165) },
                        { 166, new AnimationPair(5, 6, 166, 166) },
                        { 167, new AnimationPair(5, 6, 167, 167) },
                        { 168, new AnimationPair(5, 6, 168, 168) },
                        { 169, new AnimationPair(5, 6, 169, 169) },
                        { 170, new AnimationPair(5, 6, 170, 170) },
                        { 171, new AnimationPair(5, 6, 171, 171) },
                        { 172, new AnimationPair(5, 6, 172, 172) },
                        { 173, new AnimationPair(5, 6, 173, 173) },
                        { 174, new AnimationPair(5, 6, 174, 174) },
                        { 175, new AnimationPair(5, 6, 175, 175) },
                        { 176, new AnimationPair(5, 6, 176, 176) },
                        { 177, new AnimationPair(5, 6, 177, 177) },
                        { 178, new AnimationPair(5, 6, 178, 178) },
                        { 179, new AnimationPair(5, 6, 179, 179) },
                        { 180, new AnimationPair(5, 6, 180, 180) },
                        { 181, new AnimationPair(5, 6, 181, 181) },
                        { 182, new AnimationPair(5, 6, 182, 182) },
                        { 183, new AnimationPair(5, 6, 183, 183) },
                        { 184, new AnimationPair(5, 6, 184, 184) },
                        { 185, new AnimationPair(5, 6, 185, 185) },
                        { 186, new AnimationPair(5, 6, 186, 186) },
                        { 187, new AnimationPair(5, 6, 187, 187) },
                        { 188, new AnimationPair(5, 6, 188, 188) },
                        { 189, new AnimationPair(5, 6, 189, 189) },
                        { 190, new AnimationPair(5, 6, 190, 190) },
                        { 191, new AnimationPair(5, 6, 191, 191) },
                        { 192, new AnimationPair(5, 6, 192, 192) },
                        { 193, new AnimationPair(5, 6, 193, 193) },
                        { 194, new AnimationPair(5, 6, 194, 194) },
                        { 195, new AnimationPair(5, 6, 195, 195) },
                        { 196, new AnimationPair(5, 6, 196, 196) },
                        { 197, new AnimationPair(5, 6, 197, 197) },
                        { 198, new AnimationPair(5, 6, 198, 198) },
                        { 199, new AnimationPair(5, 6, 199, 199) },
                        { 200, new AnimationPair(5, 6, 200, 200) },
                    }
                },
                {
                    7,
                    new Dictionary<int, AnimationPair>
                    {
                        { 201, new AnimationPair(7, 8, 201, 201) },
                        { 204, new AnimationPair(7, 8, 204, 204) },
                        { 211, new AnimationPair(7, 8, 211, 211) },
                        { 212, new AnimationPair(7, 8, 212, 212) },
                        { 213, new AnimationPair(7, 8, 213, 213) },
                        { 208, new AnimationPair(7, 8, 208, 208) },
                    }
                },
                {
                    8,
                    new Dictionary<int, AnimationPair>
                    {
                        { 201, new AnimationPair(7, 8, 201, 201) },
                        { 204, new AnimationPair(7, 8, 204, 204) },
                        { 211, new AnimationPair(7, 8, 211, 211) },
                        { 212, new AnimationPair(7, 8, 212, 212) },
                        { 213, new AnimationPair(7, 8, 213, 213) },
                        { 208, new AnimationPair(7, 8, 208, 208) },
                    }
                },
                {
                    9, new Dictionary<int, AnimationPair>
                    {
                        { 215, new AnimationPair(9, 216, 215, true) },
                        { 216, new AnimationPair(9, 215, 216, true) },
                        { 217, new AnimationPair(9, 218, 217, true) },
                        { 218, new AnimationPair(9, 217, 218, true) },
                        { 219, new AnimationPair(9, 220, 219, true) },
                        { 220, new AnimationPair(9, 219, 220, true) },
                        { 221, new AnimationPair(9, 222, 221, true) },
                        { 222, new AnimationPair(9, 221, 222, true) },
                        { 223, new AnimationPair(9, 224, 223, true) },
                        { 224, new AnimationPair(9, 223, 224, true) },
                    }
                },
                {
                    10,
                    new Dictionary<int, AnimationPair>
                    {
                        { 225, new AnimationPair(10, 225, 226, 227, true) },
                        { 226, new AnimationPair(10, 225, 226, 227, true) },
                        { 227, new AnimationPair(10, 225, 226, 227, true) },
                    }
                },
                {
                    14,
                    new Dictionary<int, AnimationPair>
                    {
                        { 229, new AnimationPair(14, 14, 229, 230) },
                        { 230, new AnimationPair(14, 14, 229, 230) },
                    }
                },
                {
                    18,
                    new Dictionary<int, AnimationPair>
                    {
                        { 232, new AnimationPair(18, 18, 232, 233) },
                        { 233, new AnimationPair(18, 18, 232, 233) },
                        { 234, new AnimationPair(18, 18, 234, 235) },
                        { 235, new AnimationPair(18, 18, 234, 235) },
                    }
                },
                {
                    22,
                    new Dictionary<int, AnimationPair>
                    {
                        { 236, new AnimationPair(22, 23, 236, 236) },
                        { 237, new AnimationPair(22, 23, 237, 237) },
                        { 238, new AnimationPair(22, 23, 238, 238) },
                        { 239, new AnimationPair(22, 23, 239, 239) },
                    }
                },
                {
                    23,
                    new Dictionary<int, AnimationPair>
                    {
                        { 236, new AnimationPair(22, 23, 236, 236) },
                        { 237, new AnimationPair(22, 23, 237, 237) },
                        { 238, new AnimationPair(22, 23, 238, 238) },
                        { 239, new AnimationPair(22, 23, 239, 239) },
                    }
                },
                {
                    24,
                    new Dictionary<int, AnimationPair>
                    {
                        { 240, new AnimationPair(24, 25, 240, 240) },
                        { 241, new AnimationPair(24, 25, 241, 241) },
                        { 242, new AnimationPair(24, 25, 242, 242) },
                        { 243, new AnimationPair(24, 25, 243, 243) },
                        { 244, new AnimationPair(24, 25, 244, 244) },
                        { 245, new AnimationPair(24, 25, 245, 245) },
                        { 246, new AnimationPair(24, 25, 246, 246) },
                        { 247, new AnimationPair(24, 25, 247, 247) },
                        { 248, new AnimationPair(24, 25, 248, 248) },
                        { 249, new AnimationPair(24, 25, 249, 249) },
                        { 250, new AnimationPair(24, 25, 250, 250) },
                        { 290, new AnimationPair(24, 25, 290, 290) },
                    }
                },
                {
                    25,
                    new Dictionary<int, AnimationPair>
                    {
                        { 240, new AnimationPair(24, 25, 240, 240) },
                        { 241, new AnimationPair(24, 25, 241, 241) },
                        { 242, new AnimationPair(24, 25, 242, 242) },
                        { 243, new AnimationPair(24, 25, 243, 243) },
                        { 244, new AnimationPair(24, 25, 244, 244) },
                        { 245, new AnimationPair(24, 25, 245, 245) },
                        { 246, new AnimationPair(24, 25, 246, 246) },
                        { 247, new AnimationPair(24, 25, 247, 247) },
                        { 248, new AnimationPair(24, 25, 248, 248) },
                        { 249, new AnimationPair(24, 25, 249, 249) },
                        { 250, new AnimationPair(24, 25, 250, 250) },
                        { 290, new AnimationPair(24, 25, 290, 290) },
                    }
                },
                {
                    //fsh
                    26,
                    new Dictionary<int, AnimationPair>
                    {
                        { 251, new AnimationPair(26, 27, 251, 251) },
                        { 252, new AnimationPair(26, 27, 252, 252) },
                        { 254, new AnimationPair(26, 27, 254, 254) },
                        { 255, new AnimationPair(26, 27, 255, 255) },
                    }
                },
                {
                    //msh
                    207,
                    new Dictionary<int, AnimationPair>
                    {
                        { 251, new AnimationPair(26, 27, 251, 251) },
                        { 252, new AnimationPair(26, 27, 252, 252) },
                        { 254, new AnimationPair(26, 27, 254, 254) },
                        { 255, new AnimationPair(26, 27, 255, 255) },
                    }
                },
                {
                    // ggh
                    29, new Dictionary<int, AnimationPair>
                    {
                        { 281, new AnimationPair(29, 281, 282, 283, true) },
                        { 282, new AnimationPair(29, 281, 282, 283, true) },
                        { 283, new AnimationPair(29, 281, 282, 283, true) },
                        { 284, new AnimationPair(29, 284, 285, 286, true) },
                        { 285, new AnimationPair(29, 284, 285, 286, true) },
                        { 286, new AnimationPair(29, 284, 285, 286, true) },
                        { 287, new AnimationPair(29, 287, 288, 289, true) },
                        { 288, new AnimationPair(29, 287, 288, 289, true) },
                        { 289, new AnimationPair(29, 287, 288, 289, true) },
                    }
                },
                {
                    // mgh
                    30,
                    new Dictionary<int, AnimationPair>
                    {
                        { 260, new AnimationPair(30, 260, 261, 262, false) },
                        { 261, new AnimationPair(30, 260, 261, 262, false) },
                        { 262, new AnimationPair(30, 260, 261, 262, false) },
                        { 263, new AnimationPair(30, 263, 264, 265, false) },
                        { 264, new AnimationPair(30, 263, 264, 265, false) },
                        { 265, new AnimationPair(30, 263, 264, 265, false) },
                        { 266, new AnimationPair(30, 266, 267, 268, false) },
                        { 267, new AnimationPair(30, 266, 267, 268, false) },
                        { 268, new AnimationPair(30, 266, 267, 268, false) },
                        { 269, new AnimationPair(30, 269, 270, 271, false) },
                        { 270, new AnimationPair(30, 269, 270, 271, false) },
                        { 271, new AnimationPair(30, 269, 270, 271, false) },
                        { 272, new AnimationPair(30, 272, 273, 274, false) },
                        { 273, new AnimationPair(30, 272, 273, 274, false) },
                        { 274, new AnimationPair(30, 272, 273, 274, false) },
                        { 275, new AnimationPair(30, 275, 276, 277, false) },
                        { 276, new AnimationPair(30, 275, 276, 277, false) },
                        { 277, new AnimationPair(30, 275, 276, 277, false) },
                        { 278, new AnimationPair(30, 278, 279, 280, false) },
                        { 279, new AnimationPair(30, 278, 279, 280, false) },
                        { 280, new AnimationPair(30, 278, 279, 280, false) },
                    }
                },
                {
                    // sih
                    32, new Dictionary<int, AnimationPair>
                    {
                        { 291, new AnimationPair(32, 32, 291, 292) },
                        { 292, new AnimationPair(32, 32, 291, 292) },
                        { 293, new AnimationPair(32, 32, 293, 294) },
                        { 294, new AnimationPair(32, 32, 293, 294) },
                    }
                },
                {
                    // smh
                    34, new Dictionary<int, AnimationPair>
                    {
                        { 295, new AnimationPair(34, 34, 295, 296) },
                        { 296, new AnimationPair(34, 34, 295, 296) },
                        { 297, new AnimationPair(34, 34, 297, 298) },
                        { 298, new AnimationPair(34, 34, 297, 298) },
                    }
                },
                {
                    // addwh
                    36, new Dictionary<int, AnimationPair>
                    {
                        { 299, new AnimationPair(36, 37, 299, 299) },
                        { 300, new AnimationPair(36, 37, 300, 300) },
                        { 301, new AnimationPair(36, 37, 301, 301) },
                        { 302, new AnimationPair(36, 37, 302, 302) },
                        { 303, new AnimationPair(36, 37, 303, 303) },
                        { 304, new AnimationPair(36, 37, 304, 304) },
                        { 305, new AnimationPair(36, 37, 305, 305) },
                        { 306, new AnimationPair(36, 37, 306, 306) },
                        { 307, new AnimationPair(36, 37, 307, 307) },
                        { 308, new AnimationPair(36, 37, 308, 308) },
                        { 309, new AnimationPair(36, 37, 309, 309) },
                        { 312, new AnimationPair(36, 37, 312, 312) },
                        { 313, new AnimationPair(36, 37, 313, 314, 315, true) },
                        { 314, new AnimationPair(36, 37, 313, 314, 315, true) },
                        { 316, new AnimationPair(36, 37, 316, 317, 318, true) },
                        { 317, new AnimationPair(36, 37, 316, 317, 318, true) },
                    }
                },
                {
                    // addmh
                    37, new Dictionary<int, AnimationPair>
                    {
                        { 299, new AnimationPair(36, 37, 299, 299) },
                        { 300, new AnimationPair(36, 37, 300, 300) },
                        { 301, new AnimationPair(36, 37, 301, 301) },
                        { 302, new AnimationPair(36, 37, 302, 302) },
                        { 303, new AnimationPair(36, 37, 303, 303) },
                        { 304, new AnimationPair(36, 37, 304, 304) },
                        { 305, new AnimationPair(36, 37, 305, 305) },
                        { 306, new AnimationPair(36, 37, 306, 306) },
                        { 307, new AnimationPair(36, 37, 307, 307) },
                        { 308, new AnimationPair(36, 37, 308, 308) },
                        { 309, new AnimationPair(36, 37, 309, 309) },
                        { 312, new AnimationPair(36, 37, 312, 312) },
                        { 315, new AnimationPair(36, 37, 313, 314, 315, true) },
                        { 318, new AnimationPair(36, 37, 316, 317, 318, true) },
                    }
                }
            };
    }
}