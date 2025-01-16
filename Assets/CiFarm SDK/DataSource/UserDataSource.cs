using System;
using System.Collections;
using CiFarm.Core.Credentials;
using CiFarm.Core.Databases;
using CiFarm.Utils;
using Imba.Utils;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm
{
    /// <summary>
    /// Manager for fetching and rendering user data, easier to manage and maintain
    /// </summary>
    public class UserDataSource : ManualSingletonMono<UserDataSource>
    {
        /// <summary>
        /// User
        /// </summary>
        private UserEntity User { get; set; }

        /// <summary>
        /// Display user
        /// </summary>
        [SerializeField]
        private string _user;

        private IEnumerator Start()
        {
            // Wait until the user is authenticated
            yield return new WaitUntil(() => CiFarmSDK.Instance.Authenticated);

            // Query the user data
            FetchUserAsync();
        }

        public async void FetchUserAsync()
        {
            // Query the user data
            User = await CiFarmSDK.Instance.GraphQLClient.QueryUserAsync();
        }
    }
}
