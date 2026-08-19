using System;
using System.Collections.Generic;
using System.Linq;
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
        addPasswordWindow.ShowDialog(this);
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

    private void DisplayPasswordToggle_Click(object? sender, RoutedEventArgs e)
    {
        DisplayPasswordBox.RevealPassword = !DisplayPasswordBox.RevealPassword;
    }

    private void EditButton_Click(object? sender, RoutedEventArgs e)
    {
        if (PasswordListBox.SelectedItem != null)
        {
            PasswordEntry selectedEntry = (PasswordEntry)PasswordListBox.SelectedItem;
            //need to get the password info to pop up on the add password window

            //This didn't seem to work as it is likely creating a new instance of the window maybe?
            var addPasswordWindow = new AddPasswordWindow(passwordManagerService, this, selectedEntry);
            //ShowDialog(this) is better than just Show() as it disables the mainwindow whilst the popup window is open
            addPasswordWindow.ShowDialog(this);
        }
    }

    private void DeleteButton_Click(object? sender, RoutedEventArgs e)
    {
        //Still need to add a pop up confirmation before deleting entry
        if (PasswordListBox.SelectedItem != null)
        {
            PasswordEntry selectedEntry = (PasswordEntry)PasswordListBox.SelectedItem;
            passwordManagerService.RemovePassword(selectedEntry);
            
            RefreshList();
        }
    }

    public void RefreshList()
    {
        //This just deselects the currently selected item
        PasswordListBox.SelectedItem = null;
        PasswordListBox.ItemsSource = null;
        //The .ToList() forces avalonia to redraw the UI. 
        //I was deleting the entry from the List but for some reason it wasn't redrawing the UI
        PasswordListBox.ItemsSource = passwordManagerService.GetPasswords().ToList();
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