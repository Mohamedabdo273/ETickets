namespace ETickets.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int OrderId { get; set; }
        public string OrderName { get; set; }
        public int Count { get; set; }
        public DateTime Date { get; set; }

    }
}
