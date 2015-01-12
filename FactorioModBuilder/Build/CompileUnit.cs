using FactorioModBuilder.Build.Directives;
using FactorioModBuilder.Build.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build
{
    public class CompileUnit : IEnumerable<CompileUnit>, IEnumerable<KeyValuePair<string, CompileUnit>>
    {
        public List<CompilerDirective> Directives { get; private set; }

        public object Data { get; private set; }
        public DataType Type { get; private set; }
        public ExtensionType ExtensionType { get; private set; }

        public enum DataType
        {
            None,
            Text,
            Number,
            Bool,
            List,
            Dictionary
        }

        public CompileUnit()
            : this(null, DataType.None, ExtensionType.None)
        {
        }

        public CompileUnit(ExtensionType type)
            : this(null, DataType.None, type)
        {
        }

        public CompileUnit(bool value)
            : this(value, DataType.Bool, ExtensionType.None)
        {
        }

        public CompileUnit(string value)
            : this(value, DataType.Text, ExtensionType.None)
        {
        }

        public CompileUnit(int value)
            : this(value, DataType.Number, ExtensionType.None)
        {
        }

        public CompileUnit(IEnumerable<CompileUnit> values, ExtensionType type)
            : this(values, DataType.List, type)
        {
        }

        public CompileUnit(IDictionary<string, CompileUnit> values, ExtensionType type)
            : this(values, DataType.Dictionary, type)
        {
        }

        private CompileUnit(object data, DataType type, ExtensionType extType)
        {
            this.Type = type;
            this.Data = data;
            this.ExtensionType = extType;
            this.Directives = new List<CompilerDirective>();
        }

        public void Add(CompileUnit unit)
        {
            if (!(this.Type == DataType.None || this.Type == DataType.List))
                throw new InvalidOperationException("Inconsistent invocation of the add method");
            if (this.Type == DataType.None)
            {
                this.Data = new List<CompileUnit>();
                this.Type = DataType.List;
            }
            ((List<CompileUnit>)this.Data).Add(unit);
        }

        public void Add(string key, CompileUnit value)
        {
            if (!(this.Type == DataType.None || this.Type == DataType.Dictionary))
                throw new InvalidOperationException("Inconsisten invocation of the add method");
            if (this.Type == DataType.None)
            {
                this.Data = new Dictionary<string, CompileUnit>();
                this.Type = DataType.Dictionary;
            }
            ((Dictionary<string, CompileUnit>)this.Data).Add(key, value);
        }

        public override string ToString()
        {
            if (this.Data == null)
                return "";
            switch (this.Type)
            {
                case DataType.Text:
                case DataType.Number: 
                    return this.Data.ToString();
                case DataType.List:
                case DataType.Dictionary:
                    return "Count = " + ((IEnumerable<CompileUnit>)this.Data).Count();
                default:
                    throw new InvalidOperationException("Unknown DataType enumeration");
            }
        }

        public IEnumerator<CompileUnit> GetEnumerator()
        {
            if (this.Type == DataType.None)
                return new List<CompileUnit>().GetEnumerator();
            if (this.Type != DataType.List)
                throw new InvalidOperationException("Cannot iterate over a non-list collection as a list");
            return ((IEnumerable<CompileUnit>)this.Data).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return (System.Collections.IEnumerator)this.GetEnumerator();
        }

        IEnumerator<KeyValuePair<string, CompileUnit>> IEnumerable<KeyValuePair<string, CompileUnit>>.GetEnumerator()
        {
            if (this.Type == DataType.None)
                return new Dictionary<string, CompileUnit>().GetEnumerator();
            if (this.Type != DataType.Dictionary)
                throw new InvalidOperationException("Cannot iterate over a non-dictionary collection as a dictionary");
            return ((IDictionary<string, CompileUnit>)this.Data).GetEnumerator();
        }
    }
}
