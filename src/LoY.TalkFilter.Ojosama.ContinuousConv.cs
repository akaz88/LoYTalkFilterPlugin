using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using NMeCab;


namespace LoYTalkFilterPlugin
{

/* 特定の単語が連続したら一つの単語として結合するための変換ルール
 * ex: "壱", "百", "満天", "原", "サロメ" -> 壱百満天原サロメ
 */
class ContinuousConv
    {
        //string to;
            //結合後の単語
        //PoS.type type;
            //結合後の単語のタイプ
        string[] pattern;
            //連続パターン
        public Token token;
            //結合語のトークン

        /* 結合後の単語の品詞を指定するパターン */
        public ContinuousConv(string feature, string[] pattern)
        {
            List<string> f = Token.feature2list(feature);
            this.pattern = pattern;
            this.token = new Token(
                    String.Join("", pattern),
                    f,
                    PoS.get_type(f)
                );
        }

        /* 結合後のタイプだけを指定するパターン */
        public ContinuousConv(PoS.type type, string[] pattern)
        {
            this.pattern = pattern;
            this.token = new Token(
                    String.Join("", pattern),
                    PoS.get_feature(type).ToList(),
                    type
                );
        }

        public MeCabNode match(MeCabNode node)
        {
            MeCabNode p = node;
            for(int i = 0; pattern.Length > i; ++i)
            {
                if(p == null)
                    return node;
                if(p.Surface != this.pattern[i])
                    return node;
                p = p.Next;
            }
            return p.Prev;
        }

        public override string ToString()
        {
            return $"{this.token}({String.Join(",", this.pattern)})";
        }
    }

}