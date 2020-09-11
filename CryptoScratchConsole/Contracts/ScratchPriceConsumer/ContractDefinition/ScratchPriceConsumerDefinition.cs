using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts;
using System.Threading;

namespace Contracts.Contracts.ScratchPriceConsumer.ContractDefinition
{


    public partial class ScratchPriceConsumerDeployment : ScratchPriceConsumerDeploymentBase
    {
        public ScratchPriceConsumerDeployment() : base(BYTECODE) { }
        public ScratchPriceConsumerDeployment(string byteCode) : base(byteCode) { }
    }

    public class ScratchPriceConsumerDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "608060405234801561001057600080fd5b50600080546001600160a01b03191673396c5e36dd0a0f5a5d33dae44368d4193f69a1f0179055610162806100466000396000f3fe608060405234801561001057600080fd5b50600436106100365760003560e01c80630d0d2b011461003b5780638e15f47314610055575b600080fd5b61004361005d565b60408051918252519081900360200190f35b6100436100dd565b60008060009054906101000a90046001600160a01b03166001600160a01b0316638205bf6a6040518163ffffffff1660e01b815260040160206040518083038186803b1580156100ac57600080fd5b505afa1580156100c0573d6000803e3d6000fd5b505050506040513d60208110156100d657600080fd5b5051905090565b60008060009054906101000a90046001600160a01b03166001600160a01b03166350d25bcd6040518163ffffffff1660e01b815260040160206040518083038186803b1580156100ac57600080fdfea2646970667358221220d016646e4572a96ca284320b07700bec22ee9f01aa81658eded8c60e02c2e56c64736f6c63430006070033";
        public ScratchPriceConsumerDeploymentBase() : base(BYTECODE) { }
        public ScratchPriceConsumerDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class GetLatestPriceFunction : GetLatestPriceFunctionBase { }

    [Function("getLatestPrice", "int256")]
    public class GetLatestPriceFunctionBase : FunctionMessage
    {

    }

    public partial class GetLatestPriceTimestampFunction : GetLatestPriceTimestampFunctionBase { }

    [Function("getLatestPriceTimestamp", "uint256")]
    public class GetLatestPriceTimestampFunctionBase : FunctionMessage
    {

    }

    public partial class GetLatestPriceOutputDTO : GetLatestPriceOutputDTOBase { }

    [FunctionOutput]
    public class GetLatestPriceOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("int256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class GetLatestPriceTimestampOutputDTO : GetLatestPriceTimestampOutputDTOBase { }

    [FunctionOutput]
    public class GetLatestPriceTimestampOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }
}
