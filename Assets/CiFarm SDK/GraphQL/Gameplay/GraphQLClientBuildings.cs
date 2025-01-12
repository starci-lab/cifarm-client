using System;
using System.Collections.Generic;
using CiFarm.Core.Databases;
using Cysharp.Threading.Tasks;

namespace CiFarm.GraphQL
{
    public partial class GraphQLClient
    {
        public async UniTask<BuildingEntity> QueryBuildingAsync(Guid id, string query = null)
        {
            var name = "building";
            var variables = new GraphQLVariables<Guid>() { Args = id };
            query ??=
                $@"
query($args: ID!) {{
    {name}(id: $args) {{
        availableInShop
        createdAt
        id
        maxUpgrade
        price
        type
        updatedAt
    }}
}}";

            return await QueryAsync<GraphQLVariables<Guid>, BuildingEntity>(name, query, variables);
        }

        public async UniTask<List<BuildingEntity>> QueryBuildingsAsync(
            GetBuildingsArgs args,
            string query = null
        )
        {
            var name = "buildings";
            var variables = new GraphQLVariables<GetBuildingsArgs>() { Args = args };
            query ??=
                $@"
query($args: GetBuildingsArgs!) {{
    {name}(args: $args) {{
        availableInShop
        createdAt
        id
        maxUpgrade
        price
        type
        updatedAt
    }}
}}";

            return await QueryAsync<GraphQLVariables<GetBuildingsArgs>, List<BuildingEntity>>(
                name,
                query,
                variables
            );
        }
    }
}
