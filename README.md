# Mac Window Template for WPF

macOS風のモダンなウィンドウデザインを実現するWPFテンプレートライブラリです。美しいmacOSライクなUIコンポーネントと、使いやすいカスタムコントロールを提供します。

## 🎨 主な特徴

- **macOS風デザイン**: 本物のmacOSアプリケーションのような外観
- **トラフィックライトボタン**: 赤・黄・緑の特徴的なウィンドウボタン
- **モダンなタイポグラフィ**: システムフォントを使用した美しいテキスト
- **レスポンシブデザイン**: 様々なサイズに対応
- **カスタマイズ可能**: 色やフォントを簡単に変更可能
- **再利用可能**: 任意のWPFプロジェクトで使用可能

## 📦 プロジェクト構成

```
MacWindowTemplate/
├── App.xaml                           (アプリケーション定義)
├── App.xaml.cs                        (アプリケーションロジック)
├── MainWindow.xaml                    (メインウィンドウ)
├── MainWindow.xaml.cs                 (メインウィンドウロジック)
├── MacWindowTemplateControl.xaml      (カスタムUserControl)
├── MacWindowTemplateControl.xaml.cs   (カスタムUserControlロジック)
└── MacDesignResourceDictionary.xaml   (スタイル定義)
```

## 🚀 セットアップ手順

### 1. プロジェクトの準備

1. 新しいWPFプロジェクトを作成、または既存のプロジェクトを開く
2. 提供されたファイルをプロジェクトに追加

### 2. リソースディクショナリの登録

`App.xaml`にリソースディクショナリを登録します：

```xml
<Application x:Class="YourApp.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             StartupUri="MainWindow.xaml">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ResourceDictionary Source="MacDesignResourceDictionary.xaml"/>
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</Application>
```

### 3. 基本的な使用方法

メインウィンドウで`MacWindowTemplateControl`を使用：

```xml
<Window x:Class="YourApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:local="clr-namespace:MacWindowTemplate"
        Title="My Mac App" Height="600" Width="800"
        Style="{StaticResource MacWindowStyle}">
    
    <local:MacWindowTemplateControl WindowTitle="My Mac App">
        <local:MacWindowTemplateControl.MainContent>
            <!-- ここにメインコンテンツを配置 -->
            <Grid Background="{StaticResource MacContentBrush}">
                <TextBlock Text="Hello, Mac World!" 
                           Style="{StaticResource MacHeaderText}"
                           HorizontalAlignment="Center"
                           VerticalAlignment="Center"/>
            </Grid>
        </local:MacWindowTemplateControl.MainContent>
        
        <local:MacWindowTemplateControl.ToolbarContent>
            <!-- ツールバーコンテンツ（オプション） -->
            <Button Content="Settings" 
                    Style="{StaticResource MacSecondaryButton}"/>
        </local:MacWindowTemplateControl.ToolbarContent>
    </local:MacWindowTemplateControl>
</Window>
```

## 🎨 利用可能なスタイル

### ボタンスタイル

| スタイル名 | 用途 | 外観 |
|-----------|------|------|
| `MacPrimaryButton` | 主要なアクション | 青色の塗りつぶしボタン |
| `MacSecondaryButton` | 副次的なアクション | 白色の枠線ボタン |
| `MacDangerButton` | 危険なアクション | 赤色の塗りつぶしボタン |
| `MacSidebarButton` | サイドバー用 | 透明背景、左寄せ |
| `MacTrafficLightButton` | ウィンドウ制御 | 円形の交通信号ボタン |

### テキストスタイル

| スタイル名 | 用途 | サイズ |
|-----------|------|--------|
| `MacTitleText` | メインタイトル | 28pt, Bold |
| `MacHeaderText` | セクションヘッダー | 20pt, SemiBold |
| `MacBodyText` | 本文テキスト | 14pt, Regular |
| `MacSecondaryText` | 副次テキスト | 14pt, グレー |
| `MacSmallText` | 小さなテキスト | 11pt, 薄いグレー |

### レイアウトスタイル

| スタイル名 | 用途 | 特徴 |
|-----------|------|------|
| `MacCard` | カード形式のコンテナ | 角丸、シャドウ、パディング |
| `MacSidebar` | サイドバー | 固定幅240px、境界線 |
| `MacSeparator` | セパレーター | 1px高さの境界線 |
| `MacSecondaryContainer` | 副次コンテナ | 薄いグレー背景 |

## 🔧 プロパティとメソッド

### MacWindowTemplateControl

#### プロパティ

| プロパティ | 型 | 説明 | デフォルト値 |
|-----------|----|----|-------------|
| `WindowTitle` | string | ウィンドウタイトル | "MacDesign App" |
| `MainContent` | object | メインコンテンツ | null |
| `ToolbarContent` | object | ツールバーコンテンツ | null |
| `ShowMinimizeButton` | bool | 最小化ボタンの表示 | true |
| `ShowMaximizeButton` | bool | 最大化ボタンの表示 | true |

#### メソッド

```csharp
// トラフィックライトボタンの表示制御
public void SetTrafficLightButtons(bool showClose = true, bool showMinimize = true, bool showMaximize = true)

// ウィンドウ状態の取得
public WindowState GetWindowState()

// ウィンドウ状態の設定
public void SetWindowState(WindowState state)
```

## 🎨 カスタマイズ方法

### 色の変更

`MacDesignResourceDictionary.xaml`で色を変更できます：

```xml
<!-- アクセント色を変更 -->
<SolidColorBrush x:Key="MacAccentBrush" Color="#FF6B46"/>
<SolidColorBrush x:Key="MacAccentHoverBrush" Color="#E55A3F"/>

<!-- 背景色を変更 -->
<SolidColorBrush x:Key="MacContentBrush" Color="#FAFAFA"/>
```

