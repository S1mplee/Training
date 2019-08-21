using System;

namespace Eventstore
{
    public class Menu
    {
        private SaleService _service;
        public Menu(SaleService service)
        {
            this._service = service;
        }

        public void show()
        {
            var choice = "";
            while (choice != "1" && choice != "2" && choice != "3")
            {
                Console.WriteLine("Make Your Choice :");
                Console.WriteLine("1) Add new Sale ");
                Console.WriteLine("2) Show list of products and quantities  ");
                Console.WriteLine("3) Show the total amount of sale ");
                Console.WriteLine("Press q to Quit : ");

                _service.Connect();
                _service.SubscribeValueChange("salesStream");
                
                choice = Console.ReadLine();
                if (choice == "q") break;
                switch(choice)
                {
                    case "1":
                        AddSale();
                        break;
                    case "2": 
                        ShowList();
                        break;
                    case "3":
                        ShowTotal();
                        break;
                }
            }
        }

        private void AddSale()
        {
            Console.WriteLine("Write your Sale informations :");
            Console.WriteLine("Write ProductName :");
            var name = Console.ReadLine();
            Console.WriteLine("Write Quantities :");
            var qts = int.Parse(Console.ReadLine());
            Console.WriteLine("Write the price :");
            var price = decimal.Parse(Console.ReadLine());

            _service.WriteEvent(name, qts, price);
            show();
        }

        private void ShowList()
        {
            var list = _service.GetProductsSold();
            foreach(var elem in list)
            {
                Console.WriteLine("Product Name :( {0} ) Quantite Sold : ({1}) ", elem.Name, elem.Qts);
            }
            Console.Read();
            show();
        }

        private void ShowTotal()
        {
            var total = _service.getTotal();
            Console.WriteLine("Total Amount of Sales : " + total);
            Console.Read();
            show();
        }
    }
}
