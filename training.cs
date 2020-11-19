using System;
using System.Security.Cryptography.X509Certificates;

namespace ConsoleApp1
{
    class People
    {
        Staff[] data;
        public static int counter = 0; // Общее поле для всех обЪектов
        public string name;
        public int age=0; // можно проинициализировать сразу в отличие от структуры
        public int Age  // свойство
        {
            get
            {
                return Age;
            }
            set
            {
                if (Age < 0) Console.WriteLine("Возраст некорректен"); // Например, нам надо установить проверку по возрасту
            }
        }

        public People(int group)
        {
            data = new Staff[group];
            counter++;
        }
        public Staff this[int index] // индексатор
        {
            get
            {
                return data[index];
            }
            set
            {
                data[index] = value; //Ключевое слово value ссылается на значение, которое клиентский код пытается присвоить свойству или индексатору.
            }
        }
        public People() : this("Неизвестно") //Вызывается второй конструктор
        {
        }
        public People(string name) : this(name, 18) // Вызовет третий
        {
        }
        public People(string name, int age)
        {
            this.name = name; // Ключевое слово this представляет ссылку на текущий экземпляр класса
            this.age = age;
            counter++;

        }

        public virtual void GetInfo() // Можно переопределять в унаследованных классах
        {
            Console.WriteLine($"Name : {name} Age : {age}"); 
        }

        public override string ToString() // Переопределение метода ToString 
        {
            return name;
        }

        public override int GetHashCode() // два объекта People, которые имеют одно и то же имя, будут возвращать один и тот же хеш-код.
        {
            return name.GetHashCode(); 
        }
        public override bool Equals(object obj) // переопределение метода  Equals (сравнение по имени )
        {
            if (obj.GetType() != this.GetType()) return false;
            People people = (People)obj;
            return (this.name == people.name);
        }

        public static void DisplayCounter() // общий статический метод для всех объектов класса People. Результат его выполнения не зависит от конкретного обЪекта
        {
            Console.WriteLine($"counter = {counter}");
        }

        public static bool operator > (People p1 , People p2) // перегрузка оператора > для обЪектов класса People ( сравнение по возрасту )
        {
            if (p1.age > p2.age) return true;
            else return false;
        }
        public static bool operator <(People p1, People p2) // то же можно сделать и для сравнения ==  Если мы переопределяем одну из этих операций сравнения, то мы также должны переопределить вторую из этих операций.
        {
            if (p1.age < p2.age) return true;
            else return false;
        }
    }

    class Student : People // Student наследуется от People
    {
        public string University { get; set; }
        public int Course { get; set; }

        public Student(string name, int age, string university, int course)
            :base (name,age) // Ключевое слово base вызывает соответствующий код класса People
        {
            this.University = university;
            this.Course = course;
        }
        public override void GetInfo() // переопределение
        {
            base.GetInfo(); // Вызываем тот же метод из базового класса
            Console.WriteLine($"University : {University} Course: {Course}" );
        }
       
    }
    struct Staff
    {
        public string name;
        public string position;

        public Staff(string name, string position) // Все поля структуры должны быть проинициализированы
        {
            this.name = name; // Нельзя проинициализировать сразу, в отличие от класса
            this.position = position;

        }
        public void GetInfo()
        {
            Console.WriteLine($"Name : {name} Position : {position}");
        }

        abstract class Figure // Абстрактный класс
                              //мы не можем использовать конструктор абстрактного класса для создания его объекта
        {
            public abstract double Perimeter(); // абстрактные методы должны быть переопределены с помощью override в унаследованных классах
            public abstract double Area();
        }
        class Rectangle : Figure // Класс прямоугольников наследуется от абстрактного класса фигур
        {
            public float Width { get; set; }
            public float Height { get; set; }

            public Rectangle(float width, float height)
            {
                this.Width = width;
                this.Height = height;
            }
            // переопределение получения периметра ( это надо сделать обязательно, иначе ошибка )
            public override double Perimeter()
            {
                return Width * 2 + Height * 2;
            }
            // переопрелеление получения площади
            public override double Area()
            {
                return Width * Height;
            }
        }
    }
    class Account<T> // обобщенный класс
    {
        public T Id = default(T); // начальное значение по умолчанию
        public int Sum { get; set; }

    }
    class training
    {
        static void Main(string[] args)
        {
            People Tom = new People("Tom", 20); // Инициализация объекта класса
            Tom.GetInfo();
            People Kate = new People("Kate", 22);
            Tom = Kate; // Получает ссылку на объект Kate
            Kate.age = 23;
            Tom.GetInfo(); // Класс это составной ссылочный тип
            Staff Dan = new Staff("Dan", "teacher"); // Инициализация объекта структуры
            Dan.GetInfo();
            Staff Mark = new Staff("Mark", "manager");
            Dan = Mark;// Получают копию объекта Mark
            Mark.position = "director";
            Dan.GetInfo(); // Все равно будет manager, а не director
            People people = new People(5); // мы можем работать с объектом People как с набором объектов Staff:
            people[0] = new Staff("Fill", "cleaner");
            people[0].GetInfo();
            Staff John = new Staff("John", "teacher");
            people[1] = John;
            People Kostya = new Student("Kostya", 20, "NSU", 3); // объект класса Student также является объектом класса People:
            People.DisplayCounter(); // Сколько создали объектов People
            Kostya.GetInfo();   //   не можем использовать функцию GetUniversityInfo    
            Student student = (Student)Kostya; // Нисходящее преобразование
            student.GetInfo(); // Теперь можем использовать функцию GetUniversityInfo

            Console.WriteLine(student.ToString());
            People p1 = new People();
            Console.WriteLine(p1); //неявно вызывается метод ToString() 
            object p2 = new Student("Dima", 24, "Omsu", 4);
            if (p2.GetType() == typeof(Student)) Console.WriteLine("Это реально объект класса Student"); // typeof возвращает тип класса, метод GetType() возвращает тип объекта
            Account<int> ac1 = new Account<int> { Sum = 20000 };
            Console.WriteLine(ac1.Id); // Значение по умолчанию 0
            Account<string> ac2 = new Account<string> { Id = "qwert", Sum = 300000 };

        }
    }
}
