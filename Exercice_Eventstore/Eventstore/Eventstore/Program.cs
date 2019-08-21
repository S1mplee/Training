
namespace Eventstore
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var service = new SaleService();
            var menu = new Menu(service);
            menu.show();
         
         
        }
    }
}
