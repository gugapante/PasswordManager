//This gives us access to true random generators, as using Rand is too easily guessed
using System.Security.Cryptography;
//Gives us access to string builder
using System.Text;

namespace PasswordManager.Services;

public class PasswordGeneratorService()
{
    //A ,ethod that returns a string and has parameters that sets a default passwordlength of 16 and if it includes special characters
    public string GeneratePassword(int passwordLength = 16, bool includeSpecials = true)
    {
        //We use const here as these strings cannot be changed from elsewhere in the code
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        const string specials = "!@#£$%^&*()_+-=[]{}|";

        //A ternary operator (shorter version of 'if / else') to determine whether we want special characters
        string validChars = includeSpecials ? chars + specials : chars;

        StringBuilder generatedPassword = new();

        for (int i = 0; i < passwordLength; i++)
        {
            //validChars.Length measures the total count of allowed characters
            //RandomNumberGenerator.GetInt32 Picks a cryptographically secure random number between 0 and the length of validChars
            //generatedPassword.Append takes the picked character and places it at the end of the string
            generatedPassword.Append(validChars[RandomNumberGenerator.GetInt32(validChars.Length)]);
        }

        //Converts the string builder object back to a string and returns it
        return generatedPassword.ToString();

    }
}