

    string? subj = " ", gam = " ";
    Subject math = new Subject("Математика");
    Game lapta = new Game("Лапта");
    Student Tom = new Student("Том",18);
    //Tom._name = "Томми"            из-за того что у поля _name стоит модификатор доступа private мы не можем изменять его  вне класса
    Tom.subject = math;
    Tom.game = lapta;
    Tom.WriteInfo();
    Tom.WriteFavourite();
    Info(Tom, out subj, out gam);
    Console.WriteLine(subj + " " + gam);
    int vozrast = Tom.age;
    byValue(vozrast);                             //Здесь можно увидеть что увеличивая возрас по значению, конечный итог не сохранится
    Tom.age = vozrast;
    Tom.WriteInfo();
    byLink(ref vozrast);
    Tom.age = vozrast;
    Tom.WriteInfo();


static void Info( Student name, out string subj, out string gam)
{
    subj = name.subject.Title;
    gam = name.game.name;

}

static void byValue( int a)
{
    a++;
}

static void byLink(ref int a)
{
    a++;
}

class Student
{
    private string? _name;
    private int Age;
    private Subject? FavSubject;
    private Game? FavGame;

    public Subject? subject
    {
        get { return FavSubject; }
        set { FavSubject = value; }
    }

    public Game? game
    {
        get { return FavGame; }
        set { FavGame = value; }

    }

    public string? name
    {
        get { return _name; }
        set { _name = value; }
    }

    public int age
    {
        get { return Age; }
        set { Age = value; }
    }
    public Student(string name,  Subject favSubject, Game favGame)
    {
        _name = name;
        FavSubject = favSubject;
        FavGame = favGame; 
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


    public void WriteFavourite()
    {
        Console.WriteLine("Любимая игра студента " + _name + " - " + FavGame.name + ", а любимый предмет - " + FavSubject.title);
    }
}


class Subject
{
    public string? Title;
    public string title
    {
        get { return Title; }
        set { Title = value; }
    }

     public Subject(string subj)
    {
        Title = subj;
    }

}
class Game
{
    public string? name;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public Game(string game)
    {
        name = game;
    }
 
}
