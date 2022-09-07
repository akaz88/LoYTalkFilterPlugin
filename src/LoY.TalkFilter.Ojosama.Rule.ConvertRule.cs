using System;
using System.Collections;
using System.Collections.Generic;


namespace LoYTalkFilter
{

/* OjosamaTranslatorのコンストラクタで使うための変換ルール */
class RuleConvertRule
{
    internal static List<ConvertRule> load()
    {
        return new List<ConvertRule>{
                //LoYTalkFilterPlugin.OjosamaTranslator追加分
                new ConvertRule(
                    "@1ですと",
                    new Token[]{
                        new Token(PoS.type.NounsGeneral),
                        new Token("だ", PoS.type.AuxiliaryVerb),
                        new Token(PoS.type.ConnAssistant)
                    },
                    false, true
                ),
                new ConvertRule(
                    "売れますわ",
                    new List<Token>{
                        new Token("売れる", "動詞,自立,一段,基本形"),
                        new Token(PoS.type.SentenceEndingParticle)
                    },
                    true, true
                ),
                new ConvertRule(
                    "皆様方",
                    new List<Token>{
                        new Token("みんな", PoS.type.PronounGeneral)
                    }
                ),
                //話しておいたわ -> 話しておきましたわ
                new ConvertRule(
                    "おきましたわ",
                    new List<Token>{
                        new Token("おい", PoS.type.VerbNotIndependence),
                        new Token("た", PoS.type.AuxiliaryVerb),
                        new Token("わ", PoS.type.SentenceEndingParticle, true)
                    }
                ),
                new ConvertRule(
                    "課長さん",
                    new List<Token>{
                        new Token("課長", PoS.type.NounsGeneral),
                        new Token("君", PoS.type.PronounGeneral)
                    }
                ),
                new ConvertRule(
                    "部長さん",
                    new List<Token>{
                        new Token("部長", PoS.type.NounsGeneral),
                        new Token("君", PoS.type.PronounGeneral)
                    }
                ),
                new ConvertRule(
                    "そうですわ",
                    new List<Token>{
                        new Token("そう", "名詞,接尾,助動詞語幹"),
                        new Token("よ", PoS.type.SentenceEndingParticle)
                    }
                ),
                new ConvertRule(
                    "ですと",
                    new List<Token>{
                        new Token("だ", PoS.type.AuxiliaryVerb),
                        new Token("と", "助詞,格助詞,引用")
                    },
                    ignore_after: new Token("か", PoS.type.SubParEndParticle)
                ),
                new ConvertRule(
                    "ですけれども",
                    new Token[]{
                        new Token("です", PoS.type.AuxiliaryVerb),
                        new Token("が", PoS.type.ConnAssistant)
                    },
                    true, true,
                    ignore_after:new Token(PoS.type.SubParEndParticle)
                ),new ConvertRule(
                    "おりますわ",
                    new List<Token>{
                        new Token("いる", PoS.type.VerbNotIndependence)
                    },
                    true, true
                ),
                new ConvertRule(
                    "皆様方",
                    new Token[]{
                        new Token("諸君", PoS.type.NounsGeneral)
                    }
                ),
                new ConvertRule(
                    "わたくし達",
                    new Token[]{
                        new Token("我々", PoS.type.NounsGeneral)
                    }
                ),
                new ConvertRule(
                    "くださいまし",
                    new Token[]{
                        new Token("やり", PoS.type.VerbNotIndependence),
                        new Token("なさい", PoS.type.VerbNotIndependence)
                    },
                    false, true
                ),
                new ConvertRule(
                    "もの",
                    new List<Token>{
                        new Token("もん", PoS.type.NounsGeneral)
                    }
                ),
                new ConvertRule(
                    "ですわ",
                    new List<Token>{
                        new Token("で", PoS.type.AuxiliaryVerb),
                        new Token("ね", PoS.type.SentenceEndingParticle)
                    },
                    true, true
                ),
                new ConvertRule(
                    "このような",
                    new List<Token>{
                        new Token("こういう", PoS.type.AdnominalAdjective)
                    }
                ),
                new ConvertRule(
                    "燃やさないで",
                    new List<Token>{
                        new Token("燃やさ", PoS.type.VerbIndependence),
                        new Token("ん", PoS.type.AuxiliaryVerb),
                        new Token("で", PoS.type.ConnAssistant)
                    }
                ),
                new ConvertRule(
                    "くださいまし",
                    new List<Token>{
                        new Token("下さい", PoS.type.VerbNotIndependence),
                        new Token("よ", PoS.type.SentenceEndingParticle)
                    }
                ),
                new ConvertRule(
                    "これは",
                    new List<Token>{
                        new Token("こりゃ", PoS.type.Interjection)
                    }
                ),
                new ConvertRule(
                    "まあ",
                    new List<Token>{
                        new Token("おお", PoS.type.Interjection)
                    },
                    true, true
                ),
                new ConvertRule(
                    "ですの",
                    new List<Token>{
                        new Token("です", PoS.type.AuxiliaryVerb),
                        new Token("かい", PoS.type.SentenceEndingParticle)
                    }
                ),
                new ConvertRule(
                    "ですわね",
                    new List<Token>{
                        new Token("よ", PoS.type.SentenceEndingParticle),
                        new Token("ね", PoS.type.SentenceEndingParticle)
                    }
                ),
                new ConvertRule(
                    "あら",
                    new List<Token>{
                        new Token("ほほ", PoS.type.None),
                        new Token("う", PoS.type.Interjection)
                    },
                    true, true
                ),
                new ConvertRule(
                    "兼ねますわ",
                    new List<Token>{
                        new Token("兼ねる", PoS.type.VerbIndependence),
                        new Token("って", PoS.type.None),
                        new Token("ね", PoS.type.SentenceEndingParticle)
                    }
                ),
                new ConvertRule(
                    "@1ありませんわ",
                    new List<Token>{
                        new Token(PoS.type.AdjectivesSelfSupporting),
                        new Token("ない", PoS.type.AuxiliaryVerb),
                        new Token("わ", PoS.type.SentenceEndingParticle),
                        new Token("ね", PoS.type.SentenceEndingParticle, true)
                    },
                    true, true
                ),
                new ConvertRule(
                    "ございませんわ",
                    new List<Token>{
                        new Token("あり", PoS.type.VerbIndependence),
                        new Token("ませ", PoS.type.AuxiliaryVerb),
                        new Token("ん", PoS.type.AuxiliaryVerb)
                    },
                    true, true
                ),
                new ConvertRule(
                    "おきませんと",
                    new List<Token>{
                        new Token("おか", PoS.type.VerbNotIndependence),
                        new Token("ない", PoS.type.AuxiliaryVerb),
                        new Token("と", PoS.type.ConnAssistant)
                    }
                ),
                new ConvertRule(
                    "たまりませんわ",
                    new List<Token>{
                        new Token("たまら", PoS.type.VerbIndependence),
                        new Token("ん", PoS.type.AuxiliaryVerb)
                    },
                    false, true
                ),
                new ConvertRule(
                    "ないで",
                    new List<Token>{
                        new Token("んで", PoS.type.ConnAssistant)
                    }
                ),new ConvertRule(
                    "たのかしら",
                    new List<Token>{
                        new Token("た", PoS.type.AuxiliaryVerb),
                        new Token("の", PoS.type.SentenceEndingParticle)
                    }
                ),
                new ConvertRule(
                    "ありませんわ",
                    new List<Token>{
                        new Token("ない", PoS.type.AdjectivesSelfSupporting),
                        new Token("わ", PoS.type.SentenceEndingParticle)
                    },
                    true, true
                ),
                new ConvertRule(
                    "くださいまし",
                    new List<Token>{
                        new Token("ちょうだい", PoS.type.None)
                    }
                ),
                new ConvertRule(
                    "ませんの",
                    new List<Token>{
                        new Token("ませ", PoS.type.AuxiliaryVerb),
                        new Token("ん", PoS.type.AuxiliaryVerb)
                    }
                ),
                new ConvertRule(
                    "よろしければ",
                    new List<Token>{
                        new Token("よけれ", PoS.type.AdjectivesSelfSupporting),
                        new Token("ば", PoS.type.ConnAssistant)
                    }
                ),
                new ConvertRule(
                    "ますわ",
                    new List<Token>{
                        new Token("ましょ", PoS.type.AuxiliaryVerb),
                        new Token("う", PoS.type.AuxiliaryVerb)
                    },
                    true, true
                ),
                new ConvertRule(
                    "挑まれた",
                    new List<Token>{
                        new Token("挑ん", PoS.type.VerbIndependence),
                        new Token("だ", PoS.type.AuxiliaryVerb)
                    }
                ),
                new ConvertRule(
                    "くださいましたの？",
                    new List<Token>{
                        new Token("くれ", PoS.type.VerbNotIndependence),
                        new Token("た", PoS.type.AuxiliaryVerb),
                        new Token("の", PoS.type.NounsGeneral),
                        new Token("です", PoS.type.AuxiliaryVerb),
                        new Token("か", PoS.type.SubParEndParticle),
                        new Token("？", PoS.type.None)
                    }
                ),
                new ConvertRule(
                    "ですが",
                    new List<Token>{
                        new Token("でも", PoS.type.None)
                    }
                ),
                new ConvertRule(
                    "ですわね",
                    new List<Token>{
                        new Token("です", PoS.type.AuxiliaryVerb),
                        new Token("よ", PoS.type.SentenceEndingParticle, true),
                        new Token("ね", PoS.type.SentenceEndingParticle)
                    }
                ),
                new ConvertRule(
                    "いただきました",
                    new List<Token>{
                        new Token("もらい", PoS.type.VerbNotIndependence),
                        new Token("まし", PoS.type.AuxiliaryVerb)
                    }
                ),
                new ConvertRule(
                    "ですとか",
                    new List<Token>{
                        new Token("だ", PoS.type.AuxiliaryVerb),
                        new Token("と", PoS.type.None),
                        new Token("か", PoS.type.SubParEndParticle)
                    }
                ),
                new ConvertRule(
                    "くださいません",
                    new List<Token>{
                        new Token("くれ", PoS.type.VerbNotIndependence),
                        new Token("ない", PoS.type.AuxiliaryVerb)
                    }
                ),
                new ConvertRule(
                    "ですわ",
                    new Token[]{
                        new Token("よう", "名詞,非自立,助動詞語幹")
                    }
                ),
                new ConvertRule(
                    "パパ上",
                    new Token[]{
                        new Token("おとうさま", PoS.type.NounsGeneral)
                    },
                    ignore_after:new Token("上")
                ),
                /*
                new ConvertRule(
                    "",
                    new Token[]{
                        new Token("", PoS.type.)
                    }
                ),
                */
                //LoYTalkFilterPlugin.OjosamaTranslator追加分ここまで
                new ConvertRule(
                    "いたしますわ",
                    new List<Token>{
                        new Token("し", "動詞,自立"),
                        new Token("ます", "助動詞")
                    },
                    true, true
                ),
                new ConvertRule(
                    "ですので",
                    new Token[]{
                        new Token("だ", "助動詞"),
                        new Token("から", "助詞,接続助詞")
                    },
                    false, true
                ),
                new ConvertRule(
                    "なんですの",
                    new Token[]{
                        new Token("な", "助動詞"),
                        new Token("ん", "名詞,非自立,一般"),
                        new Token("だ", "助動詞")
                    },
                    false, true
                ),
                new ConvertRule(
                    "ですわ",
                    new Token[]{
                        new Token("だ", "助動詞"),
                        new Token("よ", "助詞,終助詞")
                    },
                    false, true
                ),
                new ConvertRule(
                    "なんですの",
                    new Token[]{
                        new Token("なん", PoS.type.PronounGeneral),
                        new Token("じゃ", PoS.type.SubPostpositionalParticle)
                    },
                    false, true
                ),
                new ConvertRule(
                    "なんですの",
                    new Token[]{
                        new Token("なん", PoS.type.PronounGeneral),
                        new Token("だ", PoS.type.AuxiliaryVerb)
                    },
                    false, true
                ),
                new ConvertRule(
                    "なんですの",
                    new Token[]{
                        new Token("なん", PoS.type.PronounGeneral),
                        new Token("や", PoS.type.AssistantParallelParticle)
                    },
                    false, true
                ),
                new ConvertRule(
                    "@1ですの",
                    new Token[]{
                        new Token(PoS.type.NounsGeneral),
                        new Token("じゃ", PoS.type.AuxiliaryVerb)
                    },
                    false, true
                ),
                new ConvertRule(
                    "@1ですの",
                    new Token[]{
                        new Token(PoS.type.NounsGeneral),
                        new Token("だ", PoS.type.AuxiliaryVerb)
                    },
                    false, true,
                    //「あたしのルートだと」
                    ignore_after: new Token("と")
                ),
                new ConvertRule(
                    "@1ですの",
                    new Token[]{
                        new Token(PoS.type.NounsGeneral),
                        new Token("や", PoS.type.AuxiliaryVerb)
                    },
                    false, true
                ),
                new ConvertRule(
                    "@1ですの",
                    new Token[]{
                        new Token(PoS.type.PronounGeneral),
                        new Token("じゃ", PoS.type.AuxiliaryVerb)
                    },
                    false, true
                ),
                new ConvertRule(
                    "@1ですの",
                    new Token[]{
                        new Token(PoS.type.PronounGeneral),
                        new Token("だ", PoS.type.AuxiliaryVerb)
                    },
                    false, true,
                    //「あたしのルートだと」
                    ignore_after: new Token(PoS.type.ConnAssistant)
                ),
                new ConvertRule(
                    "@1ですの",
                    new Token[]{
                        new Token(PoS.type.PronounGeneral),
                        new Token("や", PoS.type.AuxiliaryVerb)
                    },
                    false, true
                ),
                new ConvertRule(
                    "@1をいたしましたわ",
                    new Token[]{
                        new Token(PoS.type.NounsGeneral),
                        new Token("し", PoS.type.VerbIndependence),
                        new Token("た", PoS.type.AuxiliaryVerb),
                        new Token(PoS.type.SentenceEndingParticle)
                    },
                    false, true
                ),
                new ConvertRule(
                    "@1をいたしましたわ",
                    new Token[]{
                        new Token(PoS.type.NounsGeneral),
                        new Token("やっ", PoS.type.VerbIndependence),
                        new Token("た", PoS.type.AuxiliaryVerb),
                        new Token(PoS.type.SentenceEndingParticle)
                    },
                    false, true
                ),
                //一人称
                new ConvertRule(
                    "私",
                    new Token[]{
                        new Token("俺", PoS.type.PronounGeneral)
                    }
                ),
                new ConvertRule(
                    "ワタクシ",
                    new Token[]{
                        new Token("オレ", PoS.type.PronounGeneral)
                    }
                ),
                new ConvertRule(
                    "わたくし",
                    new Token[]{
                        new Token("おれ", PoS.type.PronounGeneral)
                    }
                ),
                new ConvertRule(
                    "私",
                    new Token[]{
                        new Token("僕", PoS.type.PronounGeneral)
                    }
                ),
                new ConvertRule(
                    "ワタクシ",
                    new Token[]{
                        new Token("ボク", PoS.type.PronounGeneral)
                    }
                ),
                new ConvertRule(
                    "わたくし",
                    new Token[]{
                        new Token("ぼく", PoS.type.PronounGeneral)
                    }
                ),
                new ConvertRule(
                    "わたくし",
                    new Token[]{
                        new Token("あたし", PoS.type.PronounGeneral)
                    }
                ),
                new ConvertRule(
                    "わたくし",
                    new Token[]{
                        new Token("わたし", PoS.type.PronounGeneral)
                    }
                ),
                //二人称
                new ConvertRule(
                    "貴方",
                    new Token[]{
                        new Token("あなた", PoS.type.PronounGeneral)
                    }
                ),
                new ConvertRule(
                    "貴方",
                    new Token[]{
                        new Token("あんた", PoS.type.PronounGeneral)
                    }
                ),
                new ConvertRule(
                    "貴方",
                    new Token[]{
                        new Token("おまえ", PoS.type.PronounGeneral)
                    }
                ),
                new ConvertRule(
                    "貴方",
                    new Token[]{
                        new Token("お前", PoS.type.PronounGeneral)
                    }
                ),
                new ConvertRule(
                    "貴方",
                    new Token[]{
                        new Token("てめぇ", PoS.type.PronounGeneral)
                    }
                ),
                new ConvertRule(
                    "貴方",
                    new Token[]{
                        new Token("てめえ", PoS.type.PronounGeneral)
                    }
                ),
                new ConvertRule(
                    "貴方",
                    new Token[]{
                        new Token("貴様", PoS.type.PronounGeneral)
                    }
                ),
                new ConvertRule(
                    "貴方",
                    new Token[]{
                        new Token("君", PoS.type.PronounGeneral)
                    }
                ),
                //三人称
                new ConvertRule(
                    "皆様方",
                    new Token[]{
                        new Token("皆", PoS.type.PronounGeneral)
                    }
                ),
                new ConvertRule(
                    "皆様方",
                    new Token[]{
                        new Token("皆様", PoS.type.PronounGeneral)
                    }
                ),
                new ConvertRule(
                    "パパ上",
                    new Token[]{
                        new Token("パパ", PoS.type.NounsGeneral)
                    },
                    ignore_after:new Token("上")
                ),
                new ConvertRule(
                    "ママ上",
                    new Token[]{
                        new Token("ママ", PoS.type.NounsGeneral)
                    },
                    ignore_after:new Token("上")
                ),
                //こそあど言葉
                new ConvertRule(
                    "こちら",
                    new Token[]{
                        new Token("これ", PoS.type.PronounGeneral)
                    }
                ),
                new ConvertRule(
                    "そちら",
                    new Token[]{
                        new Token("それ", PoS.type.PronounGeneral)
                    }
                ),
                new ConvertRule(
                    "あちら",
                    new Token[]{
                        new Token("あれ", PoS.type.PronounGeneral)
                    }
                ),
                new ConvertRule(
                    "どちら",
                    new Token[]{
                        new Token("どれ", PoS.type.PronounGeneral)
                    }
                ),
                new ConvertRule(
                    "こちら",
                    new Token[]{
                        new Token("ここ", PoS.type.PronounGeneral)
                    }
                ),
                new ConvertRule(
                    "そちら",
                    new Token[]{
                        new Token("そこ", PoS.type.PronounGeneral)
                    }
                ),
                new ConvertRule(
                    "あちら",
                    new Token[]{
                        new Token("あそこ", PoS.type.PronounGeneral)
                    }
                ),
                new ConvertRule(
                    "どちら",
                    new Token[]{
                        new Token("どこ", PoS.type.PronounGeneral)
                    }
                ),
                new ConvertRule(
                    "こちらの",
                    new Token[]{
                        new Token("この", PoS.type.AdnominalAdjective)
                    }
                ),
                new ConvertRule(
                    "そちらの",
                    new Token[]{
                        new Token("その", PoS.type.AdnominalAdjective)
                    }
                ),
                new ConvertRule(
                    "あちらの",
                    new Token[]{
                        new Token("あの", PoS.type.AdnominalAdjective)
                    }
                ),
                new ConvertRule(
                    "どちらの",
                    new Token[]{
                        new Token("どの", PoS.type.AdnominalAdjective)
                    }
                ),
                new ConvertRule(
                    "このような",
                    new Token[]{
                        new Token("こんな", PoS.type.AdnominalAdjective)
                    }
                ),
                new ConvertRule(
                    "そのような",
                    new Token[]{
                        new Token("そんな", PoS.type.AdnominalAdjective)
                    }
                ),
                new ConvertRule(
                    "あのような",
                    new Token[]{
                        new Token("あんな", PoS.type.AdnominalAdjective)
                    }
                ),
                new ConvertRule(
                    "どのような",
                    new Token[]{
                        new Token("どんな", PoS.type.AdnominalAdjective)
                    }
                ),
                //その他一般？
                new ConvertRule(
                    "もの",
                    new Token[]{
                        new Token("もん", PoS.type.NotIndependenceGeneral)
                    }
                ),
                new ConvertRule(
                    "ですわ",
                    new Token[]{
                        new Token("です", PoS.type.AuxiliaryVerb)
                    },
                    true, true,
                    ignore_after:new Token(PoS.type.SubParEndParticle)
                ),
                new ConvertRule(
                    "ですわ",
                    new Token[]{
                        new Token("だ", PoS.type.AuxiliaryVerb)
                    },
                    true, true,
                    ignore_after:new Token(PoS.type.SubParEndParticle)
                ),
                new ConvertRule(
                    "いたしますわ",
                    new Token[]{
                        new Token("する", PoS.type.VerbIndependence)
                    },
                    true, true,
                    before_sep: true
                ),
                new ConvertRule(
                    "なりますわ",
                    new Token[]{
                        new Token("なる", PoS.type.VerbIndependence)
                    },
                    true, true,
                    before_sep: true
                ),
                new ConvertRule(
                    "あります",
                    new Token[]{
                        new Token("ある", PoS.type.VerbIndependence)
                    }
                ),
                new ConvertRule(
                    "では",
                    new Token[]{
                        new Token("じゃ", PoS.type.SubPostpositionalParticle)
                    }
                ),
                new ConvertRule(
                    "の",
                    new Token[]{
                        new Token("か", PoS.type.SubParEndParticle)
                    },
                    //AorBのときは変換しない
                    ignore_before:new Token("なる", PoS.type.VerbIndependence)
                ),
                new ConvertRule(
                    "ですわ",
                    new Token[]{
                        new Token("わ", PoS.type.SentenceEndingParticle)
                    },
                    true, true,
                    ignore_before: new Token("", "記号,空白")
                ),
                new ConvertRule(
                    "ね",
                    new Token[]{
                        new Token("な", PoS.type.SentenceEndingParticle)
                    }
                ),
                new ConvertRule(
                    "",
                    new Token[]{
                        new Token("さ", PoS.type.SentenceEndingParticle)
                    }
                ),
                new ConvertRule(
                    "ので",
                    new Token[]{
                        new Token("から", PoS.type.ConnAssistant)
                    }
                ),
                new ConvertRule(
                    "けれど",
                    new Token[]{
                        new Token("けど", PoS.type.ConnAssistant)
                    }
                ),
                new ConvertRule(
                    "ですし",
                    new Token[]{
                        new Token("し", PoS.type.ConnAssistant)
                    }
                ),
                new ConvertRule(
                    "おりまし",
                    new Token[]{
                        new Token("い", PoS.type.VerbNotIndependence, true),
                        new Token("まし", PoS.type.AuxiliaryVerb)
                    },
                    ignore_before: new Token("", "動詞")
                ),
                new ConvertRule(
                    "ますわ",
                    new Token[]{
                        new Token("ます", PoS.type.AuxiliaryVerb)
                    },
                    true, true,
                    //「～だと思いますが」が「ますわが」にされるのを防ぐ
                    ignore_after: new Token("が", PoS.type.ConnAssistant)
                ),
                new ConvertRule(
                    "たわ",
                    new Token[]{
                        new Token("た", PoS.type.AuxiliaryVerb)
                    },
                    true, true,
                    before_sep: true,
                    ignore_before: new Token("", "記号,空白")
                ),
                new ConvertRule(
                    "でしょう",
                    new Token[]{
                        new Token("だろ", PoS.type.AuxiliaryVerb)
                    }
                ),
                new ConvertRule(
                    "ありません",
                    new Token[]{
                        new Token("ない", PoS.type.AuxiliaryVerb)
                    },
                    ignore_before: new Token(PoS.type.VerbIndependence)
                ),
                new ConvertRule(
                    "くださいまし",
                    new Token[]{
                        new Token("ください", PoS.type.VerbNotIndependence)
                    },
                    false, true
                ),
                new ConvertRule(
                    "くださいまし",
                    new Token[]{
                        new Token("くれ", PoS.type.VerbNotIndependence)
                    },
                    false, true
                ),
                new ConvertRule(
                    "ありがとうございますわ",
                    new Token[]{
                        new Token("ありがとう", PoS.type.Interjection)
                    }
                ),
                new ConvertRule(
                    "それでは",
                    new Token[]{
                        new Token("じゃぁ", PoS.type.Interjection)
                    }
                ),
                new ConvertRule(
                    "それでは",
                    new Token[]{
                        new Token("じゃあ", PoS.type.Interjection)
                    }
                ),
                new ConvertRule(
                    "くれます",
                    new Token[]{
                        new Token("くれる", PoS.type.VerbNotIndependence)
                    }
                ),
                new ConvertRule(
                    "きったねぇ",
                    new Token[]{
                        new Token("汚い", PoS.type.AdjectivesSelfSupporting)
                    }
                ),
                new ConvertRule(
                    "きったねぇ",
                    new Token[]{
                        new Token("きたない", PoS.type.AdjectivesSelfSupporting)
                    }
                ),
                new ConvertRule(
                    "くっせぇ",
                    new Token[]{
                        new Token("臭い", PoS.type.AdjectivesSelfSupporting)
                    }
                ),
                new ConvertRule(
                    "くっせぇ",
                    new Token[]{
                        new Token("くさい", PoS.type.AdjectivesSelfSupporting)
                    }
                ),
                new ConvertRule(
                    "おほ",
                    new Token[]{
                        new Token("うふ", PoS.type.Interjection)
                    }
                ),
                new ConvertRule(
                    "おほほ",
                    new Token[]{
                        new Token("うふふ", PoS.type.Interjection)
                    }
                ),
                new ConvertRule(
                    "お",
                    new Token[]{
                        new Token("う", PoS.type.Interjection)
                    }
                ),
                new ConvertRule(
                    "ほほほ",
                    new Token[]{
                        new Token("ふふふ", PoS.type.Interjection)
                    }
                ),
                new ConvertRule(
                    "@1ですわ",
                    new Token[]{
                        new Token(PoS.type.AdjectivesSelfSupporting)
                    },
                    true, true,
                    before_sep: true
                ),
                /*
                new ConvertRule(
                    "",
                    new Token[]{
                        new Token("", PoS.type.)
                    }
                ),
                */
            };
    }
}

}