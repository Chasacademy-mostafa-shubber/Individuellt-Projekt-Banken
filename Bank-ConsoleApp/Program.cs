using Bank_ConsoleApp;
using System;

public class Program
{
    public static void Main(string[] args)
    {

        //Array med 5 olika användare
        Account[] accounts = new Account[]
        {
            new Account{Username="Mostafa", Pincode="1987", Salaryaccount=28500.70f, Savingaccount=150000.50f},
            new Account{Username="David", Pincode="1984", Salaryaccount=35000.50f, Savingaccount=350000.50f },
            new Account{Username="Josefin", Pincode="1985", Salaryaccount=70000.70f, Savingaccount=500000.50f},
            new Account{Username="Karin", Pincode="1946", Salaryaccount=120000.75f, Savingaccount=350000.50f},
            new Account{Username="Mohammed", Pincode="1985", Salaryaccount=33000.50f, Savingaccount=408000.50f}
        };

        //Deklarerade 2 variabler.
        string username;
        string pincode;





        //Programmet startar med Login metoden
        Login();


        //Metod för inloggningen
        void Login()
        {

            Console.Clear();
            //Count variabeln räknar antal försök för inloggningen
            int Count = 0;
            Console.WriteLine("Välkommen ");

            //Fortsätter loppa igenom om du anger rätt användarnamn och pinkod
            //Programmet avslutas om count blir lika med 3
            while (true && Count < 3)
            {
                //Du måste välja både användarnamn och pinkod
                //Värden läggs in variablerna
                Console.WriteLine("Ange användarnamn");
                username = Console.ReadLine();
                Console.WriteLine("Ange pinkod");
                pincode = Console.ReadLine();

                //Variabeln hämtar informationen från användaren som vill logga in
                var userexist = accounts.FirstOrDefault(x => x.Username == username && x.Pincode == pincode);

               //Om användaren finns och du anger både rätt användarnamn och pinkod, fortsätter du till huvudmenyn
                if (userexist != null)
                {
                    Menu();
                }

                //Om du anger fel användarnman och pinkod, får du ett meddelande. 
                //Du kommer få ny chans
                else if (username == "" || pincode == "")
                {
                    Console.WriteLine("Användarnamnet och pinkoden får inte vara tomt\n");
                    continue;
                }

                // Här fortsätter loopen maximalt 3 gånger ifall man anger felaktig användarnamn och pinkod.
                //Programmet avslutas om personen inte lyckas logga in efter tredje försöket
               else if (userexist == null)
                {
                    Count++;
                    Console.WriteLine("Ogiltigt val");
                    continue;
                }
            }


        }

        //Metod för huvudmenyn
        void Menu()
        {
            Console.Clear();
            Console.WriteLine($"Nu har du fyra olika val {username}");
            Console.WriteLine("1. Se dina konton och saldo");
            Console.WriteLine("2. Överföring mellan konton");
            Console.WriteLine("3. Ta ut pengar");
            Console.WriteLine("4. Logga ut");

            while (true)
            {

                //Du måste välja ett nummer för att komma åt metoderna
                string UserInput = Console.ReadLine();

                //Om du väljer ett nummer mellan 1-4, kommer du åt metoderna
                if (UserInput == "1")
                {
                    Balance();

                }

                else if (UserInput == "2")
                {
                    Transfer();
                }

                else if (UserInput == "3")
                {
                    withdraw();
                }

                else if (UserInput == "4")
                {
                    Login();
                }

                //Om du inte väljer ett nummer mellan 1-4, kommer du få ett meddelande.
                //Får nya försök varje gång du väljer fel nummer
                else if (UserInput != "1" || UserInput != "2" || UserInput != "3" || UserInput != "4")
                {
                    Console.WriteLine("Ogiltigt val. Försök igen");
                    continue;
                }
            }
        }

        //Metod för att se saldon i konton
        void Balance()
        {
            Console.Clear();

            //Hämtar information om saldon i konton för användaren som är inloggad
            foreach (var user in accounts.Where(x=> x.Username==username))
            {
                Console.WriteLine($"Lönekonto: {user.Salaryaccount}");
                Console.WriteLine($"Sparkonto: {user.Savingaccount}");

                Console.WriteLine("Tryck enter för att komma till huvudmenyn");

                //Kommer tillbaka till huvudmenyn om du trycker på enter
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    Menu();
                }
            }

           
        }

