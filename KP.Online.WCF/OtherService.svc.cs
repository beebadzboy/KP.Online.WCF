using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using KP.Online.BL;
using KP.Online.BL.DBModel;
using KP.Online.BL.ServiceModel;
using KP.Online.Service;

namespace KP.Online.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "OtherService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select OtherService.svc or OtherService.svc.cs at the Solution Explorer and start debugging.
    public class OtherService : IOtherService
    {
        private string _connStr;
        public OtherService() { _connStr = ConfigurationManager.ConnectionStrings["OrderConnectionString"].ConnectionString; }
        public OtherService(string connStr) { _connStr = connStr; }

        public async Task<Flight> CheckFlightsAsync(string fight_code)
        {
            FlightService srv = new FlightService(_connStr);
            Flight result = srv.CheckFlights(fight_code);

            return await Task.FromResult(result);
        }

        public async Task<Flight> CheckFlightsByAsync(string fight_code, string fight_date)
        {
            FlightService srv = new FlightService(_connStr);
            Flight result = srv.CheckFlights(fight_code, fight_date);

            return await Task.FromResult(result);
        }

        public async Task<FlightsAll> GetDataAllAsync()
        {
            FlightService srv = new FlightService(_connStr);
            FlightsAll result = srv.GetDataAll();

            return await Task.FromResult(result);
        }

        public async Task<List<Flight>> GetDataArrivalAsync()
        {
            FlightService srv = new FlightService(_connStr);
            List<Flight> result = srv.GetDataArrival();

            return await Task.FromResult(result);
        }

        public async Task<List<Flight>> GetDataDepartureAsync()
        {
            FlightService srv = new FlightService(_connStr);
            List<Flight> result = srv.GetDataDeparture();

            return await Task.FromResult(result);
        }

        public async Task<List<Flight>> GetDataTransferAsync()
        {
            FlightService srv = new FlightService(_connStr);
            List<Flight> result = srv.GetDataTransfer();

            return await Task.FromResult(result);
        }

        public async Task<Flight> GetDataFlightsAsync(string fight_code)
        {
            FlightService srv = new FlightService(_connStr);
            Flight result = srv.GetDataFlights(fight_code);

            return await Task.FromResult(result);
        }

        public async Task<List<SaleQueue>> SaleQueueOnlineAsync(string airport_code, char terminal)
        {
            List<SaleQueue> ret = new List<SaleQueue>();
            OrderService srv = new OrderService(_connStr);
            var posConn = srv.GetConnectionPOSAirport(airport_code);
            if (posConn != null)
            {
                var posDB = new POSAirPortClassesDataContext(posConn);
                ret = srv.SaleQueue(posDB, terminal);
            }
            return await Task.FromResult(ret);
        }

    }

}
