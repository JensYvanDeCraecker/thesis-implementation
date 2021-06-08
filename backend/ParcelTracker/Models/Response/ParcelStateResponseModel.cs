using System;

namespace ParcelTracker.Models.Response
{
    public class ParcelStateResponseModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTimeOffset Created { get; set; }
    }
}