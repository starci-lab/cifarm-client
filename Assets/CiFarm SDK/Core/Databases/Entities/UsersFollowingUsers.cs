using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.Core.Databases
{
    // Represents a user following another user
    [Serializable]
    public class UsersFollowingUsersEntity : UuidAbstractEntity
    {
        // Private backing fields for follower and followee IDs
        [SerializeField] // Unity serialization
        private Guid _followerId;

        [JsonProperty("followerId")]
        public Guid FollowerId
        {
            get => _followerId;
            set => _followerId = value;
        }

        [SerializeField] // Unity serialization
        private Guid _followeeId;

        [JsonProperty("followeeId")]
        public Guid FolloweeId
        {
            get => _followeeId;
            set => _followeeId = value;
        }

        // Navigation properties for follower and followee users
        [SerializeField] // Unity serialization
        private UserEntity _follower;

        [JsonProperty("follower")]
        public UserEntity Follower
        {
            get => _follower;
            set => _follower = value;
        }

        [SerializeField] // Unity serialization
        private UserEntity _followee;

        [JsonProperty("followee")]
        public UserEntity Followee
        {
            get => _followee;
            set => _followee = value;
        }
    }
}
