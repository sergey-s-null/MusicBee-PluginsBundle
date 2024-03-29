﻿using System.Windows;
using Plugin.Main.GUI.ViewModels;

namespace Plugin.Main.GUI.Views;

public sealed partial class SettingsDialog : Window
{
    private readonly SettingsDialogVM _viewModel;
        
    public SettingsDialog(SettingsDialogVM viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
        DataContext = viewModel;
    }

    private void Ok_Click(object sender, RoutedEventArgs e)
    {
        if (_viewModel.Save())
        {
            DialogResult = true;
        }
    }
}