using System;
using System.Windows;

namespace NGSL.SampleApp
{
    public partial class MainWindow : Window
    {
        private readonly lib.NGSLEngine engine;

        public MainWindow()
        {
            InitializeComponent();

            engine = new lib.NGSLEngine();

            engine.OnNewAssetDiscovered += Engine_OnNewAssetDiscovered;

            StartEngine();
        }

        public async void StartEngine()
        {
            await engine.Start();
        }

        private void Engine_OnNewAssetDiscovered(object? sender, lib.Objects.NGSLAsset e)
        {
            txtBxMain.Text += $"{e}{Environment.NewLine}";
        }
    }
}