        //Metod för att kunna överföra pengar mellan konton
        void Transfer()
        {
            Console.Clear();
   
            Console.WriteLine("1. Överföring från lönekonto till kapitalkonto");
            Console.WriteLine("2. Överföring från sparkonto till lönekonto");
            Console.WriteLine("3. Gå tillbaka till huvudmenyn");
          
            while (true)
            {
                //Väljer ett konto
                string UserInput = Console.ReadLine();
                
                if(UserInput == "1")
                {
                    Console.WriteLine("Ange belopp");
                    
                    //Loopar igenom den inloggade användaren
                    foreach (var user in accounts.Where(x=> x.Username==username))
                    {
                        try
                        {
                            //Du väljer en summa
                            float amount = float.Parse(Console.ReadLine());
                            //Om summan finns i lönekontot kontot, överförs pengarna
                            if (amount < user.Salaryaccount)
                            {
                                //Summan försvinner från ena kontot och sätts in i andra kontot
                                user.Salaryaccount -= amount; 
                                user.Savingaccount += amount; 
                                Console.WriteLine($"Nu har du {user.Salaryaccount} i lönekontot" +
                                    $" och {user.Savingaccount} i sparkontot");
                                Console.WriteLine("Tryck enter för att komma till huvudmenyn");

                                //Kommer till huvudmenyn om du trycker på enter
                                if (Console.ReadKey().Key == ConsoleKey.Enter)
                                {
                                    Menu();
                                }

                            }

                            //Om du inte har så mycket pengar i kontot, får du ett meddelande
                            else if (amount > user.Salaryaccount)
                            {
                                Console.WriteLine("Täckning saknas\nTryck enter för att komma till huvudmenyn");

                                //Kommer till huvudmenyn om du trycker på enter
                                if (Console.ReadKey().Key == ConsoleKey.Enter)
                                {
                                    Menu();
                                }
                            }
                        }

                        catch(Exception e)
                        {
                            //Får ett felmeddelande om du inte väljer en summa
                            Console.WriteLine(e.Message);
                        }
                    }
                }

                else if(UserInput == "2")
                {
                    Console.WriteLine("Ange belopp");
                    foreach (var user in accounts.Where(x=> x.Username==username))
                    {
                        try
                        {
                            //Du väljer en summa
                            float amount = float.Parse(Console.ReadLine());

                            //Om summan finns i sparkontot, överförs pengarna
                            if (amount < user.Savingaccount)
                            {
                                //Summan försvinner från ena kontot och sätts in i andra kontot
                                user.Savingaccount -= amount; 
                                user.Salaryaccount += amount; 
                                Console.WriteLine($"Nu har du {user.Salaryaccount} i lönekontot" +
                                    $" och {user.Savingaccount} i sparkontot");
                                Console.WriteLine("Tryck enter för att komma till huvudmenyn");

                                //Kommer till huvudmenyn om du trycker på enter
                                if (Console.ReadKey().Key == ConsoleKey.Enter)
                                {
                                    Menu();
                                }
                            }

                            //Om du inte har så mycket pengar i kontot, får du ett meddelande
                            else if (amount > user.Savingaccount)
                            {
                                Console.WriteLine("Täckning saknas");
                                Console.WriteLine("Tryck enter för att komma till huvudmenyn");

                                //Kommer till huvudmenyn om du trycker på enter
                                if (Console.ReadKey().Key == ConsoleKey.Enter)
                                {
                                    Menu();
                                }
                            }

                            
                        }
                        catch (Exception e)
                        {
                            //Får ett felmeddelande om du inte väljer en summa
                            Console.WriteLine(e.Message);

                        }

                    }
                }

                //Kommer till huvudmenyn om du väljer nummer 3
                else if (UserInput == "3")
                {
                    Menu();
                }

                //Om du inte väljer ett nummer mallan 1-3, kommer du få ett meddelande
                //Du får en ny chans
               else if (UserInput != "1" || UserInput != "2" || UserInput != "3")
                {
                    Console.WriteLine("Ogiltigt val. Försök igen");
                    continue;
                 
                }

              
                
            }

        }


