using System.Numerics;
using Akka.Actor;
using Neo;
using Neo.Ledger;
using Neo.Network.P2P.Payloads;
using Neo.SmartContract.Native;
using Neo.VM;
using Neo.Wallets;

namespace FeeDoS
{
    public static class Tx
    {
        public static BigInteger MakeTx(byte[] script, Wallet w, NeoSystem system, long sysfee, long netfee)
        {
            var tx = new Transaction
            {
                Version = 0,
                Nonce = (uint)new Random().NextInt64(),
                Script = script,
                Signers = new Signer[]{
                    new Signer {
                        Scopes = WitnessScope.Global,
                        Account = w.GetAccounts().First().ScriptHash,
                    },
                },
                ValidUntilBlock = NativeContract.Ledger.CurrentIndex(system.StoreView) + 5,
                Attributes = System.Array.Empty<TransactionAttribute>(),
                SystemFee = sysfee,
                NetworkFee = netfee,
            };

            var key = w.GetAccounts().First().GetKey();
            var contract = Neo.SmartContract.Contract.CreateSignatureContract(key.PublicKey);
            byte[] signature = tx.Sign(key, system.Settings.Network);
            tx.Witnesses = new Witness[]{
                new Witness {
                    VerificationScript = contract.Script,
                    InvocationScript = new ScriptBuilder().EmitPush(signature).ToArray(),
                }
            };
            var relayResult = tx.Verify(system.Settings, system.StoreView, new TransactionVerificationContext());
            if (relayResult != VerifyResult.Succeed)
            {
                $"relayresult1 failed: {relayResult.ToString()}".Log();
            }
            system.Blockchain.Tell(tx);
            $"Success, {tx.Hash}".Log();
            return tx.SystemFee + tx.NetworkFee;
        }
        public static void Log<T>(this T val) => Console.Error.WriteLine(val);
    }
}