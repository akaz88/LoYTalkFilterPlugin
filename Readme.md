# 黄泉ヲ裂ク華 会話文変換フィルタ MOD

Steam版黄泉ヲ裂ク華の会話文（アイテム説明を含む「」で括られた文章全て）に変換フィルタをかけます。<br>
現在以下のフィルタが実装されています。<br>
- 大阪弁
- 壱百満天原サロメお嬢様風の口調風(experimental)

## 注意事項

本MODは無保証です。<br>
本MODの使用により生じたあらゆる得失に対し作者は一切の責任を負いません。<br>
なお、本MODの大阪弁化辞書は[yan氏](http://www.yansite.jp/index.html)の「大阪弁化フィルタ」より一部改変したものを、<br>
壱百満天原サロメお嬢様風の口調風はjiro4989お嬢様の[ojosama](https://github.com/jiro4989/ojosama)を適当にこねくりましたものを使用しておりますわ！

## インストール

本MODは[BepInEx](https://github.com/BepInEx/BepInEx)と[NMeCab](https://ja.osdn.net/projects/nmecab/)を使用しています。

### 使用ライブラリのインストール

#### BepInEx

githubのReleaseから64bit版BepInEx最新版をダウンロードし、黄泉ヲ裂ク華のルートフォルダ(例：C:\Program Files (x86)\Steam\steamapps\common\Undernauts Labyrinth of Yomi\)にそのまま展開します。<br>
Labyrinth of Yomi.exeとwinhttp.dllが同じフォルダに存在するように展開すればOKです。

#### NMeCab

LibNMeCabは現在githubで開発されているバージョンではなく、OSDNで公開されているバージョンを使用します。<br>
まずは先程インストールしたBepInExのpluginsフォルダにLoY.TalkFilter.Pluginフォルダを作成します。<br>
OSDNからNMeCab 0.07をダウンロードし、NMeCab0.07.zipからdicフォルダとbin/LibNMeCab.dllを展開し、両方をLoY.TalkFilter.Pluginフォルダに入れます。<br>
例えばC:\Program Files (x86)\Steam\steamapps\common\Undernauts Labyrinth of Yomi\BepInEx\plugins\LoY.TalkFilter.Plugin\LibNMeCab.dllというような階層になっていればOK。

### LoY.TalkFilter.Pluginのインストール

黄泉ヲ裂ク華のルートフォルダに本MODのBepInExフォルダをそのまま上書きします。<br>
例えばC:\Program Files (x86)\Steam\steamapps\common\Undernauts Labyrinth of Yomi\BepInEx\plugins\LoY.TalkFilter.Plugin.dllというような階層になっていればOK。

## 設定ファイル

BepInEx\config\LoY.TalkFilter.Plugin.cfgとして自動で設定されます。<br>
以下の三種が有効となっていますが、デフォルトでは"無効"となっています。<br>
- 無効
  - 変換を行いません
- 大阪
  - 大阪弁に変換します
- お嬢様
  - お嬢様風に変換します
  - あんまりうまく変換されない場合がほとんどです

## LoYUtilPlugin用MODファイル

同時にLoYUtilPluginも公開したので、本MODでは対応できなかった一部のセリフも変換できるようになりました。<br>
これは正確には本MODでの変換ではなく、事前に用意した文章を表示するだけではあるのですが、表示されれば一緒なんで良いんじゃないでしょうか。<br>
本MODのReleaseに含まれるLoYUtilResourceをLoYUtilPluginの同名フォルダに上書きして下さい。<br>

## LoYUtilPlugin用フラグID

LoYUtilPluginでロードされるスクリプトから参照可能なフラグは以下となります。
| フラグID | フラグ番号 |
| ------ | ------- |
| ModTalkFilter | 1920 |
| TalkFilterOsaka | 1520 |
| TalkFilterOjosama | 1521 |

## change log

0.0.1
- どっとうpろだで公開

0.1.0
- githubに引っ越し
- 壱百満天原サロメお嬢様風の口調風変換を追加
- LoYOsakaPluginからLoYTalkFilterPluginに改名
- LoYUtilPluginにフラグ変数を提供するよう変更


## 開発環境

- Windows10 Pro 64bit
- BepInEx x64_5.4.19
- mono-6.12.0.107-x64付属のコンパイラ
