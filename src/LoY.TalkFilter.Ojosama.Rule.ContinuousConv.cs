using System;
using System.Collections;
using System.Collections.Generic;


namespace LoYTalkFilterPlugin
{

class RuleContinuousConv
{
    internal static List<ContinuousConv> load()
    {
        return new List<ContinuousConv>{
                new ContinuousConv(
                    PoS.type.SpecificGeneral,
                    new string[]{"壱", "百", "満天", "原", "サロメ"}
                ),
                new ContinuousConv(
                    PoS.type.SpecificGeneral,
                    new string[]{"壱", "百", "満天", "原"}
                ),
                new ContinuousConv(
                    PoS.type.SpecificGeneral,
                    new string[]{"壱", "百", "満点"}
                ),
                new ContinuousConv(
                    PoS.type.SpecificGeneral,
                    new string[]{"『", "なめ", "ん", "な", "よ", "』"}
                ),
                new ContinuousConv(
                    PoS.type.NounsGeneral,
                    new string[]{"探", "行", "士"}
                ),
                new ContinuousConv(
                    PoS.type.SpecificGeneral,
                    new string[]{"『", "霊", "樹", "』"}
                ),
                new ContinuousConv(
                    PoS.type.SpecificGeneral,
                    new string[]{"霊", "樹"}
                ),
                new ContinuousConv(
                    PoS.type.SpecificGeneral,
                    new string[]{"『", "融合", "炉", "』"}
                ),
                new ContinuousConv(
                    PoS.type.SpecificGeneral,
                    new string[]{"融合", "炉"}
                ),
                new ContinuousConv(
                    PoS.type.NounsGeneral,
                    new string[]{"花", "石"}
                ),
                new ContinuousConv(
                    PoS.type.NounsGeneral,
                    new string[]{"黄泉", "技術"}
                ),
                new ContinuousConv(
                    PoS.type.SpecificGeneral,
                    new string[]{"異人", "子宮"}
                ),
                new ContinuousConv(
                    PoS.type.SpecificGeneral,
                    new string[]{"邦人", "子宮"}
                ),
                new ContinuousConv(
                    PoS.type.SpecificGeneral,
                    new string[]{"黄泉", "子宮"}
                ),
                new ContinuousConv(
                    PoS.type.SpecificGeneral,
                    new string[]{"黄泉", "区"}
                ),
                new ContinuousConv(
                    PoS.type.SpecificGeneral,
                    new string[]{"黄泉", "公社"}
                ),
                new ContinuousConv(
                    PoS.type.SpecificGeneral,
                    new string[]{"黄泉", "駅前"}
                ),
                new ContinuousConv(
                    PoS.type.NounsGeneral,
                    new string[]{"黄泉", "族"}
                ),
                new ContinuousConv(
                    PoS.type.SpecificGeneral,
                    new string[]{"黄泉", "王", "の", "匣"}
                ),
                new ContinuousConv(
                    PoS.type.SpecificGeneral,
                    new string[]{"大", "聖堂"}
                ),
                new ContinuousConv(
                    PoS.type.SpecificGeneral,
                    new string[]{"月", "望", "台"}
                ),
                new ContinuousConv(
                    PoS.type.SpecificGeneral,
                    new string[]{"臥竜", "の", "森"}
                ),
                new ContinuousConv(
                    PoS.type.SpecificGeneral,
                    new string[]{"死", "星", "砦"}
                ),
                new ContinuousConv(
                    PoS.type.SpecificGeneral,
                    new string[]{"ルキ", "の", "森"}
                ),
                new ContinuousConv(
                    PoS.type.SpecificGeneral,
                    new string[]{"高", "純度", "アルゲン"}
                ),
                new ContinuousConv(
                    PoS.type.SpecificGeneral,
                    new string[]{"超", "アルゲン"}
                ),
                new ContinuousConv(
                    PoS.type.SpecificGeneral,
                    new string[]{"特異", "アルゲン"}
                ),
                new ContinuousConv(
                    PoS.type.SpecificGeneral,
                    new string[]{"神", "の", "子"}
                ),
                new ContinuousConv(
                    PoS.type.NounsGeneral,
                    new string[]{"『", "これ", "く", "た", "ー", "』"}
                ),
                new ContinuousConv(
                    PoS.type.SpecificGeneral,
                    new string[]{"『", "ルキ", "』"}
                ),
                new ContinuousConv(
                    PoS.type.SpecificGeneral,
                    new string[]{"『", "久世戸", "』"}
                ),
                new ContinuousConv(
                    PoS.type.SpecificGeneral,
                    new string[]{"『", "久世戸", "様", "』"}
                ),
                new ContinuousConv(
                    PoS.type.SpecificGeneral,
                    new string[]{"看守", "長"}
                ),
                new ContinuousConv(
                    PoS.type.SpecificGeneral,
                    new string[]{"狂", "王"}
                ),
                new ContinuousConv(
                    PoS.type.SpecificGeneral,
                    new string[]{"灰", "十", "字"}
                ),
                new ContinuousConv(
                    PoS.type.None,
                    new string[]{"ごみ", "クズ"}
                ),
                new ContinuousConv(
                    "副詞",
                    new string[]{"わり", "と"}
                ),
                new ContinuousConv(
                    PoS.type.SpecificGeneral,
                    new string[]{"極", "夜", "の", "地"}
                ),
                new ContinuousConv(
                    PoS.type.SpecificGeneral,
                    new string[]{"嵐", "の", "塔"}
                ),
                new ContinuousConv(
                    PoS.type.SpecificGeneral,
                    new string[]{"異邦", "人"}
                ),
                new ContinuousConv(
                    PoS.type.SpecificGeneral,
                    new string[]{"精霊", "神"}
                ),
                new ContinuousConv(
                    PoS.type.NounsGeneral,
                    new string[]{"光", "の", "騎士"}
                ),
                new ContinuousConv(
                    PoS.type.NounsGeneral,
                    new string[]{"礼儀", "作法"}
                ),
                /*
                new ContinuousConv(
                    PoS.type.,
                    new string[]{""}
                ),
                */
            };
    }
}

}