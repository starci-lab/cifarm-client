using System.Collections;
using AYellowpaper.SerializedCollections;
using CiFarm.Core.Credentials;
using CiFarm.GraphQL;
using CiFarm.RestApi;
using CiFarm.Utils;
using Firesplash.GameDevAssets.SocketIOPlus;

namespace CiFarm
{
    /// <summary>
    /// Main SDK component for initializing the CiFarm SDK and configuring the environment.
    /// This component handles configuration for REST API, GraphQL, and Socket.IO connections,
    /// as well as authentication and credentials management.
    /// </summary>
    [UnityEngine.AddComponentMenu("CiFarm/CiFarm SDK")]
    public class CiFarmSDK : SingletonPersistent<CiFarmSDK>
    {
        #region REST API Configuration
        /// <summary>
        /// Dictionary to store REST API URLs for different environments (Local, Development, Staging, Production).
        /// Used to set the correct endpoint for API requests based on the selected environment.
        /// </summary>
        [UnityEngine.Header("REST API")]
        [UnityEngine.Tooltip("Set the REST API URLs for each environment")]
        [UnityEngine.SerializeField]
        private SerializedDictionary<Environment, string> _restApiUrls = new()
        {
            { Environment.Local, "http://localhost:3001" },
            { Environment.Development, "https://api.cifarm.dev.starci.net" },
            { Environment.Staging, "https://api.cifarm.staging.starci.net" },
            { Environment.Production, "https://api.cifarm.starci.net" },
        };

        /// <summary>
        /// The version of the REST API to be used for requests.
        /// </summary>
        [UnityEngine.Tooltip("Set the REST API version")]
        [UnityEngine.SerializeField]
        private RestApiVersion _restApiVersion = RestApiVersion.V1;
        #endregion

        #region GraphQL Configuration
        /// <summary>
        /// Dictionary to store GraphQL API URLs for different environments.
        /// Used to set the correct endpoint for GraphQL queries and mutations.
        /// </summary>
        [UnityEngine.Header("GraphQL")]
        [UnityEngine.Tooltip("Set the GraphQL API URLs for each environment")]
        [UnityEngine.SerializeField]
        private SerializedDictionary<Environment, string> _graphQLUrls = new()
        {
            { Environment.Local, "http://localhost:3006/graphql" },
            { Environment.Development, "https://graphql.cifarm.dev.starci.net/graphql" },
            { Environment.Staging, "https://graphql.cifarm.staging.starci.net/graphql" },
            { Environment.Production, "https://graphql.cifarm.starci.net/graphql" },
        };
        #endregion

        #region Socket.IO Configuration
        /// <summary>
        /// Dictionary to store Socket.IO server URLs for different environments.
        /// Used for establishing Socket.IO connections to the server for real-time communication.
        /// </summary>
        [UnityEngine.Header("Socket.IO")]
        [UnityEngine.Tooltip("Set the Socket.IO server address for each environment")]
        [UnityEngine.SerializeField]
        private SerializedDictionary<Environment, string> _socketIoUrls = new()
        {
            { Environment.Local, "http://localhost:3003" },
            { Environment.Development, "https://ws.cifarm.dev.starci.net" },
            { Environment.Staging, "https://ws.cifarm.staging.starci.net" },
            { Environment.Production, "https://ws.cifarm.starci.net" },
        };

        /// <summary>
        /// Socket.IO client instance used for real-time communication with the server.
        /// </summary>
        [UnityEngine.Tooltip("Set the Socket.IO client instance, leave empty to create a new one")]
        [UnityEngine.SerializeField]
        private SocketIOClient _socketIOClient;
        #endregion

        #region Common Configuration
        /// <summary>
        /// The environment setting for the SDK (Local, Development, Staging, Production).
        /// Determines which API and Socket.IO URLs to use based on the selected environment.
        /// </summary>
        [UnityEngine.Header("Common")]
        [UnityEngine.Tooltip("Set the environment for using CiFarm SDK")]
        [UnityEngine.SerializeField]
        private Environment _environment = Environment.Local;

        /// <summary>
        /// Log scope setting that helps in debugging by controlling the verbosity of logs.
        /// </summary>
        [UnityEngine.Tooltip("Set the log scope, useful for debugging")]
        [UnityEngine.SerializeField]
        private LogScope _logScope = LogScope.All;

        /// <summary>
        /// The interval (in milliseconds) between retry attempts in case of a failed request.
        /// </summary>
        [UnityEngine.Tooltip("Set the retry interval")]
        [UnityEngine.SerializeField]
        private int _retryInterval = 2000;

        /// <summary>
        /// The maximum number of retry attempts in case of a failed request.
        /// </summary>
        [UnityEngine.Tooltip("Set the retry count")]
        [UnityEngine.SerializeField]
        private int _retryCount = 2;
        #endregion

        #region Editor Configuration
        /// <summary>
        /// Credentials for the editor that are used for authentication and signature generation.
        /// </summary>
        [UnityEngine.Header("Editor")]
        [UnityEngine.Tooltip("Credentials for the developer")]
        [UnityEngine.SerializeField]
        private Credentials _credentials;

