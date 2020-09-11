using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Contracts;
using System.Threading;
using Contracts.Contracts.APIConsumer.ContractDefinition;

namespace Contracts.Contracts.APIConsumer
{
    public partial class APIConsumerService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, APIConsumerDeployment aPIConsumerDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<APIConsumerDeployment>().SendRequestAndWaitForReceiptAsync(aPIConsumerDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, APIConsumerDeployment aPIConsumerDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<APIConsumerDeployment>().SendRequestAsync(aPIConsumerDeployment);
        }

        public static async Task<APIConsumerService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, APIConsumerDeployment aPIConsumerDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, aPIConsumerDeployment, cancellationTokenSource);
            return new APIConsumerService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public APIConsumerService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<BigInteger> EthereumPriceQueryAsync(EthereumPriceFunction ethereumPriceFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<EthereumPriceFunction, BigInteger>(ethereumPriceFunction, blockParameter);
        }

        
        public Task<BigInteger> EthereumPriceQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<EthereumPriceFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> FulfillRequestAsync(FulfillFunction fulfillFunction)
        {
             return ContractHandler.SendRequestAsync(fulfillFunction);
        }

        public Task<TransactionReceipt> FulfillRequestAndWaitForReceiptAsync(FulfillFunction fulfillFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(fulfillFunction, cancellationToken);
        }

        public Task<string> FulfillRequestAsync(byte[] requestId, BigInteger price)
        {
            var fulfillFunction = new FulfillFunction();
                fulfillFunction.RequestId = requestId;
                fulfillFunction.Price = price;
            
             return ContractHandler.SendRequestAsync(fulfillFunction);
        }

        public Task<TransactionReceipt> FulfillRequestAndWaitForReceiptAsync(byte[] requestId, BigInteger price, CancellationTokenSource cancellationToken = null)
        {
            var fulfillFunction = new FulfillFunction();
                fulfillFunction.RequestId = requestId;
                fulfillFunction.Price = price;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(fulfillFunction, cancellationToken);
        }

        public Task<string> RequestEthereumPriceRequestAsync(RequestEthereumPriceFunction requestEthereumPriceFunction)
        {
             return ContractHandler.SendRequestAsync(requestEthereumPriceFunction);
        }

        public Task<string> RequestEthereumPriceRequestAsync()
        {
             return ContractHandler.SendRequestAsync<RequestEthereumPriceFunction>();
        }

        public Task<TransactionReceipt> RequestEthereumPriceRequestAndWaitForReceiptAsync(RequestEthereumPriceFunction requestEthereumPriceFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(requestEthereumPriceFunction, cancellationToken);
        }

        public Task<TransactionReceipt> RequestEthereumPriceRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<RequestEthereumPriceFunction>(null, cancellationToken);
        }
    }
}
