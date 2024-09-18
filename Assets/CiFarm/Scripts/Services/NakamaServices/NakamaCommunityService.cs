using CiFarm.Scripts.Utilities;
using Imba.Utils;
using Nakama;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace CiFarm.Scripts.Services.NakamaServices
{
    public class NakamaCommunityService : ManualSingletonMono<NakamaCommunityService>
    {   
        public override void Awake()
        {
            base.Awake();
        }

        [ReadOnly]
        public List<User> searchUsers;

        [ReadOnly]
        public string visitUserId;

        [ReadOnly]
        public User randomUser;

        public IEnumerator Start()
        {
            yield return new WaitUntil(() => NakamaInitializerService.Instance.authenticated);
            SetRandomUserAsync();
        }

        public async void SearchAsync(string value)
        {
            var response = await NakamaRpcService.Instance.SearchUsersRpcAsync(new()
            {
                value = value,
            }
            );
            searchUsers = response.users;
        }

        public async void SetRandomUserAsync()
        {
            var response = await NakamaRpcService.Instance.GetRandomUserAsync();
            randomUser = response.user;
        }

        public async void VisitAsync(string userId)
        {
            try
            {
                await NakamaRpcService.Instance.VisitRpc(new()
                {
                    userId = userId,
                });
                visitUserId = userId;
            }
            catch (Exception ex)
            {
                DLogger.LogError(ex.Message);
            }  
        }

        public async void ReturnAsync()
        {
            try
            {
                await NakamaRpcService.Instance.ReturnRpc();
                visitUserId = null;
            }
            catch (Exception ex)
            {
                DLogger.LogError(ex.Message);
            }
        }

        public async void AddFriendByUsernameAsync(string username)
        {
            var client = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            await client.AddFriendsAsync(session, null, new List<string> { username });
        }

        public async Task<IApiFriendList> ListFriendsAsync(string cursor)
        {
            var client = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;
            return await client.ListFriendsAsync(session, null, 10, cursor);
        }
    }
}