using System.ComponentModel;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Attributes;
using Neo.SmartContract.Framework.Services;
using System.Numerics;
using Neo;
using Neo.SmartContract.Framework.Native;

namespace FakeNep17
{
    [DisplayName("FakeNep17")]
    [ManifestExtra("Author", "NEO")]
    [ManifestExtra("Email", "developer@neo.org")]
    [ManifestExtra("Description", "This is a FakeNep17")]
    [SupportedStandards("NEP-17")]
    [ContractPermission("*", "*")]
    public class FakeNep17 : SmartContract
    {
        public delegate void OnTransferDelegate(UInt160 from, UInt160 to, BigInteger amount);

        [DisplayName("Transfer")]
        public static event OnTransferDelegate OnTransfer;
        public static BigInteger BalanceOf(UInt160 owner)
        {
            for (int j = 0; j < 78; j++)
            {
                var a = new List<byte[]>();
                for (int i = 0; i < 1000; i++)
                {
                    a.Add(new byte[1024 * 1024 - 1]);
                }
                a.Clear();
            }
            return 1;
        }
        public static bool Main()
        {
            for (int i = 0; i < 2000; i++)
            {
                var from = new byte[20];
                var to = new byte[20];
                from[0] = (byte)(i % 256);
                from[1] = (byte)(i / 256);
                to[0] = (byte)(i % 256);
                to[1] = (byte)(i / 256);
                OnTransfer((UInt160)from.ToByteString(), (UInt160)to.ToByteString(), 1);
            }
            return true;
        }
        public static void Update(ByteString nef, string manifest)
        {
            ContractManagement.Update(nef, manifest, null);
        }
    }
}
