﻿using System;
using System.ComponentModel;
using System.Numerics;
using Neo;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Attributes;
using Neo.SmartContract.Framework.Native;
using Neo.SmartContract.Framework.Services;

namespace Lottery
{
    [DisplayName("Lottery")]
    [ManifestExtra("Author", "NEO")]
    [ManifestExtra("Email", "developer@neo.org")]
    [ManifestExtra("Description", "This is a Lottery. Send 1 NEO and you will probably get 2 NEO.")]
    public class Lottery : SmartContract
    {
        public static void OnNEP17Payment(UInt160 from, BigInteger amount, object data)
        {
            if (Runtime.CallingScriptHash == NEO.Hash && amount == 1)
            {
                if (Runtime.GetRandom() % 2 == 1) {
                    ExecutionEngine.Assert(NEO.Transfer(Runtime.ExecutingScriptHash, from, 2, data));
                }
            }
        }

        // public static void Attack() {
        //     var rand = Runtime.GetRandom();
        //     for (int i = 0; i < 10; i++) {
        //         var nextrand = (BigInteger)Contract.Call(CryptoLib.Hash, "murmur128", CallFlags.All, new object[]{rand, Runtime.GetNetwork()});
        //         if (nextrand % 2 == 1) {
        //             NEO.Transfer((UInt160)"0xTODO".ToByteArray(), (UInt160)"0xTODO".ToByteArray(), 1, null);
        //         }

        //     }
        // }

        // public static void Update(ByteString nefFile, string manifest)
        // {
        //     ContractManagement.Update(nefFile, manifest, null);
        // }
    }
}
