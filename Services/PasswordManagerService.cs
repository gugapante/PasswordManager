//This is where the passwords live while the app is running
using System.Collections.Generic;
using PasswordManager.Models;

namespace PasswordManager.Services;

public class PasswordManagerService
{
    private List<PasswordEntry> passwords = new List<PasswordEntry>();

    //We then need methods to add passwords and maybe delete them
    public void AddPassword(PasswordEntry entry)
    {
        //call the list and add said entry passed as a parameter here
        passwords.Add(entry);
    }

    //We also need a method to return the current list of passwords
    public List<PasswordEntry> GetPasswords()
    {
        return passwords;
    }
}