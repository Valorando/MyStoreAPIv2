namespace AddOrderService.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public string OrderDate { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string PhoneNumber { get; set; }
        public string ProductName { get; set; }
        public int Amount { get; set; }
    }
}
