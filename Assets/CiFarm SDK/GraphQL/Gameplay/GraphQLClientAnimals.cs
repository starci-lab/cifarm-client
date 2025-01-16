using System;
using System.Collections.Generic;
using CiFarm.Core.Databases;
using CiFarm.Utils;
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
        yieldTime  
        placedItemType {{
            id
            type
        }}
        inventoryType {{
            id
            type
        }}
        product {{
            id
            type
        }}
    }}
}}";

            return await QueryAsync<GraphQLVariables<Guid>, AnimalEntity>(name, query, variables);
        }

        public async UniTask<List<AnimalEntity>> QueryAnimalsAsync(string query = null)
        {
            var name = "animals";
            query ??=
                $@"
query {{
    {name} {{
        availableInShop
        basicHarvestExperiences
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
        yieldTime  
        placedItemType {{
            id
            type
        }}
        inventoryType {{
            id
            type
        }}
        product {{
            id
            type
        }}
    }}
}}";

            return await QueryAsync<Empty, List<AnimalEntity>>(name, query);
        }
    }
}
