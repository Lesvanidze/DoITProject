using System;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            try
            {
                Console.WriteLine("Console Calculator");
                Console.WriteLine("Enter the first number:");
                double num1 = double.Parse(Console.ReadLine());

                Console.WriteLine("Enter the second number:");
                double num2 = double.Parse(Console.ReadLine());

                Console.WriteLine("Choose the operation [+ - * /]:");
                char operation = char.Parse(Console.ReadLine());

                double result = 0;

                switch (operation)
                {
                    case '+':
                        result = num1 + num2;
                        break;
                    case '-':
                        result = num1 - num2;
                        break;
                    case '*':
                        result = num1 * num2;
                        break;
                    case '/':
                        if (num2 != 0)
                        {
                            result = num1 / num2;
                        }
                        else
                        {
                            throw new DivideByZeroException("Cannot divide by zero.");
                        }
                        break;
                    default:
                        throw new ArgumentException("Invalid operation.");
                }

                Console.WriteLine($"Result: {result}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input format. Please enter a valid number.");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Number is too large or too small.");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Do you want to perform another calculation? (Y/N)");
            char choice = Console.ReadKey().KeyChar;
            Console.WriteLine();
            if (char.ToUpper(choice) != 'Y' && char.ToUpper(choice) != 'N')
            {
                throw new ArgumentException("Invalid choice. Please enter 'Y' or 'N'.");
            }
            if (char.ToUpper(choice) != 'Y')
            {
                break;
            }
        }
    }
}
