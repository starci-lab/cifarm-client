using System;
using System.Collections.Generic;
using CiFarm.Core.Databases;
using Cysharp.Threading.Tasks;

namespace CiFarm.GraphQL
{
    public partial class GraphQLClient
    {
        public async UniTask<AnimalEntity> QueryAnimalAsync(Guid id, string query = null)
        {
            var name = "animal";
            var variables = new GraphQLVariables<Guid>() { Args = id };
            query ??=
                $@"
query($args: ID!) {{
    {name}(id: $args) {{
        availableInShop
        basicHarvestExperiences
        createdAt
        growthTime
        hungerTime
        id
        isNFT
        maxHarvestQuantity
        minHarvestQuantity
        offspringPrice
        premiumHarvestExperiences
        price
        type
        updatedAt
        yieldTime  
    }}
}}";

            return await QueryAsync<GraphQLVariables<Guid>, AnimalEntity>(name, query, variables);
        }

        public async UniTask<List<AnimalEntity>> QueryAnimalsAsync(
            GetAnimalsArgs args,
            string query = null
        )
        {
            var name = "animals";
            var variables = new GraphQLVariables<GetAnimalsArgs>() { Args = args };
            query ??=
                $@"
query($args: GetAnimalsArgs!) {{
    {name}(args: $args) {{
        availableInShop
        basicHarvestExperiences
        createdAt
        growthTime
        hungerTime
        id
        isNFT
        maxHarvestQuantity
        minHarvestQuantity
        offspringPrice
        premiumHarvestExperiences
        price
        type
        updatedAt
        yieldTime  
    }}
}}";

            return await QueryAsync<GraphQLVariables<GetAnimalsArgs>, List<AnimalEntity>>(
                name,
                query,
                variables
            );
        }
    }
}
