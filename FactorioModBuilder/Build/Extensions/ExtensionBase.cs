using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Extensions
{
    public abstract class ExtensionBase : ICompilerExtension
    {
        public abstract string SupportedUnitName { get; }

        public Compiler Parent { get; set; }

        public string Indent { get; set; }

        public ExtensionBase()
        {
            this.Indent = "    ";
        }

        public abstract bool BuildUnit(CompileUnit unit, DirectoryInfo outDir);

        protected string BuildTable(CompileUnit data)
        {
            StringBuilder sb = new StringBuilder();
            this.BuildTable(data, sb);
            return sb.ToString();
        }

        private void BuildTable(CompileUnit data, StringBuilder sb)
        {
            Action<string> ap;
            if(data.SingleLine)
                ap = (x => sb.Append(x));
            else
                ap = (x => sb.AppendLine(x));

            switch (data.UType)
	        {
                case CompileUnit.UnitType.List:
                    ap("{");
                    for (int i = 0; i < data.ListValues.Count; ++i)
                    {
                        string elemSep = "";
                        if (i < data.ListValues.Count - 1)
                            elemSep = ",";
                        var elem = data.ListValues[i];
                        switch (elem.UType)
                        {
                            case CompileUnit.UnitType.Value:
                                this.WriteValue(elem, ap, elemSep);
                                break;
                            case CompileUnit.UnitType.List:
                            case CompileUnit.UnitType.Struct:
                                this.BuildTable(elem);
                                break;
                            default:
                                throw new InvalidOperationException("Unknown compile unit type");
                        }
                    }
                    ap("}");
                    break;
                case CompileUnit.UnitType.Struct:
                    ap("{");
                    var lst = data.StructValues.ToList();
                    for (int i = 0; i < lst.Count; ++i)
                    {
                        var kvp = lst[i];
                        string elemSep = "";
                        if (i < lst.Count - 1)
                            elemSep = ",";
                        switch (kvp.Value.UType)
                        {
                            case CompileUnit.UnitType.Value:
                                this.WriteDictPair(kvp.Value, ap, kvp.Key, data.KeyValueSeparator, elemSep);
                                break;
                            case CompileUnit.UnitType.List:
                            case CompileUnit.UnitType.Struct:
                                ap(this.Indent + kvp.Key + data.KeyValueSeparator);
                                this.BuildTable(kvp.Value, sb);
                                ap(elemSep);
                                break;
                            default:
                                throw new InvalidOperationException("Unknown compile unit type");
                        }
                    }
                    ap("}");
                    break;
		        default:
                    throw new InvalidOperationException("Unknown compile unit type");
	        }
        }

        protected void WriteDictPair(CompileUnit value, Action<string> ap, string key, string kvSep, string elemSep)
        {
            switch (value.VType)
            {
                case CompileUnit.ValueType.Number:
                    ap(this.Indent + key + kvSep + value.IValue + elemSep);
                    break;
                case CompileUnit.ValueType.Text:
                    ap(this.Indent + key + kvSep + this.WrapText(value.SValue) + elemSep);
                    break;
                default:
                    throw new InvalidOperationException("Invalid compile unit value type");
            }
        }

        protected void WriteValue(CompileUnit value, Action<string> ap, string elemSep)
        {
            switch (value.VType)
            {
                case CompileUnit.ValueType.Number:
                    ap(this.Indent + value.IValue + elemSep);
                    break;
                case CompileUnit.ValueType.Text:
                    ap(this.Indent + this.WrapText(value.SValue) + elemSep);
                    break;
                default:
                    throw new InvalidOperationException("Invalid compile unit value type");
            }
        }

        protected string WrapText(string value)
        {
            return "\"" + value + "\"";
        }
    }
}
