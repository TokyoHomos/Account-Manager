using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account_Manager
{
    internal class Program
    {
        public static int chooseService;
        public static string chooseLetter;
        public static string platform;
        public static string email;
        public static string password;
        public static string playerNeed;
        public static int index = 0;

        public static int countLinesFile = 0;
        public static string checkLine;

        public static void LetterChoice()
        {
            Console.Write("\t\t\t\t\t\tNeed Return To Interface (Y/N): ");
            chooseLetter = Console.ReadLine();
            if (chooseLetter == "Y" || chooseLetter == "y")
            {
                Console.Clear();
                InterfacePage();
            }
            if (chooseLetter == "n" || chooseLetter == "N")
            {
                Environment.Exit(0);
            }
        }
        public static void InterfacePage()
        {
            Console.WriteLine("\t\t\t\t\t\t Account Manager");
            Console.WriteLine();
            Console.WriteLine("\t\t\t\t\t\t 1) Insert Data");
            Console.WriteLine("\t\t\t\t\t\t 2) Search Data");
            Console.WriteLine("\t\t\t\t\t\t 3) Print All Data");
            Console.WriteLine();
            Console.Write("\t\t\t\t\t\t Select A Service (1/2/3): ");
            chooseService = int.Parse(Console.ReadLine());

            if (chooseService == 1)
            {
                Console.Clear();
                InsertData();
            }
            else if (chooseService == 2)
            {
                Console.Clear();
                SearchData();
            }
            else
            {
                Console.Clear();
                PrintAllData();
            }

            LetterChoice();
        }
        public static void InsertData()
        {
            Console.Write("\t\t\t\t\t\tPlatform: ");
            platform = Console.ReadLine();
            Console.WriteLine("\t\t\t\t\t\t--------------");

            Console.Write("\t\t\t\t\t\tEmail: ");
            email = Console.ReadLine();
            Console.WriteLine("\t\t\t\t\t\t--------------");

            Console.Write("\t\t\t\t\t\tPassword: ");
            password = Console.ReadLine();
            Console.WriteLine("\t\t\t\t\t\t--------------");

            SaveClientData(platform,email,password);
        }
        public static void SearchData()
        {
            Console.Write("\t\t\t\t\t\tWhich Platform You Search: ");
            playerNeed = Console.ReadLine();

            SearchClientData(playerNeed);

        }
        public static void SaveClientData(string platform, string email, string password)
        {
            if (File.Exists("PasswordManeger.txt"))
            {
                using(StreamWriter saveAccount = new StreamWriter("PasswordManeger.txt", true))
                {
                    saveAccount.WriteLine($"{platform}");
                    saveAccount.WriteLine($"{email}");
                    saveAccount.WriteLine($"{password}");

                    saveAccount.Close();

                }
            }
            else
            {
                Console.WriteLine("The File toptenlist.txt does not Exist\n Check the path.."); // this for me to know if there is a problem or not
            }
        }
        public static void SearchClientData(string playerNeed)
        {

            if (File.Exists("PasswordManeger.txt"))
            {
                using (StreamReader searchAccount = new StreamReader("PasswordManeger.txt", true))
                {
                    while (!searchAccount.EndOfStream)
                    {
                        bool found = false;

                        Console.WriteLine($"\n\t\t\t\t\t\tResults for platform: {playerNeed}\n");
                        Console.WriteLine("\t\t\t\t\t\t-----------------------------");

                        while(!searchAccount.EndOfStream)
                        {
                            string platform =searchAccount.ReadLine();
                            string email =searchAccount.ReadLine();
                            string password =searchAccount.ReadLine();

                            if(platform.Contains(playerNeed))  // I can use this command because this command do same function " platform.Equals(playerNeed,StringComparison.OrdinalIgnoreCase) "
                            {
                                Console.WriteLine($"\t\t\t\t\t\tEmail: {email}");
                                Console.WriteLine($"\t\t\t\t\t\tPassword: {password}");
                                Console.WriteLine("\t\t\t\t\t\t-----------------------------");
                                found = true;
                            }
                        }

                        if (!found)
                        {
                            Console.WriteLine($"\t\t\t\t\t\tNo accounts found for platform: {playerNeed}");
                        }

                    }
                }
            }
            else
            {
                Console.WriteLine("The File toptenlist.txt does not Exist\n Check the path.."); // this for me to know if there is a problem or not
            }
        }
        public static void PrintAllData()
        {
            List<string[]> allAccounts = new List<string[]>();

            if (File.Exists("PasswordManeger.txt"))
            {
                using(StreamReader reader = new StreamReader("PasswordManeger.txt", true))
                {
                    
                    while(!reader.EndOfStream)
                    {
                        string platform = reader.ReadLine();
                        string email = reader.ReadLine();
                        string password = reader.ReadLine();

                        if(!string.IsNullOrEmpty(platform) &&  !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
                        {
                            allAccounts.Add(new string[] {  platform, email, password });
                        }
                        
                    }
                    reader.Close();
                }
                Console.WriteLine("\n\t\t\t\t\t\tAll Accounts: ");
                Console.WriteLine("\t\t\t\t\t\t-----------------------------");

                foreach (string[] account in allAccounts)
                {
                    Console.WriteLine($"\t\t\t\t\t\t{account[0]}");
                    Console.WriteLine($"\t\t\t\t\t\t{account[1]}");
                    Console.WriteLine($"\t\t\t\t\t\t{account[2]}");
                    Console.WriteLine($"\t\t\t\t\t\t-----------------------------");
                }
            }
            else
            {
                Console.WriteLine("The File toptenlist.txt does not Exist\n Check the path.."); // this for me to know if there is a problem or not
            }
        }
        static void Main(string[] args)
        {
            InterfacePage();
            LetterChoice();



            Console.ReadKey();

        }
    }
}
