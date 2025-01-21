using System.Linq.Expressions;

namespace DesignPattern.Behavioral
{
    // Context class: stores variables and values
    public interface IContext<T>
    {
        void SetVariable(string name, T value);
        bool GetVariable(string name);
    }
    public class LogicExpressionContext : IContext<bool>
    {
        private Dictionary<string, bool> _variables = new Dictionary<string, bool>();

        public void SetVariable(string name, bool value)
        {
            _variables[name] = value;
        }

        public bool GetVariable(string name)
        {
            if (!_variables.ContainsKey(name))
            {
                throw new KeyNotFoundException($"Variable '{name}' is not defined.");
            }
            return _variables[name];
        }
    }

    // Abstract Expression: Base interface for all expressions
    public interface IExpression<T>
    {
        T Interpret(IContext<bool> context);
    }

    // Terminal Expression: Constant value (e.g., numbers)
    public class BooleanExpression : IExpression<bool>
    {
        private bool _value;

        public BooleanExpression(bool value)
        {
            _value = value;
        }

        public bool Interpret(IContext<bool> context)
        {
            return _value;
        }
    }

    // Terminal Expression: Variable expression
    public class VariableExpression : IExpression<bool>
    {
        private string _name;

        public VariableExpression(string name)
        {
            _name = name;
        }

        public bool Interpret(IContext<bool> context)
        {
            return context.GetVariable(_name);
        }
    }

    // Non-Terminal Expression: And operation
    public class AndExpression : IExpression<bool>
    {
        private IExpression<bool> _leftExpression;
        private IExpression<bool> _rightExpression;

        public AndExpression(IExpression<bool> left, IExpression<bool> right)
        {
            _leftExpression = left;
            _rightExpression = right;
        }

        public bool Interpret(IContext<bool> context)
        {
            return _leftExpression.Interpret(context) && _rightExpression.Interpret(context);
        }
    }

    // Non-Terminal Expression: Or operation
    public class OrExpression : IExpression<bool>
    {
        private IExpression<bool> _leftExpression;
        private IExpression<bool> _rightExpression;

        public OrExpression(IExpression<bool> left, IExpression<bool> right)
        {
            _leftExpression = left;
            _rightExpression = right;
        }

        public bool Interpret(IContext<bool> context)
        {
            return _leftExpression.Interpret(context) || _rightExpression.Interpret(context);
        }
    }

    // Non-Terminal Expression: Not operation
    public class NotExpression : IExpression<bool>
    {
        private IExpression<bool> _expression;

        public NotExpression(IExpression<bool> expression)
        {
            _expression = expression;
        }

        public bool Interpret(IContext<bool> context)
        {
            return !_expression.Interpret(context);
        }
    }

    public class Interpreter
    {
        static void Main(string[] args)
        {
            // Test above classes
            var context = new LogicExpressionContext();
            context.SetVariable("A", true);
            context.SetVariable("B", false);
            context.SetVariable("C", true);
            context.SetVariable("D", false);
            context.SetVariable("E", true);
            context.SetVariable("F", false);
            bool result = new OrExpression(
                               new AndExpression(
                                   new VariableExpression("A"),
                                   new VariableExpression("B")),
                               new AndExpression(
                                   new VariableExpression("C"),
                                   new VariableExpression("D")))
                            .Interpret(context);
            Console.WriteLine("Evaluating: (A AND B) OR (C AND D)");
            Console.WriteLine($"Result: {result}"); // Output: False

            // Example with NOT operation: NOT (A OR B)
            IExpression<bool> notExpression = new NotExpression(
                new OrExpression(
                    new VariableExpression("E"),
                    new VariableExpression("F"))
            );

            Console.WriteLine("\nEvaluating: NOT (A OR B)");
            bool notResult = notExpression.Interpret(context);
            Console.WriteLine($"Result: {notResult}"); // Output: False
        }
    }
}
