
using FactorioModBuilder.Build.Directives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build
{
    public class CompileUnit
    {
        public enum UnitType
        {
            Value,
            List,
            Struct
        }

        public enum ValueType
        {
            Number,
            Text,
            None
        }

        public UnitType UType { get; private set; }

        public ValueType VType { get; private set; }
        public string SValue { get; private set; }
        public int IValue { get; private set; }

        public List<CompileUnit> ListValues { get; private set; }
        public List<CompilerDirective> Directives { get; private set; }
        public Dictionary<string, CompileUnit> StructValues { get; private set; }
        public string KeyValueSeparator { get; private set; }
        public bool SingleLine { get; private set; }

        private CompileUnit() { }

        public CompileUnit(string val)
            : this(0, val, null, null, null, UnitType.Value, ValueType.Text, true)
        {
        }

        public CompileUnit(int val)
            : this(val, null, null, null, null, UnitType.Value, ValueType.Number, true)
        {
        }

        public CompileUnit(IEnumerable<CompileUnit> units)
            : this(0, null, units, null, null, UnitType.List, ValueType.None,
            (units != null && units.Count() > 0) ? 
            units.Select(o => o.SingleLine).Aggregate((a, b) => a && b) : true)
        {
        }

        public CompileUnit(IDictionary<string, CompileUnit> members)
            : this(members, " = ")
        {
        }

        public CompileUnit(IDictionary<string, CompileUnit> members, string keyValueSeparator)
            : this(0, null, null, members, keyValueSeparator, UnitType.Struct, ValueType.None, false)
        {
        }

        protected CompileUnit(int iVal, string sVal, IEnumerable<CompileUnit> listVal, 
            IDictionary<string, CompileUnit> members, string keyValueSeparator, 
            UnitType uType, ValueType vType, bool singleLine)
        {
            this.UType = uType;
            this.VType = vType;
            this.IValue = iVal;
            this.SValue = sVal;
            if(listVal != null)
                this.ListValues = new List<CompileUnit>(listVal);
            if(members != null)
                this.StructValues = new Dictionary<string, CompileUnit>(members);
            this.KeyValueSeparator = keyValueSeparator;
            this.Directives = new List<CompilerDirective>();
        }

        public override string ToString()
        {
            switch (this.UType)
            {
                case UnitType.Value:
                    switch (this.VType)
                    {
                        case ValueType.Number: return this.IValue.ToString();
                        case ValueType.Text: return "\"" + this.SValue + "\"";
                    }
                    break;
                case UnitType.List: return "Count = " + this.ListValues.Count;
                case UnitType.Struct: return "Count = " + this.StructValues.Count;
            }

            throw new Exception("Internal consistency error");
        }
    }
}
