using System;
using System.Collections.Generic;
using CiFarm.Core.Databases;
using Cysharp.Threading.Tasks;

namespace CiFarm.GraphQL
{
    public partial class GraphQLClient
    {
        public async UniTask<TileEntity> QueryTileAsync(Guid id, string query = null)
        {
            var name = "tile";
            var variables = new GraphQLVariables<Guid>() { Args = id };
            query ??=
                $@"
query($args: ID!) {{
    {name}(id: $args) {{
        availableInShop
        createdAt
        id
        isNFT
        maxOwnership
        price
        updatedAt
    }}
}}";

            return await QueryAsync<GraphQLVariables<Guid>, TileEntity>(name, query, variables);
        }

        public async UniTask<List<TileEntity>> QueryTilesAsync(
            GetTilesArgs args,
            string query = null
        )
        {
            var name = "tiles";
            var variables = new GraphQLVariables<GetTilesArgs>() { Args = args };
            query ??=
                $@"
query($args: GetTilesArgs!) {{
    {name}(args: $args) {{
        availableInShop
        createdAt
        id
        isNFT
        maxOwnership
        price
        updatedAt
    }}
}}";

            return await QueryAsync<GraphQLVariables<GetTilesArgs>, List<TileEntity>>(
                name,
                query,
                variables
            );
        }
    }
}
