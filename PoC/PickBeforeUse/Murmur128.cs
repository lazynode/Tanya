// Copyright (C) 2015-2021 The Neo Project.
// 
// The neo is free software distributed under the MIT software license, 
// see the accompanying file LICENSE in the main directory of the
// project or http://www.opensource.org/licenses/mit-license.php 
// for more details.update 0x274ba0bf029412c33a1a6e399c1c7c1af241cc35 /Users/dusmart/go/src/github.com/lazynode/Tanya/PoC/PickBeforeUse/bin/sc/Lottery.nef /Users/dusmart/go/src/github.com/lazynode/Tanya/PoC/PickBeforeUse/bin/sc/Lottery.manifest.json NLLvsqs7AyBNmQT6NThUxYWDFwV5b1evaK
// 
// Redistribution and use in source and binary forms with or without
// modifications are permitted.

using System;
using System.Buffers.Binary;
using System.Numerics;
using System.Runtime.CompilerServices;
using Neo.SmartContract.Framework;
using System.Collections.Generic;

using System.IO;
using System.Diagnostics.Contracts;

namespace Lottery
{
    /// <summary>
    /// Computes the 128 bits murmur hash for the input data.
    /// </summary>
    public sealed class Murmur128
    {
        private const ulong c1 = 0x87c37b91114253d5;
        private const ulong c2 = 0x4cf5ad432745937f;
        private const int r1 = 31;
        private const int r2 = 33;
        private const uint m = 5;
        private const uint n1 = 0x52dce729;
        private const uint n2 = 0x38495ab5;

        public static byte[] HashCore(byte[] array, uint seed)
        {
            ulong H1 = seed;
            ulong H2 = seed;
            // for (int i = 0; i < array.Length; i += 16)
            {
                ulong k1 = (ulong)new BigInteger(array.Take(8).Concat(new byte[]{0x00}));

                k1 *= c1;
                k1 &= 0xffffffffffffffff;
                k1 = RotateLeft(k1, r1);
                k1 *= c2;
                k1 &= 0xffffffffffffffff;
                H1 ^= k1;
                H1 = RotateLeft(H1, 27);
                H1 += H2;
                H1 &= 0xffffffffffffffff;
                H1 = H1 * m + n1;
                H1 &= 0xffffffffffffffff;

                ulong k2 = (ulong)new BigInteger(array.Last(8).Concat(new byte[] {0x00}));
                k2 *= c2;
                k2 &= 0xffffffffffffffff;
                k2 = RotateLeft(k2, r2);
                k2 *= c1;
                k2 &= 0xffffffffffffffff;
                H2 ^= k2;
                H2 = RotateLeft(H2, 31);
                H2 += H1;
                H2 &= 0xffffffffffffffff;
                H2 = H2 * m + n2;
                H2 &= 0xffffffffffffffff;
            }

            ulong len = (ulong)array.Length;
            H1 ^= len; 
            H2 ^= len;

            H1 += H2;
            H1 &= 0xffffffffffffffff;
            H2 += H1;
            H2 &= 0xffffffffffffffff;

            H1 = FMix(H1);
            H2 = FMix(H2);

            H1 += H2;
            H1 &= 0xffffffffffffffff;
            H2 += H1;
            H2 &= 0xffffffffffffffff;

            return ((BigInteger)H1).ToByteArray().Concat(((BigInteger)H2).ToByteArray());
        }

        private static ulong FMix(ulong h)
        {
            h = (h ^ (h >> 33)) * 0xff51afd7ed558ccd;
            h &= 0xffffffffffffffff;
            h = (h ^ (h >> 33)) * 0xc4ceb9fe1a85ec53;
            h &= 0xffffffffffffffff;

            return (h ^ (h >> 33));
        }

        public static ulong RotateLeft(ulong value, int offset)
            => ((value << offset) & 0xffffffffffffffff) | (value >> (64 - offset));
    }
}





