using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using PasswordManager.Models;
using PasswordManager.Services;

//This means that this script belongs in the 'Views' Folder
namespace PasswordManager.Views;

public partial class MainWindow : Window
{
    //This creates a variable but then it is null
    private PasswordManagerService passwordManagerService;
    public MainWindow()
    {
        InitializeComponent();

        //This then declares the variable as an object of name 'passwordManagerService' of type 'PasswordManagerService'
        passwordManagerService = new PasswordManagerService();
    }

    //So you need to give your button a name and name the method the same name
    private void AddPasswordButton_Click(object? sender, RoutedEventArgs e)
    {
        //We need to pass the service and 'this' mainwindow to the popup
        var addPasswordWindow = new AddPasswordWindow(passwordManagerService, this);
        addPasswordWindow.Show();
    }

    private void PasswordListBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (PasswordListBox.SelectedItem != null)
        {
            PasswordEntry selectedEntry = (PasswordEntry)PasswordListBox.SelectedItem;

            DisplayWebsiteBox.Text = selectedEntry.Website;
            DisplayUsernameBox.Text = selectedEntry.Username;
            DisplayPasswordBox.Text = selectedEntry.Password;
            DisplayNotesBox.Text = selectedEntry.Notes;

            DisplayDateLastModified.Text = $"Last Updated: {selectedEntry.LastUpdated:G}";
        }
        else
        {
            Clear();
        }
    }

    private void ShowPasswordToggle(object? sender, RoutedEventArgs e)
    {
        DisplayPasswordBox.RevealPassword = !DisplayPasswordBox.RevealPassword;
    }

    public void RefreshList()
    {
        PasswordListBox.ItemsSource = null;
        PasswordListBox.ItemsSource = passwordManagerService.GetPasswords();
        Clear();
    }
    public void Clear()
    {
        DisplayWebsiteBox.Text = null;
        DisplayUsernameBox.Text = null;
        DisplayPasswordBox.Text = null;
        DisplayNotesBox.Text = null;
        DisplayDateLastModified.Text = "Last Updated: ";
    }
}