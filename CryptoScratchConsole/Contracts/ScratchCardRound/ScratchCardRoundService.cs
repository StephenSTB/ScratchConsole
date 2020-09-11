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
using Contracts.Contracts.ScratchCardRound.ContractDefinition;

namespace Contracts.Contracts.ScratchCardRound
{
    public partial class ScratchCardRoundService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, ScratchCardRoundDeployment scratchCardRoundDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<ScratchCardRoundDeployment>().SendRequestAndWaitForReceiptAsync(scratchCardRoundDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, ScratchCardRoundDeployment scratchCardRoundDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<ScratchCardRoundDeployment>().SendRequestAsync(scratchCardRoundDeployment);
        }

        public static async Task<ScratchCardRoundService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, ScratchCardRoundDeployment scratchCardRoundDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, scratchCardRoundDeployment, cancellationTokenSource);
            return new ScratchCardRoundService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public ScratchCardRoundService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<bool> RoundOverThresholdQueryAsync(RoundOverThresholdFunction roundOverThresholdFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<RoundOverThresholdFunction, bool>(roundOverThresholdFunction, blockParameter);
        }

        
        public Task<bool> RoundOverThresholdQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<RoundOverThresholdFunction, bool>(null, blockParameter);
        }

        public Task<string> ClaimPrizeRequestAsync(ClaimPrizeFunction claimPrizeFunction)
        {
             return ContractHandler.SendRequestAsync(claimPrizeFunction);
        }

        public Task<TransactionReceipt> ClaimPrizeRequestAndWaitForReceiptAsync(ClaimPrizeFunction claimPrizeFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(claimPrizeFunction, cancellationToken);
        }

        public Task<string> ClaimPrizeRequestAsync(string player, byte[] requestId, BigInteger randomNumber)
        {
            var claimPrizeFunction = new ClaimPrizeFunction();
                claimPrizeFunction.Player = player;
                claimPrizeFunction.RequestId = requestId;
                claimPrizeFunction.RandomNumber = randomNumber;
            
             return ContractHandler.SendRequestAsync(claimPrizeFunction);
        }

        public Task<TransactionReceipt> ClaimPrizeRequestAndWaitForReceiptAsync(string player, byte[] requestId, BigInteger randomNumber, CancellationTokenSource cancellationToken = null)
        {
            var claimPrizeFunction = new ClaimPrizeFunction();
                claimPrizeFunction.Player = player;
                claimPrizeFunction.RequestId = requestId;
                claimPrizeFunction.RandomNumber = randomNumber;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(claimPrizeFunction, cancellationToken);
        }

        public Task<BigInteger> GetCardPriceQueryAsync(GetCardPriceFunction getCardPriceFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetCardPriceFunction, BigInteger>(getCardPriceFunction, blockParameter);
        }

        
        public Task<BigInteger> GetCardPriceQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetCardPriceFunction, BigInteger>(null, blockParameter);
        }

        public Task<UnclaimedPrizesOutputDTO> UnclaimedPrizesQueryAsync(UnclaimedPrizesFunction unclaimedPrizesFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<UnclaimedPrizesFunction, UnclaimedPrizesOutputDTO>(unclaimedPrizesFunction, blockParameter);
        }

        public Task<UnclaimedPrizesOutputDTO> UnclaimedPrizesQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<UnclaimedPrizesFunction, UnclaimedPrizesOutputDTO>(null, blockParameter);
        }
    }
}
