using System;
using System.Diagnostics;
TestIsLowercaseLetter();
TestIsValidInput();
TestShiftLetter();
TestShiftMessage();

Console.WriteLine("this program encrypts the characters of a message using the Vigenere method");
Console.WriteLine("Please enter the message");
string userMessage = Console.ReadLine();

if (!IsValidInput(userMessage))
{
    Console.WriteLine("sory we only support lower case letters");
    return;
}

Console.WriteLine("please enter an enecryption key");
string userKey = Console.ReadLine();

if (!IsValidInput(userKey))
{
    Console.WriteLine("sorry we only support lower case letters.");
    return;
}

string encryptedResult = ShiftMessage(userMessage, userKey);
Console.WriteLine("Here is the encrypted message");
Console.WriteLine($"        {encryptedResult}");


// check if char is lowercase
bool IsLowercaseLetter(char c)
{
    return c >= 'a' && c <= 'z';
}

// validate input string
bool IsValidInput(string input)
{
    if (string.IsNullOrEmpty(input))
        return false;
    
    foreach (char c in input)
    {
        if (!IsLowercaseLetter(c))
        {
            return false;
        }
    }
    return true;
}

// shift one letter by the key
char ShiftLetter(char messageChar, char keyChar)
{
    int pos1 = messageChar - 'a';
    int pos2 = keyChar - 'a';
    // had to use 26 here to wrap around the letter
    int newPos = (pos1 + pos2) % 26;
    return (char)('a' + newPos);
}

// encrypt the whole message
string ShiftMessage(string msg, string key)
{
    string encrypted = "";
    
    for (int i = 0; i < msg.Length; i++)
    {
        // modulo makes the key repeat if message is longer
        char keyChar = key[i % key.Length];
        encrypted += ShiftLetter(msg[i], keyChar);
    }
    
    return encrypted;
}

void TestIsLowercaseLetter()
{
    Debug.Assert(IsLowercaseLetter('a'));
    Debug.Assert(IsLowercaseLetter('z'));
    Debug.Assert(!IsLowercaseLetter('A'));
}

void TestIsValidInput()
{
    Debug.Assert(IsValidInput("hello"));
    Debug.Assert(!IsValidInput("Hello"));
    Debug.Assert(!IsValidInput("hello world"));
}

void TestShiftLetter()
{
    Debug.Assert(ShiftLetter('e', 'b') == 'f');
    Debug.Assert(ShiftLetter('z', 'b') == 'a');
    Debug.Assert(ShiftLetter('a', 'a') == 'a');
}

void TestShiftMessage()
{
    Debug.Assert(ShiftMessage("endzz", "b") == "foeaa");
    Debug.Assert(ShiftMessage("endzz", "bc") == "fpeba");
}