using System;

namespace ParcelTracker.Options
{
    public class CacheOptions
    {
        public TimeSpan AbsoluteExpiration { get; set; }

        public TimeSpan SlidingExpiration { get; set; }
    }
}