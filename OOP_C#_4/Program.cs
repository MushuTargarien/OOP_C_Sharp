
Student Tommy = new Student("Томас", "Шелби", "Итнит",25);
Tommy.Walk();
Tommy.WriteInfo();
Tommy.BecomeOlder();
Console.WriteLine(Tommy);

Student Artur = new ITStudent("Артур", "Шелби", "Junior");
Artur.WriteInfo();                                      // переопределённый метод - будет выполняться из ITStudent
Artur.BecomeOlder();                                    // скрытый метод - будет выполняться из класса Student                                    

class Student : Person
{
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

    public Student(string name, string secondName) : base(name, secondName) { }
    
    public Student(string name, string secondName, string Inst, int Age) : base(name,secondName)
    {
        inst = Inst;
        age = Age;
    }
    public Student(string name, string secondName, string Inst) : base(name, secondName)
    {
        inst = Inst;
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

    public  void BecomeOlder()
    {
        age++;
        Console.WriteLine($"{name} {secondName},теперь на год старше ему {age}");
    }

    public override string ToString()
    {
        return ($"Значение было украдено, простите :( | это сделал {name}, только я вам это не говорил)");
    }
}

abstract class Person
{
    public string? name { get; }
    public string? secondName {  get; }

    public Person (String Name, String SecondName)
    {
        name = Name;
        secondName = SecondName;
    }

    public void Walk()
    {
        
        Console.WriteLine ($"{name} {secondName} Идёт в университет!");
    }
}

class ITStudent : Student 
{ 
    public string? lvl {  get; set; }

    public ITStudent( String Name, String SecondName, string Lvl) : base(Name, SecondName)
    { 
        lvl = Lvl;
    }
    public ITStudent(String Name, String SecondName) : base(Name, SecondName)
    {
    }

    public override void WriteInfo()
    {
        if (lvl != null)
        {
            Console.WriteLine($"{name} {secondName} уровень -  {lvl}");
        }
        else
        {
            Console.WriteLine($"{name} {secondName} ещё новичок");
        }
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
            default:
                lvl = "Junoir";
                Console.WriteLine($"{name} {secondName} теперь {lvl}");
                break;

        }
    }
}