        //Metoden för att dra ut pengar från ett konto
        void withdraw()
        {
            Console.Clear();
            Console.WriteLine("1. Uttag från lönekontot");
            Console.WriteLine("2. Uttag från sparkontot");
            Console.WriteLine("3. Gå tillbaka till huvudmenyn");

            while(true)
            {
                //Väljer ett konto
                string UserInput = Console.ReadLine();

                if (UserInput == "1")
                {
                   //Loopar igenom användaren som är inloggad
                    foreach (var user in accounts.Where(x=> x.Username==username))
                    {
                       
                        try
                        {

                            //Välj en summa och ange pinkod
                            Console.WriteLine("Enge belopp");
                            float amount = float.Parse(Console.ReadLine());
                            Console.WriteLine("Ange pinkod");
                            string code = Console.ReadLine();

                            //Om pinkoden är korrekt och summan finns i kontot, lyckas du dra ut pengar
                            if (user.Pincode == code && amount < user.Salaryaccount)
                            {
                                //Summan försvinner från kontot
                                user.Salaryaccount -= amount;
                                
                                Console.WriteLine($"Nu har du {user.Salaryaccount} i lönekontot och " +
                                    $"{user.Savingaccount} i sparkontot");
                                Console.WriteLine("Tryck enter för att komma till huvudmenyn");

                                //Kommer till huvudmenyn om du trycker på enter
                                if (Console.ReadKey().Key == ConsoleKey.Enter)
                                {
                                    Menu();
                                }
                            }

                            //Får ett meddelande om du anger fel pinkod
                            else if (user.Pincode != code)
                            {
                                Console.WriteLine("Fel pinkod. Tryck enter för att komma till huvudmenyn");

                                //Kommer till huvudmenyn om du trycker på enter
                                if (Console.ReadKey().Key == ConsoleKey.Enter)
                                {
                                    Menu();
                                }
                            }

                            //Får ett meddelande om du inte har så mycket pengar i kontot
                            else if (amount > user.Salaryaccount)
                            {
                                Console.WriteLine("Täckning saknas. Tryck enter för att komma till huvudmenyn");

                                //Kommer till huvudmenyn om du trycker på enter
                                if (Console.ReadKey().Key == ConsoleKey.Enter)
                                {
                                    Menu();
                                }
                            }
                        }
                        catch(Exception e) 
                        {
                            //Får ett meddelande om du inte väljer en summa
                            Console.WriteLine(e.Message);
                            
                        }
                        

                       
                    }
                }

                else if(UserInput == "2")
                {

                    //Loppar igenom användaren som är inloggad
                    foreach (var user in accounts.Where(x=> x.Username==username))
                    {
                        try
                        {

                            //Välj en summa och ange pinkod
                            Console.WriteLine("Ange belopp");
                            float amount = float.Parse(Console.ReadLine());
                            Console.WriteLine("Ange pinkod");
                            string code = Console.ReadLine();

                            //Om pinkoden är korrekt och summan finns i kontot, lyckas du dra ut pengar
                            if (user.Pincode == code && amount < user.Savingaccount)
                            {
                                //Summan försvinner från kontot
                                user.Savingaccount -= amount;
                              
                                Console.WriteLine($"Nu har du {user.Salaryaccount} i lönekontot och " +
                                    $"{user.Savingaccount} i sparkontot");
                                Console.WriteLine("Tryck enter för att komma till huvudmenyn");
                                if (Console.ReadKey().Key == ConsoleKey.Enter)
                                {
                                    Menu();
                                }
                            }

                            //Får ett meddelande om du anger fel pinkod
                            else if (user.Pincode != code)
                            {
                                Console.WriteLine("Fel pinkod. Tryck enter för att komma till huvudmenyn");

                                //Kommer till huvudmenyn om du trycker på enter
                                if (Console.ReadKey().Key == ConsoleKey.Enter)
                                {
                                    Menu();
                                }
                            }

                            //Får ett meddelande om du inte har så mycket pengar i kontot
                            else if (amount > user.Savingaccount)
                            {
                                Console.WriteLine("Täckning saknas. Tryck enter för att komma till huvudmenyn");

                                //Kommer till huvudmenyn om du trycker på enter
                                if (Console.ReadKey().Key == ConsoleKey.Enter)
                                {
                                    Menu();
                                }
                            }
                        }
                        catch(Exception e)
                        {
                            //Får ett felmeddelande om du inte väljer en summa
                            Console.WriteLine(e.Message);
                        }
                    }
                }

                //Kommer till huvudmenyn om du väljer nummer 3
                else if(UserInput== "3")
                {
                    Menu();
                }


                //Får ett meddelande om du inte väljer nummer mellan 1-3
                //Får ny chans att välja
                else if (UserInput != "1" || UserInput != "2" || UserInput != "3")
                {
                    Console.WriteLine("Ogiltigt val. Försök igen");
                    continue;
                   
                }
            }

        }

    }
}
