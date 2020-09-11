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
using Contracts.Contracts.ScratchPriceConsumer.ContractDefinition;

namespace Contracts.Contracts.ScratchPriceConsumer
{
    public partial class ScratchPriceConsumerService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, ScratchPriceConsumerDeployment scratchPriceConsumerDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<ScratchPriceConsumerDeployment>().SendRequestAndWaitForReceiptAsync(scratchPriceConsumerDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, ScratchPriceConsumerDeployment scratchPriceConsumerDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<ScratchPriceConsumerDeployment>().SendRequestAsync(scratchPriceConsumerDeployment);
        }

        public static async Task<ScratchPriceConsumerService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, ScratchPriceConsumerDeployment scratchPriceConsumerDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, scratchPriceConsumerDeployment, cancellationTokenSource);
            return new ScratchPriceConsumerService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public ScratchPriceConsumerService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<BigInteger> GetLatestPriceQueryAsync(GetLatestPriceFunction getLatestPriceFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetLatestPriceFunction, BigInteger>(getLatestPriceFunction, blockParameter);
        }

        
        public Task<BigInteger> GetLatestPriceQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetLatestPriceFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> GetLatestPriceTimestampQueryAsync(GetLatestPriceTimestampFunction getLatestPriceTimestampFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetLatestPriceTimestampFunction, BigInteger>(getLatestPriceTimestampFunction, blockParameter);
        }

        
        public Task<BigInteger> GetLatestPriceTimestampQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetLatestPriceTimestampFunction, BigInteger>(null, blockParameter);
        }
    }
}
