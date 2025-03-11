namespace DesignPattern.Behavioral
{
    interface ITaxStrategy
    {
        double CalculateTax(double income);
    }

    class USTaxStrategy : ITaxStrategy
    {
        public double CalculateTax(double income)
        {
            return income * 0.1;
        }
    }

    class UKTaxStrategy : ITaxStrategy
    {
        public double CalculateTax(double income)
        {
            return income * 0.2;
        }
    }

    class GermanyTaxStrategy : ITaxStrategy
    {
        public double CalculateTax(double income)
        {
            return income * 0.15;
        }
    }

    class TaxCalculator
    {
        private ITaxStrategy? _taxStrategy;
        public void SetTaxStrategy(ITaxStrategy taxStrategy)
        {
            _taxStrategy = taxStrategy;
        }
        public double CalculateTax(double income)
        {
            if (_taxStrategy == null)
            {
                Console.WriteLine("🚫 No tax strategy selected.");
                return 0;
            }
            return _taxStrategy.CalculateTax(income);
        }
    }

    public class Strategy
    {
        /*public void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var taxCalculator = new TaxCalculator();
            taxCalculator.SetTaxStrategy(new USTaxStrategy());
            var tax = taxCalculator.CalculateTax(1000);
            System.Console.WriteLine(tax);

            taxCalculator.SetTaxStrategy(new UKTaxStrategy());
            tax = taxCalculator.CalculateTax(1000);
            System.Console.WriteLine(tax);

            taxCalculator.SetTaxStrategy(new GermanyTaxStrategy());
            tax = taxCalculator.CalculateTax(1000);
            System.Console.WriteLine(tax);
        }*/
    }
}
