﻿namespace Easy_Laundry.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public int Quantity { get; set; }
        public string ClothType { get; set; }
        public string LaundryType { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime? CompletionDate { get; set; }
        public OrderModel()
        {
            CompletionDate = DateTime.Now.AddDays(2);
        }

    }
}
