# op-semantic-kernel-demo

Doco [here](https://medium.com/@jack.a.watts/impersonating-yourself-with-chatgpt-and-microsoft-semantic-kernel-719e08a1b529)

# Pre-requisites
* dotnet 7.0
* Docker with Qdrant installed as described above
* Azure OpenAI or OpenAI API access

## Docker
```
docker-compose -f docker/op-sk-demo/docker-compose.yml up -d
```

## Using OpenAI
To use OpenAI
1. Get your API keys as discussed in the article above
2. go to: src/Op.SemanticKernel.Demo.Azure
3. Add the user secrets:
```
dotnet user-secrets set "ApiKey" "the_api_key_you_copied"
```

## Azure OpenAI
To use Azure OpenAI
1. Deploy ChatGPT (gpt-35-turbo) and Ada (text-embedding-ada-002) as documented [here](https://learn.microsoft.com/en-au/azure/cognitive-services/openai/how-to/create-resource?pivots=web-portal#deploy-a-model) and copy the deployment names (the name you specified for each one)
2. go to: src/Op.SemanticKernel.Demo.Azure
3. Add the user secrets:
```
dotnet user-secrets set "ApiKey" "key_copied_from_Keys_and_Endpoint"
dotnet user-secrets set "Endpoint" "Endpoint_copied_from_Keys_and_Endpoint"
dotnet user-secrets set "ChatDeploymentName" "Model_deployment_name_of_chat_model"
dotnet user-secrets set "EmbeddingDeploymentName" "Model_deployment_name_of_embedding_model"
```

# Getting started
1. Choose and configure the appropriate starting project ie Op.SemanticKernel.Demo.Azure or Op.SemanticKernel.Demo.OpenAI as per pre-requisites
2. Debug/Run
3. Profit!

NOTE: On subsequent reruns you can comment out the BuildMemory line as it only needs to be run once to store the document in Qdrant.
