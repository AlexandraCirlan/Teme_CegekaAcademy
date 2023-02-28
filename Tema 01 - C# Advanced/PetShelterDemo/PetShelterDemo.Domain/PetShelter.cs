using PetShelterDemo.DAL;

namespace PetShelterDemo.Domain;

public class PetShelter
{
    private readonly IRegistry<Pet> petRegistry;
    private readonly IRegistry<Person> donorRegistry;
    private readonly IRegistry<Donor_fund> donor_fund_Registry;
    private int donationsInRon = 0;
    private int donations_fund = 0;

    public PetShelter()
    {
        donorRegistry = new Registry<Person>(new Database());
        petRegistry = new Registry<Pet>(new Database());
        donor_fund_Registry = new Registry<Donor_fund>(new Database());
    }

    public void RegisterPet(Pet pet)
    {
        petRegistry.Register(pet);
    }

    public IReadOnlyList<Pet> GetAllPets()
    {
        return petRegistry.GetAll().Result; // Actually blocks thread until the result is available.
    }

    public Pet GetByName(string name)
    {
        return petRegistry.GetByName(name).Result;
    }

    public Donor_fund GetByName_d(string name)
    {
        return donor_fund_Registry.GetByName_d(name).Result;
    }



    public void Donate(Person donor, int amountInRON)
    {
        donorRegistry.Register(donor);
        donationsInRon += amountInRON;
    }

    public void Donate_fund(Donor_fund  donor_fund, int amount)
    {
        donor_fund_Registry.Register(donor_fund);
        donations_fund += amount;
    }


    public int GetTotalDonationsInRON()
    {
        return donationsInRon;
    }

    public IReadOnlyList<Person> GetAllDonors()
    {
        return donorRegistry.GetAll().Result;
    }

    public int GetTotalDonations_fund()
    {
        return donations_fund;
    }
    public IReadOnlyList<Donor_fund> GetAllDonors_fund()
    {
        return donor_fund_Registry.GetAll().Result;
    }
}
