using MonopakApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonopakApp.Services
{
    public class OrdersService
    {
        #region Define as Singleton
        private static OrdersService _Instance;

        public static OrdersService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new OrdersService();
                }

                return (_Instance);
            }
        }

        private OrdersService()
        {
        }
        #endregion
        public bool SaveOrder(Order order)
        {
            MonoDB context = new MonoDB();

            context.Orders.Add(order);

            return context.SaveChanges() > 0;
        }
        public bool AddOrderHistory(OrderHistory orderHistory)
        {
            MonoDB context = new MonoDB();

            context.OrderHistories.Add(orderHistory);

            return context.SaveChanges() > 0;
        }

        public Order GetOrderByID(int ID)
        {
            MonoDB context = new MonoDB();
            var orderid = context.Orders.Include("OrderHistory").Include("OrderItems.Product").First(x=>x.ID==ID);
            return orderid;
        }

        public int SearchOrdersCount()
        {
            MonoDB context = new MonoDB();

            return context.Orders.Count();
        }


        public List<Order> SearchOrders()
        {
            MonoDB context = new MonoDB();
            return context.Orders.Include("OrderHistory").OrderByDescending(x => x.OrderedAt).ToList();
        }
        
        public List<Order> GetUserOrders(string userEmail, int? orderID, int? orderStatus, int? pageNo, int pageSize)
        {
            MonoDB context = new MonoDB();

            var orders = context.Orders.Where(x => x.CustomerEmail.Equals(userEmail));

            if(orderID.HasValue)
            {
                orders = orders.Where(x => x.ID == orderID.Value);
            }

            if (orderStatus.HasValue)
            {
                orders = orders.Where(x => x.OrderHistory.OrderByDescending(y => y.ModifiedOn).FirstOrDefault().OrderStatus == orderStatus);
            }

            pageNo = pageNo ?? 1;

            var skipCount = (pageNo.Value - 1) * pageSize;

            return orders.OrderByDescending(x => x.OrderedAt).Skip(skipCount).Take(pageSize).ToList();
        }
    }
}