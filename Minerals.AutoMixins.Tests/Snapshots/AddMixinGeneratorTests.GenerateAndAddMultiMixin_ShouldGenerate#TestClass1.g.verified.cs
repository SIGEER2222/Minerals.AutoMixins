﻿using System;
using Minerals.AutoMixins;
namespace Minerals.Test2
{
    [global::System.Diagnostics.DebuggerNonUserCode]
    [global::System.Runtime.CompilerServices.CompilerGenerated]
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public partial class TestClass1
    {
        // TestMixin1
        [Obsolete] public int Property1 { get; set; } = 1;
        public int Field1 = 1;
        private int _filed2 = 2;
        public int Method1(int arg0, int arg1)
        {
            return arg0 + arg1;
        }
        protected int Method2(int arg0, int arg1)
        {
            return arg0 - arg1;
        }
        // TestMixin2
        [Obsolete] public int Property1_1 { get; set; } = 1;
        public int Field1_1 = 1;
        private int _filed2_1 = 2;
        public int Method1_1(int arg0, int arg1)
        {
            return arg0 + arg1;
        }
        protected int Method2_1(int arg0, int arg1)
        {
            return arg0 - arg1;
        }
    }
}