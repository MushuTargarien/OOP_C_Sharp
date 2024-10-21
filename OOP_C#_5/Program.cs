 class Program
{
    private static void Main(string[] args)
    {
        Student Tommy = new Student("Томас", "Шелби", "итнит");
        Tommy.Born();
        Tommy.WriteInfo();

        ITStudent Jhon = new ITStudent("Tomas", "Shelby", "Junior", "C#");
        Jhon.WriteInfo();
        ISpecialist spec = Jhon;
        spec.Work();

        Subject MAth = new Subject("Математика");
        Tommy.favouriteSubject = MAth;
        Tommy.WriteSubject();

        var Bob = (Student)Tommy.Clone();
        Bob.name = "БОБ";
        Bob.WriteInfo();
    }
}

class Student : IPerson, IClonable, IComparable
{
    public string? name { get; set; }
    public string? secondName { get; set; }
    public Subject? favouriteSubject { get; set; }
    public string? inst;
    public int age;

    public string Inst
    {
        get { return inst; }
        set { inst = value; }
    }

    public int Age
    {
        get { return age; }
        set { age = value; }
    }

    public Student() { }
    public Student(string Name, string SecondName) 
    {
        name = Name;
        secondName = SecondName;
    }
    
    public Student(string Name, string SecondName, string Inst, int Age) 
    {
        name = Name;
        secondName = SecondName;
        inst = Inst;
        age = Age;
    }
    public Student(string Name, string SecondName, string Inst) 
    {
        name = Name;
        secondName = SecondName;
        inst = Inst;
    }

    public object Clone()
    {
        return new Student(name, secondName, inst);
    }

    public int CompareTo(Student? obj)
    {
        if (obj == null)
        {
            return 1;
        }
        else
        {
            return name.CompareTo(obj.name);
        }
    }

    public virtual void WriteInfo()
    {
        if (age == 0)
        {
            Console.WriteLine($"Студент: {name} {secondName}, институт: {inst}");
        }
        else
        {
            Console.WriteLine($"Студент: {name} {secondName}, возраст: {age}, институт: {inst}");
        }
    }

    public void WriteSubject()
    {
        Console.WriteLine($"{name} {secondName} рассказал, что его любимый предмет - {favouriteSubject.subjName} ");
    } 

    public  void BecomeOlder()
    {
        age++;
        Console.WriteLine($"{name} {secondName},теперь на год старше ему {age}");
    }

    public override string ToString()
    {
        return ($"Значение было украдено, простите :( | это сделал {name}, только я вам это не говорил)");
    }

     public void Born()
    {
        Console.WriteLine($"Родился {name} {secondName}");
    }
}

class ITStudent : Student, ISpecialist
{ 
    public string? lvl {  get; set; }
    public string lang { get; set; }

    public ITStudent( String Name, String SecondName, string Lvl, string Lang) : base(Name, SecondName)
    { 
        lvl = Lvl;
        lang = Lang;
    }

    public override void WriteInfo()
    {
            Console.WriteLine($"{name} {secondName} уровень - {lvl}, ЯП - {lang}");
      
    }

    public new void BecomeOlder()
    {
        switch (lvl)
        {
            case "Junior":
                lvl = "Middle";
                Console.WriteLine($"{name} {secondName} теперь {lvl}");
                break;
            case "Middle":
                lvl = "Senior";
                Console.WriteLine($"{name} {secondName} теперь {lvl}");
                break;
            case " Senior":
                lvl = "TimLead";
                Console.WriteLine($"{name} {secondName} теперь {lvl}");
                break;
            case "TimLead":
                Console.WriteLine($"Выше некуда, {name} {secondName} и так {lvl}");
                break;
            default:
                lvl = "Junoir";
                Console.WriteLine($"{name} {secondName} теперь {lvl}");
                break;

        }
    }
    void  ISpecialist.Work()                              // явная реализация интерфейса 
    {
        Console.WriteLine($"{name} работает на {lang}");
    }

}

class Subject
{
    public string? subjName {  get; set; }

    public Subject(string? SubjName)
    {
        subjName = SubjName;
    }
}

interface ISpecialist : IPerson                 // интрефейс ISpecialist
{
    string lang { get; set; }
    void Work();
}

interface IPerson                           // интрефейс Iperson
{

    void Born()
    {

    }
}

internal interface IClonable
{
    object Clone();
}

internal interface IComparable
{
    int CompareTo(Student? obj);
}