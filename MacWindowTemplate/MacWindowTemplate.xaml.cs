using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MacWindowTemplate
{
    /// <summary>
    /// MacWindowTemplateControl.xaml の相互作用ロジック
    /// </summary>
    public partial class MacWindowTemplateControl : UserControl
    {
        private Window _parentWindow;

        public MacWindowTemplateControl()
        {
            InitializeComponent();
            Loaded += MacWindowTemplateControl_Loaded;
        }

        private void MacWindowTemplateControl_Loaded(object sender, RoutedEventArgs e)
        {
            _parentWindow = Window.GetWindow(this);
            if (_parentWindow != null)
            {
                // ウィンドウドラッグを有効化
                this.MouseLeftButtonDown += MacWindowTemplateControl_MouseLeftButtonDown;

                // ウィンドウタイトルを更新
                UpdateWindowTitle();
            }
        }

        #region 依存関係プロパティ

        /// <summary>
        /// ウィンドウタイトル
        /// </summary>
        public static readonly DependencyProperty WindowTitleProperty =
            DependencyProperty.Register("WindowTitle", typeof(string), typeof(MacWindowTemplateControl),
                new PropertyMetadata("MacDesign App", OnWindowTitleChanged));

        public string WindowTitle
        {
            get { return (string)GetValue(WindowTitleProperty); }
            set { SetValue(WindowTitleProperty, value); }
        }

        private static void OnWindowTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is MacWindowTemplateControl control)
            {
                control.UpdateWindowTitle();
            }
        }

        /// <summary>
        /// メインコンテンツ
        /// </summary>
        public static readonly DependencyProperty MainContentProperty =
            DependencyProperty.Register("MainContent", typeof(object), typeof(MacWindowTemplateControl),
                new PropertyMetadata(null));

        public object MainContent
        {
            get { return GetValue(MainContentProperty); }
            set { SetValue(MainContentProperty, value); }
        }

        /// <summary>
        /// ツールバーコンテンツ
        /// </summary>
        public static readonly DependencyProperty ToolbarContentProperty =
            DependencyProperty.Register("ToolbarContent", typeof(object), typeof(MacWindowTemplateControl),
                new PropertyMetadata(null));

        public object ToolbarContent
        {
            get { return GetValue(ToolbarContentProperty); }
            set { SetValue(ToolbarContentProperty, value); }
        }

        /// <summary>
        /// 最小化ボタンの表示制御
        /// </summary>
        public static readonly DependencyProperty ShowMinimizeButtonProperty =
            DependencyProperty.Register("ShowMinimizeButton", typeof(bool), typeof(MacWindowTemplateControl),
                new PropertyMetadata(true, OnShowMinimizeButtonChanged));

        public bool ShowMinimizeButton
        {
            get { return (bool)GetValue(ShowMinimizeButtonProperty); }
            set { SetValue(ShowMinimizeButtonProperty, value); }
        }

        private static void OnShowMinimizeButtonChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is MacWindowTemplateControl control)
            {
                var button = control.FindName("MinimizeButton") as Button;
                if (button != null)
                {
                    button.Visibility = (bool)e.NewValue ? Visibility.Visible : Visibility.Collapsed;
                }
            }
        }

        /// <summary>
        /// 最大化ボタンの表示制御
        /// </summary>
        public static readonly DependencyProperty ShowMaximizeButtonProperty =
            DependencyProperty.Register("ShowMaximizeButton", typeof(bool), typeof(MacWindowTemplateControl),
                new PropertyMetadata(true, OnShowMaximizeButtonChanged));

        public bool ShowMaximizeButton
        {
            get { return (bool)GetValue(ShowMaximizeButtonProperty); }
            set { SetValue(ShowMaximizeButtonProperty, value); }
        }

        private static void OnShowMaximizeButtonChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is MacWindowTemplateControl control)
            {
                var button = control.FindName("MaximizeButton") as Button;
                if (button != null)
                {
                    button.Visibility = (bool)e.NewValue ? Visibility.Visible : Visibility.Collapsed;
                }
            }
        }

        #endregion

        #region イベントハンドラー

        /// <summary>
        /// ウィンドウドラッグ処理
        /// </summary>
        private void MacWindowTemplateControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_parentWindow != null && e.OriginalSource is FrameworkElement element)
            {
                // タイトルバー内でのクリックの場合のみドラッグを有効化
                if (IsInTitleBar(element))
                {
                    try
                    {
                        _parentWindow.DragMove();
                    }
                    catch (InvalidOperationException)
                    {
                        // ドラッグ中に他の操作が発生した場合の例外を無視
                    }
                }
            }
        }

        /// <summary>
        /// 要素がタイトルバー内にあるかどうかを判定
        /// </summary>
        private bool IsInTitleBar(FrameworkElement element)
        {
            // ボタンの場合は除外
            if (element is Button)
                return false;

            // 親要素を辿ってタイトルバーかどうかを判定
            var parent = element;
            while (parent != null)
            {
                if (parent.Name == "TitleBar")
                {
                    return true;
                }
                parent = parent.Parent as FrameworkElement;
            }

            return true; // デフォルトではドラッグ可能
        }

        /// <summary>
        /// 閉じるボタンクリック処理
        /// </summary>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            _parentWindow?.Close();
        }

        /// <summary>
        /// 最小化ボタンクリック処理
        /// </summary>
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (_parentWindow != null)
            {
                _parentWindow.WindowState = WindowState.Minimized;
            }
        }

        /// <summary>
        /// 最大化ボタンクリック処理
        /// </summary>
        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (_parentWindow != null)
            {
                _parentWindow.WindowState = _parentWindow.WindowState == WindowState.Maximized
                    ? WindowState.Normal
                    : WindowState.Maximized;
            }
        }

        #endregion

        #region プライベートメソッド

        /// <summary>
        /// ウィンドウタイトルを更新
        /// </summary>
        private void UpdateWindowTitle()
        {
            if (WindowTitleTextBlock != null)
            {
                WindowTitleTextBlock.Text = this.WindowTitle;
            }
        }

        #endregion

        #region パブリックメソッド

        /// <summary>
        /// トラフィックライトボタンの表示/非表示を設定
        /// </summary>
        /// <param name="showClose">閉じるボタンの表示</param>
        /// <param name="showMinimize">最小化ボタンの表示</param>
        /// <param name="showMaximize">最大化ボタンの表示</param>
        public void SetTrafficLightButtons(bool showClose = true, bool showMinimize = true, bool showMaximize = true)
        {
            if (CloseButton != null)
                CloseButton.Visibility = showClose ? Visibility.Visible : Visibility.Collapsed;

            if (MinimizeButton != null)
                MinimizeButton.Visibility = showMinimize ? Visibility.Visible : Visibility.Collapsed;

            if (MaximizeButton != null)
                MaximizeButton.Visibility = showMaximize ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// ウィンドウの状態を取得
        /// </summary>
        public WindowState GetWindowState()
        {
            return _parentWindow?.WindowState ?? WindowState.Normal;
        }

        /// <summary>
        /// ウィンドウの状態を設定
        /// </summary>
        public void SetWindowState(WindowState state)
        {
            if (_parentWindow != null)
            {
                _parentWindow.WindowState = state;
            }
        }

        #endregion
    }
}