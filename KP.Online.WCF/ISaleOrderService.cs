using KP.Online.BL.DBModel;
using KP.Online.BL.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace KP.Online.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISaleOrderService" in both code and config file together.
    [ServiceContract]
    public interface ISaleOrderService
    {
        // TODO: Add your service operations here
        [OperationContract]
        Task<SaleAmountByPassport> ValidateAllowSaleOnlineAsync(string airport_code, string passort, DateTime flight_date, string flight_code);

        [OperationContract]
        Task<OrderSession> SaveOrderOnlineAsync(OrderHeader order);

        [OperationContract]
        Task<OrderSession> HoleOrderOnlineAsync(string order);

        [OperationContract]
        Task<OrderSession> CancelOrderOnlineAsync(string order_no);

        [OperationContract]
        Task<OrderSession> VoidOrderOnlineAsync(string order_no);

        [OperationContract]
        Task<OrderSession> CompleteOrderOnlineAsync(string order_no);

        [OperationContract]
        Task<OrderSession> GetOrderOnlineAsync(string order_no);

        [OperationContract]
        Task<List<OrderSession>> GetOrderOnlineListAsync(string airport_code, int skip, int take);

        [OperationContract]
        Task<OrderSession> UpdateStatusOrderOnlineAsync(string order_no, string status);
    }
}
