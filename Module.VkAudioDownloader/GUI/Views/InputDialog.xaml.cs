﻿using System.Windows;
using Module.VkAudioDownloader.GUI.ViewModels;

namespace Module.VkAudioDownloader.GUI.Views;

/// <summary>
/// Логика взаимодействия для InputDialog.xaml
/// </summary>
public partial class InputDialog : Window
{
    public string TitleText
    {
        get => _viewModel.TitleText;
        set => _viewModel.TitleText = value;
    }

    public string InputText
    {
        get => _viewModel.InputText;
        set => _viewModel.InputText = value;
    }

    private InputDialogVM _viewModel = new InputDialogVM();

    public InputDialog()
    {
        InitializeComponent();
        DataContext = _viewModel;
    }

    public bool ShowDialog(string titleText, out string? inputText)
    {
        TitleText = titleText;
        return ShowDialog(out inputText);
    }

    private bool ShowDialog(out string? inputText)
    {
        var res = ShowDialog();
        if (res == true)
        {
            inputText = InputText;
            return true;
        }
            
        inputText = null;
        return false;
    }

    private void Ok_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
    }
}