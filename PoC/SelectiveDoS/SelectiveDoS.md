# Selective DoS on NEO Blockchain by SystemFee

> Selective DoS can make the normal transactions be denied while make some specific transactons bypass the DoS.

## TARGET COMMIT

V3.4.0

## Possible Attack Scenario

* Selective DoS the chain. Withdraw money from someone such as CEX, then the tx will not be packed into new blocks for a specific period. Argue the fund is not received, and let them send again. After DoS, attacker will receive the fund two or more times.
* Selective DoS the chain. wait until there is a profit between CEX and DEX, send a profitable tx that can bypass the DoS.
* Selective DoS the chain by a few blocks on specific time when token price is changing violently. Some CEX/DEX arbitrage bot such as `Nc6LJ79RodHzaz5BghHGChMZYRa9GqJvES` will send the arbitrage transaction once and once again. After the DoS, those bot will lose money for sending too many transactions while attackers can earn from that.

## Background

* NEO node will sort the uncommitted transactions by their **networkFee per byte**, see [source code](https://github.com/neo-project/neo/blob/77ee2cc5b6ea371efdf3be506b173c6304b0fc01/src/Neo/Ledger/PoolItem.cs#L46-L58)
* NEO consensus node will restrict the max systemFee used by a block, current amount is **1500 GAS** by default, see [source code](https://github.com/neo-project/neo-modules/blob/7db1c7956ac68758793a6ea30b5329cceb6ab1bc/src/DBFTPlugin/Settings.cs#L31)
* NEO consensus node will pick transactons one by one in the above order until the total systemFee reach the upper limit, **the last transaction that makes the block reach the upper limit will be dropped and put into the transaction pool again** and no more transaction will be packed into the block, see [source code](https://github.com/neo-project/neo-modules/blob/7db1c7956ac68758793a6ea30b5329cceb6ab1bc/src/DBFTPlugin/Consensus/ConsensusContext.MakePayload.cs#L88-L101)

## Attack Method and Cost Analysis

1. Build a transaction (named **FilterTx**) who have a relatively large networkFeePerByte and a very large systemFee which exceed the block's upper limit. Set the transaction's valid time to a specific point which must be in 1 blocktime to 1 day
2. Build some profitable transactions with a networkFee slightly higher than the **FilterTx**, then our transactions can be packed into new blocks while other normal users' transaction will not unless they add enough networkFee
3. collect other users' normal transactions especially those who contain NEP17Transfer
4. resend those collected profitable transactions to make a profit

Only the transactions bypass the DoS will cost some GAS. Because **FilterTx** will not be packed into blocks, attacker only need to hold instead of spend 1500.00000001 GAS.

## Effect

* The blockchain **seems good as usual** while normal users' transaction can not be packed into the new blocks.
* Some users may send their transactions again and again and lose money from this. They may think that their previous transactions are dropped.

## Experiment

* build a neo-node extension with one command named **a**, it accept two arguments - systemFee and networkFee, and will send an empty transaction with the specified fee, it will be valid during next 5 blocks

### Experiment 1: DoS

#### 1. steps

* before block 1, `a 2000 2` will send a **FilterTx** to the blockchain
* in the next block, use `a 1 1`, it will send a normal transaction to the blockchain
* in the next block, use `a 1 1`, it will send a normal transaction to the blockchain

#### 1. results

* during block 1 to block 5, no transaction is packed
* in the block 6, two transaction made by `a 1 1` are packed because the DoS is end

### Experiment 2: Selective DoS

#### 2. steps

* before block 1, `a 2000 2` will send a **FilterTx** to the blockchain
* in the next block, use `a 1 1`, it will send a normal transaction to the blockchain
* at the same block, use `a 1 3`, it will send a selective transaction which can bypass the DoS

#### 2. results

* during block 1 to block 5, the transaction made by `a 1 1` is not shown, it is packed in block 6
* the transaction made by `a 1 3` is packed into block 2
