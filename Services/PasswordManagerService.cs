//This is where the passwords live while the app is running
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.IO;
using PasswordManager.Models;
using System;

namespace PasswordManager.Services;

public class PasswordManagerService
{
    private readonly string filePath = Path.Combine(Directory.GetCurrentDirectory(), "passwords.json");
    private List<PasswordEntry> passwordList = new List<PasswordEntry>();
    
    public PasswordManagerService()
    {
        LoadFromFile();
    }
    //We then need methods to add passwords and maybe delete them
    public void AddPassword(PasswordEntry entry)
    {
        //call the list and add said entry passed as a parameter here
        passwordList.Add(entry);
    }

    public void RemovePassword(PasswordEntry entry)
    {
        //Checks that randomly generated identifier matches the selected one to the one in the list and removes it
        var itemToRemove = passwordList.FirstOrDefault(p => p.ID == entry.ID);

        if (itemToRemove != null)
        {
            passwordList.Remove(itemToRemove);
        }
    }

    //We also need a method to return the current list of passwords
    public List<PasswordEntry> GetPasswords()
    {
        return passwordList;
    }

    //Save current list to passwords.json
    public void SaveToFile()
    {
        //Try and Catch blocks are used to catch exceptions in order to stop the application from crashing
        try
        {
            //This line is needed to format the JSON file as it would normally output one continuous string of text
            var options = new JsonSerializerOptions {WriteIndented = true};
            //This converts the list from a C# object into a raw data bytes so taht it can be saved on the disk using the formatting option
            string json = JsonSerializer.Serialize(passwordList, options);
            //Opens the file from the file path and writes the string to iot and immediately closes it
            File.WriteAllText(filePath, json);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error saving passwords: {ex.Message}");
        }
    }

    //Tries to load the list from passwords.json if it exists
    private void LoadFromFile()
    {
        try
        {
            //Checks if there is a file with the name 'passwords.json' before trying to load anything
            if (File.Exists(filePath))
            {
                //This reads all content from the file and loads it onto a local string variable
                string json = File.ReadAllText(filePath);
                //This deserializes the data and in case the json is empty, we load in an empty list so it doesnt throw a null reference exception
                passwordList = JsonSerializer.Deserialize<List<PasswordEntry>>(json) ?? new List<PasswordEntry>();
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error loading passwords: {ex.Message}");
            //Instantiates an empty fresh list in case there is an exception
            passwordList = new List<PasswordEntry>();
        }
    }
}