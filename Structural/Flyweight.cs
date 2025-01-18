using System.Text;

namespace DesignPattern.Structural
{
    public class Sentence
    {
        private string plainText = string.Empty;
        public List<int> wordIndices = new List<int>();
        private static readonly WordTokenFactory tokenFactory = new WordTokenFactory();
        private static readonly WordToken DummyToken = new WordToken("invalid")
        {
            Capitalize = false
        };

        public Sentence(string plainText)
        {
            this.plainText = plainText;
            foreach (var text in plainText.Split(' '))
            {

                wordIndices.Add(tokenFactory.GetOrAddToken(text));
            }
        }

        public WordToken this[int index]
        {
            get
            {
                if (index >= 0 && index <= wordIndices.Max())
                {
                    return tokenFactory.GetToken(index);
                }
                else
                {
                    Console.WriteLine($"Index {index} is out of range. Returning dummy token.");
                    return DummyToken;
                }
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var index in wordIndices)
            {
                if (tokenFactory.GetToken(index).Capitalize) sb.Append($"{tokenFactory.GetToken(index).Text.ToUpper()} ");
                else sb.Append($"{tokenFactory.GetToken(index).Text} ");
            }

            return sb.ToString().Trim();
        }

        // Flyweight Factory
        private class WordTokenFactory
        {
            private readonly List<WordToken> tokens = new List<WordToken>();

            public int GetOrAddToken(string text)
            {
                var token = tokens.FirstOrDefault(t => t.Text == text);
                if (token != null)
                {
                    return tokens.IndexOf(token);
                }

                tokens.Add(new WordToken(text));
                return tokens.Count - 1;
            }

            public WordToken GetToken(int index)
            {
                return tokens[index];
            }
        }
        public class WordToken
        {
            public string Text;
            public bool Capitalize;

            public WordToken(string text)
            {
                this.Text = text;
            }
        }
    }
    public class Flyweight
    {
        /*static void Main(string[] args)
        {
            Sentence s = new Sentence("alpha gamma beta delta alpha gamma beta delta alpha gamma beta delta");
            s[1].Capitalize = true;
            s[3].Capitalize = true;
            Console.WriteLine(s);
            s[3].Text = "omega";
            Console.WriteLine(s);
            s[4].Capitalize = false;
            s.wordIndices.ForEach(s => Console.Write($"{s} "));
        }*/
    }
}

// - Use: space optimization that let us use less memory by storing externally the data
//        associated with similar objects.