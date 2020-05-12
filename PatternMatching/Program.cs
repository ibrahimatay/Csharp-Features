using System;

namespace PatternMatching
{

    /// <summary>
    /// Pattern Matching
    /// https://docs.microsoft.com/en-us/archive/msdn-magazine/2019/may/csharp-8-0-pattern-matching-in-csharp-8-0
    /// https://docs.microsoft.com/en-us/dotnet/csharp/pattern-matching
    /// https://docs.microsoft.com/en-us/dotnet/csharp/tutorials/pattern-matching
    /// https://github.com/dotnet/roslyn/blob/master/docs/features/patterns.md
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            IInstrument stock01 = new Stock();
            Console.WriteLine($"Stock volatility is {GetVolatilityUsingifBlock(stock01)}");

            IInstrument bond01 = new Bond();
            Console.WriteLine($"Bond volatility is {GetVolatilityUsingifBlock(bond01)}");

            SNP500IndexFund sNP500 = new SNP500IndexFund();
            Console.WriteLine($"SNP500IndexFund volatility is {GetVolatilityUsingSwitchBlock(sNP500)}");

            EmergingMarketFund emergingMarketFund = new EmergingMarketFund();
            Console.WriteLine($"EmergingMarketFund volatility is {GetVolatilityUsingSwitchBlock(emergingMarketFund)}");

            Console.WriteLine($"Authorize is {Authorize("123",string.Empty, false)}");
            Console.WriteLine($"Authorize is {Authorize(string.Empty, string.Empty, true)}");
            Console.WriteLine($"Authorize is {Authorize(string.Empty, string.Empty, false)}");

            Stock stock02 = new Stock(920.12);
            Console.WriteLine($"Stock should be price {ShouldBuy(stock02, 170)}");

            Stock stock03 = new Stock(920.12);
            Console.WriteLine($"Stock should be price {ShouldBuy(stock03, 1200.10)}");
        }

        static VolatilityType GetVolatilityUsingifBlock(IInstrument instrument)
        {
            // csharp-7.0
            if (instrument is Stock s)
            {
                return s.Volatility;
            }

            if (instrument is Bond b)
            {
                return b.Volatility;
            }

            return VolatilityType.Unknown;
        }

        static VolatilityType GetVolatilityUsingSwitchBlock(IInstrument instrument)
        {
            // csharp-7.0
            switch (instrument)
            {
                case Stock s when s is SNP500IndexFund:
                    return s.Volatility;
                case Stock s when s is EmergingMarketFund:
                    return s.Volatility;
            }

            return VolatilityType.Unknown;
        }

        // csharp-7.0
        static VolatilityType GetVolatilityUsingSwitchBlockV2(IInstrument instrument) =>
           instrument switch
           {
               Stock s when s is SNP500IndexFund => s.Volatility,
               Stock s when s is EmergingMarketFund => s.Volatility,
               _ => VolatilityType.Unknown
           };

        // csharp-8.0 - tupe-switch
        static bool Authorize(string apiKey, string authToken, bool isAuthTokenValid) =>
            (apiKey, authToken, isAuthTokenValid) switch
            {
                ("123", _, _) => true,
                (_,_,true)=> true,
                (_, _, false) => false,
            };

        // csharp-8.0
        static bool ShouldBuy(Stock stock, double maxPrice) =>
            (stock, maxPrice) switch
            {
                var (Volatility, price) when price < maxPrice => true,
                var (Volatility, price) when price > maxPrice => false,
                _=>false
            };

        public interface IInstrument
        {
            VolatilityType Volatility { get; set; }
        }

        public enum VolatilityType
        {
            Unknown = 0,
            Low = 1,
            High = 2
        }

        public class Stock : IInstrument
        {
            public VolatilityType Volatility { get; set; } = VolatilityType.High;

            public double Price { get; set; }

            public Stock(double price) => (price) = (Price);
           
            public Stock()
            {

            }
        }

        public class Bond : IInstrument
        {
            public virtual VolatilityType Volatility { get; set; } = VolatilityType.Low;
        }

        public class SNP500IndexFund : Bond
        {

        }

        public class EmergingMarketFund : Bond
        {
            public override VolatilityType Volatility { get; set; } = VolatilityType.High;
        }
    }
}



