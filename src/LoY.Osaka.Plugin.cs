using System;
//using System.IO;
using System.Reflection;
using BepInEx;
using HarmonyLib;

using Experience;

//削除キー：loyosaka
//https://dotup.org/uploda/dotup.org2853958.zip.html
namespace LoYOsakaPlugin
{

[BepInPlugin("LoY.Osaka.Plugin", "LoY Osaka-Ben Plug-In", "0.0.0.1")]
public class LoYOsakaPlugin : BaseUnityPlugin
{
    static readonly string id = "LoY.Osaka.Plugin.Patcher";
    static OsakaTranslator osk = new OsakaTranslator();

    public void Awake()
    {
        Harmony hm = new Harmony(id);

        var org = typeof(DisplayString).GetMethod("get_Text");
        var hook = typeof(LoYOsakaPlugin).GetMethod("get_OsakaText");
        hm.Patch(org, postfix: new HarmonyMethod(hook));

        //Console.Write("------------------------------");
        //Console.Write("Osaka Plugin loaded.");
        //Console.Write("------------------------------");
    }

    public static void get_OsakaText(ref string __result)
    {
        __result = osk.translate_talk(__result);
    }
}

}