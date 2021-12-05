using BepInEx;
using HarmonyLib;
using IL_AnimationPair;
using KKAPI;
using KKAPI.Studio;

[BepInDependency(KoikatuAPI.GUID)]
[BepInPlugin(GUID, "AI_AnimationPairing", VERSION)]
public class AnimationPairing : BaseUnityPlugin
{
    public const string GUID = "com.hooh.ai.animpari";
    public const string VERSION = "1.0.0";

    public AnimationPairing Instance { get; set; }

    private void Awake()
    {
        Instance = this;
        if (!StudioAPI.InsideStudio) return;
        Harmony.CreateAndPatchAll(typeof(GameHooks));
    }
}