using System;
using System.Collections.Generic;
using ConsoleIO;
using System.Linq;
using Serialization;
using Types;

namespace Refactoring
{
    public class Tusc
    {
        public static void Start(List<User> users, List<Product> products)
        {
            ShowWelcomeHeader();

            User loggedInUser = Login(users);
            if (loggedInUser == null)
            {
                Exit();
                return;
            }
            
            ShowWelcomeMessage(loggedInUser);

            ShowBalance(loggedInUser);

            ShowProducts(users, products, loggedInUser);
        }

        private static void ShowProducts(List<User> users, List<Product> products, User loggedInUser)
        {
            /* ~~~ dhaase -- TODO: Ran out of time. Did not finish refactoring... ~~~ */
            /* ~~~ dhaase -- TODO: Code coverage dropped. Did not analyze... ~~~ */

            double newBalance = loggedInUser.Balance;

            while (true)
            {
                int menuOption = ShowProductPrompt(products);

                if (UserExitted(menuOption, products))
                {
                    loggedInUser.Balance = newBalance;

                    Save(products, users);

                    Exit();

                    return;
                }

                Product product = products[menuOption - 1];

                ShowAccountStatus(product, newBalance);

                int quantity = ConsoleReader.PromptInteger("Enter amount to purchase:");

                if (!SufficientFunds(newBalance, product, quantity))
                {
                    ConsoleWriter.WriteLine("You do not have enough money to buy that.", ConsoleColor.Red);
                    continue;
                }

                if (!ProductInStock(product, quantity))
                {
                    ConsoleWriter.WriteLine("Sorry, " + product.Name + " is out of stock", ConsoleColor.Red);
                    continue;
                }

                newBalance = Purchase(quantity, newBalance, product);
            }
        }

        private static double Purchase(int quantity, double balance, Product product)
        {
            if (quantity <= 0)
            {
                ShowPurchaseCancelled();
                return balance;
            }

            double newBalance = balance - product.Price * quantity;

            product.Qty = product.Qty - quantity;

            ShowPurchase(quantity, product, newBalance);

            return newBalance;
        }

        private static void ShowPurchaseCancelled()
        {
            ConsoleWriter.WriteLine("Purchase cancelled", ConsoleColor.Yellow);
        }

        private static void ShowPurchase(int quantity, Product product, double newBalance)
        {
            ConsoleWriter.Write("You bought " + quantity + " " + product.Name, ConsoleColor.Green);
            ConsoleWriter.Write("Your new balance is " + newBalance.ToString("C"), ConsoleColor.Green);
        }

        private static void ShowAccountStatus(Product product, double newBalance)
        {
            ConsoleWriter.WriteLine("You want to buy: " + product.Name);
            ConsoleWriter.Write("Your balance is " + newBalance.ToString("C"));
        }

        private static void ShowWelcomeHeader()
        {
            ConsoleWriter.Write("Welcome to TUSC");
            ConsoleWriter.Write("---------------");
        }

        private static User Login(List<User> users)
        {
            while (true)
            {
                string name = ConsoleReader.Prompt("Enter Username:");

                if (string.IsNullOrEmpty(name))
                    return null;

                bool userIsValid = users.Any(u => u.Name == name);
                if (!userIsValid)
                {
                    ConsoleWriter.WriteLine("You entered an invalid user.", ConsoleColor.Red);
                    continue;
                }

                string password = ConsoleReader.Prompt("Enter Password:");

                bool passwordIsValid = users.Any(u => u.Name == name && u.Password == password);
                if (!passwordIsValid)
                {
                    ConsoleWriter.WriteLine("You entered an invalid password.", ConsoleColor.Red);
                    continue;
                }

                return  users.First(u => u.Name == name && u.Password == password);
            }
        }

        private static void Exit()
        {
            ConsoleReader.Prompt("Press Enter key to exit");
        }

        private static void Save(List<Product> products, List<User> users)
        {
            Serializer.Serialize(users);

            Serializer.Serialize(products);
        }

        private static bool UserExitted(int menuOption, List<Product> products)
        {
            int exitMenuOption = products.Count + 1;

            if (menuOption == exitMenuOption)
            {
                return true;
            }

            return false;
        }

        private static bool ProductInStock(Product product, int quantity)
        {
            // dhaase - fixed bug in original code that prevented user from taking the last item
            return product.Qty >= quantity;
        }

        private static bool SufficientFunds(double newBalance, Product product, int quantity)
        {
            return newBalance - product.Price * quantity > 0;
        }

        private static int ShowProductPrompt(List<Product> prods)
        {
            int lineNumber = 1;

            ConsoleWriter.WriteLine("What would you like to buy?");
            foreach (Product product in prods)
            {
                ConsoleWriter.Write(lineNumber++ + ": " + product.Name + " (" + product.Price.ToString("C") + ")");
            }

            ConsoleWriter.Write(lineNumber + ": Exit");

            return ConsoleReader.PromptInteger("Enter a number:");
        }

        private static void ShowWelcomeMessage(User loggedInUser)
        {
            ConsoleWriter.WriteLine("Login successful! Welcome " + loggedInUser.Name + "!", ConsoleColor.Green);
        }

        private static void ShowBalance(User loggedInUser)
        {
            ConsoleWriter.WriteLine("Your balance is " + loggedInUser.Balance.ToString("C"));
        }
    }
}
