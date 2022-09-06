using System;
using System.IO;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;

using Experience;

//削除キー：loyosaka
//https://dotup.org/uploda/dotup.org2853958.zip.html
namespace LoYTalkFilterPlugin
{

[BepInPlugin("LoY.TalkFilter.Plugin", "LoY TalkFilter Plug-In", "0.0.0.2")]
[BepInDependency("LoY.Util.Plugin")]
public class LoYTalkFilterPlugin : BaseUnityPlugin
{
    static readonly string id = "LoY.TalkFilter.Plugin.Patcher";
    static OsakaTranslator osk = null;
    static OjosamaTranslator ojs = null;

    public void Awake()
    {
        Harmony hm = new Harmony(id);
        ConfigFile cfg = Config;

        ConfigEntry<string> ttype = cfg.Bind(
                "Selection", "Translation", "無効",
                "変換フィルターの選択。無効/大阪弁/お嬢様。"
            );

        var org = typeof(DisplayString).GetMethod("get_Text");
        MethodInfo hook;
        var tp = typeof(LoYTalkFilterPlugin);
        switch(ttype.Value)
        {
            case "無効":
                return;
            case "大阪弁":
                hook = tp.GetMethod("get_OsakaText");
                osk = new OsakaTranslator();
                break;
            case "お嬢様":
                hook = tp.GetMethod("get_OjosamaText");
                ojs = new OjosamaTranslator();
                break;
            default:
                Console.Write("ttype.Value is not valid.");
                return;
        }
        hm.Patch(org, postfix: new HarmonyMethod(hook));

        //Console.Write("------------------------------");
        //Console.Write("TalkFilter Plugin loaded.");
        //Console.Write("------------------------------");
    }

    public static void get_OsakaText(ref string __result)
    {
        __result = osk.translate_talk(__result);
    }

    public static void get_OjosamaText(ref string __result)
    {
        __result = ojs.translate_talk(__result);
    }
}

}