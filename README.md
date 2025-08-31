# 8番階段 (Stairway No. 8) - Unity Project Framework

これは、企画書に基づいて作成された心理ホラーゲーム「8番階段」のUnityプロジェクト用コードフレームワークです。
このリポジトリには、ゲームを動作させるためのC#スクリプト一式と、セットアップに必要な情報が含まれています。

## 🎮 ゲームの操作方法

- **移動**: `W`, `A`, `S`, `D` キー / ゲームパッドの左スティック
- **視点移動**: マウス / ゲームパッドの右スティック

## 🛠️ Unityプロジェクトのセットアップ手順

このコードをUnityで動作させるには、Unity Hubで新しいプロジェクトを作成し、以下の手順に従って手動でセットアップする必要があります。

### 1. 新規プロジェクトの作成

1.  **Unity Hub**を開きます。
2.  `新しいプロジェクト` をクリックします。
3.  テンプレートとして **`3D`** を選択します。
4.  プロジェクト名（例: `8ban-kaidan`）と保存場所を入力し、`プロジェクトを作成` をクリックします。

### 2. 必須パッケージのインストール

1.  Unityエディタでプロジェクトが開いたら、メニューバーから `Window > Package Manager` を開きます。
2.  左上の `Packages` ドロップダウンを `Unity Registry` に設定します。
3.  以下のパッケージを見つけて、それぞれ `Install` ボタンを押してインストールします。
    -   **`Input System`**
4.  （推奨）品質向上のため、`memo.txt`に記載されている他の推奨パッケージ（URP, TextMeshProなど）も同様にインストールしてください。
5.  `Input System`のインストール後、プロジェクト設定を更新するか尋ねるダイアログが表示されたら、`Yes`をクリックしてください。エディタが自動的に再起動します。

### 3. スクリプトとアセットのインポート

1.  ダウンロードしたこのリポジトリから、`Assets` フォルダの中身を、先ほど作成したUnityプロジェクトの `Assets` フォルダにすべてコピーします。

### 4. シーンのセットアップ

1.  `File > New Scene` から新しいシーンを作成し、保存します（例: `MainScene`）。
2.  **Playerのセットアップ**:
    -   `Hierarchy`で右クリックし、`3D Object > Capsule` を作成し、名前を `Player` に変更します。
    -   `Player`オブジェクトの `Capsule Collider` を削除します。
    -   `Player`に `Character Controller` コンポーネントを追加します。
    -   `Player`に `Player Controller (Script)` コンポーネントを追加します。
    -   `Player`に `Player Input` コンポーネントを追加します。`Actions` には、新しく作成したInput Actionsアセットを設定してください。（`Create Actions...` ボタンから生成できます）
    -   `Player`のタグを `Player` に設定します。
    -   `Player`の子オブジェクトとして、`Camera`を配置します。（Main Cameraをドラッグ＆ドロップします）
3.  **管理オブジェクトのセットアップ**:
    -   `Hierarchy`で右クリックし、`Create Empty` を作成し、名前を `Managers` に変更します。
    -   `Managers`オブジェクトに、以下のスクリプトをアタッチします。
        -   `GameManager`
        -   `UIManager`
        -   `SoundManager`
        -   `AnomalyGenerator`
4.  **レベルの作成**:
    -   床や壁、階段などの3Dモデルを配置して、ゲームの舞台となる廊下を作成します。
    -   階段の上り口と下り口に、それぞれ判定用のトリガー（`3D Object > Cube`で作成し、`Box Collider`の`Is Trigger`にチェック、`Mesh Renderer`は無効化）を配置します。
    -   これらのトリガーに `StairsTrigger` スクリプトをアタッチし、インスペクターで `Stair Direction` を `Up` または `Down` に設定します。
5.  **NavMeshのベイク**:
    -   `Window > AI > Navigation` を開きます。
    -   `Bake` タブを選択し、`Bake` ボタンを押して、AIが移動可能な範囲を生成します。
6.  **キャラクターと異変のセットアップ**:
    -   **女子高生(JK)**: 3Dモデルをシーンに配置し、`Jk Controller` と `Nav Mesh Agent` コンポーネントをアタッチします。インスペクターで `Player` や `Idle Position`（待機位置を示す空のGameObject）などを設定します。
    -   **JKトリガー**: `JkTrigger` スクリプトをアタッチしたトリガーを、イベントを開始させたい場所に配置します。
    -   **異変**: シーン内の任意のオブジェクトに、`Anomalies` フォルダ内の異変スクリプト（例: `FlickeringLight`）をアタッチします。
7.  **UIのセットアップ**:
    -   `Hierarchy`で右クリックし、`UI > Canvas` を作成します。
    -   `Canvas`内に、階層表示用の `UI > Text`、エンディングパネル用の `UI > Panel`、フェード用の `UI > Image` などを配置します。
    -   `Managers`オブジェクトの`UIManager`コンポーネントを開き、インスペクターの各フィールド（`Floor Text`, `Ending Panel`など）に、作成したUI要素をドラッグ＆ドロップで割り当てます。

### 5. 実行

以上でセットアップは完了です。Unityエディタ上部の再生ボタンを押して、ゲームを開始してください。
