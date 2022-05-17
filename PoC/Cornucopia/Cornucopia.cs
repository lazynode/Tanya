public class cornucopia : SmartContract
{
    public static void OnNEP17Payment(UInt160 from, BigInteger amount, object data)
    {
        if (Runtime.CallingScriptHash == NEO.Hash && amount == 0) {
            var a = NEO.BalanceOf(Runtime.ExecutingScriptHash);
            Storage.Put(Storage.CurrentContext, "from", from);
            Storage.Put(Storage.CurrentContext, "amount", a);
            NEO.Transfer(Runtime.ExecutingScriptHash, from, a, null);
            return;
        }
        if (Runtime.CallingScriptHash == GAS.Hash && Storage.Get(Storage.CurrentContext, "from") is not null)
        {
            var f = (UInt160)Storage.Get(Storage.CurrentContext, "from");
            var a = (BigInteger)Storage.Get(Storage.CurrentContext, "amount");
            Storage.Delete(Storage.CurrentContext, "from");
            NEO.Transfer(Runtime.ExecutingScriptHash, f, a, null);
        }
    }
}