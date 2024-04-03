## 目次

* [アプリをダウンロード](#download)
* [ToF ARについて](#about)
* [ToF AR Serverの概要](#overview)
* [開発環境](#environment)
* [注意事項](#notes)
* [コントリビューション](#contributing)


<a name="download"></a>
# アプリをダウンロード

ツールを使って ToF AR を使った開発のデバッグをしましょう。  
以下のストアでアプリを見つけることができます。

[<img alt="App Store からダウンロード" src="./Docs/images/App_Store_Badge_JP_100317.svg" height="60">](https://apps.apple.com/jp/developer/id1601362415)
&nbsp;&nbsp;&nbsp;&nbsp;
[<img alt="Google Play で手に入れよう" src="./Docs/images/google-play-badge_jp.png" height="68">](https://play.google.com/store/apps/developer?id=Sony+Semiconductor+Solutions+Corporation)


<a name="about"></a>
# ToF ARについて

ToF AR は、Time-of-Flight(ToF)センサを持つiOS/Andoroidスマートフォン向けの、Unity向けツールキットライブラリです。

Structured light 方式など、ToF 以外のDepthセンサでもToF ARは動作します。

ToF AR Serverのビルドと実行には、この ToF AR の他に、UnityとToFセンサを搭載した対応スマートフォンが必要です。

ToF AR のパッケージや開発ドキュメント、ToF ARを使ったアプリケーションソフト、対応スマートフォンのリストにつきましては、[ToF AR サイト](https://tof-ar.com/)をご覧ください。


<a name="overview"></a>
# ToF AR Serverの概要

ToF AR Serverは、ToF ARを用いたアプリケーションをデバッグするためのプログラムです。

ToF AR Server は Android/iOS向けのアプリケーション開発用のツールで、Unityエディター上でのプロジェクト実行中に、スマホのRGBやDepth入力をUnityに接続することができます。

ToF ARを使ったアプリの開発時にToF AR Serverを使う事で、アプリを毎回ビルドしてデバイスに転送することなく、PC上での動作確認が可能となります。

Unity Editor上で使用しているToF ARのバージョンとToF AR Serverで使用されているToF ARのバージョンは、同じバージョンにする必要があります。


<a name="environment"></a>
# 開発環境

## ビルド用ライブラリ

ビルドには、ToF AR が必要です。 [ToF AR サイト](https://tof-ar.com/)からダウンロードし、インポートして使用して下さい。  
インポート前にプロジェクトを開くと、設定によってはセーフモードへの移行確認メッセージが表示されます。  
セーフモードに移行した場合、セーフモードメニューなどからセーフモードを終了してインポートを行って下さい。


## ドキュメント

ToF AR Serverの使い方については、[ToF AR user manual](https://tof-ar.com/files/2/tofar/manual_reference/ToF_AR_User_Manual_ja.html)の[TofARServerを用いたDebug](https://tof-ar.com/files/2/tofar/manual_reference/ToF_AR_User_Manual_ja.html#_debug_with_tofarserver)をご覧ください。

対応OSなど制限事項につきましては、ToF AR user manualの[制限事項等](https://tof-ar.com/files/2/tofar/manual_reference/ToF_AR_User_Manual_ja.html#_%E5%88%B6%E9%99%90%E4%BA%8B%E9%A0%85%E7%AD%89)をご確認ください。


## 動作検証環境

動作検証は、下記の環境で行っています。

* Unity Version  : 2021.3.31f1
* ToF AR Version : 1.4.0


<a name="notes"></a>
# 注意事項

認識可能なハンドジェスチャーは国・地域によって異なる意味を有する場合があります。  
事前に確認されることをお勧めします。


<a name="contributing"></a>
# コントリビューション

**現在、プルリクエストは受け付けておりません。** バグ報告や新規機能のリクエストがありましたらissueとして登録して下さい。

このプログラムはToF ARを広く利用して頂けるようリリースしております。ご報告頂いたissueについては、検討の上、更新で対応する可能性があります。


