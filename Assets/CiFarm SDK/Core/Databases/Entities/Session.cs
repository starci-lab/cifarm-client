using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.Core.Databases
{
    [Serializable] // Makes the class serializable for Unity
    public class SessionEntity : UuidAbstractEntity
    {
        // Private backing field for token
        [SerializeField] // Expose this field for Unity serialization
        private string _token;

        // Public property for token
        [JsonProperty("token")] // Custom JSON property name in camelCase
        public string Token
        {
            get => _token;
            set => _token = value;
        }

        // Private backing field for expiredAt
        [SerializeField] // Expose this field for Unity serialization
        private DateTime _expiredAt;

        // Public property for expiredAt
        [JsonProperty("expiredAt")] // Custom JSON property name in camelCase
        public DateTime ExpiredAt
        {
            get => _expiredAt;
            set => _expiredAt = value;
        }

        // Private backing field for userId
        [SerializeField] // Expose this field for Unity serialization
        private string _userId;

        // Public property for userId
        [JsonProperty("userId")] // Custom JSON property name in camelCase
        public string UserId
        {
            get => _userId;
            set => _userId = value;
        }

        // Private backing field for isActive
        [SerializeField] // Expose this field for Unity serialization
        private bool _isActive;

        // Public property for isActive
        [JsonProperty("isActive")] // Custom JSON property name in camelCase
        public bool IsActive
        {
            get => _isActive;
            set => _isActive = value;
        }

        // Private backing field for deviceInfo
        [SerializeField] // Expose this field for Unity serialization
        private string _deviceInfo;

        // Public property for deviceInfo
        [JsonProperty("deviceInfo")] // Custom JSON property name in camelCase
        public string DeviceInfo
        {
            get => _deviceInfo;
            set => _deviceInfo = value;
        }

        // Navigation property for UserEntity (many-to-one relationship)
        [JsonProperty("user")] // Custom JSON property name in camelCase
        public UserEntity User { get; set; }
    }
}
