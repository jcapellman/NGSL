using System;
using System.Windows;

namespace NGSL.SampleApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var engine = new lib.NGSLEngine();

            engine.OnNewAssetDiscovered += Engine_OnNewAssetDiscovered;

            engine.Start();
        }

        private void Engine_OnNewAssetDiscovered(object? sender, lib.Objects.NGSLAsset e)
        {
            txtBxMain.Text += $"{e}{Environment.NewLine}";
        }
    }
}