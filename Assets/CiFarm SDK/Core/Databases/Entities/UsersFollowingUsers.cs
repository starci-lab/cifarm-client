using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.Core.Databases
{
    // Represents a user following another user
    [Serializable]
    public class UsersFollowingUsersEntity : UuidAbstractEntity
    {
        // Public properties for UsersFollowingUsersEntity without SerializeField attributes

        [JsonProperty("followerId")]
        public Guid FollowerId { get; set; }

        [JsonProperty("followeeId")]
        public Guid FolloweeId { get; set; }

        [JsonProperty("follower")]
        public UserEntity Follower { get; set; }

        [JsonProperty("followee")]
        public UserEntity Followee { get; set; }
    }
}
