
Student Tom = new Student("tom",2);
Tom.WriteInfo();
Tom.name = "Tom";
Tom.age = 18;
Tom.WriteInfo();
Tom.BecomeOlder();
Tom.WriteInfo();

Student Tim = new Student("213");
Tim.name = "Tim";
Tim.WriteInfo();


class Student
{
    private string? _name;
    public int Age;

    public string name
    {
        get { return _name; }
        set { _name = value; }
    }

    public int age
    {
        get { return Age; }
        set { Age = value; }
    }
    public Student(string name, int age)
    {
        _name = name;
        Age = age;
    }
    public Student(string name)
    {
        _name = name;
    }

    public void WriteInfo()
    {
        if (age == 0)
        {
            Console.WriteLine("Студент: " + name);
        }
        else
        {
            Console.WriteLine("Студент: " + name + ", возраст: " + age);
        }
    }

    public void BecomeOlder()
    {
        age ++;
    }
}

