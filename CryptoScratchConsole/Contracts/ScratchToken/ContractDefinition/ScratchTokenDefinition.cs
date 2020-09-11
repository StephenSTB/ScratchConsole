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

namespace Contracts.Contracts.ScratchToken.ContractDefinition
{


    public partial class ScratchTokenDeployment : ScratchTokenDeploymentBase
    {
        public ScratchTokenDeployment() : base(BYTECODE) { }
        public ScratchTokenDeployment(string byteCode) : base(byteCode) { }
    }

    public class ScratchTokenDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "608060405234801561001057600080fd5b50604051610a62380380610a628339818101604052606081101561003357600080fd5b810190808051604051939291908464010000000082111561005357600080fd5b90830190602082018581111561006857600080fd5b825164010000000081118282018810171561008257600080fd5b82525081516020918201929091019080838360005b838110156100af578181015183820152602001610097565b50505050905090810190601f1680156100dc5780820380516001836020036101000a031916815260200191505b5060408181526020830151920180519294919391928464010000000082111561010457600080fd5b90830190602082018581111561011957600080fd5b825164010000000081118282018810171561013357600080fd5b82525081516020918201929091019080838360005b83811015610160578181015183820152602001610148565b50505050905090810190601f16801561018d5780820380516001836020036101000a031916815260200191505b50604052505060006002555082516101ac9060049060208601906101eb565b506005805460ff191660ff841617905580516101cf9060069060208401906101eb565b5050600380546001600160a01b03191633179055506102869050565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282601f1061022c57805160ff1916838001178555610259565b82800160010185558215610259579182015b8281111561025957825182559160200191906001019061023e565b50610265929150610269565b5090565b61028391905b80821115610265576000815560010161026f565b90565b6107cd806102956000396000f3fe608060405234801561001057600080fd5b50600436106100b45760003560e01c806340c10f191161007157806340c10f191461020a5780635c6581651461023857806370a082311461026657806395d89b411461028c578063a9059cbb14610294578063dd62ed3e146102c0576100b4565b806306fdde03146100b9578063095ea7b31461013657806318160ddd1461017657806323b872dd1461019057806327e235e3146101c6578063313ce567146101ec575b600080fd5b6100c16102ee565b6040805160208082528351818301528351919283929083019185019080838360005b838110156100fb5781810151838201526020016100e3565b50505050905090810190601f1680156101285780820380516001836020036101000a031916815260200191505b509250505060405180910390f35b6101626004803603604081101561014c57600080fd5b506001600160a01b03813516906020013561037c565b604080519115158252519081900360200190f35b61017e6103e2565b60408051918252519081900360200190f35b610162600480360360608110156101a657600080fd5b506001600160a01b038135811691602081013590911690604001356103e8565b61017e600480360360208110156101dc57600080fd5b50356001600160a01b031661051b565b6101f461052d565b6040805160ff9092168252519081900360200190f35b6102366004803603604081101561022057600080fd5b506001600160a01b038135169060200135610536565b005b61017e6004803603604081101561024e57600080fd5b506001600160a01b03813581169160200135166105b7565b61017e6004803603602081101561027c57600080fd5b50356001600160a01b03166105d4565b6100c16105ef565b610162600480360360408110156102aa57600080fd5b506001600160a01b03813516906020013561064a565b61017e600480360360408110156102d657600080fd5b506001600160a01b0381358116916020013516610704565b6004805460408051602060026001851615610100026000190190941693909304601f810184900484028201840190925281815292918301828280156103745780601f1061034957610100808354040283529160200191610374565b820191906000526020600020905b81548152906001019060200180831161035757829003601f168201915b505050505081565b3360008181526001602090815260408083206001600160a01b038716808552908352818420869055815186815291519394909390927f8c5be1e5ebec7d5bd14f71427d1e84f3dd0314c0f7b2291e5b200ac8c7c3b925928290030190a350600192915050565b60025481565b6001600160a01b03831660008181526001602090815260408083203384528252808320549383529082905281205490919083118015906104285750828110155b6104635760405162461bcd60e51b815260040180806020018281038252603981526020018061075f6039913960400191505060405180910390fd5b6001600160a01b03808516600090815260208190526040808220805487019055918716815220805484900390556000198110156104c5576001600160a01b03851660009081526001602090815260408083203384529091529020805484900390555b836001600160a01b0316856001600160a01b03167fddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef856040518082815260200191505060405180910390a3506001949350505050565b60006020819052908152604090205481565b60055460ff1681565b6003546001600160a01b0316331461058c576040805162461bcd60e51b8152602060048201526014602482015273125b9d985b1a59081b5a5b9d08185d1d195b5c1d60621b604482015290519081900360640190fd5b6002805460010190556001600160a01b03909116600090815260208190526040902080549091019055565b600160209081526000928352604080842090915290825290205481565b6001600160a01b031660009081526020819052604090205490565b6006805460408051602060026001851615610100026000190190941693909304601f810184900484028201840190925281815292918301828280156103745780601f1061034957610100808354040283529160200191610374565b336000908152602081905260408120548211156106985760405162461bcd60e51b815260040180806020018281038252602f815260200180610730602f913960400191505060405180910390fd5b33600081815260208181526040808320805487900390556001600160a01b03871680845292819020805487019055805186815290519293927fddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef929181900390910190a350600192915050565b6001600160a01b0391821660009081526001602090815260408083209390941682529190915220549056fe746f6b656e2062616c616e6365206973206c6f776572207468616e207468652076616c756520726571756573746564746f6b656e2062616c616e6365206f7220616c6c6f77616e6365206973206c6f776572207468616e20616d6f756e7420726571756573746564a264697066735822122076251d863d4d30402d2f8dc76bc709834949f1126551ba5c3300223efb268ce264736f6c63430006070033";
        public ScratchTokenDeploymentBase() : base(BYTECODE) { }
        public ScratchTokenDeploymentBase(string byteCode) : base(byteCode) { }
        [Parameter("string", "_tokenName", 1)]
        public virtual string TokenName { get; set; }
        [Parameter("uint8", "_decimalUnits", 2)]
        public virtual byte DecimalUnits { get; set; }
        [Parameter("string", "_tokenSymbol", 3)]
        public virtual string TokenSymbol { get; set; }
    }

    public partial class AllowanceFunction : AllowanceFunctionBase { }

    [Function("allowance", "uint256")]
    public class AllowanceFunctionBase : FunctionMessage
    {
        [Parameter("address", "_owner", 1)]
        public virtual string Owner { get; set; }
        [Parameter("address", "_spender", 2)]
        public virtual string Spender { get; set; }
    }

    public partial class AllowedFunction : AllowedFunctionBase { }

    [Function("allowed", "uint256")]
    public class AllowedFunctionBase : FunctionMessage
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
        [Parameter("address", "", 2)]
        public virtual string ReturnValue2 { get; set; }
    }

    public partial class ApproveFunction : ApproveFunctionBase { }

    [Function("approve", "bool")]
    public class ApproveFunctionBase : FunctionMessage
    {
        [Parameter("address", "_spender", 1)]
        public virtual string Spender { get; set; }
        [Parameter("uint256", "_value", 2)]
        public virtual BigInteger Value { get; set; }
    }

    public partial class BalanceOfFunction : BalanceOfFunctionBase { }

    [Function("balanceOf", "uint256")]
    public class BalanceOfFunctionBase : FunctionMessage
    {
        [Parameter("address", "_owner", 1)]
        public virtual string Owner { get; set; }
    }

    public partial class BalancesFunction : BalancesFunctionBase { }

    [Function("balances", "uint256")]
    public class BalancesFunctionBase : FunctionMessage
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class DecimalsFunction : DecimalsFunctionBase { }

    [Function("decimals", "uint8")]
    public class DecimalsFunctionBase : FunctionMessage
    {

    }

    public partial class MintFunction : MintFunctionBase { }

    [Function("mint")]
    public class MintFunctionBase : FunctionMessage
    {
        [Parameter("address", "receiver", 1)]
        public virtual string Receiver { get; set; }
        [Parameter("uint256", "amount", 2)]
        public virtual BigInteger Amount { get; set; }
    }

    public partial class NameFunction : NameFunctionBase { }

    [Function("name", "string")]
    public class NameFunctionBase : FunctionMessage
    {

    }

    public partial class SymbolFunction : SymbolFunctionBase { }

    [Function("symbol", "string")]
    public class SymbolFunctionBase : FunctionMessage
    {

    }

    public partial class TotalSupplyFunction : TotalSupplyFunctionBase { }

    [Function("totalSupply", "uint256")]
    public class TotalSupplyFunctionBase : FunctionMessage
    {

    }

    public partial class TransferFunction : TransferFunctionBase { }

    [Function("transfer", "bool")]
    public class TransferFunctionBase : FunctionMessage
    {
        [Parameter("address", "_to", 1)]
        public virtual string To { get; set; }
        [Parameter("uint256", "_value", 2)]
        public virtual BigInteger Value { get; set; }
    }

    public partial class TransferFromFunction : TransferFromFunctionBase { }

    [Function("transferFrom", "bool")]
    public class TransferFromFunctionBase : FunctionMessage
    {
        [Parameter("address", "_from", 1)]
        public virtual string From { get; set; }
        [Parameter("address", "_to", 2)]
        public virtual string To { get; set; }
        [Parameter("uint256", "_value", 3)]
        public virtual BigInteger Value { get; set; }
    }

    public partial class ApprovalEventDTO : ApprovalEventDTOBase { }

    [Event("Approval")]
    public class ApprovalEventDTOBase : IEventDTO
    {
        [Parameter("address", "_owner", 1, true )]
        public virtual string Owner { get; set; }
        [Parameter("address", "_spender", 2, true )]
        public virtual string Spender { get; set; }
        [Parameter("uint256", "_value", 3, false )]
        public virtual BigInteger Value { get; set; }
    }

    public partial class TransferEventDTO : TransferEventDTOBase { }

    [Event("Transfer")]
    public class TransferEventDTOBase : IEventDTO
    {
        [Parameter("address", "_from", 1, true )]
        public virtual string From { get; set; }
        [Parameter("address", "_to", 2, true )]
        public virtual string To { get; set; }
        [Parameter("uint256", "_value", 3, false )]
        public virtual BigInteger Value { get; set; }
    }

    public partial class AllowanceOutputDTO : AllowanceOutputDTOBase { }

    [FunctionOutput]
    public class AllowanceOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "remaining", 1)]
        public virtual BigInteger Remaining { get; set; }
    }

    public partial class AllowedOutputDTO : AllowedOutputDTOBase { }

    [FunctionOutput]
    public class AllowedOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }



    public partial class BalanceOfOutputDTO : BalanceOfOutputDTOBase { }

    [FunctionOutput]
    public class BalanceOfOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "balance", 1)]
        public virtual BigInteger Balance { get; set; }
    }

    public partial class BalancesOutputDTO : BalancesOutputDTOBase { }

    [FunctionOutput]
    public class BalancesOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class DecimalsOutputDTO : DecimalsOutputDTOBase { }

    [FunctionOutput]
    public class DecimalsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint8", "", 1)]
        public virtual byte ReturnValue1 { get; set; }
    }



    public partial class NameOutputDTO : NameOutputDTOBase { }

    [FunctionOutput]
    public class NameOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class SymbolOutputDTO : SymbolOutputDTOBase { }

    [FunctionOutput]
    public class SymbolOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class TotalSupplyOutputDTO : TotalSupplyOutputDTOBase { }

    [FunctionOutput]
    public class TotalSupplyOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }




}
