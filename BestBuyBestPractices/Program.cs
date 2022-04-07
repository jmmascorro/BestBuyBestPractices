using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace BestBuyBestPractices
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Configuration
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");
            #endregion

            IDbConnection conn = new MySqlConnection(connString);
            DapperDepartmentRepository repo = new DapperDepartmentRepository(conn);
            DapperProductRepository prodRepo = new DapperProductRepository(conn);

            Console.WriteLine("Hello user, here are the current departments:");
            Console.WriteLine("Please press enter. . .");
            Console.ReadLine();

            var depos = repo.GetAllDepartments();
            Print(depos);


            Console.WriteLine("Do you want to add a department?");
            string userResponse = Console.ReadLine();

            if (userResponse.ToLower() == "yes")
            {
                Console.WriteLine("What is the name of your new Department?");
                userResponse = Console.ReadLine();

                repo.InsertDepartment(userResponse);
                Print(repo.GetAllDepartments());
            }
            else if (userResponse.ToLower() == "no")
            {
                Console.WriteLine("Okay, here are the current products:");
                Console.WriteLine("Please press enter . . .");
                Console.ReadLine();

                var products = prodRepo.GetAllProducts();
                foreach (var prod in products)
                {
                    Console.WriteLine($"ProductID: {prod.ProductID}  Name:{prod.Name}  Price:{prod.Price}");
                }

            }

            Console.WriteLine("Do you want to add a product?");
            userResponse = Console.ReadLine();

            if (userResponse.ToLower() == "yes")
            {
                Console.WriteLine("What is the name of your new Product?");
                var name = Console.ReadLine();

                Console.WriteLine("What is the new product's price?");
                var price = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine("What is the new product's category id?");
                var categoryID = Convert.ToInt32(Console.ReadLine());

                prodRepo.CreateProduct(name, price, categoryID);

                Console.WriteLine("Here's a list of your updated products:");

                var products = prodRepo.GetAllProducts();
                foreach (var prod in products)
                {
                    Console.WriteLine($"ProductID: {prod.ProductID}  Name:{prod.Name}  Price:{prod.Price}");
                }

                Console.WriteLine("Do you want to update a product?");
                userResponse = Console.ReadLine();

                if (userResponse == "yes")
                {
                    Console.WriteLine("What is the name of the product you want to update?");
                    var updateName = Console.ReadLine();

                    Console.WriteLine("What is the product id of the product you want to update?");
                    var productID = Convert.ToInt32(Console.ReadLine());

                    prodRepo.UpdateProduct(productID, updateName);
                    products = prodRepo.GetAllProducts();
                    foreach (var prod in products)
                    {
                        Console.WriteLine($"ProductID: {prod.ProductID}  Name:{prod.Name}  Price:{prod.Price}");
                    }
                }
                else if (userResponse == "no")
                {
                    Console.WriteLine("Okay, do you want to delete a product?");
                    userResponse = Console.ReadLine();

                    if (userResponse.ToLower() == "yes")
                    {
                        Console.WriteLine("Please enter the product id of the product you want to delete.");
                        var productID = Convert.ToInt32(Console.ReadLine());

                        prodRepo.DeleteProduct(productID);

                        products = prodRepo.GetAllProducts();
                        foreach (var prod in products)
                        {
                            Console.WriteLine($"ProductID: {prod.ProductID}  Name:{prod.Name}  Price:{prod.Price}");
                        }

                        Console.WriteLine("Thank you, have a nice day.");
                    }
                }

            }
            else if (userResponse == "no")
            {
                Console.WriteLine("Okay, Do you want to update a product?");
                userResponse = Console.ReadLine();

                if(userResponse.ToLower() == "yes")
                {
                    Console.WriteLine("What is the name of the product you want to update?");
                    var updateName = Console.ReadLine();

                    Console.WriteLine("What is the product id of the product you want to update?");
                    var productID = Convert.ToInt32(Console.ReadLine());

                    prodRepo.UpdateProduct(productID, updateName);

                    var products = prodRepo.GetAllProducts();
                    foreach (var prod in products)
                    {
                        Console.WriteLine($"ProductID: {prod.ProductID}  Name:{prod.Name}  Price:{prod.Price}");
                    }
                }
                else if (userResponse == "no")
                {
                    Console.WriteLine("Okay, do you want to delete a product?");
                    userResponse = Console.ReadLine();

                    if( userResponse.ToLower() == "yes")
                    {
                        Console.WriteLine("Please enter the product id of the product you want to delete.");
                        var productID = Convert.ToInt32(Console.ReadLine());

                        prodRepo.DeleteProduct(productID);

                        var products = prodRepo.GetAllProducts();
                        foreach (var prod in products)
                        {
                            Console.WriteLine($"ProductID: {prod.ProductID}  Name:{prod.Name}  Price:{prod.Price}");
                        }

                        Console.WriteLine("Thank you, have a nice day.");
                    }
                    else if (userResponse.ToLower() == "no")
                    {
                        Console.WriteLine("Thank you, have a nice day.");
                    }
                }
            }
              
        }

        private static void Print(IEnumerable<Department> depos)
        {
            foreach (var depo in depos)
            {
                Console.WriteLine($"Id: {depo.DepartmentID} Name: {depo.Name}");
            }
        }
    
    }
}
