using System;
using System.Collections;
using System.Collections.Generic;

namespace LoYTalkFilter
{

/* internal/pos/pos.goより拝借&一部追加 */
class PoS
    {
        public static readonly string[] PronounGeneral = new string[]{"名詞", "代名詞", "一般"};
        public static readonly string[] NounsGeneral = new string[]{"名詞", "一般"};
        public static readonly string[] SpecificGeneral = new string[]{"名詞", "固有名詞", "一般"};
        public static readonly string[] NotIndependenceGeneral = new string[]{"名詞", "非自立", "一般"};
        public static readonly string[] AdnominalAdjective = new string[]{"連体詞"};
        public static readonly string[] AdjectivesSelfSupporting = new string[]{"形容詞", "自立"};
        public static readonly string[] Interjection = new string[]{"感動詞"};
        public static readonly string[] VerbIndependence = new string[]{"動詞", "自立"};
        public static readonly string[] VerbNotIndependence = new string[]{"動詞", "非自立"};
        public static readonly string[] SentenceEndingParticle = new string[]{"助詞", "終助詞"};
        public static readonly string[] SubPostpositionalParticle = new string[]{"助詞", "副助詞"};
        public static readonly string[] AssistantParallelParticle = new string[]{"助詞", "並立助詞"};
        public static readonly string[] SubParEndParticle = new string[]{"助詞", "副助詞／並立助詞／終助詞"};
        public static readonly string[] ConnAssistant = new string[]{"助詞", "接続助詞"};
        public static readonly string[] AuxiliaryVerb = new string[]{"助動詞"};
        public static readonly string[] NounsSaDynamic = new string[]{"名詞", "サ変接続"};
        //句読点を追加
        public static readonly string[] Period = new string[]{"記号", "句点"};
        public static readonly string[] ReadingPoint = new string[]{"記号", "読点"};
        public static readonly string[] ConjunctionNoun = new string[]{"接頭詞", "名詞接続"};
        public enum type
        {
            NULL,
            None,
            PronounGeneral,
            NounsGeneral,
            SpecificGeneral,
            NotIndependenceGeneral,
            AdnominalAdjective,
            AdjectivesSelfSupporting,
            Interjection,
            VerbIndependence,
            VerbNotIndependence,
            SentenceEndingParticle,
            SubPostpositionalParticle,
            AssistantParallelParticle,
            SubParEndParticle,
            ConnAssistant,
            AuxiliaryVerb,
            NounsSaDynamic,
            Punctuation,
            ConjunctionNoun
        };

        /* feature(["名詞", "一般"]など)からtypeを選択して返す */
        public static type get_type(List<string> feat)
        {
            if(_cmp(feat, PronounGeneral))
                return type.PronounGeneral;
            //SpecificGeneralを先に調べないとNounsGeneralに誤解釈される
            else if(_cmp(feat, SpecificGeneral))
                return type.SpecificGeneral;
            else if(_cmp(feat, NounsGeneral))
                return type.NounsGeneral;
            else if(_cmp(feat, NotIndependenceGeneral))
                return type.NotIndependenceGeneral;
            else if(_cmp(feat, AdnominalAdjective))
                return type.AdnominalAdjective;
            else if(_cmp(feat, AdjectivesSelfSupporting))
                return type.AdjectivesSelfSupporting;
            else if(_cmp(feat, Interjection))
                return type.Interjection;
            else if(_cmp(feat, VerbIndependence))
                return type.VerbIndependence;
            else if(_cmp(feat, VerbNotIndependence))
                return type.VerbNotIndependence;
            else if(_cmp(feat, SentenceEndingParticle))
                return type.SentenceEndingParticle;
            else if(_cmp(feat, SubPostpositionalParticle))
                return type.SubPostpositionalParticle;
            else if(_cmp(feat, AssistantParallelParticle))
                return type.AssistantParallelParticle;
            else if(_cmp(feat, SubParEndParticle))
                return type.SubParEndParticle;
            else if(_cmp(feat, ConnAssistant))
                return type.ConnAssistant;
            else if(_cmp(feat, AuxiliaryVerb))
                return type.AuxiliaryVerb;
            else if(_cmp(feat, NounsSaDynamic))
                return type.NounsSaDynamic;
            else if(_cmp(feat, Period))
                return type.Punctuation;
            else if(_cmp(feat, ReadingPoint))
                return type.Punctuation;
            else if(_cmp(feat, ConjunctionNoun))
                return type.ConjunctionNoun;
            else
                return type.None;
        }

        /* typeからデフォルトのfeatureを返す */
        public static string[] get_feature(type t)
        {
            switch(t)
            {
                case type.PronounGeneral:
                    return PoS.PronounGeneral;
                case type.NounsGeneral:
                    return PoS.NounsGeneral;
                case type.SpecificGeneral:
                    return PoS.SpecificGeneral;
                case type.NotIndependenceGeneral:
                    return PoS.NotIndependenceGeneral;
                case type.AdnominalAdjective:
                    return PoS.AdnominalAdjective;
                case type.AdjectivesSelfSupporting:
                    return PoS.AdjectivesSelfSupporting;
                case type.Interjection:
                    return PoS.Interjection;
                case type.VerbIndependence:
                    return PoS.VerbIndependence;
                case type.VerbNotIndependence:
                    return PoS.VerbNotIndependence;
                case type.SentenceEndingParticle:
                    return PoS.SentenceEndingParticle;
                case type.SubPostpositionalParticle:
                    return PoS.SubPostpositionalParticle;
                case type.AssistantParallelParticle:
                    return PoS.AssistantParallelParticle;
                case type.SubParEndParticle:
                    return PoS.SubParEndParticle;
                case type.ConnAssistant:
                    return PoS.ConnAssistant;
                case type.AuxiliaryVerb:
                    return PoS.AuxiliaryVerb;
                case type.NounsSaDynamic:
                    return PoS.NounsSaDynamic;
                case type.Punctuation:
                    //PunctuationはReadingPointとPeriodの二種類があるのだが、まあどっちでもかまわんだろう
                    return PoS.ReadingPoint;
                case type.ConjunctionNoun:
                    return PoS.ConjunctionNoun;
                case type.NULL:
                case type.None:
                default:
                    return new string[]{""};
            }
        }

        static bool _cmp(List<string> feature, string[] pos)
        {
            foreach(var f in pos)
                if(!feature.Contains(f))
                    return false;
            return true;
        }
    }

}