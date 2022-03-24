# Target

https://gleeder.app/

A dAPP on NEO blockchain.

# Description

this website provides the ability to exchange NEO to GAS without providing GAS yourself

# Analysis

* Gleeder uses a [contract](https://dora.coz.io/contract/neo3/testnet_rc4/0x4962afa13fb9367bc1a64d08603c0e66b2022b5e) as sender that will provide GAS for us.
* The transaction's script is verified carefully in the `verify` method by gleeder's contract.
* Because the check makes sure the user gives the Global witness scope, the contract can transfer NEO from the user's account. 
* It'll swap NEO to bNEO and from bNEO to GAS. It'll keep some GAS for the contract as a fee and send the other back to the user.

# Approach

The `verify` can be cheated in one block. Assume that I have 1 NEO, then I can swap it to GAS using gleeder. 
Before that happened, I can use my NEO in another way such as transferring to someone. 
Then the swap will fail, and the transaction fee is deducted from gleeder's account.

The simple POC is to swap many times in one block using gleeder while there is only 1 NEO in the attackers' account.

POC: [NEO testnet block 1359455](https://dora.coz.io/block/neo3/testnet_rc4/1359455)

* 45 transactions are sent and only [the first one](https://dora.coz.io/transaction/neo3/testnet_rc4/0x005e9492a7d22acc0d5a371cb261007089f9983f3992cccf2179006f5be6fb51) succeeded
* the contract had 10 GAS before that block and less than 2 GAS remained after that
