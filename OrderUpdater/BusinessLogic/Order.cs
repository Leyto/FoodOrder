using System;
using System.ComponentModel.DataAnnotations;

namespace OrderUpdater.BusinessLogic
{
    /// <summary>
    /// Заказы
    /// </summary>
    public class Order
    {
        public Guid Id { get; set; }

        // TODO Переделать под enum или словарь?
        public string System_Type { get; set; }

        public int Order_Number {get; set;}

        [MaxLength]
        public string Source_Order { get; set; }

        [MaxLength]
        public string Converted_Order { get; set; }

        public DateTime Created_At { get; set; }

    }
}
