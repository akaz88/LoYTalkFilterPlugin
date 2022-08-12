using System;
//using System.IO;
using System.Reflection;
using BepInEx;
using HarmonyLib;

using Experience;

//削除キー：loyosaka
//https://dotup.org/uploda/dotup.org2853958.zip.html
namespace LoYTalkFilterPlugin
{

[BepInPlugin("LoY.TalkFilter.Plugin", "LoY TalkFilter Plug-In", "0.0.0.2")]
public class LoYTalkFilterPlugin : BaseUnityPlugin
{
    static readonly string id = "LoY.TalkFilter.Plugin.Patcher";
    static OsakaTranslator osk = new OsakaTranslator();

    public void Awake()
    {
        Harmony hm = new Harmony(id);

        var org = typeof(DisplayString).GetMethod("get_Text");
        var hook = typeof(LoYOsakaPlugin).GetMethod("get_OsakaText");
        hm.Patch(org, postfix: new HarmonyMethod(hook));

        //Console.Write("------------------------------");
        //Console.Write("TalkFilter Plugin loaded.");
        //Console.Write("------------------------------");
    }

    public static void get_OsakaText(ref string __result)
    {
        __result = osk.translate_talk(__result);
    }
}

}