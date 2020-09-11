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
using Contracts.Contracts.Scratch.ContractDefinition;

namespace Contracts.Contracts.Scratch
{
    public partial class ScratchService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, ScratchDeployment scratchDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<ScratchDeployment>().SendRequestAndWaitForReceiptAsync(scratchDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, ScratchDeployment scratchDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<ScratchDeployment>().SendRequestAsync(scratchDeployment);
        }

        public static async Task<ScratchService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, ScratchDeployment scratchDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, scratchDeployment, cancellationTokenSource);
            return new ScratchService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public ScratchService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<BigInteger> MintAmountQueryAsync(MintAmountFunction mintAmountFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<MintAmountFunction, BigInteger>(mintAmountFunction, blockParameter);
        }

        
        public Task<BigInteger> MintAmountQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<MintAmountFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> RoundNumberQueryAsync(RoundNumberFunction roundNumberFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<RoundNumberFunction, BigInteger>(roundNumberFunction, blockParameter);
        }

        
        public Task<BigInteger> RoundNumberQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<RoundNumberFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> BuyScatchCardRequestAsync(BuyScatchCardFunction buyScatchCardFunction)
        {
             return ContractHandler.SendRequestAsync(buyScatchCardFunction);
        }

        public Task<TransactionReceipt> BuyScatchCardRequestAndWaitForReceiptAsync(BuyScatchCardFunction buyScatchCardFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(buyScatchCardFunction, cancellationToken);
        }

        public Task<string> BuyScatchCardRequestAsync(BigInteger userProvidedSeed)
        {
            var buyScatchCardFunction = new BuyScatchCardFunction();
                buyScatchCardFunction.UserProvidedSeed = userProvidedSeed;
            
             return ContractHandler.SendRequestAsync(buyScatchCardFunction);
        }

        public Task<TransactionReceipt> BuyScatchCardRequestAndWaitForReceiptAsync(BigInteger userProvidedSeed, CancellationTokenSource cancellationToken = null)
        {
            var buyScatchCardFunction = new BuyScatchCardFunction();
                buyScatchCardFunction.UserProvidedSeed = userProvidedSeed;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(buyScatchCardFunction, cancellationToken);
        }

        public Task<BigInteger> CurrentPoolTotalQueryAsync(CurrentPoolTotalFunction currentPoolTotalFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CurrentPoolTotalFunction, BigInteger>(currentPoolTotalFunction, blockParameter);
        }

        
        public Task<BigInteger> CurrentPoolTotalQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CurrentPoolTotalFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> GetCardRoundQueryAsync(GetCardRoundFunction getCardRoundFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetCardRoundFunction, string>(getCardRoundFunction, blockParameter);
        }

        
        public Task<string> GetCardRoundQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetCardRoundFunction, string>(null, blockParameter);
        }

        public Task<BigInteger> GetContractAllowanceQueryAsync(GetContractAllowanceFunction getContractAllowanceFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetContractAllowanceFunction, BigInteger>(getContractAllowanceFunction, blockParameter);
        }

        
        public Task<BigInteger> GetContractAllowanceQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetContractAllowanceFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> GetLinkBalanceQueryAsync(GetLinkBalanceFunction getLinkBalanceFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetLinkBalanceFunction, BigInteger>(getLinkBalanceFunction, blockParameter);
        }

        
        public Task<BigInteger> GetLinkBalanceQueryAsync(string owner, BlockParameter blockParameter = null)
        {
            var getLinkBalanceFunction = new GetLinkBalanceFunction();
                getLinkBalanceFunction.Owner = owner;
            
            return ContractHandler.QueryAsync<GetLinkBalanceFunction, BigInteger>(getLinkBalanceFunction, blockParameter);
        }

        public Task<string> GetTokenQueryAsync(GetTokenFunction getTokenFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetTokenFunction, string>(getTokenFunction, blockParameter);
        }

        
        public Task<string> GetTokenQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetTokenFunction, string>(null, blockParameter);
        }

        public Task<string> NewRoundRequestAsync(NewRoundFunction newRoundFunction)
        {
             return ContractHandler.SendRequestAsync(newRoundFunction);
        }

        public Task<string> NewRoundRequestAsync()
        {
             return ContractHandler.SendRequestAsync<NewRoundFunction>();
        }

        public Task<TransactionReceipt> NewRoundRequestAndWaitForReceiptAsync(NewRoundFunction newRoundFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(newRoundFunction, cancellationToken);
        }

        public Task<TransactionReceipt> NewRoundRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<NewRoundFunction>(null, cancellationToken);
        }

        public Task<BigInteger> NoncesQueryAsync(NoncesFunction noncesFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<NoncesFunction, BigInteger>(noncesFunction, blockParameter);
        }

        
        public Task<BigInteger> NoncesQueryAsync(byte[] returnValue1, BlockParameter blockParameter = null)
        {
            var noncesFunction = new NoncesFunction();
                noncesFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryAsync<NoncesFunction, BigInteger>(noncesFunction, blockParameter);
        }

        public Task<string> RawFulfillRandomnessRequestAsync(RawFulfillRandomnessFunction rawFulfillRandomnessFunction)
        {
             return ContractHandler.SendRequestAsync(rawFulfillRandomnessFunction);
        }

        public Task<TransactionReceipt> RawFulfillRandomnessRequestAndWaitForReceiptAsync(RawFulfillRandomnessFunction rawFulfillRandomnessFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(rawFulfillRandomnessFunction, cancellationToken);
        }

        public Task<string> RawFulfillRandomnessRequestAsync(byte[] requestId, BigInteger randomness)
        {
            var rawFulfillRandomnessFunction = new RawFulfillRandomnessFunction();
                rawFulfillRandomnessFunction.RequestId = requestId;
                rawFulfillRandomnessFunction.Randomness = randomness;
            
             return ContractHandler.SendRequestAsync(rawFulfillRandomnessFunction);
        }

        public Task<TransactionReceipt> RawFulfillRandomnessRequestAndWaitForReceiptAsync(byte[] requestId, BigInteger randomness, CancellationTokenSource cancellationToken = null)
        {
            var rawFulfillRandomnessFunction = new RawFulfillRandomnessFunction();
                rawFulfillRandomnessFunction.RequestId = requestId;
                rawFulfillRandomnessFunction.Randomness = randomness;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(rawFulfillRandomnessFunction, cancellationToken);
        }

        public Task<string> ReceiveDividendRequestAsync(ReceiveDividendFunction receiveDividendFunction)
        {
             return ContractHandler.SendRequestAsync(receiveDividendFunction);
        }

        public Task<string> ReceiveDividendRequestAsync()
        {
             return ContractHandler.SendRequestAsync<ReceiveDividendFunction>();
        }

        public Task<TransactionReceipt> ReceiveDividendRequestAndWaitForReceiptAsync(ReceiveDividendFunction receiveDividendFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(receiveDividendFunction, cancellationToken);
        }

        public Task<TransactionReceipt> ReceiveDividendRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<ReceiveDividendFunction>(null, cancellationToken);
        }

        public Task<BigInteger> RequestLinkPriceQueryAsync(RequestLinkPriceFunction requestLinkPriceFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<RequestLinkPriceFunction, BigInteger>(requestLinkPriceFunction, blockParameter);
        }

        
        public Task<BigInteger> RequestLinkPriceQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<RequestLinkPriceFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> RequestRandomnessRequestAsync(RequestRandomnessFunction requestRandomnessFunction)
        {
             return ContractHandler.SendRequestAsync(requestRandomnessFunction);
        }

        public Task<TransactionReceipt> RequestRandomnessRequestAndWaitForReceiptAsync(RequestRandomnessFunction requestRandomnessFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(requestRandomnessFunction, cancellationToken);
        }

        public Task<string> RequestRandomnessRequestAsync(byte[] keyHash, BigInteger fee, BigInteger seed)
        {
            var requestRandomnessFunction = new RequestRandomnessFunction();
                requestRandomnessFunction.KeyHash = keyHash;
                requestRandomnessFunction.Fee = fee;
                requestRandomnessFunction.Seed = seed;
            
             return ContractHandler.SendRequestAsync(requestRandomnessFunction);
        }

        public Task<TransactionReceipt> RequestRandomnessRequestAndWaitForReceiptAsync(byte[] keyHash, BigInteger fee, BigInteger seed, CancellationTokenSource cancellationToken = null)
        {
            var requestRandomnessFunction = new RequestRandomnessFunction();
                requestRandomnessFunction.KeyHash = keyHash;
                requestRandomnessFunction.Fee = fee;
                requestRandomnessFunction.Seed = seed;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(requestRandomnessFunction, cancellationToken);
        }
    }
}
