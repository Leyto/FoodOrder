using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace OrderUpdater.Service
{
    /// <summary>
    /// Сервис по обработке заказов.
    /// </summary>
    public class OrderHandlerService : BackgroundService
    {
        private readonly TimeSpan interval = TimeSpan.FromSeconds(5);

        private readonly ILogger<OrderHandlerService> _logger;

        private Timer _timer;

        public OrderHandlerService(ILogger<OrderHandlerService> logger)
        {
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(UpdateOrders, null, TimeSpan.Zero, interval);

            return Task.FromResult(0);
        }

        public void UpdateOrders(object state)
        {
            using (var context = new CustomContext())
            {
                var nw = DateTime.Now;
                var rawOrders = context.Orders.Where(o => o.Converted_Order == null || o.Converted_Order == string.Empty).ToList();

                if (rawOrders.Count > 0)
                {
                    foreach (var order in rawOrders)
                    {
                        try
                        {
                            order.Converted_Order = ProcessOrder(order.System_Type, order.Source_Order);
                        }
                        catch(Exception e)
                        {
                            _logger.LogInformation("Problem into order processing.", e.ToString());
                        }
                    }
                }

                context.SaveChanges();
            }
        }

        public string ProcessOrder(string type, string json)
        {
            switch(type)
            {
                case "talabat":
                    var orderView = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.View.OrderView>(json.ToString());
                    if (orderView == null || orderView.products.Count <= 0) return string.Empty;

                    var convertedView = new Model.View.OrderView
                    {
                        orderNumber = orderView.orderNumber,
                        createdAt = orderView.createdAt,
                        products = new System.Collections.Generic.List<Model.View.ProductView>()
                    };

                    foreach (var product in orderView.products)
                    {
                        Model.View.ProductView covertedProduct = product;
                        covertedProduct.PaidPrice = product.PaidPrice * (-1);
                        convertedView.products.Add(covertedProduct);
                    }

                    return Newtonsoft.Json.JsonConvert.SerializeObject(convertedView);

                case "uber":
                    throw new Exception("Problem order");
                default:
                    return string.Empty;
            }
        }

        public override void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
