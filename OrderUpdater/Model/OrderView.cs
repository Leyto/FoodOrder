using System;
using System.Collections.Generic;

namespace Model.View
{
    /// <summary>
    /// Предстваление заказа
    /// </summary>
    public class OrderView 
    {
        public int orderNumber { get; set; }
        public DateTime createdAt { get; set; }
        public List<ProductView> products { get; set; }
    }
}