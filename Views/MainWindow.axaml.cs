using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
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
        Console.WriteLine("Add Password button clicked");

        //The meaning of this '?? ""' is if Text has a value then use it else if it's null then use ""
        string website = WebsiteTextBox.Text ?? "";
        string username = UsernameTextBox.Text ?? "";
        string password = PasswordTextBox.Text ?? "";
        string notes = NotesTextBox.Text ?? "";

        Console.WriteLine("Website: " + website);
        Console.WriteLine("Username: " + username);
        Console.WriteLine("Password: " + password);
        Console.WriteLine("Notes: " + notes);
    }
}