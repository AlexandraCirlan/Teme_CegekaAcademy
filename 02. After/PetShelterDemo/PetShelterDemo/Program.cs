//// See https://aka.ms/new-console-template for more information
//// Syntactic sugar: Starting with .Net 6, Program.cs only contains the code that is in the Main method.
//// This means we no longer need to write the following code, but the compiler still creates the Program class with the Main method:
//// namespace PetShelterDemo
//// {
////    internal class Program
////    {
////        static void Main(string[] args)
////        { actual code here }
////    }
//// }

using PetShelterDemo.DAL;
using PetShelterDemo.Domain;

internal class Program
{
    private static void Main(string[] args)
    {
        var shelter = new PetShelter();

        Console.WriteLine("Hello, Welcome the the Pet Shelter!");

        var exit = false;
        try
        {
            while (!exit)
            {
                PresentOptions(
                    "Here's what you can do.. ",
                    new Dictionary<string, Action>
                    {
                { "Register a newly rescued pet", RegisterPet },
                { "Donate", Donate },
                { "See current donations total", SeeDonations },
                { "See our residents", SeePets },
                { "Break our database connection", BreakDatabaseConnection },
                { "Fundraisers", Fundraisers },
                { "Leave:(", Leave }
                    }
                );
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Unfortunately we ran into an issue: {e.Message}.");
            Console.WriteLine("Please try again later.");
        }


        void RegisterPet()
        {
            var name = ReadString("Name?");
            var description = ReadString("Description?");

            var pet = new Pet(name, description);

            shelter.RegisterPet(pet);
        }

        void Donate()
        {
            Console.WriteLine("What's your name? (So we can credit you.)");
            var name = ReadString();

            Console.WriteLine("What's your personal Id? (No, I don't know what GDPR is. Why do you ask?)");
            var id = ReadString();
            var person = new Person(name, id);

            Console.WriteLine("How much would you like to donate? (RON)");
            var amountInRon = ReadInteger();
            shelter.Donate(person, amountInRon);
        }

        void SeeDonations()
        {
            Console.WriteLine($"Our current donation total is {shelter.GetTotalDonationsInRON()}RON");
            Console.WriteLine("Special thanks to our donors:");
            var donors = shelter.GetAllDonors();
            foreach (var donor in donors)
            {
                Console.WriteLine(donor.Name);
            }
        }

        void SeePets()
        {

            var pets = shelter.GetAllPets();

            var petOptions = new Dictionary<string, Action>();
            foreach (var pet in pets)
            {
                petOptions.Add(pet.Name, () => SeePetDetailsByName(pet.Name));
            }

            PresentOptions("We got..", petOptions);
        }

        void SeePetDetailsByName(string name)
        {
            var pet = shelter.GetByName(name);
            Console.WriteLine($"A few words about {pet.Name}: {pet.Description}");
        }

        void BreakDatabaseConnection()
        {
            Database.ConnectionIsDown = true;
        }

        void Fundraisers()
        {
            Console.WriteLine("Gray needs surgery");
            Console.WriteLine("\x0A");
            Console.WriteLine("Description:  Gray, has been sick for more than a week now and he " +
                "has been very weak. He has lost a lot of weight and his stomach started to bloat. Please donate to support this case!");
            Console.WriteLine("\x0A");
            Console.WriteLine("Target: 5000 RON");
            Console.WriteLine("\x0A");
            Console.WriteLine("\x0A");

            PresentOptions_donations(
                   "Here's what you can do.. ",
                   new Dictionary<string, Action>
                   {
                { "Donate", Donate_fund },
                { "See donations total", SeeDonations_fund },
                { "Leave", Leave_fund }
                   }

                ) ;

        }

        void Leave()
        {
            Console.WriteLine("Good bye!");
            exit = true;
        }

        void Donate_fund()
        {
            var name = ReadString("Name?");
            Console.WriteLine("What's your personal Id?");
            var id_fund = ReadString();
            Console.WriteLine("Amount to donate in RON:");
            var amount = ReadInteger();
           
            var donor_fund = new Donor_fund(name,id_fund);

            shelter.Donate_fund(donor_fund, amount);
        }

        void SeeDonations_fund()
        {
            Console.WriteLine($"Our current donation total for the fundraiser is {shelter.GetTotalDonations_fund()}RON");
            Console.WriteLine("Special thanks to our donors:");
            
            var donors_fund = shelter.GetAllDonors_fund();
            
            foreach (var donor_f in donors_fund)
            {
                Console.WriteLine(donor_f.Name);
            }
        }

   
        void Leave_fund()
        {
            Console.WriteLine("Goodbye!");
            exit = true;
        }
        void PresentOptions(string header, IDictionary<string, Action> options)
        {

            Console.WriteLine(header);

            for (var index = 0; index < options.Count; index++)
            {
                Console.WriteLine(index + 1 + ". " + options.ElementAt(index).Key);
            }

            var userInput = ReadInteger(options.Count);

            options.ElementAt(userInput - 1).Value();
        }

        void PresentOptions_donations(string header, IDictionary<string, Action> options_fund)
        {

            Console.WriteLine(header);

            for (var index = 0; index < options_fund.Count; index++)
            {
                Console.WriteLine(index + 1 + ". " + options_fund.ElementAt(index).Key);
            }

            var userInput = ReadInteger(options_fund.Count);

            options_fund.ElementAt(userInput - 1).Value();
        }

        string ReadString(string? header = null)
        {
            if (header != null) Console.WriteLine(header);

            var value = Console.ReadLine();
            Console.WriteLine("");
            return value;
        }

        int ReadInteger(int maxValue = int.MaxValue, string? header = null)
        {
            if (header != null) Console.WriteLine(header);

            var isUserInputValid = int.TryParse(Console.ReadLine(), out var userInput);
            if (!isUserInputValid || userInput > maxValue)
            {
                Console.WriteLine("Invalid input");
                Console.WriteLine("");
                return ReadInteger(maxValue, header);
            }

            Console.WriteLine("");
            return userInput;
        }
    }
}