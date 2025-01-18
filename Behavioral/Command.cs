namespace DesignPattern.Behavioral
{
    // Command interface
    public interface ICommand
    {
        void Execute();
        void Undo();
    }

    // Receiver: Order
    public class Order
    {
        private List<string> _dishes = new List<string>();

        public void Add(string dish)
        {
            _dishes.Add(dish);
        }

        public void Remove(string dish)
        {
            _dishes.Remove(dish);
        }

        public override string ToString()
        {
            return $"Current Order: {string.Join(", ", _dishes)}";
        }
    }

    // Concrete command: AddDishCommand
    public class AddDishCommand : ICommand
    {
        private readonly Order _order;
        private readonly string _dish;

        public AddDishCommand(Order order, string dish)
        {
            _order = order;
            _dish = dish;
        }

        public void Execute()
        {
            _order.Add(_dish);
        }

        public void Undo()
        {
            _order.Remove(_dish);
        }
    }

    // Concrete command: RemoveDishCommand
    public class RemoveDishCommand : ICommand
    {
        private readonly Order _order;
        private readonly string _dish;

        public RemoveDishCommand(Order order, string dish)
        {
            _order = order;
            _dish = dish;
        }

        public void Execute()
        {
            _order.Remove(_dish);
        }

        public void Undo()
        {
            _order.Add(_dish);
        }
    }

    // Invoker: OrderManager
    public class OrderManager
    {
        private readonly Stack<ICommand> _undoStack = new Stack<ICommand>();
        private readonly Stack<ICommand> _redoStack = new Stack<ICommand>();

        public void Invoke(ICommand command)
        {
            command.Execute();
            _undoStack.Push(command);
            _redoStack.Clear(); // Clear redo stack on new command
        }

        public void Undo()
        {
            if (_undoStack.Count > 0)
            {
                var command = _undoStack.Pop();
                command.Undo();
                _redoStack.Push(command);
            }
            else
            {
                Console.WriteLine("Nothing to undo.");
            }
        }

        public void Redo()
        {
            if (_redoStack.Count > 0)
            {
                var command = _redoStack.Pop();
                command.Execute();
                _undoStack.Push(command);
            }
            else
            {
                Console.WriteLine("Nothing to redo.");
            }
        }
    }
    public class Command
    {
        /*static void Main(string[] args)
        {
            var order = new Order();
            var orderManager = new OrderManager();
                                       
            var addDishCommand1 = new AddDishCommand(order, "Pizza");
            var addDishCommand2 = new AddDishCommand(order, "Burger");
            var removeDishCommand = new RemoveDishCommand(order, "Pizza");
                                                                        
            orderManager.Invoke(addDishCommand1);
            Console.WriteLine(order);
            orderManager.Invoke(addDishCommand2);
            Console.WriteLine(order);
            orderManager.Invoke(removeDishCommand);
            Console.WriteLine(order);
            orderManager.Undo();
            Console.WriteLine(order);
            orderManager.Undo();
            Console.WriteLine(order);
            orderManager.Redo();
            Console.WriteLine(order);
            orderManager.Redo();
            Console.WriteLine(order);
        }*/
    }
}
