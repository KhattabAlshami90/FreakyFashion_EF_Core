using FreakyFashion_EF_Core.Data;
using FreakyFashion_EF_Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using static System.Console;


 class Program
{
    static AppDbContesxt context = new AppDbContesxt();
  
    static void Main()
    {
        context.Database.Migrate();
        bool applicationRunning = true;
        bool isAccessGranted = false;
        ConsoleKeyInfo userInput;

        CursorVisible = false;
        using (var context = new AppDbContesxt())
        {
            context.Database.Migrate();

        }

        do
        {
            Logo.ClearScreen();
            CursorVisible = false;

            WriteLine($"1. Logga in");
            WriteLine($"2. Avsluta");



            bool invalidSelection = true;

            do
            {
                userInput = ReadKey(true);

                invalidSelection = !(
                    userInput.Key == ConsoleKey.D1 ||
                    userInput.Key == ConsoleKey.NumPad1 ||
                    userInput.Key == ConsoleKey.D2 ||
                    userInput.Key == ConsoleKey.NumPad2
                    );

            } while (invalidSelection);

            Logo.ClearScreen();
            CursorVisible = true;

            switch (userInput.Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    isAccessGranted = LogIn();
                    do
                    {
                        Huvudmeny();
                        do
                        {
                            userInput = ReadKey(true);

                            invalidSelection = !(
                                userInput.Key == ConsoleKey.D1 ||
                                userInput.Key == ConsoleKey.NumPad1 ||
                                userInput.Key == ConsoleKey.D2 ||
                                userInput.Key == ConsoleKey.NumPad2 ||
                                userInput.Key == ConsoleKey.D3 ||
                                userInput.Key == ConsoleKey.NumPad3 ||
                                userInput.Key == ConsoleKey.D4 ||
                                userInput.Key == ConsoleKey.NumPad4 ||
                                userInput.Key == ConsoleKey.D5 ||
                                userInput.Key == ConsoleKey.NumPad5 ||
                                userInput.Key == ConsoleKey.D6 ||
                                userInput.Key == ConsoleKey.NumPad6
                                );

                        } while (invalidSelection);
                        CursorVisible = true;
                        switch (userInput.Key)
                        {
                            case ConsoleKey.D1:
                            case ConsoleKey.NumPad1:

                                AddProduct();

                                break;

                            case ConsoleKey.D2:
                            case ConsoleKey.NumPad2:
                                SearchProduct();

                                break;

                            case ConsoleKey.D3:
                            case ConsoleKey.NumPad3:
                                AddCategory();

                                break;

                            case ConsoleKey.D4:
                            case ConsoleKey.NumPad4:
                                AddProductToCategory();

                                break;

                            case ConsoleKey.D5:
                            case ConsoleKey.NumPad5:
                                listOfCategories();

                                break;



                            case ConsoleKey.D6:
                            case ConsoleKey.NumPad6:
                                isAccessGranted = false;
                                break;
                        }
                    } while (isAccessGranted);

                    break;

                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    applicationRunning = false;

                    break;
            }
        } while (applicationRunning);

        

    }

    static void listOfCategories()
    {
        string price = "Pris";
        Logo.ClearScreen();
        WriteLine($" Name {price,20}");
        WriteLine("--------------------------------------------------------");
        var categories = context.Category
            .Include(x=>x.Products).ToList();
        foreach ( var category in categories )
        {
            WriteLine($"{category.Name} ({category.Products.Count})");
            WriteLine();
            var categoryProducts = category.Products.ToList();
            foreach ( var product in categoryProducts )
            {
               
                
                Write($"  {product.Name} ");
                int space = 18 - product.Name.Length;
                for (int i = 0; i<=space; i++)
                {
                    Write(" ");
                }
                WriteLine($"{product.Price}");
            }
            WriteLine();
            WriteLine();
        }
       ConsoleKeyInfo userInput;
        CursorVisible = false;
        do
        {
            userInput = ReadKey(true);
        }while(userInput.Key != ConsoleKey.Escape);
    }

