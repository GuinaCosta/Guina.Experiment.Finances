using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guina.Experiment.Finances
{
    class Program
    {
        static void Main(string[] args)
        {
            
            GuiainvestSearch.GetTickersPrices(ConfigurationManager.AppSettings["tickers"].Split('|'));

            Console.ReadLine();
        }
    }
}
