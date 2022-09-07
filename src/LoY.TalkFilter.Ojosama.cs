using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

#if !TEST_BUILD
using BepInEx;
#endif

using NMeCab;


namespace LoYTalkFilter
{

/* 壱百満天原サロメお嬢様風の口調風変換風クラス
 * 変換ルールはjiro4989お嬢様の「ojosama」より大幅に省力化のうえ拝借いたしましたわ
 * いろいろと手を加えたせいで何がなんだかよくわからなくなっておりましてよ
 * https://github.com/jiro4989/ojosama
 */
class OjosamaTranslator
{
#if TEST_BUILD
    readonly string ipadic = @"dic\ipadic\";
#else
    readonly string ipadic = Path.Combine(Paths.PluginPath, @"LoY.TalkFilter.Plugin\dic\ipadic\");
#endif
    Regex pt, ascii;
    MeCabTagger tg;
    string[] mtype;
    string[] no_prefix;
    List<ContinuousConv> continous;
    List<ConvertRule> rules;

    public string translate_talk(string str)
    {
        //改行はNMeCabが削除してしまうので適当な改行タグに置き換え
        str = str.Replace("\n", "<br>");
        Match m = this.pt.Match(str);
        if(m == Match.Empty)
            return str.Replace("<br>", "\n");
        str = str.Replace(m.Value, this.translate(m.Value));
        return str.Replace("<br>", "\n");
    }

    public string translate(string str)
    {
        List<Token> tokens = this.tokenize(str);
        string result = "";
        int i = 0;
#if DEBUG
        for(; i < tokens.Count; ++i)
            Console.WriteLine($"{i}: {tokens[i]}");
        i = 0;
#endif
        while(i < tokens.Count)
        {
            //英数字はそのまま流す
            //名前に反して全角英数字と一部記号もそのまま流していく
            if(this.ascii.Match(tokens[i].surface) != Match.Empty)
            {
                result += tokens[i].surface;
                ++i;
                continue;
            }
            //名詞+動詞+(助動詞)+終助詞
            else if(i != (i += this.convertSentenceEndingParticle(tokens, i, ref result)))
                continue;
            //連続変換と通常のお嬢様言葉変換を統合？
            else if(i != (i += this.convertContinuousConditions(tokens, i, ref result)))
                continue;
            //変換処理を無視してそのまま出力に書き出す
            else if(i != (i += this.matchExcludeRule(tokens, i, ref result)))
                continue;
            else
            {
                result += tokens[i].surface;
                ++i;
            }
        }
#if DEBUG && !TEST_BUILD
        Console.WriteLine($"{result}");
#endif
        return result;
    }

    /* 入力文字列strをNMeCabでトークンに分割 */
    List<Token> tokenize(string str)
    {
        var node = this.tg.ParseToNode(str);
        List<Token> tokens = new List<Token>();
        do
        {
            Token tk;
            if(node.Feature == null)
                continue;
            if((tk = this.concat_token(ref node)) != null)
                tokens.Add(tk);
            else
                tokens.Add(new Token(node.Surface, node.Feature));
        }
        while((node = node.Next) != null);
        return tokens;
    }

    /* 特定の単語が連続した際に一つの単語として結合する */
    Token concat_token(ref MeCabNode node)
    {
        foreach(ContinuousConv cont in this.continous)
            if(node != (node = cont.match(node)))
                return cont.token;
        return null;
    }

    /* 名詞+動詞+(助動詞)+終助詞の場合の語尾の変換
     * 例：野球しようぜ -> お野球をいたしませんこと
     */
    int convertSentenceEndingParticle(List<Token> tokens, int i, ref string result)
    {
        int c = 0;
        //最低3個は必要
        if(tokens.Count < (i + 3))
            return 0;
        //名詞/サ行変格
        else if(tokens[i].type != PoS.type.NounsGeneral && tokens[i].type != PoS.type.NounsSaDynamic)
            return 0;
        //動詞
        else if(tokens[i+1].type != PoS.type.VerbIndependence)
            return 0;
        //助動詞はあってもなくても良いが、スキップする必要がある
        else if(tokens[i+2].type == PoS.type.AuxiliaryVerb)
        {
            c = 1;
            //長さを再チェックする必要がある
            if(tokens.Count < (i + 4))
                return 0;
        }
        //終助詞
        if(!tokens[i+2+c].cmp_feature("終助詞"))
            return 0;

        result += "お" + tokens[i].surface;
        var end = tokens[i+2+c];
        //語尾を文脈に合わせたものにする
        //しようぜ -> をいたしませんこと
        if("ぜよべ".Contains(end.surface))
            result += this.mtype[0];
        //するか -> をいたしますわ
        else if(end.surface == "か" && end.type == PoS.type.SubParEndParticle)
            result += this.mtype[1];
        //するな -> をしてはいけませんわ
        else if(end.surface == "な")
            result += this.mtype[2];
        //するぞ -> をいたしますわよ
        else if("ぞの".Contains(end.surface))
            result += this.mtype[3];
        return 3 + c;
    }

    /* 変換ルールにマッチしたものを変換する
     * また、ルールにマッチしなかった場合でも接頭辞付与可能である場合は接頭辞を付与
     * 本来ojosamaではここで"壱", "百", "満天", "原", "サロメ"の変換も行っていたが、
     * OjosamaTranslatorではTokenize中にconcat_tokenで変換するようにした
     * ojosamaにおけるconvertContinuousConditionsとconvertを統合したような感じになっている
     */
    int convertContinuousConditions(List<Token> tokens, int i, ref string result)
    {
        string prefix = append_prefix(tokens, i);

        foreach(ConvertRule rule in this.rules)
        {
            if(rule.match(tokens, i))
            {
#if DEBUG
                Console.WriteLine($"\t{rule}");
                for(int j = 0; j < rule.cost; ++j)
                    Console.WriteLine($"\t\t{i+j}: {tokens[i+j]}");
#endif
                result += prefix;
                return rule.conv(tokens, i, ref result);
            }
        }

        if(prefix != "")
        {
            result += prefix + tokens[i].surface;
            return 1;
        }

        return 0;
    }

    /* 変換処理を無視してそのまま書き出す
     * ルールをちゃんとコードに落とすの面倒だったんでそのまま書くことに
     */
    int matchExcludeRule(List<Token> tokens, int i, ref string result)
    {
        if(tokens[i].surface == "カス" && tokens[i].type == PoS.type.SpecificGeneral)
        {
            result += tokens[i].surface;
            return 1;
        }
        else if(tokens[i].type == PoS.type.NounsGeneral && Regex.Match(tokens[i].surface, "^(ー+|～+)$") != Match.Empty)
        {
            result += tokens[i].surface;
            return 1;
        }
        return 0;
    }

    /* 接頭辞「お」を付与する
     * 付与ルール
     * 　・no_prefixに該当単語がない
     * 　・一般名詞か固有名詞である
     * 　・すでに「お/オ/御」が付与されていない/これらの文字から始まる言葉でない
     * 　・次の単語が動詞ではない
     * 　・前の単語が接頭詞,名詞接続でない
     * 　・前の単語が名詞,サ変接続でない
     */
    string append_prefix(List<Token> token_list, int i)
    {
        Token current = token_list[i];

        if(this.no_prefix.Contains(current.surface))
            return "";

        if(!current.cmp_feature("名詞,一般", true) && !current.cmp_feature("名詞,固有名詞", true))
            return "";

        //丁寧語には接頭辞不要
        if(current.surface.StartsWith("お") || current.surface.StartsWith("オ") || current.surface.StartsWith("御"))
            return "";

        //次が動詞なら接頭辞不要
        if(token_list.Count > (i + 1))
        {
            Token next = token_list[i+1];
            if(next.type == PoS.type.VerbIndependence)
                return "";
        }

        if(i != 0)
        {
            Token prev = token_list[i-1];
            //前が接頭詞/名詞接続なら付与しない
            if(prev.type == PoS.type.ConjunctionNoun)
                return "";
            //前が名詞/サ変接続でも付与しない
            if(prev.type == PoS.type.NounsSaDynamic)
                return "";
        }
        return "お";
    }

    public OjosamaTranslator()
    {
        this.pt = new Regex("[「|｢](.|\n)+[」|｣]");
        this.ascii = new Regex(@"[\d|a-z|A-Z|\<|\>|\=|\/|ａ-ｚ|Ａ-Ｚ|\{|\}]+");
        MeCabParam p = new MeCabParam();
        p.DicDir = this.ipadic;
        this.tg = MeCabTagger.Create(p);
        this.mtype = new string[]{
                "をいたしませんこと",
                "をいたしますわ",
                "をしてはいけませんわ",
                "をいたしますわよ"
            };
        this.no_prefix = new string[]{
                "ん", "こいつ", "ベテラン", "代物", "戦い", "大", "小",
                "エルダー", "当て所", "運", "異邦人", "魂", "魔王",
                "臭い", "竜王", "汗", "獣", "神", "たち", "情熱",
                "歌姫", "辺境", "舞台", "瞳", "竜", "姫", "闇",
                "光の騎士"
            };
        //以下2つはいっぱいありすぎたんで別ファイルに移動
        this.continous = RuleContinuousConv.load();
        this.rules = RuleConvertRule.load();
    }
}

}