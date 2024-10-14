
using System.Net.Cache;

Student Tom = new Student("Томас", 23);                   // 1 способ создания 
Tom.WriteInfo();
Student Jhon = new Student()                              // 2 способ создания 
{
    name = "Джон",
    age = 24
};                                                                   
Jhon.WriteInfo();
Student Artur = new();        // 3 способ создания 
Artur.name = "Артур";
Artur.age = 27;
Artur.WriteInfo();
StudHelper.BecomeOlder(Artur);
Student.HowManyStudents();


class Student
{
    private string? _name;
    public int Age;
    private static int countStudent;       // статическое поле

    public static int StudentCount                                      //статическое свойство
    {
        get { return countStudent; }
    }

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
    public Student()
    {
        countStudent++;
    }
    public Student(string name, int age)
    {
        _name = name;
        Age = age;
        countStudent++;
    }
    static Student()                  //статический конструктор 
    {
        countStudent = 0;
        
    }
    
    public void WriteInfo()
    {

        if (age == 0)
        {
            Console.WriteLine("Студент: " + _name);
        }
        else
        {
            Console.WriteLine("Студент: " + _name + ", возраст: " + age);
        }
    }


    public static void HowManyStudents()                    //статический метод
    {
        Console.WriteLine("Всего студентов: " + countStudent);
    }
}

static class StudHelper                    //статический класс
{
    public static void BecomeOlder(Student student)
    {
        student.age++;
        Console.WriteLine("Студент " + student.name + " стал старше, теперь ему " + student.age );
    }
}