using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace LoYTalkFilterPlugin
{

/* 変換ルール
 * 変換ルールにマッチしたものを変換するための変換メソッドも備える
 */
class ConvertRule
    {
        public string to;
            //変換後文字列
        public List<Token> tokens;
            //変換ルール
        public int cost;
            //変換に何個のトークンを使用するか
            //トークンのignorableの使用状況により変化するため、matchで毎回変更される
        public bool append_ln;
            //"～"を追加するか
        public bool punc2exclamation;
            //直後の句読点を"！"に変換するか
        public Token ignore_before;
            //直前のTokenにこれが来たらこのルールは無視
        public Token ignore_after;
            //直後のTokenにこれが来たらこのルールは無視
        public bool before_sep;
            //文の区切り（直後に句読点が来るか、なにもない）場合のみ変換を実施する
        public Regex m = new Regex(@"[\!|\?|！|？]+(｣|」)?");

        //ConvertRule("to", new Token[]{...}, ...)
        public ConvertRule(string to, Token[] tokens, bool append_ln=false, bool punc2exclamation=false, Token ignore_before=null, Token ignore_after=null, bool before_sep=false)
            :this(to, tokens.ToList(), append_ln, punc2exclamation, ignore_before, ignore_after, before_sep)
        {;}

        //ConvertRule("to", new List<Token>{...}, ...)
        public ConvertRule(string to, List<Token> tokens, bool append_ln=false, bool punc2exclamation=false, Token ignore_before=null, Token ignore_after=null, bool before_sep=false)
        {
            this.to = to;
            this.tokens = tokens;
            this.append_ln = append_ln;
            this.punc2exclamation = punc2exclamation;
            this.ignore_before = ignore_before;
            this.ignore_after = ignore_after;
            this.before_sep = before_sep;
        }

        /* トークンのパターンが変換ルールに合うかどうかのチェック
         * また、変換の際に何個のトークンを消費するかもここで決める
         */
        public bool match(List<Token> token_list, int cnt)
        {
            this.cost = this.tokens.Count;
            //このトークンが頭の場合は無視
            if(this.ignore_before != null && cnt != 0)
                if(this.ignore_before.match(token_list[cnt-1]))
                    return false;
            for(int j = 0; this.tokens.Count > j; ++j)
            {
                var t = this.tokens[j];
                //あってもなくてもいいやつはマッチしたらカウンタを進め、しなかったらカウンタを動かさない（++jされるので代わりに--iする）
#pragma warning disable CS0642
                if(t.ignorable)
                    if(t.match(token_list[cnt + j]))
                        ;
                    else
                    {
                        //バグりそうな気がする
                        --cnt;
                        this.cost -= 1;
                    }
#pragma warning restore CS0642
                //通常はマッチしなかったら失敗
                else if(!t.match(token_list[cnt + j]))
                    return false;
            }
            //次にこのトークンが来るなら無視
            /*Console.WriteLine(this);
            Console.WriteLine(this.ignore_after);
            Console.WriteLine($"{this.ignore_after != null}, {token_list.Count >= (cnt + this.cost)}");
            Console.WriteLine($"");*/
            if(this.ignore_after != null)
                if(token_list.Count >= (cnt + this.cost))
                    if(this.ignore_after.match(token_list[cnt + this.cost]))
                        return false;
            //次に文章の区切りが来る場合のみ変換を実行
            //Console.WriteLine($"{token_list.Count >= (cnt + this.cost)}");
            if(this.before_sep)
                if(token_list.Count >= (cnt + this.cost))
                    if(!token_list[cnt + this.cost].is_sep())
                        return false;
                    //if(token_list[cnt + this.cost].type != PoS.type.Punctuation)
                        //return false;
            //Console.WriteLine($"matched: {this} / {token_list[cnt]}");
            return true;
        }

        /* 連続したトークンがマッチした場合の変換
         * このとき文章が句読点で終わっていれば！を、！/？で終わっていれば～！？を付与
         * NMeCabでの形態素解析ではなぜか！と」が分離されずに！」となってしまう？？
         */
        public int conv(List<Token> token_list, int cnt, ref string result)
        {
            //接頭辞「お」を付ける
            //string to = OjosamaTranslator.append_prefix(token_list, cnt) + this.to;
            string to = this.to;
            if(to.Contains("@1"))
                to = Regex.Replace(to, "@1", token_list[cnt].surface);
            //cnt += this.cost - 1;
            /*string t = "";
            for(int i = 0; this.cost > i; ++i)
                t += ", " + token_list[i + cnt].surface;
            Console.WriteLine($"[{cnt}:{cnt+this.cost}]{t} -> {to}");*/
            //Console.WriteLine($"{cnt}, {token_list.Count}, {this.cost}, {token_list[cnt].surface}, {this.to}, {this.punc2exclamation}, {this.append_ln}");
            //Console.WriteLine((token_list.Count > (cnt+1)));
            //Console.WriteLine(token_list[cnt+1].type == PoS.type.Punctuation);
            //句読点を検出したらそれを！に置き換え
            if(this.punc2exclamation && token_list.Count > (cnt+1) && token_list[cnt+1].type == PoS.type.Punctuation)
            {
                //「句点と～が同時に発生することは無いので早期リターンで良い」とのこと
                result += to + "！";
                //return this.tokens.Count + 1;
                return this.cost + 1;
            }
            //文末に～！？を付ける
            //Console.WriteLine($"{token_list[cnt+this.cost].surface}");
            if(this.append_ln && token_list.Count > (cnt+this.cost) && this.m.Match(token_list[cnt+this.cost].surface) != Match.Empty)
            {
                //cnt += this.tokens.Count - 1;
                //return this.append_long_note(token_list, ref cnt, to);
                int p = cnt + this.cost;
                while(token_list.Count > (p + 1) && this.m.Match(token_list[p+1].surface) != Match.Empty)
                    ++p;
                result += to + this.append_long_note(token_list, p);
                //return this.tokens.Count + (p - cnt);
                return this.cost + (p - cnt);
            }
            //Console.WriteLine($"{cnt}, {this.tokens.Count}, {this.to}");
            //cnt += this.tokens.Count - 1;
            //return to;
            result += to;
            //return this.tokens.Count;
            return this.cost;
        }

        /* ！/？で文章が終わっていたら～！？に置き換える
         * この際、！と？のどちらを使うかはランダムで決める
         * ～と！/？の数は1～3個でランダム
         */
        string append_long_note(List<Token> token_list, int cnt)
        {
            /*do
                ++cnt;
            while(token_list.Count > cnt && this.m.Match(token_list[cnt].surface) != Match.Empty);*/

            char t = char.Parse(token_list[cnt].surface.Substring(0, 1));
            //Console.WriteLine($"t: {t}");
            //トークンが"!」"のときは最後の鉤括弧を戻してやる
            //たぶん辞書かNMeCabが悪いのかがうまく分割できていないときがある
            string end = "";
            string surface = token_list[cnt].surface;
            if(surface.EndsWith("」") || surface.EndsWith("｣"))
                end = surface.Substring(surface.Length - 1, 1);
            //Console.WriteLine($"{surface}: {end}");
            Random rnd = new Random();
            return new string('～', rnd.Next(1,4)) + new string(t, rnd.Next(1,4)) + end;
        }

        public override string ToString()
        {
            string s = "";
            foreach(var tk in this.tokens)
                s += tk.surface + ", ";
            return $"{s.Substring(0, s.Length - 2)} -> {this.to}";
        }
    }

}