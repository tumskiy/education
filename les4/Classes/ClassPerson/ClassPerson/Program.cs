class Person
{
    public Name name = new Name();
    public int id;
    //public byte age;
    public char gender;
    public bool isworking;
    public DateTime dateOfBirth;
    private DateTime today = DateTime.Today;
    public void Print()
    {
        Console.WriteLine(
            $"\nИмя: {name.firstname}" +
            $"\nФамилия: {name.secondname}" +
            $"\nОтчество: {name.thirdname}" +
            $"\nКод: {id}" +
            $"\nВозраст: {Age}" +
            $"\nПол: {gender}"
            );
    }
    public DateTime Age() 
    {
        return new DateTime(today.Millisecond - dateOfBirth.Millisecond);        
    }
}
class Name
{
    public string firstname;
    public string secondname;
    public string thirdname;
    public void Print()
    {
        Console.WriteLine($"\nИмя{firstname}+\nФамилия{secondname}+\nОтчество{thirdname}");
    }
}
class Worker : Person
{
    public string structure;
    public string department;
    public string post;
    public void Print()
    {
        if (isworking == true)
        {
            Console.WriteLine($"\nПодразделение{structure}+\nОтдел{department}+\nДолжность{post}");
        }
    }
}
class User : Person
{
    public string company;
    public string contractNumber;
    public void Print()
    {
        if (isworking == false)
        {
            Console.WriteLine($"\nКомпания{company}+\n№Договора{contractNumber}");
        }
    }
}
class Program
{
    static void Main()
    {
        Worker firstUser = new Worker();
        firstUser.id = 1;
        firstUser.gender = 'M';
        firstUser.isworking = true;
        firstUser.name.firstname = "Vasiliy";
        firstUser.name.secondname = "--";
        firstUser.name.thirdname = "Alibabaevich";
        firstUser.dateOfBirth = new DateTime(1999, 2, 15);
        ///////////////////////////////////////
        firstUser.structure = "SIT";
        firstUser.department = "DUP";
        firstUser.post = "GIP";
        //////////////////////////////////////
        firstUser.Print();
    }
}