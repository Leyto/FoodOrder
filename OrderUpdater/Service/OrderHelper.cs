using System;
using Newtonsoft.Json;
using System.Text.Json;
using OrderUpdater.BusinessLogic;

namespace OrderUpdater.Service
{
    /// <summary>
    /// Хелпер для работы с заказами
    /// </summary>
    public static class OrderHelper
    {
        public static bool BuildAndSaveOrder(JsonElement orderJson, string orderType)
        {
            using (var context = new CustomContext())
            {
                if (orderJson.ToString() == string.Empty) return false;

                var orderView = JsonConvert.DeserializeObject<Model.View.OrderView>(orderJson.ToString());

                if (orderView == null) return false;

                var order = new Order();
                order.Id = Guid.NewGuid();
                order.Order_Number = orderView.orderNumber;
                order.Created_At = DateTime.Now;
                order.System_Type = orderType;
                order.Source_Order = orderJson.ToString();

                context.Orders.Add(order);
                
                return context.SaveChanges() > 0;
            }
        }
    }
}
