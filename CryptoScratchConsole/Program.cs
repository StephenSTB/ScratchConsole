using System;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using Contracts.Contracts.Scratch;
using Contracts.Contracts.Scratch.ContractDefinition;

using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Nethereum.Contracts;
using Nethereum.ABI.FunctionEncoding.Attributes;
using System.Numerics;
using Nethereum.ABI.Util;
using Nethereum.RLP;
using Nethereum.Hex.HexConvertors.Extensions;
using System.Security.Principal;

using Contracts.Contracts.ScratchCardRound;
using Contracts.Contracts.ScratchCardRound.ContractDefinition;
using Org.BouncyCastle.Bcpg;
using Nethereum.RPC.Eth.Transactions;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Util;
using Contracts.Contracts.ScratchToken;

namespace ScratchConsole
{
    class Program
    {

        static Account account = new Account("d871d74211c60eb4b0baa664961362abcc0c98c6dc533c9e38ace62361dc21f5", null);

        //static Web3 web3 = new Web3(account, "https://ropsten.infura.io/v3/bf32e2587d834489b5b823d63d08efac");

        //static Web3 web3 = new Web3(account, "https://rinkeby.infura.io/v3/bf32e2587d834489b5b823d63d08efac");

        static Web3 web3 = new Web3(account, "https://kovan.infura.io/v3/bf32e2587d834489b5b823d63d08efac");

        static string ScratchContractAddress;
        static string RoundContractAddress;

        static ScratchService CsService;

        static ScratchCardRoundService RService;

        static void Main(string[] args)
        {

            InitializeSettings();

            Console.WriteLine("     Scratch Console Testing.\n");

            while (true)
            {
                Console.WriteLine("Enter Command: ");

                string commandString = Console.ReadLine();

                string[] command = commandString.Split(" ");

                Console.WriteLine("");

                switch (command[0])
                {
                    case "deploy":
                        Deploy().Wait();
                        break;
                    case "test":
                        Test().Wait();
                        break;
                    case "buycard":
                        BuyCard().Wait();
                        break;
                    case "cardinfo":
                        CardInfo().Wait();
                        break;
                    case "getbalance":
                        GetBalance().Wait();
                        break;
                    case "thresh":
                        ThresholdMet().Wait();
                        break;
                    case "round":
                        NewRoundTest().Wait();
                        break;
                    case "div":
                        ReceiveDividend().Wait();
                        break;
                    case "roundt":
                        TransferRoundLink().Wait();
                        break;
                    //case "req":
                        //RequestRandom().Wait();
                        //break;
                    default:
                        Console.WriteLine("Invalid command.");
                        break;
                }
            }
            

            /*
            var clientReq = s.ClientQueryAsync();

            clientReq.Wait();

            var Client = clientReq.Result;

            Console.WriteLine($"Client: {Client}");



            var transferLinktoClient = s.TransferLinkToClientRequestAndWaitForReceiptAsync(Web3.Convert.ToWei(1));

            transferLinktoClient.Wait();

            rec1 = s.GetLinkBalanceQueryAsync(Client);

            rec1.Wait();

            Console.WriteLine($"Link sent to Client: {Web3.Convert.FromWei(rec1.Result)}");
            */

            //GetLinkPrice().Wait();

            //GetScratchCard(s).Wait();

        }