        public void SetCredentials(Credentials credentials)
        {
            _credentials = credentials;
        }

        /// <summary>
        /// Chain key used for the editor's authentication process.
        /// </summary>
        [UnityEngine.SerializeField]
        private SupportedChainKey _chainKey;

        /// <summary>
        /// The network setting for the editor's authentication process.
        /// </summary>
        [UnityEngine.SerializeField]
        private Network _network;

        /// <summary>
        /// Account number for the editor's authentication process.
        /// </summary>
        [UnityEngine.SerializeField]
        private int _accountNumber;
        #endregion

        /// <summary>
        /// API Client for performing REST API requests.
        /// </summary>
        public RestApiClient RestApiClient { get; set; }

        /// <summary>
        /// GraphQL Client for performing GraphQL queries and mutations.
        /// </summary>
        public GraphQLClient GraphQLClient { get; set; }

        /// <summary>
        /// Socket.IO Client for managing real-time communication with the server.
        /// </summary>
        public SocketIOClient SocketIOClient
        {
            get => _socketIOClient;
            set => _socketIOClient = value;
        }

        /// <summary>
        /// Starts the authentication process for the SDK.
        /// In the editor, it will authenticate using the provided credentials.
        /// </summary>
        public void Authenticate()
        {
            StartCoroutine(AuthenticateCoroutine());
        }

        /// <summary>
        /// Coroutine that waits for credentials to be available and starts the authentication process.
        /// In the Unity editor, it proceeds asynchronously without waiting for credentials.
        /// </summary>
        private IEnumerator AuthenticateCoroutine()
        {
#if UNITY_EDITOR
            AuthenticateAsync();
#else
            yield return new WaitUntil(() => _credentials != null);
            AuthenticateAsync(false);
#endif
            yield return null;
        }

        /// <summary>
        /// Handles the actual authentication process asynchronously.
        /// This involves generating a signature and verifying it to obtain access tokens.
        /// </summary>
        private async void AuthenticateAsync(bool editor = true)
        {
            if (editor)
            {
                // Generate signature for the editor using account and network details
                var generateSignatureResponse = await RestApiClient.GenerateSignatureAsync(
                    new()
                    {
                        ChainKey = _chainKey,
                        Network = _network,
                        AccountNumber = _accountNumber,
                    }
                );

                // Set the credentials with the generated signature and other details
                _credentials.ChainKey = generateSignatureResponse.ChainKey;
                _credentials.Message = generateSignatureResponse.Message;
                _credentials.PublicKey = generateSignatureResponse.PublicKey;
                _credentials.Signature = generateSignatureResponse.Signature;
                _credentials.Network = generateSignatureResponse.Network;
                _credentials.TelegramInitDataRaw = generateSignatureResponse.TelegramInitDataRaw;
                _credentials.AccountAddress = generateSignatureResponse.AccountAddress;
            }

            // Verify the generated signature and obtain the authentication token
            var verifyMessageResponse = await RestApiClient.VerifyMessageAsync(
                new()
                {
                    Message = _credentials.Message,
                    PublicKey = _credentials.PublicKey,
                    Signature = _credentials.Signature,
                    ChainKey = _credentials.ChainKey,
                    Network = _credentials.Network,
                    AccountAddress = _credentials.AccountAddress,
                }
            );

            // Save the authentication tokens (access and refresh tokens)
            AuthToken.Save(verifyMessageResponse.AccessToken, verifyMessageResponse.RefreshToken);
        }

        /// <summary>
        /// Initialize the SDK by setting up REST API, GraphQL, and Socket.IO clients.
        /// </summary>

        private string _restApiUrl;
        private string _graphQLUrl;
        private string _socketIoUrl;

        public void Start()
        {
            // Set the log scope for debugging
            ConsoleLogger.LogScope = _logScope;

            // Initialize the REST API client with the correct base URL and version
            _restApiUrl = _restApiUrls[_environment];
            RestApiClient = new RestApiClient()
            {
                BaseUrl = _restApiUrl,
                ApiVersion = _restApiVersion,
                RetryInterval = _retryInterval,
                RetryCount = _retryCount,
            };

            // Initialize the GraphQL client with the correct base URL and associated REST API client
            _graphQLUrl = _graphQLUrls[_environment];
            GraphQLClient = new GraphQLClient()
            {
                BaseUrl = _graphQLUrl,
                RestApiClient = RestApiClient,
                RetryCount = _retryCount,
                RetryInterval = _retryInterval,
            };

            // Initialize the Socket.IO client with the correct server address
            _socketIoUrl = _socketIoUrls[_environment];
            if (SocketIOClient == null)
            {
                SocketIOClient = gameObject.AddComponent<SocketIOClient>();
                SocketIOClient.serverAddress = _socketIoUrl;
            }

            ConsoleLogger.LogSuccess("CiFarm SDK initialized successfully");
        }
    }

    /// <summary>
    /// Enum to represent different environments (Local, Development, Staging, Production)
    /// Used for switching between different environments and configurations.
    /// </summary>
    public enum Environment
    {
        Local,
        Development,
        Staging,
        Production,
    }
}