    static void AddProductToCategory()
    {

        Logo.ClearScreen();
        Write("Ange artikelnummer: ");
        string itemNumber = ReadLine();
        var product = FindProduct(itemNumber);
        bool productFound = product != null;

        if (productFound)
        {

            WriteLine($"Product: {product.Name}");
            Write("Ange kategori: ");
            string categoryName = ReadLine();
            var category = FindCategory(categoryName);
            bool categoryFound = category != null;
            if (categoryFound)
            {
                var productInCategory = category.Products.FirstOrDefault(x => x.Number == product.Number);
                bool producAllreadyAdded = productInCategory != null;

                if (!producAllreadyAdded)
                {
                    try
                    {
                        category.Products.Add(product);
                        context.SaveChanges();

                        ConfirmMsg("Produkten tillagd till kategorin");
                    }
                    catch (Exception ex)
                    {
                        ErrorMsg(ex.Message.ToString());
                    }
                }
                else
                {
                    ErrorMsg("Produkten redan tillagd till kategorin");
                }
            }
            else
            {
                ErrorMsg("Kategori saknas");
            }
        }
        else

        {
            ErrorMsg("Produkt saknas");
        }
    }

    static void AddCategory()
    {
        bool isSure;
        do
        {
            Logo.ClearScreen();
            Write("Namn: ");
            string categoryName = ReadLine();
            Logo.ClearScreen();
            Category category = new Category(categoryName);
            WriteLine($"Namn: {category.Name}");
            WriteLine("Är detta korrekt? (J)a (N)ej");
            isSure = MakeSure();
            if (!isSure) { continue; }


            context.Category.Add(category);
            context.SaveChanges();
            ConfirmMsg("Kategori skapad");

        } while (!isSure);
    }

    static void SearchProduct()
    {
        bool isSure = false;
        ConsoleKeyInfo userInput;

        Logo.ClearScreen();
        Write("Ange artikelnummer: ");
        string itemNumber = ReadLine();
        var product = FindProduct(itemNumber);

        if (product != null)
        {
            bool repeat = false;
            do
            {
                WriteLine("Produkten hittades: ");
                WriteLine();
                WriteProduct(product);
                WriteLine();
                WriteLine("(U)ppdatera (R)adera");
                bool invalidSelection = true;

                do
                {
                    invalidSelection = true;
                    userInput = ReadKey(true);
                    invalidSelection = !(
                            userInput.Key == ConsoleKey.Escape ||
                            userInput.Key == ConsoleKey.U ||
                            userInput.Key == ConsoleKey.R

                            );
                } while (invalidSelection);
                switch (userInput.Key)
                {


                    case ConsoleKey.U:
                        UpdateProduct(product);
                        break;

                    case ConsoleKey.R:
                        WriteProduct(product);
                        WriteLine("Radera produkt? (Ja) (N)ej");

                        isSure = MakeSure();
                        if (isSure)
                        {
                            RemoveProduct(product);
                        }
                        break;

                    case ConsoleKey.Escape:
                        break;
                }
                repeat = !isSure && userInput.Key != ConsoleKey.Escape && userInput.Key != ConsoleKey.U;

            } while (repeat);



        }
        else
        {
            ErrorMsg("Produkt saknas");
        }
    }

    static void RemoveProduct(Product product)
    {




        context.Product.Remove(product);
        context.SaveChanges();
        ConfirmMsg("Produkt raderad");



    }

    static void UpdateProduct(Product product)
    {
        product = FindProduct(product.Number);
        bool isSure;
        do
        {

            CursorVisible = false;
            Logo.ClearScreen();
            Write("Artikelnummer: ");
            WriteLine(product.Number);

            Write("         Namn: ");
            CursorVisible = true;
            string itemName = ReadLine();
            Write("  Beskrivning: ");
            string itemDescription = ReadLine();
            Write("         Pris: ");
            decimal itemPrice = decimal.Parse(ReadLine());

            product.Name = itemName;
            product.Description = itemDescription;
            product.Price = itemPrice;
            Logo.ClearScreen();
            WriteProduct(product);
            WriteLine("Är detta korrekt? (J)a (N)ej");
            isSure = MakeSure();
            if (!isSure) { ErrorMsg("OK, Du får försöka igen."); }
        } while (!isSure);

         try
        {

        context.Product.Attach(product);
        context.Entry(product).State = EntityState.Modified;

        context.SaveChanges();
        ConfirmMsg("Produkt uppdaterad");


        }
        catch (Exception e)
        {
          ErrorMsg(e.Message.ToString());
        }



    }

