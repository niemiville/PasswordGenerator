using System.Text;

class PasswordGenerator
{

    static void Main()
    {
        bool isRunning = true;
        while (isRunning)
        {
            GetExamplePasswords();
            Console.Write("Write \"q\" to quit, anything else regenerates passwords: ");
            var userInput = Console.ReadLine();
            if (userInput == "q") {
                isRunning = false;
            }
            Console.WriteLine();
        }
    }

    private static void GetExamplePasswords()
    {
        var numbersLen4 = GetPasswords(3, 4, PasswordType.Numbers);
        var numbersLen6 = GetPasswords(3, 6, PasswordType.Numbers);
        var basicLen12 = GetPasswords(3, 12, PasswordType.LettersNumbers);
        var basicLen20 = GetPasswords(3, 20, PasswordType.LettersNumbers);
        var specialLen15 = GetPasswords(3, 15, PasswordType.LettersNumbersSpecials);
        List<List<string>> pws = new() { numbersLen4, numbersLen6, basicLen12, basicLen20, specialLen15 };
        foreach(var p in pws)
        {
            foreach(var s in p)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine();
        }
    }

    public enum PasswordType
    {
        Numbers = 0,
        Letters = 1,
        LettersNumbers = 2,
        LettersNumbersSpecials = 3
    }

    readonly static Dictionary<PasswordType, string> characterSets = new Dictionary<PasswordType, string>
        {
            { PasswordType.Numbers, "0123456789" },
            { PasswordType.Letters, "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz" },
            { PasswordType.LettersNumbers, "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"},
            { PasswordType.LettersNumbersSpecials, "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!#¤%&/()=?@£$€{[]}€"}
        };

    public static List<string> GetPasswords(int amount, int length, PasswordType type = PasswordType.LettersNumbers)
    {
        Random random = new();
        StringBuilder stringBuilder = new();
        List<string> passwords = new();

        for (int a = 0; a < amount; a++)
        {
            for (int l = 0; l < length; l++)
            {
                int index = random.Next(characterSets[type].Length);
                stringBuilder.Append(characterSets[type][index]);
            }
            passwords.Add(stringBuilder.ToString());
            stringBuilder.Clear();
        }

        return passwords;
    }
}