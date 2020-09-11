using KP.Online.BL.DBModel;
using KP.Online.BL.ServiceModel;
using KP.Online.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using Newtonsoft;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KP.Online.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "OrderService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SaleOrderService.svc or OrderService.svc.cs at the Solution Explorer and start debugging.
    public class SaleOrderService : ISaleOrderService
    {
        private string _connStr;
        private POSAirPortClassesDataContext _posDB;
        public SaleOrderService() { _connStr = ConfigurationManager.ConnectionStrings["OrderConnectionString"].ConnectionString; }

        public async Task<OrderSession> CancelOrderOnlineAsync(string order_no)
        {
            OrderService orderSrv = new OrderService(_connStr);
            string connStrPOS = orderSrv.GetConnectionPOSOrder(order_no);
            _posDB = new POSAirPortClassesDataContext(connStrPOS);

            return await Task.FromResult(orderSrv.CancelOrderOnline(_posDB, order_no));
        }

        public async Task<OrderSession> CompleteOrderOnlineAsync(string order_no)
        {
            OrderService orderSrv = new OrderService(_connStr);
            string connStrPOS = orderSrv.GetConnectionPOSOrder(order_no);
            _posDB = new POSAirPortClassesDataContext(connStrPOS);

            return await Task.FromResult(orderSrv.CompleteOrderOnline(_posDB, order_no));
        }

        public async Task<OrderSession> GetOrderOnlineAsync(string order_no)
        {
            OrderService orderSrv = new OrderService(_connStr);
            return await Task.FromResult(orderSrv.GetOrderOnline(order_no));
        }

        public async Task<List<OrderSession>> GetOrderOnlineListAsync(string airport_code, int skip, int take)
        {
            OrderService orderSrv = new OrderService(_connStr);
            List<OrderSession> datalist = orderSrv.GetOrderOnlineList(airport_code, skip, take);
            return await Task.FromResult(datalist);
        }

        public async Task<OrderSession> HoleOrderOnlineAsync(string order_no)
        {
            OrderService orderSrv = new OrderService(_connStr);
            string connPOS = orderSrv.GetConnectionPOSOrder(order_no);
            _posDB = new POSAirPortClassesDataContext(connPOS);

            return await Task.FromResult(orderSrv.HoleOrderOnline(_posDB, order_no));
        }

        public async Task<OrderSession> SaveOrderOnlineJsonAsync(string orderJson)
        {
            OrderService orderSrv = new OrderService(_connStr);
            var order = JsonConvert.DeserializeObject<OrderHeader>(orderJson);
            string connStrPOS = orderSrv.GetConnectionPOSAirport(order.Flight.AirportCode);
            _posDB = new POSAirPortClassesDataContext(connStrPOS);

            return await Task.FromResult(orderSrv.SaveOrderOnline(_posDB, order));
        }


        public async Task<OrderSession> SaveOrderOnlineAsync(OrderHeader order)
        {
            OrderService orderSrv = new OrderService(_connStr);
            string connStrPOS = orderSrv.GetConnectionPOSAirport(order.Flight.AirportCode);
            _posDB = new POSAirPortClassesDataContext(connStrPOS);

            return await Task.FromResult(orderSrv.SaveOrderOnline(_posDB, order));
        }

        public async Task<OrderSession> UpdateStatusOrderOnlineAsync(string order_no, string status)
        {
            OrderService orderSrv = new OrderService(_connStr);
            return await Task.FromResult(orderSrv.UpdateStatusOrderOnline(order_no, status));
        }

        public async Task<SaleAmountByPassport> ValidateAllowSaleOnlineAsync(string airport_code, string passort, string dateString, string flight_code)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            var flight_date = DateTime.ParseExact(dateString, "yyyy-MM-dd", provider);
            OrderService orderSrv = new OrderService(_connStr);
            string connPOS = orderSrv.GetConnectionPOSAirport(airport_code);
            POSAirPortClassesDataContext _posDB = new POSAirPortClassesDataContext(connPOS);
            var saleObj =  _posDB.get_lt_sale_by_passport_onl(passort, flight_code, flight_date).FirstOrDefault();
            if (saleObj == null)
            {
                throw new System.ArgumentException("data not found.", nameof(saleObj));
            }

            return await Task.FromResult(new SaleAmountByPassport(saleObj));
        }

        public async Task<OrderSession> VoidOrderOnlineAsync(string order_no)
        {
            OrderService orderSrv = new OrderService(_connStr);
            string connPOS = orderSrv.GetConnectionPOSOrder(order_no);
            _posDB = new POSAirPortClassesDataContext(connPOS);

            return await Task.FromResult(orderSrv.VoidOrderOnline(_posDB, order_no));
        }
    }
}
