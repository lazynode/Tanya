using System.ComponentModel;
using System.Numerics;
using Neo;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Attributes;
using Neo.SmartContract.Framework.Native;
using Neo.SmartContract.Framework.Services;

namespace LotteryWithDefender
{
    [DisplayName("LotteryWithDefender")]
    [ManifestExtra("Author", "NEO")]
    [ManifestExtra("Email", "developer@neo.org")]
    [ManifestExtra("Description", "This is a LotteryWithDefender. Call play to earn NEO.")]
    public class LotteryWithDefender : SmartContract
    {
        [DisplayName("Lottery")]
        public static event LotteryEvent onLottery;
        public delegate void LotteryEvent(ByteString name);

        public static void OnNEP17Payment(UInt160 from, BigInteger amount, object data)
        {
        }

        public static void Play(ByteString name, UInt160 receiver)
        {
            ExecutionEngine.Assert(Runtime.EntryScriptHash == Runtime.CallingScriptHash, "FAUD: Contract call is not allowed.");
            ExecutionEngine.Assert((((Transaction)Runtime.ScriptContainer).Script.Length <= name.Length + 60), "FAUD:Transaction script length error.");
            ExecutionEngine.Assert(name.Length <= 6, "FAUD:Long name is now allowed.");
            // the calling script should be
            // PUSHDATA1 ${name}  (size = 2 + name.Length)
            // PUSHDATA1 receiver  (size = 22)
            // PUSH1 (size = 1)
            // PACK (size = 1)
            // PUSH15 (size = 1)
            // PUSHDATA1 play (size = 6)
            // PUSHDATA1 0x726cb6e0cd8628a1350a611384688911ab75f51b (size = 22)
            // SYSCALL System.Contract.Call (size = 5)
            // example normal script: DAZhdHRhY2sMFAS4eMLY2Qfs8dcYmQ+QOGfFkqETEsAfDARwbGF5DBQb9XWrEYlohBNhCjWhKIbN4LZsckFifVtS

            if (Runtime.GetRandom() % 2 == 1) {
                ExecutionEngine.Assert(NEO.Transfer(Runtime.ExecutingScriptHash, receiver, 1, null));
                onLottery(name);
            }
        }

        public static void Update(ByteString nefFile, string manifest)
        {
            ContractManagement.Update(nefFile, manifest, null);
        }
    }
}
