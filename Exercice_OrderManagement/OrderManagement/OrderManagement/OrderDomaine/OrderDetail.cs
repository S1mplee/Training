namespace TESTDICT
{
    public class OrderDetail
    {
       public decimal price;
        public int Quantite;

        public OrderDetail(decimal price,int qts)
        {
            this.price = price;
            this.Quantite = qts;
        }
    }
}