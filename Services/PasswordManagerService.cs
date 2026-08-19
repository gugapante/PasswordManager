//This is where the passwords live while the app is running
using System.Collections.Generic;
using System.Linq;
using PasswordManager.Models;

namespace PasswordManager.Services;

public class PasswordManagerService
{
    private List<PasswordEntry> passwordList = new List<PasswordEntry>();

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
}