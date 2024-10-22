
class Program
{
    private static void Main(string[] args)
    {
        try
        {
            Student Jhon = new("Джон", "Шелби", "Итнит", 25);
            Student Tomas = new("Томас", "Шелби", "итнит", -2);
            Student Artur = new("Артур", "Шелби", "", 25);

            Jhon.Age = -5;                                  // пример использования throw
           
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            Console.WriteLine("Программа завершена");
        }
       
    }
}

class Student : Person
{
    public string? inst;
    public int age;

    public string? Inst
    {
        get { return inst; }
        set {
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception("Институт не может быть пустым");
            }
            inst = value; }
    }

    public int Age
    {
        get { return age; }
        set
        {
            if (value < 0)
            {
                throw new Exception("Ошибка при смене возраста: возраст не может быть отрицательным");
            }
            age = value;
        }
    }

    public Student(string name, string secondName) : base(name, secondName) { }

    public Student(string name, string secondName, string Inst, int Age) : base(name, secondName)
    {
        try
        { 
            if (string.IsNullOrWhiteSpace(Inst)) throw new Exception();
            if (Age < 0) throw new Exception();
            age = Age;
            inst = Inst;
            Console.WriteLine($"Студент {name} создан");
        }
        catch (Exception ex) when (string.IsNullOrWhiteSpace(Inst))
        {
            Console.WriteLine($"Ошбика при создании {name}: институт не может быть пустым");
        }
        catch (Exception ex) when (Age < 0)
        {
            Console.WriteLine($"Ошибка при создании {name}. Возраст не может быть отрицательным");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
       
    }
    public Student(string name, string secondName, string Inst) : base(name, secondName)
    {

        try
        {
            if (string.IsNullOrWhiteSpace(Inst)) throw new Exception();
            inst = Inst;
            Console.WriteLine($"Студент {name} создан");
        }
        catch (Exception ex) when (string.IsNullOrWhiteSpace(Inst))
        {
            Console.WriteLine($"Ошбика при создании {name}: институт не может быть пустым");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
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

    public void BecomeOlder()
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
    public string? secondName { get; }

    public Person(String Name, String SecondName)
    {
        name = Name;
        secondName = SecondName;
    }

    public void Walk()
    {

        Console.WriteLine($"{name} {secondName} Идёт в университет!");
    }
}

class ITStudent : Student
{
    public string? lvl { get; set; }

    public ITStudent(String Name, String SecondName, string Lvl) : base(Name, SecondName)
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