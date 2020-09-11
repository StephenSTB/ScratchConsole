pragma solidity ^0.6.7;

import "./node_modules/@chainlink/contracts/src/v0.6/interfaces/AggregatorInterface.sol";

contract ScratchPriceConsumer {

    AggregatorInterface internal priceFeed;
  
    /**
     * Network: Ropsten
     * Aggregator: LINK/USD
     * Address: 0xc21c178fE75aAd2017DA25071c54462e26d8face
     */

     // Kovan address 0x396c5E36DD0a0F5a5D33dae44368D4193f69a1F0
    constructor() public {
        priceFeed = AggregatorInterface(0x396c5E36DD0a0F5a5D33dae44368D4193f69a1F0);
    }
  
    /**
     * Returns the latest price
     */
    function getLatestPrice() public view returns (int256) {
        return priceFeed.latestAnswer();
    }

    /**
     * Returns the timestamp of the latest price update
     */
    function getLatestPriceTimestamp() public view returns (uint256) {
        return priceFeed.latestTimestamp();
    }
}