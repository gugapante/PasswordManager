//This is my model, it is only responsible for representing one password at a time
using System;

namespace PasswordManager.Models;

//This describes what one password looks like, the data a password has
public class PasswordEntry
{
    //This is a randomly generated identifier attached to each entry. This allows us to track which entry is currently selected and delete/edit it
    public Guid ID {get; set;} = Guid.NewGuid();
    public string Website {get; set;} = "";
    public string Username {get; set;} = "";
    public string Password {get; set;} = "";
    public string Notes {get; set;} = "";
    public DateTime LastUpdated {get; set;}

    //There are no methods in this script, it is just data
}