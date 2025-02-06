namespace DesignPattern.Behavioral
{
    // Originator: Character
    public class Character
    {
        private string _name;
        private int _health;
        private int _mana;
        private string _weapon;


        public Character(string name, int health, int mana, string weapon)
        {
            _health = health;
            _mana = mana;
            _name = name;
            _weapon = weapon;
        }

        public void TakeDamage(int damage) => _health -= damage;
        public void ChangeWeapon(string newWeapon) => _weapon = newWeapon;
        public void ShowStatus() => Console.WriteLine($"[HP: {_health}, Mana: {_mana}, Weapon: {_weapon}]");

        // Lưu trạng thái hiện tại vào Memento
        public CharacterMemento Save()
        {
            return new CharacterMemento(_health, _mana, _weapon);
        }

        // Khôi phục trạng thái từ Memento
        public void Restore(CharacterMemento memento)
        {
            _health = memento.Health;
            _mana = memento.Mana;
            _weapon = memento.Weapon;
        }
    }

    // Memento
    public class CharacterMemento
    {
        public string? Name { get; set; }
        public int Health { get; set; }
        public int Mana { get; set; }
        public string Weapon { get; set; }

        public CharacterMemento(string name, int health, int mana, string weapon)
        {
            Health = health;
            Mana = mana;
            Name = name;
            Weapon = weapon;
        }

        public CharacterMemento(int health, int mana, string weapon)
        {
            Health = health;
            Mana = mana;
            Weapon = weapon;
        }
    }

    // Caretaker: GameSaveManager
    public class GameSaveManager
    {
        private Stack<CharacterMemento> _undoStack = new();
        private Stack<CharacterMemento> _redoStack = new();

        public void SaveState(Character character)
        {
            _undoStack.Push(character.Save());
            _redoStack.Clear(); // Clear redo stack on new save
        }

        public void Undo(Character character)
        {
            if (_undoStack.Count > 0)
            {
                _redoStack.Push(character.Save());
                _undoStack.Pop();
                character.Restore(_undoStack.Peek());
            }
            else
            {
                Console.WriteLine("No previous state to undo.");
            }
        }

        public void Redo(Character character)
        {
            if (_redoStack.Count > 0)
            {
                _undoStack.Push(character.Save());
                character.Restore(_redoStack.Pop());
            }
            else
            {
                Console.WriteLine("No redo available.");
            }
        }
    }

    public class Memento
    {
        public static void Main(string[] args)
        {
            var hero = new Character("hero", 100, 50, "Sword");
            var saveManager = new GameSaveManager();

            // Show initial status
            hero.ShowStatus();

            // Save state
            saveManager.SaveState(hero);

            // Take damage
            hero.TakeDamage(30);
            hero.ShowStatus();
            saveManager.SaveState(hero);

            // Change weapon
            hero.ChangeWeapon("Bow");
            hero.ShowStatus();
            saveManager.SaveState(hero);

            // Undo
            Console.WriteLine("\nUndo:");
            saveManager.Undo(hero);
            hero.ShowStatus();

            // Redo
            Console.WriteLine("\nRedo:");
            saveManager.Redo(hero);
            hero.ShowStatus();

            // Undo
            Console.WriteLine("\nUndo:");
            saveManager.Undo(hero);
            hero.ShowStatus();

            // Redo
            Console.WriteLine("\nRedo:");
            saveManager.Redo(hero);
            hero.ShowStatus();
        }
    }
}
