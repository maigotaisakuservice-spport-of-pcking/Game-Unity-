# 8番階段 (Stairway No. 8) - Unity Project Framework

これは、企画書に基づいて作成された心理ホラーゲーム「8番階段」のUnityプロジェクト用コードフレームワークです。
このファイルには、プロジェクトのセットアップに必要な全ての情報が含まれています。

## 目次
1.  [ゲームの操作方法](#-ゲームの操作方法)
2.  [必須・推奨パッケージ](#-必須推奨パッケージ)
3.  [スクリプト概要](#-スクリプト概要)
4.  [Unityプロジェクトのセットアップ手順](#️-unityプロジェクトのセットアップ手順)

---

## 🎮 ゲームの操作方法

- **移動**: `W`, `A`, `S`, `D` キー / ゲームパッドの左スティック
- **走行**: `Shift`キー 長押し / ゲームパッドのショルダーボタン長押し
- **視点移動**: マウス / ゲームパッドの右スティック

---

## 📦 必須・推奨パッケージ

このプロジェクトを実装・実行するために、以下のUnityパッケージの導入が必要です。
`Window > Package Manager`からインストールしてください。

### 必須
-   **Input System**
    -   **詳細:** キーボード・マウスに加え、Nintendo Switch Proコントローラーなどのゲームパッド操作を可能にするために必須のパッケージです。`PlayerController`はこのシステムの利用を前提としています。
    -   **導入方法:** インストール後、Unityエディタの再起動と、プロジェクト設定の変更（`Project Settings > Player > Active Input Handling` を `Input System Package (New)` に設定）を促すメッセージが表示されたら、`Yes`をクリックしてください。

### 推奨
-   **Post-Processing (URPまたはHDRPのVolume機能)**
    -   **詳細:** 企画書にある「壁の歪み」「光と影の錯覚」といった視覚的な恐怖演出を実装するために推奨されます。
-   **TextMeshPro**
    -   **詳細:** UIのテキスト表示の品質と柔軟性を大幅に向上させます。Unityの標準パッケージです。
-   **AI Navigation**
    -   **詳細:** 「床が奈落になる」などの動的な地盤変化にAIを対応させる場合、より柔軟なナビゲーション機能を提供します。

---

## 📄 スクリプト概要

このプロジェクトに含まれる主要なスクリプトの役割です。

-   `GameManager.cs`: ゲーム全体の状態（現在の階層、ゲームクリアなど）を管理する中心的なスクリプト。
-   `PlayerController.cs`: プレイヤーの移動、視点操作、走行を処理します。`Input System`からの入力を受け取ります。
-   `AnomalyGenerator.cs`: シーン内の異変（`IAnomaly`を実装したスクリプト）を自動で検出し、確率に基づいてランダムに発生させます。
-   `IAnomaly.cs`: 全ての異変スクリプトが従うべきルールを定義した「インターフェース」。`Activate()`と`Deactivate()`メソッドを持ちます。
-   `FlickeringLight.cs`, `PosterAnomaly.cs`: `IAnomaly`を実装した具体的な異変のサンプル。
-   `JkController.cs`: 女子高生キャラクターのAI。プレイヤーを追跡したり、待機場所に戻ったりするロジックを制御します。
-   `StairsTrigger.cs`, `JkTrigger.cs`: プレイヤーが特定のエリアに入ったことを検知するトリガースクリプト。
-   `UIManager.cs`: 階層表示やエンディング画面など、UIの表示・非表示を管理します。
-   `SoundManager.cs`: 効果音やBGMの再生を管理します。
-   `EndingSequence.cs`: 8階到達後のバス帰還演出を制御するスクリプト。

---

## 🛠️ Unityプロジェクトのセットアップ手順

### 1. 新規プロジェクトとパッケージの準備
1.  **Unity Hub**で`3D`テンプレートを使い、新しいプロジェクトを作成します。
2.  `Window > Package Manager`を開き、上記の**必須パッケージ** `Input System` をインストールします。

### 2. スクリプトのインポート
1.  このリポジトリの`Assets`フォルダの中身を、Unityプロジェクトの`Assets`フォルダにコピーします。

### 3. Input Actionsの設定
1.  `Project`ウィンドウで右クリックし、`Create > Input Actions`を選択して、`PlayerActions`などの名前でアセットを作成します。
2.  作成したアセットをダブルクリックして開きます。
3.  `Action Maps`の`+`を押し、`Player`などの名前でAction Mapを作成します。
4.  `Actions`カラムで`+`を3回押し、以下のアクションを作成します。
    -   `Move`: `Action Type`を`Value`、`Control Type`を`Vector 2`に設定。
    -   `Look`: `Action Type`を`Value`、`Control Type`を`Vector 2`に設定。
    -   `Sprint`: `Action Type`を`Button`に設定。
5.  作成した各アクションに、キーやボタンを割り当てます（例: `Move`に`WASD`、`Look`に`Mouse/Delta`、`Sprint`に`Left Shift`）。
6.  `Save Asset`ボタンを押して保存します。

### 4. シーンのセットアップ
1.  `File > New Scene`から新しいシーンを作成します。
2.  **Playerのセットアップ**:
    -   `Hierarchy`で`Capsule`を作成し、名前を`Player`に変更します。
    -   `Player`に`Player Controller (Script)`と`Player Input`コンポーネントを追加します。
    -   `Player Input`コンポーネントの`Actions`フィールドに、先ほど作成した`PlayerActions`アセットをドラッグ＆ドロップします。
    -   `Player`のタグを`Player`に設定し、子オブジェクトとして`Camera`を配置します。
3.  **管理オブジェクトのセットアップ**:
    -   `Create Empty`で`Managers`オブジェクトを作成し、`GameManager`, `UIManager`, `SoundManager`, `AnomalyGenerator`スクリプトをアタッチします。
4.  **レベルとトリガーの作成**:
    -   床や壁、階段の3Dモデルを配置します。
    -   階段の出入り口にトリガー（`Is Trigger`をオンにしたCube）を置き、`StairsTrigger`スクリプトをアタッチします。インスペクターで`Stair Direction`を設定します。
5.  **異変オブジェクトの配置**:
    -   壁に`Quad`を配置してポスターに見立て、`PosterAnomaly.cs`をアタッチします。
    -   シーンに`Light`を配置し、`FlickeringLight.cs`をアタッチします。
6.  **UIのセットアップ**:
    -   `Canvas`を作成し、`Text`や`Panel`、`Image`を配置します。
    -   `Managers`オブジェクトの`UIManager`コンポーネントを開き、インスペクターの各Publicフィールド（`Floor Text`, `Ending Panel`, `Fade Screen`など）に、作成したUI要素をドラッグ＆ドロップで割り当てます。

### 5. 実行
以上でセットアップは完了です。Unityエディタの再生ボタンを押して、ゲームを開始してください。
