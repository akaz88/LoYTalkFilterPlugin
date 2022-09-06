using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LoYTalkFilterPlugin
{

class Token
{
    public string surface;
    public List<string> feature;
    public PoS.type type;
    public bool ignorable;

    public Token(PoS.type type, bool ignorable=false)
        :this("", null, type, ignorable)
    {;}

    public Token(string surface, PoS.type type=PoS.type.NULL, bool ignorable=false)
        :this(surface, null, type, ignorable)
    {;}

    public Token(string surface, string feature, bool ignorable=false)
        :this(surface, Token.feature2list(feature), ignorable)
    {;}

    public Token(string surface, List<string> feature, bool ignorable=false)
        :this(surface, feature, PoS.get_type(feature), ignorable)
    {;}

    public Token(string surface, List<string> feature, PoS.type type, bool ignorable=false)
    {
        this.surface = surface;
        if(feature == null || feature.Count == 0)
            this.feature = null;
        else
            this.feature = feature;
        this.type = type;
        this.ignorable = ignorable;
    }

    /* Featureを*, 原形, 読み, 発音を除去しつつリストに変換
     * 品詞,品詞細分類1,品詞細分類2,品詞細分類3,活用型,活用形,原形,読み,発音
     */
    public static List<string> feature2list(string feature)
    {
        if(feature == null)
            return null;
        List<string> ls = new List<string>();
        string[] sp = feature.Split(',');
        int max = (sp.Length > 6) ? 6 : sp.Length;
        for(int i = 0; i < max; ++i)
            if(sp[i] != "*")
                ls.Add(sp[i]);
        return ls;
    }

    public bool cmp_feature(string feature, bool strict=false)
    {
        return this.cmp_feature(feature2list(feature), strict);
    }

    public bool cmp_feature(List<string> feature, bool strict=false)
    {
        if(this.feature == null || feature == null)
        {
            Console.WriteLine($"feature is null\n\t{this}/{feature}");
            return false;
        }
        foreach(var feat in feature)
            if(!this.feature.Contains(feat))
                return false;
        if(strict)
            if(this.feature.Count != feature.Count)
                return false;
        return true;
    }

    public bool cmp_feature(string[] feature, bool strict=false)
    {
        return this.cmp_feature(feature.ToList(), strict);
    }

    public bool cmp_feature(Token tk, bool strict=false)
    {
        return this.cmp_feature(tk.feature, strict);
    }

    public bool match(Token tk)
    {
        if(this.surface != "" && this.surface != tk.surface)
            return false;
        else if(this.type != PoS.type.NULL && this.type != tk.type)
            return false;
        else if(this.feature != null && tk.feature != null)
        {
            if(!tk.cmp_feature(this))
                return false;
            //foreach(var feat in this.feature)
                //if(!tk.feature.Contains(feat))
                    //return false;
        }
        return true;
    }

    public bool is_sep()
    {
        if(this.type == PoS.type.Punctuation)
            return true;
        if(this.feature != null && this.feature.Contains("記号"))
            return true;
        return false;
    }

    public override string ToString()
    {
        string s= "";
        if(this.surface != "")
            s += $"表層形: {this.surface}\t";
        if(this.feature != null)
            s += $"結果: {String.Join(",", this.feature)}\t";
        return s += $"type: {this.type}";
        //return $"表層形: {this.surface}\t結果: {String.Join(",", this.feature)}\ttype: {this.type}";
    }
}

}