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
    private PasswordEntry? entryToEdit;
    public AddPasswordWindow(PasswordManagerService passwordManagerService, MainWindow mainWindow)
    {
        InitializeComponent();
        Title = "Add New Password";
        this.passwordManagerService = passwordManagerService;
        this.mainWindow = mainWindow;
    }

    //This is our second overloaded method so that we can reuse the original add password window and just change a few things
    public AddPasswordWindow(PasswordManagerService passwordManagerService, MainWindow mainWindow, PasswordEntry entryToEdit) : this(passwordManagerService, mainWindow)
    {
        InitializeComponent();
        Title = "Edit Password";
        this.entryToEdit = entryToEdit;

        WebsiteTextBox.Text = entryToEdit.Website;
        UsernameTextBox.Text = entryToEdit.Username;
        PasswordTextBox.Text = entryToEdit.Password;
        NotesTextBox.Text = entryToEdit.Notes;
    }

    private void SaveNewPassword_Click(object? sender, RoutedEventArgs e)
    {
        bool isValid = true;

        //The meaning of this '?? ""' is if Text has a value then use it, else if it's null then use ""
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
        }

        //Stop here if the validation fails, the error messages should alert the user
        if (!isValid)
        {
            return;
        }

        //If there is no selected entry to edit then we create a new entry
        if (entryToEdit == null)
        {
            //If all the required fields are filled in then we can add the password
            if (isValid)
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
            }
        }
        //Else, there must be an entry selected so we edit it
        else
        {
            entryToEdit.Website = WebsiteTextBox.Text ?? "";
            entryToEdit.Username = UsernameTextBox.Text ?? "";
            entryToEdit.Password = PasswordTextBox.Text ?? "";
            entryToEdit.Notes = NotesTextBox.Text ?? "";
            entryToEdit.LastUpdated = DateTime.Now;
        }

        //Calls the refresh method
        mainWindow.RefreshList();
        //Closes this popup window and sends the data in entry back to mainwindow
        Close();
    }

    private void CloseNewPassword_Click(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}
