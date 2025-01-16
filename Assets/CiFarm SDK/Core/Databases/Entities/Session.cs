using System;
using Newtonsoft.Json;

namespace CiFarm.Core.Databases
{
    [Serializable] // Makes the class serializable for Unity
    public class SessionEntity : UuidAbstractEntity
    {
        // Public property for token
        [JsonProperty("token")] // Custom JSON property name in camelCase
        public string Token { get; set; }

        // Public property for expiredAt
        [JsonProperty("expiredAt")] // Custom JSON property name in camelCase
        public DateTime ExpiredAt { get; set; }

        // Public property for userId
        [JsonProperty("userId")] // Custom JSON property name in camelCase
        public string UserId { get; set; }

        // Public property for isActive
        [JsonProperty("isActive")] // Custom JSON property name in camelCase
        public bool IsActive { get; set; }

        // Public property for deviceInfo
        [JsonProperty("deviceInfo")] // Custom JSON property name in camelCase
        public string DeviceInfo { get; set; }

        // Navigation property for UserEntity (many-to-one relationship)
        [JsonProperty("user")] // Custom JSON property name in camelCase
        public UserEntity User { get; set; }
    }
}
