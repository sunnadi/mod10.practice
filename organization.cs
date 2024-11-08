using System;
using System.Collections.Generic;

public interface IOrganizationComponent
{
    string Name { get; set; }
    double Salary { get; set; }
    int GetCountEmployee();
    double GetSum();
    void Display(int indent = 0);
}

public abstract class OrganizationComponent : IOrganizationComponent
{
    public string Name { get; set; }
    public double Salary { get; set; }

    public abstract int GetCountEmployee();
    public abstract double GetSum();

    public virtual void Display(int indent = 0)
    {
        Console.WriteLine(new string(' ', indent) + Name + " (Salary: " + Salary + ")");
    }
}

public class Employee : OrganizationComponent
{
    public string Position { get; set; }

    public Employee(string name, string position, double salary)
    {
        Name = name;
        Position = position;
        Salary = salary;
    }

    public override int GetCountEmployee()
    {
        return 1; 
    }

    public override double GetSum()
    {
        return Salary; 
    }

    public override void Display(int indent = 0)
    {
        base.Display(indent);
        Console.WriteLine(new string(' ', indent) + "Position: " + Position);
    }
}

public class Department : OrganizationComponent
{
    private List<OrganizationComponent> _components = new List<OrganizationComponent>();

    public Department(string name)
    {
        Name = name;
    }

    public void Add(OrganizationComponent component)
    {
        _components.Add(component);
    }

    public void Remove(OrganizationComponent component)
    {
        _components.Remove(component);
    }

    public override int GetCountEmployee()
    {
        int count = 0;
        foreach (var component in _components)
        {
            count += component.GetCountEmployee();
        }
        return count;
    }

    public override double GetSum()
    {
        double sum = 0;
        foreach (var component in _components)
        {
            sum += component.GetSum();
        }
        return sum;
    }

    public override void Display(int indent = 0)
    {
        base.Display(indent);
        foreach (var component in _components)
        {
            component.Display(indent + 2); 
        }
    }

    public void DisplayAllEmployees(int indent = 0)
    {
        foreach (var component in _components)
        {
            if (component is Employee employee)
            {
                employee.Display(indent);
            }
            else if (component is Department department)
            {
                department.DisplayAllEmployees(indent + 2);
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Employee emp1 = new Employee("Alice", "Developer", 60000);
        Employee emp2 = new Employee("Bob", "Designer", 50000);
        Employee emp3 = new Employee("Charlie", "Manager", 80000);

        Department devDepartment = new Department("Development");
        devDepartment.Add(emp1);
        devDepartment.Add(emp2);

        Department hrDepartment = new Department("HR");
        hrDepartment.Add(emp3);

        Department mainDepartment = new Department("Main Department");
        mainDepartment.Add(devDepartment);
        mainDepartment.Add(hrDepartment);

        Console.WriteLine("Organization Structure:");
        mainDepartment.Display();
       
        Console.WriteLine($"Total Salary in Development Department: {devDepartment.GetSum()}");
        Console.WriteLine($"Total Employees in Main Department: {mainDepartment.GetCountEmployee()}");

        Console.WriteLine("nEmployees in Development Department:");
        devDepartment.DisplayAllEmployees();
    }
}

