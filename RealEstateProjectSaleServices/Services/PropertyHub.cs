using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleServices.Services
{
    public class PropertyHub : Hub
    {
        public async Task NotifyPropertyUpdated(Guid propertyId,string status)
        {
            await Clients.All.SendAsync("ReceivePropertyUpdate", propertyId,status);
        }
    }
}
