using System;
using System.Collections.Generic;

class Warehouse
{
    public int numLetters { get; set; }
    public int numDigits { get; set; }
    public List<Product> products { get; set; }

    public Warehouse(int numLetters, int numDigits)
    {
        this.numLetters = numLetters;
        this.numDigits = numDigits;
        this.products = new List<Product>();
    }

    public void addProduct(string name)
    {
        string code = generateCode();
        Product product = new Product() { name = name, code = code };
        products.Add(product);
    }

    private string generateCode()
    {
        string code = "";
        for (int i = 0; i < numLetters; i++)
        {
            code += 'A';
        }
        for (int i = 0; i < numDigits; i++)
        {
            code += '0';
        }
        bool codeExists;
        do
        {
            codeExists = false;
            foreach (Product product in products)
            {
                if (product.code == code)
                {
                    codeExists = true;
                    break;
                }
            }
            if (codeExists)
            {
                char lastChar = code[code.Length - 1];
                if (lastChar == '9')
                {
                    char lastLetter = code[numLetters - 1];
                    if (lastLetter == 'Z')
                    {
                        throw new Exception("Cannot add more products.");
                    }
                    else
                    {
                        code = code.Substring(0, numLetters - 1) + (char)(lastLetter + 1);
                        code += '0';
                    }
                }
                else
                {
                    code = code.Substring(0, code.Length - 1) + (char)(lastChar + 1);
                }
            }
        } while (codeExists);
        return code;
    }

    public void searchProduct()
    {
        string searchCode = Console.ReadLine();
        foreach (Product product in products)
        {
            if (product.code == searchCode)
            {
                Console.WriteLine("Product name: " + product.name);
                return;
            }
        }
        Console.WriteLine("Not found!");
    }
}