    static Product? FindProduct(string itemNumber)
    {

        return context.Product.FirstOrDefault(x => x.Number == itemNumber);

    }

    static void AddProduct()
    {
        bool isSure;
        Product newProduct;
        do
        {
            Logo.ClearScreen();
            CursorVisible = true;

            Write("Artikelnummer: ");
            string? itemNumber = ReadLine();
            Write("Namn: ");
            string? itemName = ReadLine();
            Write("Beskrivning: ");
            string? itemDescription = ReadLine();
            Write("Pris: ");
            decimal itemPrice = decimal.Parse(ReadLine());
            newProduct = new Product
                (
                itemNumber, itemName, itemDescription, itemPrice
                );
            WriteProduct(newProduct);
            WriteLine("Är detta korrekt? (J)a (N)ej");
            isSure = MakeSure();
            if (!isSure) { ErrorMsg("OK, Du får försöka igen."); }
        } while (!isSure);


        var product = FindProduct(newProduct.Number);

        if (product == null)
        {
            try
            {
                // Skapa (instansiera) object
                //var newStudent = new Student(firstName, lastName, socialSecurityNumber, phoneNumber, email);

                context.Product.Add(newProduct);
                context.SaveChanges();

                ConfirmMsg("Produkt tillagd");
            }
            catch (Exception ex)
            {
                ErrorMsg(ex.Message);
            }
        }
        else
        {
            ErrorMsg("Produkt finns redan");
        }


    }

    static void Huvudmeny()
        {

            Logo.ClearScreen();
            CursorVisible = false;

            WriteLine($"1. Ny produkt");
            WriteLine($"2. Sök produkt");  //Huvud meny (Main)
            WriteLine($"3. Ny kategori");
            WriteLine($"4. Lägg till produkt till kategori");
            WriteLine($"5. Lista kategorier");
            WriteLine($"6. Logga ut");


        }

    static void ConfirmMsg(string msg)
        {
            ForegroundColor = ConsoleColor.Green;
            WriteLine(msg);
            ForegroundColor = ConsoleColor.White;
            Thread.Sleep(2000);
        }

    static void ErrorMsg(string msg)
        {
            ForegroundColor = ConsoleColor.Red;
            WriteLine(msg);
            ForegroundColor = ConsoleColor.White;
            Thread.Sleep(2000);
        }

    static void WriteProduct(Product product)
        {
            Logo.ClearScreen();
            WriteLine();


            Write("             Artikelnummer: ");
            WriteLine($"{product.Number,2}");
            Write("                      Namn: ");
            WriteLine($"{product.Name,2}");
            Write("               Beskrivning: ");
            WriteLine($"{product.Description,2}");
            Write("                      Pris: ");
            WriteLine($"{product.Price.ToString(),2}");
            CursorVisible = false;
            WriteLine();
        }

    static bool MakeSure()
        {
            CursorVisible = false;
            ConsoleKeyInfo makeSureUserInput;
            bool isSure;

            do
            {
                makeSureUserInput = ReadKey(true);
            } while (makeSureUserInput.Key != ConsoleKey.J && makeSureUserInput.Key != ConsoleKey.N);

            if (makeSureUserInput.Key == ConsoleKey.N)
                isSure = false;
            else
                isSure = true;

            return isSure;
        }

    static bool LogIn()
        {
            string? username;
            bool isAccessGranted = false;


            do
            {
                Logo.ClearScreen();

                Write("Använder namn: ");
                username = ReadLine();
                Write("Lösenord: ");
                string? passWord = ReadLine();
                Logo.ClearScreen();
                var user = context.User.FirstOrDefault(x=>x.Name == username && x.PassWord == passWord);
                isAccessGranted= user != null;
                if (!isAccessGranted)
                {
                    ErrorMsg("Åtkomst nekad");
                }
            } while (!isAccessGranted);
            ConfirmMsg($"Välkommen {username}");
            return isAccessGranted;

        }

    static Category? FindCategory(string name)
    {


        return context.Category.FirstOrDefault(x => x.Name == name);
        
    }



}