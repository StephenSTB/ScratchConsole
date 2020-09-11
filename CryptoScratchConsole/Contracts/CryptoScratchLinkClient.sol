pragma solidity ^0.6.0;

import "./node_modules/@chainlink/contracts/src/v0.6/ChainlinkClient.sol";

contract CryptoScratchLinkClient is ChainlinkClient {
  
    uint256 public ChainlinkPrice;
    
    address private oracle;
    bytes32 private jobId;
    uint256 private fee;
    
    /**
     * Network: Ropsten
     * Oracle: Chainlink - 0xc99B3D447826532722E41bc36e644ba3479E4365
     * Job ID: Chainlink - 3cff0a3524694ff8834bda9cf9c779a1
     * Fee: 0.1 LINK
     */
    constructor() public{
        setPublicChainlinkToken();
        oracle = 0xc99B3D447826532722E41bc36e644ba3479E4365;
        jobId = "3cff0a3524694ff8834bda9cf9c779a1";
        fee = 0.1 * 10 ** 18; // 0.1 LINK
    }
    
    /**
     * Create a Chainlink request to retrieve API response, find the target price
     * data, then multiply by 100 (to remove decimal places from price).
     */
    function requestLinkPrice() public returns (bytes32 requestId) 
    {
        Chainlink.Request memory request = buildChainlinkRequest(jobId, address(this), this.fulfill.selector);
        
        // Set the URL to perform the GET request on
        request.add("get", "https://min-api.cryptocompare.com/data/price?fsym=LINK&tsyms=USD");
        
        // Set the path to find the desired data in the API response, where the response format is:
        // {"USD":243.33}
        request.add("path", "USD");
        
        // Multiply the result by 100 to remove decimals
        request.addInt("times", 100);
        
        // Sends the request
        return sendChainlinkRequestTo(oracle, request, fee);
    }
    
    /**
     * Receive the response in the form of uint256
     */ 
    function fulfill(bytes32 _requestId, uint256 _price) public recordChainlinkFulfillment(_requestId)
    {
        ChainlinkPrice = _price;
    }

    function getPrice() public view returns(uint price){
        return ChainlinkPrice;
    }
}