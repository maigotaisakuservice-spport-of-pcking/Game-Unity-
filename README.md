# 8番階段 (Stairway No. 8) - Unity Project Framework

これは、企画書に基づいて作成された心理ホラーゲーム「8番階段」のUnityプロジェクト用コードフレームワークです。
このファイルには、プロジェクトのセットアップに必要な全ての情報が含まれています。

## 📝 目次
1.  [前提条件](#-前提条件)
2.  [ゲームの操作方法](#-ゲームの操作方法)
3.  [必須・推奨パッケージ](#-必須推奨パッケージ)
4.  [スクリプト概要](#-スクリプト概要)
5.  [Unityプロジェクトのセットアップ手順](#️-unityプロジェクトのセットアップ手順)

---

## ✅ 前提条件

プロジェクトをスムーズにセットアップするための推奨環境です。

-   **Unityバージョン:** `Unity 2022.3.x (LTS)`
    -   長期サポート版（LTS）は安定性が高く、プロジェクトに適しています。他のバージョンでも動作する可能性はありますが、互換性の問題が発生する場合があります。
-   **レンダーパイプライン:** `Universal Render Pipeline (URP)`
    -   **理由:** URPは、幅広いプラットフォームで高いパフォーマンスを維持しつつ、ポストプロセスによる豊かな視覚表現（ブルーム、色収差など）が可能なため、本作の心理的恐怖演出に適しています。
    -   **選択方法:** Unity Hubで新規プロジェクトを作成する際に、`3D (URP)` テンプレートを選択するのが最も簡単です。既存のプロジェクトの場合は、Package ManagerからURPをインストールし、手動で設定する必要があります。
    -   *注: 提供されているスクリプトは、標準の`Built-in Render Pipeline`でも動作しますが、企画書にあるような高度な視覚効果を実装するにはURPまたはHDRPが推奨されます。*

---

## 🎮 ゲームの操作方法

- **移動**: `W`, `A`, `S`, `D` キー / ゲームパッドの左スティック
- **走行**: `Shift`キー 長押し / ゲームパッドのショルダーボタン長押し
- **視点移動**: マウス / ゲームパッドの右スティック

---

## 📦 必須・推奨パッケージ

`Window > Package Manager`からインストールしてください。

### 必須
-   **Input System**
    -   **詳細:** PC、ゲームパッド、モバイルタッチ操作など、複数の入力方法に対応するために必須のパッケージです。

### 推奨
-   **TextMeshPro**: 高品質なUIテキスト表示に。Unity標準パッケージです。
-   **AI Navigation**: 動的な地形変化にAIを対応させる場合に。

---

## 📄 スクリプト概要

-   `GameManager.cs`: ゲーム全体の進行（階層、状態）を管理する心臓部。
-   `PlayerController.cs`: プレイヤーの移動・視点・走行を処理。Input Systemからの入力を受け付けます。
-   `AnomalyGenerator.cs`: `IAnomaly`を持つ異変オブジェクトを自動検出し、ランダムに発生させます。
-   `IAnomaly.cs`: 全ての異変が従うべき共通のルール（インターフェース）。
-   `FlickeringLight.cs`, `PosterAnomaly.cs`: `IAnomaly`を実装した異変のサンプルスクリプト。
-   `JkController.cs`: 女子高生AI。プレイヤー追跡などを制御します。
-   `OnScreenJoystick.cs`: モバイル用の仮想ジョイスティックUIのロジック。

---

## 🛠️ Unityプロジェクトのセットアップ手順

### 1. プロジェクト作成とパッケージ導入
1.  **Unity Hub**で`3D (URP)`テンプレートを使い、`Unity 2022.3.x`で新規プロジェクトを作成します。
2.  `Window > Package Manager`で、上記の**必須パッケージ** `Input System` をインストールします。

### 2. スクリプトのインポート
1.  このリポジトリの`Assets`フォルダの中身を、Unityプロジェクトの`Assets`フォルダにコピーします。

### 3. Input Actionsの設定
1.  `Project`ウィンドウで右クリックし、`Create > Input Actions`で`PlayerActions`アセットを作成します。
2.  アセットを開き、`Action Maps`に`Player`を作成します。
3.  `Actions`に`Move`(Value/Vector2), `Look`(Value/Vector2), `Sprint`(Button)の3つを作成します。
4.  各アクションにキーを割り当てます（例: `Move`に`WASD`、`Look`に`Mouse/Delta`、`Sprint`に`Left Shift`）。
5.  `Save Asset`で保存します。

### 4. シーンのセットアップ
1.  `File > New Scene`で新しいシーンを作成します。
2.  **Playerのセットアップ**:
    -   `Capsule`オブジェクトを`Player`と名付け、`PlayerController`と`Player Input`スクリプトを追加します。
    -   `Player Input`の`Actions`に`PlayerActions`アセットを割り当てます。
    -   `Player`のタグを`Player`にし、子に`Camera`を配置します。
3.  **管理オブジェクトのセットアップ**:
    -   空の`Managers`オブジェクトを作成し、`GameManager`, `UIManager`, `SoundManager`, `AnomalyGenerator`をアタッチします。
4.  **レベルと異変のセットアップ**:
    -   床や壁、階段の3Dモデルを配置します。
    -   階段の出入り口にトリガーを置き、`StairsTrigger`をアタッチします。
    -   壁に`Quad`を置きポスターに見立て、`PosterAnomaly`スクリプトをアタッチします。
5.  **モバイルUIのセットアップ (任意)**:
    1.  **コントロールスキームの追加**: `Project`ウィンドウで`PlayerActions`アセットを開き、`Control Schemes`に`Touch`という名前の新しいスキームを追加します。`Requirement`は`Touchscreen`に設定します。
    2.  **仮想ジョイスティックの作成**:
        -   `Hierarchy`で`UI > Image`を作成し`JoystickArea`と名付けます。画面左下に配置します。
        -   `JoystickArea`の子に`UI > Image`を作成し`JoystickHandle`と名付けます。
        -   空の`JoystickManager`オブジェクトを作成し、`On Screen Joystick`スクリプトをアタッチします。
        -   インスペクターで`Joystick Area`と`Joystick Handle`に作成したImageを割り当てます。
        -   `Player`の`Player Controller`コンポーネントの`Joystick`フィールドに、この`JoystickManager`を割り当てます。
    3.  **視点操作エリアの作成**:
        -   `Hierarchy`で`UI > Image`を作成し`LookArea`と名付け、画面右半分を覆うように設定します。
        -   `Image`コンポーネントのColorのAlphaを0にし、`Raycast Target`はオンのままにします。
        -   `LookArea`に`Touch Look Area`スクリプトをアタッチします。
        -   `Player`の`Player Controller`コンポーネントの`Touch Look Area`フィールドに、この`LookArea`を割り当てます。
    4.  **スプリントボタンの作成**:
        -   `Hierarchy`で`UI > Button`を作成し`SprintButton`と名付けます。
        -   `SprintButton`に`Event Trigger`コンポーネントを追加します。
        -   `PointerDown`イベントを追加し、`Player`オブジェクトの`PlayerController.SetSprinting(true)`を呼び出すように設定します（チェックボックスをオン）。
        -   `PointerUp`イベントを追加し、`Player`オブジェクトの`PlayerController.SetSprinting(false)`を呼び出すように設定します（チェックボックスはオフ）。

### 6. 実行
以上でセットアップは完了です。`Player`の`Player Input`コンポーネントで`Default Scheme`を`Touch`に設定し、Unity Remoteや実機ビルドでテストするか、PCでテストする場合は`Keyboard&Mouse`に設定してください。Unityエディタの再生ボタンを押してゲームを開始します。
