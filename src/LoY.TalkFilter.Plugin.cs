using System;
using System.IO;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;

using Experience;
using Experience.ScriptEvent;

using LoYUtil;


namespace LoYTalkFilter
{

[BepInPlugin("LoY.TalkFilter.Plugin", "LoY TalkFilter Plug-In", "0.0.1.0")]
//LoYUtilPluginより後にロードすることを明示
[BepInDependency("LoY.Util.Plugin")]
public class LoYTalkFilterPlugin : BaseUnityPlugin
{
    static readonly string id = "LoY.TalkFilter.Plugin.Patcher";
    static OsakaTranslator osk = null;
    static OjosamaTranslator ojs = null;
    static readonly ScriptFlagId ModTalkFilter = (ScriptFlagId)1920;
    static readonly ScriptFlagId TalkFilterOsaka = (ScriptFlagId)1520;
    static readonly ScriptFlagId TalkFilterOjosama = (ScriptFlagId)1521;

    public void Awake()
    {
        Harmony hm = new Harmony(id);

        ConfigEntry<string> ttype = Config.Bind(
                "Selection", "Translation", "無効",
                "変換フィルターの選択。無効/大阪弁/お嬢様。"
            );

        //LoYUtilPluginでロードされるスクリプトからプラグインのロード状態を参照できるようにするためのフラグID
        //大阪弁かお嬢様口調かはswitch文の中で有効化する
        LoYUtilPlugin.mgr.add_flag(ModTalkFilter, true);
        LoYUtilPlugin.mgr.add_flag(TalkFilterOsaka, false);
        LoYUtilPlugin.mgr.add_flag(TalkFilterOjosama, false);

        var tp = typeof(LoYTalkFilterPlugin);
        MethodInfo hook;
        switch(ttype.Value)
        {
            case "無効":
                LoYUtilPlugin.mgr.set_flag(ModTalkFilter, false);
                return;
            case "大阪弁":
                hook = tp.GetMethod("get_OsakaText");
                osk = new OsakaTranslator();
                LoYUtilPlugin.mgr.set_flag(TalkFilterOsaka, true);
                break;
            case "お嬢様":
                hook = tp.GetMethod("get_OjosamaText");
                ojs = new OjosamaTranslator();
                LoYUtilPlugin.mgr.set_flag(TalkFilterOjosama, true);
                break;
            default:
                LoYUtilPlugin.mgr.set_flag(ModTalkFilter, false);
                Console.Write("ttype.Value is not valid.");
                return;
        }
        var org = typeof(DisplayString).GetMethod("get_Text");
        hm.Patch(org, postfix: new HarmonyMethod(hook));
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