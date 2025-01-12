using System;
using System.Collections.Generic;
using CiFarm.Core.Databases;
using Cysharp.Threading.Tasks;

namespace CiFarm.GraphQL
{
    public partial class GraphQLClient
    {
        public async UniTask<SupplyEntity> QuerySupplyAsync(Guid id, string query = null)
        {
            var name = "supply";
            var variables = new GraphQLVariables<Guid>() { Args = id };
            query ??=
                $@"
query($args: ID!) {{
    {name}(id: $args) {{
        availableInShop
        createdAt  
        fertilizerEffectTimeReduce
        id
        maxStack
        price
        type
        updatedAt
    }}
}}";

            return await QueryAsync<GraphQLVariables<Guid>, SupplyEntity>(name, query, variables);
        }

        public async UniTask<List<SupplyEntity>> QuerySuppliesAsync(
            GetSuppliesArgs args,
            string query = null
        )
        {
            var name = "supplies";
            var variables = new GraphQLVariables<GetSuppliesArgs>() { Args = args };
            query ??=
                $@"
query($args: GetSuppliesArgs!) {{
    {name}(args: $args) {{
        availableInShop
        createdAt  
        fertilizerEffectTimeReduce
        id
        maxStack
        price
        type
        updatedAt
    }}
}}";

            return await QueryAsync<GraphQLVariables<GetSuppliesArgs>, List<SupplyEntity>>(
                name,
                query,
                variables
            );
        }
    }
}
