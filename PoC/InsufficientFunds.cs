using System;
using System.ComponentModel;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Native;
using Neo;
using System.Numerics;
using Neo.Cryptography.ECC;
using Neo.SmartContract.Framework.Services;
using Neo.SmartContract;


namespace lazycoin
{
    [DisplayName("GASEater")]
    [ManifestExtra("Author", "lazynode")]
    [ManifestExtra("Email", "lazynode@protonmail.com")]
    [ManifestExtra("Description", "GASEater eats GAS")]
    [ContractPermission("*", "onNEP17Payment")]
    public partial class GASEater : SmartContract
    {
        [InitialValue("NhMvRrhBnZyAwZnw8y9mHoCzwSEDmZJo2n", ContractParameterType.Hash160)]
        private static readonly UInt160 owner = default;

        public static void _deploy(object data, bool update)
        {
            Storage.Put(Storage.CurrentContext, new byte[] { 0x01 }, 1900000000);
        }

        public static void OnNEP17Payment(UInt160 from, BigInteger amount, object data)
        {
            NEO.Transfer(Runtime.ExecutingScriptHash, owner, NEO.BalanceOf(Runtime.ExecutingScriptHash));
            object n = Storage.Get(Storage.CurrentContext, new byte[] { 0x01 });
            Runtime.BurnGas((long)n);
        }

        public static void Set(BigInteger n)
        {
            Storage.Put(Storage.CurrentContext, new byte[] { 0x01 }, n);
        }

        public static BigInteger Get()
        {
            return (BigInteger)Storage.Get(Storage.CurrentContext, new byte[] { 0x01 });
        }
    }
}
