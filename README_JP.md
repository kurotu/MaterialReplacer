# Material Replacer

<a href="https://github.com/kurotu/MaterialReplacer/releases/latest">
  <img alt="Release" src="https://img.shields.io/github/v/release/kurotu/MaterialReplacer">
</a>

## 概要

ルールに基づいてマテリアルを一括で置換する Unity Editor 拡張です。

VRChat などのサービスでの使用を想定して作成していますが、自由に使用できます。

## 対応状況

動作確認環境
- Unity 2019.4.31f1

## セットアップ

以下のいずれかの方法でプロジェクトにインポートします。

- 最新の `.unitypackage` を[リリースページ](https://github.com/kurotu/MaterialReplacer/releases/latest)または [Booth]() からダウンロードしてインポート
- UPM で `https://github.com/kurotu/MaterialReplacer.git` を追加

## 使い方

### MaterialReplacerRule アセットを作成する

1. プロジェクトで右クリックし *Create* -> *Material Replacer* -> *Material Replacer Rule* のメニューを選択します。
2. 作成された `MaterialReplacerRule.asset` を選択します。
3. Inspector で `Original` と `Replaced` にマテリアルを設定します。
    - Original: 置き換え対象のマテリアル
    - Replaced: 適用されるマテリアル
    > ℹ️ `Reference Object` に GameObject を設定して `Add to Original Materials` を押すこともできます。一つ一つマテリアルを追加するよりも簡単です。

### GameObject にマテリアルを適用する

1. *Window* -> *Material Replacer* メニューを選択し Material Replacer ウィンドウを表示します。
   > ℹ️ シーン内の GameObject を右クリックして *Material Replacer* メニューを選択することもできます。
2. 開いたウィンドウに `Game Object` と `Material Replacer Rule` を設定します。
3. `Apply` を押します。
4. `Game Object` と配下にあるすべての Renderer のマテリアルが `Material Replacer Rule` に従って置き換えられます。

## ライセンス

MIT License

## 連絡先

- VRCID: kurotu
- Twitter: [@kurotu](https://twitter.com/kurotu)
- GitHub: [kurotu/VRCQuestTools](https://github.com/kurotu/VRCQuestTools)
