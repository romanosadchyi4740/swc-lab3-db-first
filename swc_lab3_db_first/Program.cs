using swc_lab3_db_first.Models;

namespace swc_lab3_db_first;

internal abstract class Program
{
    public static void Main()
    {
        ControlsUserPrompt();
        var context = new BookstoresContext();
        if (!int.TryParse(Console.ReadLine().Trim(), out var option))
        {
            Console.WriteLine("Invalid input.");
            return;
        }
        
        switch (option)
        {
            case 1:
                var books = context.Books.ToList();
                books.ForEach(book => Console.WriteLine(book.ToString()));
                break;

            case 2:
                var authors = context.Authors.ToList();
                authors.ForEach(author => Console.WriteLine(author.ToString()));
                break;

            case 3:
                var payments = context.Payments.ToList();
                payments.ForEach(payment => Console.WriteLine(payment.ToString()));
                break;
            
            default:
                Console.WriteLine("Invalid option. Please restart the program and choose a correct option.");
                return;
        }
    }

    private static void ControlsUserPrompt()
    {
        Console.WriteLine("1. Get books list.");
        Console.WriteLine("2. Get authors list.");
        Console.WriteLine("3. Get payments list.");
    }
}