Groorine
==============

日本語 ＊ [English](readme.md)

[![ビルド情報](https://ci.appveyor.com/api/projects/status/7n31q63fbt037v84?svg=true)](https://ci.appveyor.com/project/Citringo/groorine) 

![このビルドの MainWindow のスクリーンショット](top-screenshot.png "MainWindow スクリーンショット")

Groorine は，DTMアプリを開発するオープンソースプロジェクトです．このプロジェクトは私の課題研究から始まっています．

## 詳細

私はよく Domino や Logic Pro X で音楽の打ち込みをしています．

この２つの良い点を持ったミュージックエディターが欲しいのです．また，以前から自分のオリジナルのミュージックエディターを作りたいと思っていました．

## ビルド方法

1. git コマンドでプロジェクトを手元に複製してください． `% git clone https://github.com/Citringo/Groorine.git`
1. Visual Studio 2015 でソリューションを開いてください．
1. `F5` を押してプロジェクトをビルドしてください．

## 動作条件
Groorine は， Windows と ".NET Framework 4.5.2" の上で動作します．

WPF をサポートしないと思われる(未検証) wine-mono では動作しないでしょう．

もしプロジェクトが完了したら，osx に移植することも考えています．

## 貢献
完全なコーディング規則を製作中です． 現在は，下記の簡易な規則に則ってください．

このプロジェクトで使用しているコーディング規則は， [C# コーディング規則](https://msdn.microsoft.com/library/ff926074.aspx)に近いです．
しかし，いくつかの例外があります:

- タブ文字は保持してください．スペースに変換しないでください．

プロジェクトに貢献してくれる方は， [プルリクエスト](/Citringo/Groorine/pulls)を投稿してください.

## 意見
意見などは [Issues](/Citringo/Groorine/issues) をご活用ください．

Issues に投稿する際は，次の内容を必ず含めてください．

- **バージョン** (例: 0.0.1)
- **あなたの環境のバージョン** (例: Windows 10 Pro Insider Preview 14328)
	- 詳細な情報をお願いします．

## ライセンス
このプロジェクトは MIT ライセンスです． [LICENSE](LICENSE) をご覧ください．

*(C) 2016 Citringo and GitHub Contributor*
