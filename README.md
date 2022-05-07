# Tanya
SHAKE IT BABY

> DISCLAIMER: Do not use any of the material presented here to cause harm. I will find out where you live, I will surprise you in your sleep and I will tickle you so hard that you will promise to behave until the end of your days.

type | location | reason | poc | issue | fix 
---- | ---- | ------ | --- | ----- | --- 
OOM | vm GC | faulty implementation of BFS | [poc](https://github.com/lazynode/Tanya/pull/1/files) | [issue](https://github.com/neo-project/neo-vm/issues/418) | [fix](https://github.com/neo-project/neo-vm/pull/416/files) 
OOM | native contract stdlib's jsonSerialize | faulty implementation of checking size | [poc](https://github.com/lazynode/Tanya/pull/2/files) | [issue](https://github.com/neo-project/neo/issues/2527) | [fix](https://github.com/neo-project/neo/pull/2529/files) 
DoS | vm GC | GC combined other slow operation O(n<sup>2</sup>) | [poc](https://github.com/lazynode/Tanya/pull/5) | [issue](https://github.com/neo-project/neo/issues/2532) | TOO HARD 
Dos | vm GC | PickItem opcode in an end-to-end array's GC is O(n<sup>2</sup>) | [poc](https://github.com/lazynode/Tanya/pull/4/files) | [issue](https://github.com/neo-project/neo/issues/2528) | TOO HARD 
OOM | native contract stdlib's deserialize | forget to limit object size | [poc](https://github.com/lazynode/Tanya/pull/6) | [issue](https://github.com/neo-project/neo/issues/2530) | [fix](https://github.com/neo-project/neo/pull/2531/files) 
OOM | native contract stdlib's jsonDeserialize | forget to limit object size | [poc](https://github.com/lazynode/Tanya/pull/7/files) | [issue](https://github.com/neo-project/neo/issues/2533) | [fix](https://github.com/neo-project/neo/pull/2553) 
OOM | vm clone opcode upon nested struct | huge object copy without limits | [poc](https://github.com/lazynode/Tanya/pull/8/files) | [issue](https://github.com/neo-project/neo-vm/issues/426) | [fix](https://github.com/neo-project/neo-vm/pull/423/files) 
OOM | vm equal opcode upon nested struct | huge object test without limits | [poc](https://github.com/neo-project/neo-vm/issues/426#issuecomment-878120245) | [issue](https://github.com/neo-project/neo-vm/issues/426) | [fix](https://github.com/neo-project/neo-vm/pull/428/files) 
Inappropriate GAS Price | vm sqrt opcode | inappropriate slow implementation | [poc](https://github.com/lazynode/Tanya/pull/9) | [issue](https://github.com/neo-project/neo-vm/issues/421) | [fix](https://github.com/neo-project/neo-vm/pull/427/files) 
OOM | vm pow opcode | forget to limit exponent value | [poc](https://github.com/lazynode/Tanya/pull/10) | [issue](https://github.com/neo-project/neo-vm/issues/425) | [fix](https://github.com/neo-project/neo-vm/pull/422/files) 
Potential Risk | NEP-17 standard | transfer hook makes attack easier | [poc](https://github.com/lazynode/Tanya/pull/12/files) | [issue](https://github.com/neo-project/neo-node/issues/822) | - 
Information Leak | neo OracleService module | forget to check http redirect | [poc](https://github.com/lazynode/Tanya/pull/14/files) | [issue](https://github.com/neo-project/neo-modules/issues/693) | [fix](https://github.com/neo-project/neo-modules/pull/692/files) 
OOM | neo json filter | do not realize filter result's explosion | [poc](https://github.com/lazynode/Tanya/pull/14/files)  | [issue](https://github.com/neo-project/neo/issues/2663) | [fix](https://github.com/neo-project/neo/pull/2665) 
DoS | neo ApplicationLogs module | forget to limit result stack | [poc](https://github.com/lazynode/Tanya/pull/15/files) | [issue](https://github.com/neo-project/neo/issues/2666) | [fix](https://github.com/neo-project/neo-modules/pull/696) [fix](https://github.com/neo-project/neo/pull/2671/files) 
Overflow | native contract ContractManagement update | update counter overflow | [poc](https://github.com/lazynode/Tanya/pull/16/files) | [issue](https://github.com/neo-project/neo/issues/2668) | [fix](https://github.com/neo-project/neo/pull/2697/files) 
DoS | neo TokensTracker module | forget to limit GAS usage | [poc](https://github.com/lazynode/Tanya/pull/17) | [issue](https://github.com/neo-project/neo/issues/2670) | [fix](https://github.com/neo-project/neo-modules/pull/697/files) 
DoS | neovm large struct equal opcode | forget to limit size | [poc](https://github.com/lazynode/Tanya/pull/19/files) | [issue](https://github.com/neo-project/neo/issues/2700) | [fix](https://github.com/neo-project/neo-vm/pull/454/files) 
Manipulating Random | syscall random | random sequence is predictable | [poc](https://github.com/lazynode/Tanya/pull/22) | [issue](https://github.com/lazynode/Tanya/issues/24) | - 
Manipulating Random | syscall random | side channel attack by GAS manipulating | [poc](https://github.com/neo-project/neo/issues/2693#issuecomment-1096021296) | [issue](https://github.com/neo-project/neo/issues/2693) | - 
DoS | syscall CreateMultisigAccount | under-priced | [poc](https://github.com/neo-project/neo/issues/2710) | [issue](https://github.com/neo-project/neo/issues/2710) | [fix](https://github.com/neo-project/neo/pull/2712/files) 
DoS | syscall CheckWitness | under-priced cache-miss | [poc](https://github.com/lazynode/Tanya/pull/27/files) | [issue](https://github.com/neo-project/neo/issues/2720) | TODO 
DoS | opcodes in O(n) | under-priced | [poc](https://github.com/lazynode/Tanya/pull/28) | [issue](https://github.com/neo-project/neo/issues/2723) | TODO 
