using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using PasswordManager.Models;
using PasswordManager.Services;

namespace PasswordManager.Views;

public partial class AddPasswordWindow : Window
{

    private PasswordManagerService passwordManagerService;
    private MainWindow mainWindow;
    private bool isValid = false;
    public AddPasswordWindow(PasswordManagerService passwordManagerService, MainWindow mainWindow)
    {
        InitializeComponent();
        this.passwordManagerService = passwordManagerService;
        this.mainWindow = mainWindow;
    }

    private void SaveNewPassword_Click(object? sender, RoutedEventArgs e)
    {
        //The meaning of this '?? ""' is if Text has a value then use it else if it's null then use ""
        string website = WebsiteTextBox.Text ?? "";
        string username = UsernameTextBox.Text ?? "";
        string password = PasswordTextBox.Text ?? "";
        string notes = NotesTextBox.Text ?? "";

        if (string.IsNullOrWhiteSpace(website))
        {
            WebsiteTextBox.Classes.Add("error");
            WebsiteTextBox.PlaceholderText = "Website name is required!";
            isValid = false;
        }
        else
        {
            WebsiteTextBox.Classes.Remove("error");
            isValid = true;
        }

        if (string.IsNullOrWhiteSpace(username))
        {
            UsernameTextBox.Classes.Add("error");
            UsernameTextBox.PlaceholderText = "Username is required!";
            isValid = false;
        }
        else
        {
            UsernameTextBox.Classes.Remove("error");
            isValid = true;
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            PasswordTextBox.Classes.Add("error");
            PasswordTextBox.PlaceholderText = "Password is required!";
            isValid = false;
        }
        else
        {
            PasswordTextBox.Classes.Remove("error");
            isValid = true;
        }

        if (isValid) //If all the required fields are filled in then we can add the password
        {
            //This creates one new password entry each with these bits of info
            PasswordEntry entry = new PasswordEntry
            {
                Website = website,
                Username = username,
                Password = password,
                Notes = notes,
                LastUpdated = DateTime.Now
            };

            passwordManagerService.AddPassword(entry);

            mainWindow.RefreshList();

            //Closes this popup window and sends the data in entry back to mainwindow
            Close();
        }
    }
}
