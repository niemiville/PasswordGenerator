using System.Text;

public class PasswordGenerator
{
    private static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            bool isRunning = true;
            while (isRunning)
            {
                PrintExamplePasswords();
                Console.Write("Write \"q\" to quit, enter regenerates passwords: ");
                var userInput = Console.ReadLine();
                if (userInput == "q")
                {
                    isRunning = false;
                }
                Console.WriteLine();
            }
        }
        else
        {
            GeneratePasswordFromUserInput(args);
            Console.Write("\nPress any key to exit...");
            Console.ReadKey();
        }
    }

    public static void GeneratePasswordFromUserInput(string[] args)
    {
        int amount, length;
        PasswordType type;
        HandleArgs(args, out amount, out length, out type);
        List<string> passwords = GetPasswords(amount, length, type);
        PrintPasswords(passwords);
    }

    public static void PrintPasswords(List<string> passwords)
    {
        foreach (var password in passwords)
        {
            Console.WriteLine(password);
        }
    }

    public static void HandleArgs(string[] args, out int amount, out int length, out PasswordType type)
    {
        amount = ReadPasswordAmount(args[0]);
        length = ReadPasswordLength(args[1]);
        type = ReadPasswordType(args[2]);
    }

    public static int ReadPasswordAmount(string amountArg)
    {
        if (string.IsNullOrEmpty(amountArg))
        {
            return 1;
        }
        else
        {
            return Convert.ToInt32(amountArg);
        }
    }

    public static int ReadPasswordLength(string lengthArg)
    {
        if (string.IsNullOrEmpty(lengthArg))
        {
            return 1;
        }
        else
        {
            return Convert.ToInt32(lengthArg);
        }
    }

    public static PasswordType ReadPasswordType(string typeArg)
    {
        if (string.IsNullOrEmpty(typeArg))
        {
            return PasswordType.LettersNumbers;
        }
        else
        {
            return (PasswordType)Enum.Parse(typeof(PasswordType), typeArg);
        }
    }

    private static void PrintExamplePasswords()
    {
        var numbersLen4 = GetPasswords(3, 4, PasswordType.Numbers);
        var numbersLen6 = GetPasswords(3, 6, PasswordType.Numbers);
        var basicLen12 = GetPasswords(3, 12, PasswordType.LettersNumbers);
        var basicLen20 = GetPasswords(3, 20, PasswordType.LettersNumbers);
        var specialLen15 = GetPasswords(3, 15, PasswordType.LettersNumbersSpecials);
        List<List<string>> pws = new() { numbersLen4, numbersLen6, basicLen12, basicLen20, specialLen15 };
        foreach (var p in pws)
        {
            foreach (var s in p)
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

    private static readonly Dictionary<PasswordType, string> characterSets = new Dictionary<PasswordType, string>
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