        private static async Task TransferRoundLink()
        {
            // Approving the contract to use link.
            var approveHandler = web3.Eth.GetContractTransactionHandler<ApproveFunction>();

            var cardPrice = await RService.GetCardPriceQueryAsync();

            Console.WriteLine($"Card price: {cardPrice}");

            var totalRoundValue = (cardPrice * 250000) - await CsService.GetLinkBalanceQueryAsync(ScratchContractAddress);

            Console.WriteLine($"Total round value: {totalRoundValue}");

            var approve = new ApproveFunction()
            {
                Spender = ScratchContractAddress,
                TokenAmount = totalRoundValue
            };

            await approveHandler.SendRequestAndWaitForReceiptAsync("0xa36085F69e2889c224210F603D836748e7dC0088", approve);

            // Transfering link.
            var transferHandler = web3.Eth.GetContractTransactionHandler<TransferFunction>();
            var transfer = new TransferFunction()
            {
                To = ScratchContractAddress,
                TokenAmount = totalRoundValue
            };

            Console.WriteLine($"Token amount: {transfer.TokenAmount} in wei");

            var transactionReceipt = await transferHandler.SendRequestAndWaitForReceiptAsync("0xa36085F69e2889c224210F603D836748e7dC0088", transfer);

            var linkBalance = await CsService.GetLinkBalanceQueryAsync(ScratchContractAddress);

            Console.WriteLine($"Contract Link Balance: {Web3.Convert.FromWei(linkBalance)}");
        }

