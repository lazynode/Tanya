using System;
using System.ComponentModel;
using System.Numerics;
using Neo;
using Neo.SmartContract;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Attributes;
using Neo.SmartContract.Framework.Native;
using Neo.SmartContract.Framework.Services;

namespace LotteryWithDefenderAttacker
{
    [DisplayName("LotteryWithDefenderAttacker")]
    [ManifestExtra("Author", "NEO")]
    [ManifestExtra("Email", "developer@neo.org")]
    [ManifestExtra("Description", "This is a LotteryWithDefenderAttacker")]
    public class LotteryWithDefenderAttacker : SmartContract
    {
        [InitialValue("NLLvsqs7AyBNmQT6NThUxYWDFwV5b1evaK", ContractParameterType.Hash160)]
        private static readonly UInt160 Owner = default;

        public static object[] a() {
            BigInteger rand = Runtime.GetRandom();
            for (int i = 0; i < 10; i++) {
                byte[] nextrand = murmur128(rand.ToByteArray().Concat(new byte[]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}).Take(16), Runtime.GetNetwork());
                if ((new BigInteger(nextrand)) % 2 == 1) {
                    return new object[] {"attack", Owner};
                }
                rand = Runtime.GetRandom();
            }
            ExecutionEngine.Abort();
            return new object[] {"attack", Owner};
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