### フォントの変更

```xml
<!-- システムフォントを変更 -->
<FontFamily x:Key="MacFontFamily">Inter, -apple-system, "Segoe UI", Arial, sans-serif</FontFamily>
```

### 角丸の調整

```xml
<!-- カードの角丸を変更 -->
<Style x:Key="MacCard" TargetType="Border">
    <Setter Property="CornerRadius" Value="8"/> <!-- 12から8に変更 -->
</Style>
```

## 📋 実装例

### サイドバー付きアプリケーション

```xml
<local:MacWindowTemplateControl WindowTitle="Notes App">
    <local:MacWindowTemplateControl.MainContent>
        <Grid>
            <Grid.ColumnDefinitions>
                <ColumnDefinition Width="240"/>
                <ColumnDefinition Width="*"/>
            </Grid.ColumnDefinitions>
            
            <!-- サイドバー -->
            <Border Grid.Column="0" Style="{StaticResource MacSidebar}">
                <StackPanel Margin="0,16">
                    <Button Content="📝 All Notes" Style="{StaticResource MacSidebarButton}"/>
                    <Button Content="📁 Folders" Style="{StaticResource MacSidebarButton}"/>
                    <Button Content="🗑️ Trash" Style="{StaticResource MacSidebarButton}"/>
                </StackPanel>
            </Border>
            
            <!-- メインコンテンツ -->
            <Grid Grid.Column="1" Background="{StaticResource MacContentBrush}">
                <Border Style="{StaticResource MacCard}" Margin="24">
                    <StackPanel>
                        <TextBlock Text="Welcome to Notes" Style="{StaticResource MacHeaderText}"/>
                        <TextBlock Text="Create your first note to get started." 
                                   Style="{StaticResource MacSecondaryText}" 
                                   Margin="0,8,0,0"/>
                    </StackPanel>
                </Border>
            </Grid>
        </Grid>
    </local:MacWindowTemplateControl.MainContent>
    
    <local:MacWindowTemplateControl.ToolbarContent>
        <StackPanel Orientation="Horizontal">
            <Button Content="+ New Note" Style="{StaticResource MacPrimaryButton}"/>
            <Button Content="Search" Style="{StaticResource MacSecondaryButton}" Margin="8,0,0,0"/>
        </StackPanel>
    </local:MacWindowTemplateControl.ToolbarContent>
</local:MacWindowTemplateControl>
```

### 設定ダイアログ

```xml
<local:MacWindowTemplateControl WindowTitle="Preferences" ShowMaximizeButton="False">
    <local:MacWindowTemplateControl.MainContent>
        <Grid Background="{StaticResource MacContentBrush}">
            <Border Style="{StaticResource MacCard}" Margin="24">
                <StackPanel>
                    <TextBlock Text="General Settings" Style="{StaticResource MacHeaderText}"/>
                    <Border Style="{StaticResource MacSeparator}"/>
                    
                    <TextBlock Text="Application Language" Style="{StaticResource MacBodyText}"/>
                    <ComboBox Margin="0,8,0,16">
                        <ComboBoxItem Content="English"/>
                        <ComboBoxItem Content="日本語"/>
                    </ComboBox>
                    
                    <TextBlock Text="Theme" Style="{StaticResource MacBodyText}"/>
                    <ComboBox Margin="0,8,0,24">
                        <ComboBoxItem Content="Light"/>
                        <ComboBoxItem Content="Dark"/>
                        <ComboBoxItem Content="Auto"/>
                    </ComboBox>
                    
                    <StackPanel Orientation="Horizontal" HorizontalAlignment="Right">
                        <Button Content="Cancel" Style="{StaticResource MacSecondaryButton}"/>
                        <Button Content="Save" Style="{StaticResource MacPrimaryButton}" Margin="8,0,0,0"/>
                    </StackPanel>
                </StackPanel>
            </Border>
        </Grid>
    </local:MacWindowTemplateControl.MainContent>
</local:MacWindowTemplateControl>
```

## ⚠️ 注意事項

### 制限事項

- **Windows専用**: このテンプレートはWPFベースのため、Windows環境でのみ動作します
- **透明度**: `AllowsTransparency="True"`を使用するため、一部の環境でパフォーマンスに影響する可能性があります
- **カスタムウィンドウスタイル**: `WindowStyle="None"`を使用するため、標準的なウィンドウ操作が一部制限されます

### パフォーマンス考慮事項

- シャドウエフェクトが多用されているため、古いハードウェアでは動作が重くなる可能性があります
- 必要に応じて`MacDropShadowEffect`の`BlurRadius`を調整してください

### 互換性

- **.NET Framework 4.7.2以上**推奨
- **Windows 10以上**で最適化されています
- **高DPI環境**での表示をサポートしています

## 📝 ライセンス

このテンプレートは自由に使用、改変、配布することができます。商用プロジェクトでの使用も可能です。

## 🤝 コントリビューション

バグ報告や機能改善の提案は、プロジェクトのIssueページでお知らせください。プルリクエストも歓迎します。

## 📖 追加リソース

- [WPF公式ドキュメント](https://docs.microsoft.com/ja-jp/dotnet/desktop/wpf/)
- [macOSヒューマンインターフェイスガイドライン](https://developer.apple.com/design/human-interface-guidelines/macos)
- [カスタムコントロールの作成方法](https://docs.microsoft.com/ja-jp/dotnet/desktop/wpf/controls/control-authoring-overview)

---

## 🎯 クイックスタート

1. プロジェクトにファイルを追加
2. `App.xaml`にリソースディクショナリを登録
3. `MainWindow.xaml`で`MacWindowTemplateControl`を使用
4. 実行してmacOS風のウィンドウを確認

これで、美しいmacOS風のWPFアプリケーションが完成します！