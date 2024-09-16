using Imba.Utils;
using Nakama;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CiFarm.Scripts.Services.NakamaServices
{
    public class NakamaUsersService : ManualSingletonMono<NakamaUsersService>
    {
        public override void Awake()
        {
            base.Awake();
        }

        public async void AddFriendByIdAsync(string userId)
        {
            if (!NakamaInitializerService.Instance.authenticated) throw new Exception("Unauthenticated");
            var client = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            await client.AddFriendsAsync(session, new List<string> { userId });
        }
        public async void AddFriendByUsernameAsync(string username)
        {
            if (!NakamaInitializerService.Instance.authenticated) throw new Exception("Unauthenticated");
            var client = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            await client.AddFriendsAsync(session, null, new List<string> { username });
        }

        public async Task<IApiFriendList> ListFriendsAsync(string cursor)
        {
            if (!NakamaInitializerService.Instance.authenticated) throw new Exception("Unauthenticated");
            var client = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;
            return await client.ListFriendsAsync(session, null, 10, cursor);
        }
    }
}