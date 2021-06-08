using System;

namespace ParcelTracker
{
    public class CacheKeys
    {
        public const string AllStates = "ParcelTracker_AllStates";

        public static string GetParcelKey(Guid id)
        {
            return $"ParcelTracker_Parcel_${id.ToString()}";
        }
    }
}