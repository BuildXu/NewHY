using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi_SY.Models
{
    public interface IOrderStorage
    {
        //添加订单、
        Task<int> AddOrder(Product order);
        //更新订单
        Task UpdateOrder(Product order);
        //根据 ID 获取订单
        Task<Product> GetOrderById(int id);
    }
}