        private static async Task NewRoundTest()
        {
            
            /*

            var buyCard = await CsService.BuyScatchCardRequestAndWaitForReceiptAsync(100);
            
            var RequestIDEvent = buyCard.DecodeAllEvents<RequestIDEventDTO>();

            var RequestID = RequestIDEvent[0].Event.RequestId.ToHex();

            // Catch the prize claim event.
            var PrizeEventHandler = RService.ContractHandler.GetEvent<PrizeClaimEventDTO>();

            var filterPrizeEvents = PrizeEventHandler.CreateFilterInput();

            // Getting the PrizeClaim event with the correct RequestID
            while (true)
            {
                var allRequestEvents = await PrizeEventHandler.GetAllChanges(filterPrizeEvents);

                if (allRequestEvents.Count > 0)
                {
                    bool brk = false;
                    foreach (EventLog<PrizeClaimEventDTO> e in allRequestEvents)
                    {
                        //Console.WriteLine($"Scratch Card result, Address: {e.Event.Player},  RequestID: {e.Event.RequestId.ToHex()},  Prize Number: {e.Event.Number}, Prize: {Web3.Convert.FromWei(e.Event.Prize)}\n");
                        if (e.Event.RequestId.ToHex().Equals(RequestID))
                        {
                            Console.WriteLine($"Scratch Card result, Address: {e.Event.Player},  RequestID: {e.Event.RequestId.ToHex()},  Prize Number: {e.Event.Number}, Prize: {Web3.Convert.FromWei(e.Event.Prize)}\n");
                            brk = true;
                        }
                    }
                    if (brk)
                        break;
                }
            }*/

            Console.WriteLine($"Calling newRound function...");

            try
            {
                var gas = await CsService.ContractHandler.EstimateGasAsync<NewRoundFunction>();

                Console.WriteLine($"Gas needed for new round: {gas}");


                var rnd = new NewRoundFunction
                {
                    Gas = (BigInteger)gas
                };
                Console.WriteLine($"Gas given for new round: {rnd.Gas}");

                var newRound = await CsService.NewRoundRequestAndWaitForReceiptAsync(rnd);

                Console.WriteLine($"Success: {newRound.Succeeded()}");

                var round = await CsService.RoundNumberQueryAsync();

                var mint = await CsService.MintAmountQueryAsync();
                mint = await CsService.MintAmountQueryAsync();

                Console.WriteLine($"Mint amount: {mint}, Round: {round}");

                if (newRound.Succeeded())
                {
                    var gasEvent = newRound.DecodeAllEvents<GasLeftEventDTO>();

                    Console.WriteLine($"Emitted gas left: {gasEvent[0].Event.Gas}\n");

                    var events = newRound.DecodeAllEvents<NewRoundEventDTO>();

                    var e = events[0].Event;

                    Console.WriteLine($"current pool liquidity: {e.CurrentPool} \nTotal pool Liquidity: {e.TotalPool} \nNext Prize pool: {e.NextPrizePool} \n Token supply : {e.TokenSupply}" +
                        $"\nMint Amount: {e.MintAmount}\nRound: {e.RoundNumber}");
                }

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        private static async Task ReceiveDividend()
        {
            var linkBalance = await CsService.GetLinkBalanceQueryAsync(account.Address);

            Console.WriteLine($"Initial Link Balance: {linkBalance}");

            try
            {
                var receiveDiv = await CsService.ReceiveDividendRequestAndWaitForReceiptAsync();

                var events = receiveDiv.DecodeAllEvents<DividendEventDTO>();

                foreach (EventLog<DividendEventDTO> e in events)
                {
                    Console.WriteLine($"Player: {e.Event.Player} Round: {e.Event.Round} Pool Percentage: {e.Event.PoolPercent}\n" +
                        $"User Pool Percentage: {e.Event.UserPoolPercent} Dividend: {e.Event.Dividend}");

                    Console.WriteLine($"Expected Link Balance after round {e.Event.Round}: {linkBalance + e.Event.Dividend}");

                    linkBalance = await CsService.GetLinkBalanceQueryAsync(account.Address);
                    Console.WriteLine($"Actual Link Balance after round{e.Event.Round}: {linkBalance}");
                }

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            

        }

        private static async Task ThresholdMet()
        {
            var success = await RService.RoundOverThresholdQueryAsync();
            Console.WriteLine($"Round over Threshold met: {success}");
        }

        private static async Task BuyCard()
        {
            var accountEtherBalance = await web3.Eth.GetBalance.SendRequestAsync(account.Address);

            var accountLinkBalance = Web3.Convert.FromWei(await CsService.GetLinkBalanceQueryAsync(account.Address));

            Console.WriteLine($" Account: {account.Address}\n  Ether Balance: {Web3.Convert.FromWei(accountEtherBalance)}\n  Link Balance: {accountLinkBalance}\n");

            // Approving the contract to use 1 link.
            var approveHandler = web3.Eth.GetContractTransactionHandler<ApproveFunction>();

            var approve = new ApproveFunction()
            {
                Spender = ScratchContractAddress,
                TokenAmount = Web3.Convert.ToWei(1)
            };

            await approveHandler.SendRequestAndWaitForReceiptAsync("0xa36085F69e2889c224210F603D836748e7dC0088", approve);

            // Get Price of a card.
            //var LinkPrice = await CsService.RequestLinkPriceQueryAsync();

            //var CardValue = Web3.Convert.FromWei((2000000000000000000 / LinkPrice) * 100000000);

            // Getting ScratchCardRound handler.
            var address = await CsService.GetCardRoundQueryAsync();

            Console.WriteLine($"Card Round address: {address}");

            ScratchCardRoundService RS = new ScratchCardRoundService(web3, address);

            var CardValue = Web3.Convert.FromWei(await RS.GetCardPriceQueryAsync() + 100000000000000000);

            Console.WriteLine($"Price of Card: {CardValue} \n");
            
            Console.WriteLine("Buying card...");

            // Generating a seed 
            Random rand = new Random();

            uint seed = (uint)rand.Next();

            var buyCard = await CsService.BuyScatchCardRequestAndWaitForReceiptAsync(seed);

            // Getting the RequestID emited from the BuyScratchCard Request.
            var RequestID = buyCard.DecodeAllEvents<RequestIDEventDTO>()[0].Event.RequestId.ToHex();

            //Console.WriteLine($"RequestID: {RequestID}");

            /*
            var fulfillHandler = CsService.ContractHandler.GetEvent<RequestFulfilledEventDTO>();

            var filterFulfill = fulfillHandler.CreateFilterInput();

            while (true)
            {
                var requests = await fulfillHandler.GetAllChanges(filterFulfill);
                if(requests.Count > 0)
                {
                    Console.WriteLine($"Raw Random Number: {requests[0].Event.Randomness}");
                    break;
                }
                
            }*/

            var PrizeEventHandler = RS.ContractHandler.GetEvent<PrizeClaimEventDTO>();

            var filterPrizeEvents = PrizeEventHandler.CreateFilterInput();

            
            decimal prize  = 0;

            // Getting the PrizeClaim event with the correct RequestID
            while (true)
            {
                var allRequestEvents = await PrizeEventHandler.GetAllChanges(filterPrizeEvents);

                if (allRequestEvents.Count > 0)
                {
                    bool brk = false;
                    foreach (EventLog<PrizeClaimEventDTO> e in allRequestEvents)
                    {
                        //Console.WriteLine($"Scratch Card result, Address: {e.Event.Player},  RequestID: {e.Event.RequestId.ToHex()},  Prize Number: {e.Event.Number}, Prize: {Web3.Convert.FromWei(e.Event.Prize)}\n");
                        if (e.Event.RequestId.ToHex().Equals(RequestID))
                        {
                            Console.WriteLine($"Scratch Card result, Address: {e.Event.Player},  RequestID: {e.Event.RequestId.ToHex()},  Prize Number: {e.Event.Number}, Prize: {Web3.Convert.FromWei(e.Event.Prize)}\n");
                            prize = Web3.Convert.FromWei(e.Event.Prize);
                            brk = true;
                        }
                    }
                    if (brk)
                        break;
                }
            }

            Console.WriteLine($"Initial Account Link Balance: {accountLinkBalance},  Card Value: {CardValue}, Prize value: {prize}");

            Console.WriteLine($"Expected Link Balance: {accountLinkBalance - CardValue + prize}\n");

            accountLinkBalance = Web3.Convert.FromWei(await CsService.GetLinkBalanceQueryAsync(account.Address));
            Console.WriteLine($"Link Balance: {accountLinkBalance}\n");

            var ScratchTokenAddress = await CsService.GetTokenQueryAsync();

            ScratchTokenService tokenService = new ScratchTokenService(web3, ScratchTokenAddress);

            var ScratchTokenBalance = await tokenService.BalanceOfQueryAsync(account.Address);

            Console.WriteLine($"Scratch Token Balance: {ScratchTokenBalance}");
                
        }


        private static async Task CardInfo()
        {
            var cardRoundAddress = await CsService.GetCardRoundQueryAsync();

            var cardRoundService = new ScratchCardRoundService(web3, cardRoundAddress);

            var cardPrice = await cardRoundService.GetCardPriceQueryAsync();

            Console.WriteLine($"The current Price of a card is: {Web3.Convert.FromWei(cardPrice)}");

            var remainingPrizes = await cardRoundService.UnclaimedPrizesQueryAsync();

            for(int i = 0; i < remainingPrizes.Num.Count; i++)
            {
                Console.WriteLine($"Remaining cards: {remainingPrizes.Num[i]},  with pay: {Web3.Convert.FromWei(remainingPrizes.Pays[i])}");
            }

            Console.WriteLine();
        }

        private static async Task Test()
        {
            // Getting link price
            var requestPrice = await CsService.RequestLinkPriceQueryAsync();

            Console.WriteLine($"Link Price: {(decimal)requestPrice / 100000000}");

            // Getting contract link balance
            var balance = await CsService.GetLinkBalanceQueryAsync(ScratchContractAddress);

            Console.WriteLine($"Contract Link Balance: {Web3.Convert.FromWei(balance)}");


            // Approving the contract to use 1 link.
            var approveHandler = web3.Eth.GetContractTransactionHandler<ApproveFunction>();

            var approve = new ApproveFunction()
            {
                Spender = ScratchContractAddress,
                TokenAmount = Web3.Convert.ToWei(1),
                FromAddress = account.Address
            };

            var approveReciept = await approveHandler.SendRequestAndWaitForReceiptAsync("0xa36085F69e2889c224210F603D836748e7dC0088", approve);

            // Confirming contract link allowance.
            var allowance = await CsService.GetContractAllowanceQueryAsync();

            Console.WriteLine($"Allowance: {Web3.Convert.FromWei(allowance)}");

            Console.WriteLine($"Card cost in LINK: {Web3.Convert.FromWei((2000000000000000000 / requestPrice) * 100000000)}");

            Console.WriteLine($"Allowance after buying card should be: {Web3.Convert.FromWei(allowance - (2000000000000000000 / requestPrice) * 100000000)}\n");

            Console.WriteLine("Buying card...");

            // Generating a seed 
            Random rand = new Random();

            uint seed = (uint) rand.Next();

            Console.WriteLine($"Seed: {seed}");

            var gas = await CsService.ContractHandler.EstimateGasAsync<BuyScatchCardFunction>();

            var fullgas = Web3.Convert.ToWei(gas.Value, Nethereum.Util.UnitConversion.EthUnit.Gwei);

            Console.WriteLine($"expected gas to buy card: {Web3.Convert.FromWei(fullgas)}");

            

            var accountBalance = await web3.Eth.GetBalance.SendRequestAsync(account.Address);

            Console.WriteLine($"{account.Address} balance: {Web3.Convert.FromWei(accountBalance)}");


            // Buy Card Testing

            var buyCardReceipt = await CsService.BuyScatchCardRequestAndWaitForReceiptAsync(seed);

            var gasUsed = Web3.Convert.ToWei(buyCardReceipt.GasUsed, Nethereum.Util.UnitConversion.EthUnit.Gwei);

            Console.WriteLine($"Total gas consumed: {Web3.Convert.FromWei(gasUsed)}, {Web3.Convert.FromWei(Web3.Convert.ToWei(buyCardReceipt.CumulativeGasUsed, Nethereum.Util.UnitConversion.EthUnit.Gwei))}");

            Console.WriteLine($"Expected balance upon lose: {Web3.Convert.FromWei(accountBalance - gasUsed)} \n");

            var events = buyCardReceipt.DecodeAllEvents<RequestIDEventDTO>();

            var RequestID = events[0].Event.RequestId.ToHex();

            Console.WriteLine($"RequestID: {RequestID}");


            // Getting ScratchCardRound handler.
            var address = await CsService.GetCardRoundQueryAsync();

            ScratchCardRoundService RS = new ScratchCardRoundService(web3, address);

            var PrizeEventHandler = RS.ContractHandler.GetEvent<PrizeClaimEventDTO>();

            var filterPrizeEvents = PrizeEventHandler.CreateFilterInput();

            while (true)
            {
                var allRequestEvents = await PrizeEventHandler.GetAllChanges(filterPrizeEvents);

                if (allRequestEvents.Count > 0)
                {
                    bool brk = false;
                    foreach (EventLog<PrizeClaimEventDTO> e in allRequestEvents)
                    {
                        if (e.Event.RequestId.ToHex().Equals(RequestID))
                        {
                            Console.WriteLine($"Scratch Card result, Address: {e.Event.Player},  RequestID: {e.Event.RequestId.ToHex()},  Prize Number: {e.Event.Number}");
                            brk = true;
                        }
                        
                    }
                    if(brk)
                        break;
                }
            }

            accountBalance = await web3.Eth.GetBalance.SendRequestAsync(account.Address);

            Console.WriteLine($"{account.Address} balance: {Web3.Convert.FromWei(accountBalance)}");

            allowance = await CsService.GetContractAllowanceQueryAsync();

            Console.WriteLine($"Allowance after buying card: {Web3.Convert.FromWei(allowance)}");

            //var transfer = await CsService.TransferToContractRequestAndWaitForReceiptAsync();

            balance = await CsService.GetLinkBalanceQueryAsync(ScratchContractAddress);

            Console.WriteLine($"Contract Balance: {Web3.Convert.FromWei(balance)}");


            // Testing Card Round Stats.

            Console.WriteLine($"Card Round Address: {address}");


            var prize = await RS.UnclaimedPrizesQueryAsync();

            foreach(BigInteger i in prize.Num)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine();
            
            foreach(BigInteger i in prize.Pays)
            {
                Console.WriteLine(Web3.Convert.FromWei(i));
            }


            // Calling Claim Prize from outside of the Scratch contract
            /*
            Console.WriteLine("attemptin to call getprize from outside contract...");

            try
            {
                var address = await CsService.GetCardRoundQueryAsync();

                Console.WriteLine($"Card Round Address: {address}");

                ScratchCardRoundService RS = new ScratchCardRoundService(web3, address);

                ClaimPrizeFunction cpf = new ClaimPrizeFunction
                {
                    Player = address,
                    RandomNumber = new BigInteger(999999)
                };

                await RS.ClaimPrizeRequestAndWaitForReceiptAsync(cpf);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
           */

            Console.WriteLine();
        }


        [Function("approve","bool")]
        public class ApproveFunction : FunctionMessage
        {
            [Parameter("address", "spender", 1)]
            public string Spender { get; set; }

            [Parameter("uint256", "value", 2)]
            public BigInteger TokenAmount { get; set; }
        }

        // Method to Initialize Saved Settings.
        public static void InitializeSettings()
        {
            // Initialize contract variables.
            Configuration configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            KeyValueConfigurationCollection Settings = configFile.AppSettings.Settings;

            // Initialize Contract Address and ABI variables.
            ScratchContractAddress = Settings["ScratchContractAddress"].Value;

            RoundContractAddress = Settings["RoundContractAddress"].Value;
            
            CsService = new ScratchService(web3, ScratchContractAddress);

            RService = new ScratchCardRoundService(web3, RoundContractAddress);

        }

        /* Unbreaking my code testing
        private static async Task RequestRandom()
        {
            Random r = new Random();
            var seed = r.Next();
            var rand = await CsService.RequestRandomRequestAndWaitForReceiptAsync(seed);

            var events = rand.DecodeAllEvents<RequestIDEventDTO>();
            if(events.Count > 0)
            {
                Console.WriteLine(events[0].Event.RequestId.ToHex());
            }


            var result = await CsService.ResultQueryAsync();

            Console.WriteLine($"{result}");

            while(true)
            {
                result = await CsService.ResultQueryAsync();
                if (result != 0)
                {
                    Console.WriteLine($"{result}");
                    break;
                }
            }

            var randomness = CsService.ContractHandler.GetEvent<RequestFulfilledEventDTO>();

            var fulfilled = randomness.CreateFilterInput();

            while (true)
            {
                var ev = await randomness.GetAllChanges(fulfilled);

                if(ev.Count > 0)
                {
                    Console.WriteLine(ev[0].Event.Randomness);
                    break;
                }

            }

        }*/

        [Function("transfer", "bool")]
        public class TransferFunction : FunctionMessage
        {
            [Parameter("address", "to", 1)]
            public string To { get; set; }

            [Parameter("uint256", "value", 2)]
            public BigInteger TokenAmount { get; set; }
        }

        private static async Task Deploy()
        {
            Console.WriteLine("Deploying contract...");

            //GetBalance().Wait(); 

            ScratchDeployment SD = new ScratchDeployment
            {
                InitialLink = Web3.Convert.ToWei(1)
                //AmountToSend = 100000000000
            };

            try
            {
                var deployReciept = await ScratchService.DeployContractAndWaitForReceiptAsync(web3, SD);
                ScratchContractAddress = deployReciept.ContractAddress;
                Console.WriteLine($"gas: {Web3.Convert.FromWei(Web3.Convert.ToWei(deployReciept.GasUsed, UnitConversion.EthUnit.Gwei))}");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            Console.WriteLine($"Scratch contract address: {ScratchContractAddress}");

            //var balance = await web3.Eth.GetBalance.SendRequestAsync(ScratchContractAddress);

            //Console.WriteLine($"Scratch contract Ether: {balance}");

            // Open Settings.
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var Settings = configFile.AppSettings.Settings;

            Settings["ScratchContractAddress"].Value = ScratchContractAddress;

            CsService = new ScratchService(web3, ScratchContractAddress);

            var rec1 = CsService.GetLinkBalanceQueryAsync(account.Address);

            Console.WriteLine($"Account Link Balance: {Web3.Convert.FromWei(rec1.Result)}\n Account Eth Balance: {Web3.Convert.FromWei(await web3.Eth.GetBalance.SendRequestAsync(account.Address))}");

            /*
            var deposit = new DepositLinkFunction()
            {
                Value = 1
            };*/

            var transferHandler = web3.Eth.GetContractTransactionHandler<TransferFunction>();
            var transfer = new TransferFunction()
            {
                To = ScratchContractAddress,
                TokenAmount = Web3.Convert.ToWei(1)
            };

            Console.WriteLine($"Token amount: {transfer.TokenAmount} in wei");

            var transactionReceipt = await transferHandler.SendRequestAndWaitForReceiptAsync("0xa36085F69e2889c224210F603D836748e7dC0088", transfer);

            rec1 = CsService.GetLinkBalanceQueryAsync(ScratchContractAddress);

            Console.WriteLine($"Link sent to Contract: {Web3.Convert.FromWei(rec1.Result)}");

            RoundContractAddress = await CsService.GetCardRoundQueryAsync();

            Settings["RoundContractAddress"].Value = RoundContractAddress;

            RService = new ScratchCardRoundService(web3, RoundContractAddress);


            // Save Settings
            configFile.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);

            Console.WriteLine("\n");
        }

        /*
        private static async Task GetLinkPrice()
        {
            Account a = new Account("d871d74211c60eb4b0baa664961362abcc0c98c6dc533c9e38ace62361dc21f5", null);

            var web3 = new Web3(a, "https://ropsten.infura.io/v3/bf32e2587d834489b5b823d63d08efac");

            var deployReciept2 = await ScratchLinkClientService.DeployContractAndWaitForReceiptAsync(web3, new ScratchLinkClientDeployment());

            string ScratchContractAddress2 = deployReciept2.ScratchContractAddress;

            ScratchLinkClientService s2 = new ScratchLinkClientService(web3, ScratchContractAddress2);

            var transferHandler = web3.Eth.GetContractTransactionHandler<TransferFunction>();
            var transfer = new TransferFunction()
            {
                To = ScratchContractAddress2,
                TokenAmount = Web3.Convert.ToWei(1)
            };

            var transactionReceipt = await transferHandler.SendRequestAndWaitForReceiptAsync("0x20fE562d797A42Dcb3399062AE9546cd06f63280", transfer);

            var req = new RequestEthereumPriceFunction();

            var priceReq = await s2.RequestEthereumPriceRequestAndWaitForReceiptAsync(req);

            while (true)
            {
                var price = await s2.EthereumPriceQueryAsync();

                if(price > 0)
                {
                    Console.WriteLine($"Price {price}");
                }
            }

        }
        */

            

        static async Task GetScratchCard(ScratchService s)
        {
            //var Filter = await RequestEvent.CreateFilterAsync();

            Console.WriteLine("Getting Random Number..");
            /*
            var rndNum = new GetScratchCardFunction();

            var Getrnd1 = s.GetScratchCardRequestAndWaitForReceiptAsync(rndNum);

            var Getrnd2 = s.GetScratchCardRequestAndWaitForReceiptAsync(rndNum);

            Getrnd1.Wait();

            Getrnd2.Wait();

            Console.WriteLine($"RND1 block: {Getrnd1.Result.BlockNumber}\nRND2 block: {Getrnd2.Result.BlockNumber}");
            */
            // Logs
            /*
            var RequestEventHandler = s.ContractHandler.GetEvent<RandomNumberRequestEventDTO>();

            var filterAllRequestEvents = RequestEventHandler.CreateFilterInput();

            while (true)
            {
                var allRequestEvents = await RequestEventHandler.GetAllChanges(filterAllRequestEvents);

                foreach(EventLog<RandomNumberRequestEventDTO> r in allRequestEvents)
                {
                    Console.WriteLine($"Random Number: {r.Event.RandomNumber}");
                }
                
            }*/
            
        }

        static async Task GetBalance()
        { 
            var balance = await web3.Eth.GetBalance.SendRequestAsync("0xf4230F3813dA49d6E8BfF025a0cbE45521F4A71e");
            Console.WriteLine($"Balance in Wei: {balance.Value}");

            var etherAmount = Web3.Convert.FromWei(balance.Value);
            Console.WriteLine($"Balance in Ether: {etherAmount}");
        }
    }
}
