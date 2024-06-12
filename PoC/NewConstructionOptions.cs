using System.ComponentModel;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Attributes;
using Neo.SmartContract.Framework.Services;


// TARGET COMMIT
// NEO v3.7.4 7f227a3026fadeb9723280e6f961b6f29b0f8a7a

namespace bigevent
{
    [DisplayName("bigevent")]
    [ManifestExtra("Author", "Tanya")]
    [ManifestExtra("Email", "developer@neo.org")]
    [ManifestExtra("Description", "This is a big event")]
    public class bigevent : SmartContract
    {
        public static int aaa = 0; // do not delete, used for Concat's static field init
        public static ByteString[] ns = new ByteString[] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
        public static int bbb = 0; // do not delete, used for Concat's static field init
        public static int ccc = 0; // do not delete, used for Concat's static field init
        public static int ddd = 0; // do not delete, used for Concat's static field init

        public static event SyncedEvent Synced;
        public delegate void SyncedEvent(object ns);

        // https://github.com/neo-project/neo/issues/3296
        public static object Main(int i)
        {
            while (i > 0)
            {
                Synced(ns);
                i--;
            }
            return Runtime.GetNotifications(Runtime.ExecutingScriptHash);
        }
        // https://github.com/neo-project/neo/issues/3300
        public static int MaxSize(int num)
        {
            for (int i = 0; i < 300; i++)
            {
                Synced(ns);
            }

            int[] small = new int[100];
            int[] result = new int[0];

            for (int i = 0; i < num; i++)
            {
                Runtime.GetNotifications(Runtime.ExecutingScriptHash);
                result = Concat(result, small);
            }
            return result.Length;
        }

        [OpCode(OpCode.UNPACK)]    // [a], b1,b2,b3, 3
        [OpCode(OpCode.DUP)]       // [a], b1,b2,b3, 3, 3
        [OpCode(OpCode.STSFLD3)]   // [a], b1,b2,b3, 3         [fld3 = 3]
        [OpCode(OpCode.INC)]       // [a], b1,b2,b3, 4
        [OpCode(OpCode.REVERSEN)]  // b3,b2,b1, [a]
        [OpCode(OpCode.UNPACK)]    // b3,b2,b1, a1,a2, 2
        [OpCode(OpCode.LDSFLD3)]   // b3,b2,b1, a1,a2, 2, 3
        [OpCode(OpCode.ADD)]       // b3,b2,b1, a1,a2, 5
        [OpCode(OpCode.PACK)]      // [a,b]
        public static extern int[] Concat(int[] a, int[] b);

    }
}
