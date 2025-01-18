using System.Text;

namespace DesignPattern.Creational
{
    public class Field
    {
        public string FieldName { get; set; } = string.Empty;
        public string FieldType { get; set; } = string.Empty;
    }

    public class CodeBlock
    {
        public string ObjectName = string.Empty;
        public List<Field> fields = new List<Field>();
    }
    public class CodeBuilder
    {
        private CodeBlock codeBlock = new CodeBlock();
        private StringBuilder code = new StringBuilder();

        public CodeBuilder(string objectName)
        {
            codeBlock.ObjectName = objectName;
            code.AppendLine($"public class {codeBlock.ObjectName}");
            code.AppendLine("{");
            code.AppendLine("}");
        }

        public CodeBuilder AddField(string name, string type)
        {
            var field = new Field
            {
                FieldName = name,
                FieldType = type
            };
            codeBlock.fields.Add(field);
            code.Replace("}", $"  public {field.FieldType} {field.FieldName};");
            code.AppendLine("}");
            return this;
        }

        public override string ToString()
        {
            return code.ToString();
        }
    }

    public class Builder
    {
        /*public static void Main(string[] args)
        {
            var cb = new CodeBuilder("Person").AddField("Name", "string").AddField("Age", "int");
            Console.WriteLine(cb);
            Console.WriteLine(cb);
        }*/
    }
}
