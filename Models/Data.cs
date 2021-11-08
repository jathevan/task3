namespace task3.Models
{
    public class Data
    {
          public string name { get; set; }
           public double totalPrice { get; set; }

           public int maxPassengers { get; set; }

        
   
 
        /* public double  pricePerPassenger { get; set; }*/

        /*     public Data(string name, double pricePerPassenger, double totalPrice)
             {
                 this.name = name;

                 this.pricePerPassenger = pricePerPassenger;
                 this.totalPrice = totalPrice;
             }*/

        public Data()
        {
            
        }
        public Data(string name, double totalPrice , int maxPassengers)
        {

            this.name = name;

            this.totalPrice = totalPrice;
            this.maxPassengers = maxPassengers;
          
        }

     /*   public Data(string message) : this()
        {

            this.message = message;

           

        }*/
    }
}