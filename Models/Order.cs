namespace Models 
{
    public class Order
    {
        public string OrderId { get; set; }
        public string PartnerNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Product { get; set; }
        public OrderStatus Status { get; set; }

        public override string ToString()
        {
            return $"OrderId: {OrderId}, PartnerNumber: {PartnerNumber}, FirstName: {FirstName}, LastName: {LastName}, Product: {Product}, Status: {Status}";
        }
    }
}