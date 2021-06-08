using System;

namespace ParcelTracker.Options
{
    public class AppOptions
    {
        public bool UseCache { get; set; } = true;

        public CacheOptions AllStates { get; set; } = new()
        {
            AbsoluteExpiration = TimeSpan.FromMinutes(5),
            SlidingExpiration = TimeSpan.FromMinutes(1)
        };

        public CacheOptions Parcels { get; set; } = new()
        {
            AbsoluteExpiration = TimeSpan.FromMinutes(5),
            SlidingExpiration = TimeSpan.FromMinutes(1)
        };
    }
}