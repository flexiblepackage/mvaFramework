using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVAFW.Common.Entity
{
    public class eTestItemSetting
    {
        public string Name { get; internal set; }
        public uint SequenceIndex { get; internal set; }
        public int TestItemIndex { get; internal set; }
        public bool Enable { get; internal set; }
        public bool Retest { get; internal set; }
        public uint Cycle { get; internal set; }
        public bool SWF { get; internal set; }
        public uint RetestNUpperLimit { get; internal set; }
        public string Until { get; set; }
        public double CycleDelayMiniutes { get; set; }
        public string AliasName { get; set; }
    }

    public class eTestItemClass
    {
        public string Name { get; internal set; }
        public string FullClassName { get; internal set; }
        public string Category { get; internal set; }
        public string Product { get; internal set; }

        public eTestItemClass(string category, string product, string name, string fullClassName)
        {
            this.Name = name;
            this.FullClassName = fullClassName;
            this.Category = category;
            this.Product = product;
        }

        public eTestItemClass()
        {
        }
    }
}
