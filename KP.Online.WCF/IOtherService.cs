using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using KP.Online.BL.ServiceModel;

namespace KP.Online.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IFlightWCF" in both code and config file together.
    [ServiceContract]
    public interface IOtherService
    {
        // TODO: Add your service operations here
        [OperationContract]
        Task<Flight> CheckFlightsAsync(string fight_code);

        [OperationContract]
        Task<Flight> CheckFlightsByAsync(string fight_code, string fight_date);

        [OperationContract]
        Task<Flight> GetDataFlightsAsync(string fight_code);

        [OperationContract]
        Task<FlightsAll> GetDataAllAsync();

        [OperationContract]
        Task<List<Flight>> GetDataDepartureAsync();

        [OperationContract]
        Task<List<Flight>> GetDataArrivalAsync();

        [OperationContract]
        Task<List<Flight>> GetDataTransferAsync();

        [OperationContract]
        Task<List<SaleQueue>> SaleQueueOnlineAsync(string airport_code, char terminal);
    }


}
