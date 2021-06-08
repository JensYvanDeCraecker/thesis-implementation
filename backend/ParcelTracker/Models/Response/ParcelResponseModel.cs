using System;
using System.Collections.Generic;

namespace ParcelTracker.Models.Response
{
    public class ParcelResponseModel
    {
        public Guid Id { get; set; }

        public AddressResponseModel ReturnAddress { get; set; }

        public AddressResponseModel DeliveryAddress { get; set; }

        public IEnumerable<ParcelStateResponseModel> States { get; set; }
    }
}