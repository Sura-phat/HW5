using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        int numLetters = int.Parse(Console.ReadLine());
        int numDigits = int.Parse(Console.ReadLine());
        Warehouse warehouse = new Warehouse(numLetters, numDigits);
        string name;
        do
        {
            name = Console.ReadLine();
            if (name != "Stop")
            {
                warehouse.addProduct(name);
            }
        } while (name != "Stop");
        warehouse.searchProduct();
    }
}
