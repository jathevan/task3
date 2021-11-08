namespace task3.Models
{
    public class Data
    {
        public string Name { get; set; }
        public double TotalPrice { get; set; }
        public int MaxPassengers { get; set; }

        public Data()
        {
        }

        public Data(string name, double totalPrice, int maxPassengers)
        {
            this.Name = name;
            this.TotalPrice = totalPrice;
            this.MaxPassengers = maxPassengers;
        }
    }
}