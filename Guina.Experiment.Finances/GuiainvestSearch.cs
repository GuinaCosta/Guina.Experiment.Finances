using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Guina.Experiment.Finances
{
    public static class GuiainvestSearch
    {
        const String GUIAINVEST_QUERY = "https://www.guiainvest.com.br/mural/";

        public static String GetTickerPrice(String ticker)
        {
            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(String.Concat(GUIAINVEST_QUERY, ticker, ".aspx"));

                webRequest.ContentType = "text/html";
                webRequest.Method = "GET";

                using (var response = (HttpWebResponse)webRequest.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var stream = new StreamReader(response.GetResponseStream()).ReadToEnd();
                        int index = stream.IndexOf("osci_" + ticker.ToUpper());
                        string conteudo = stream.Substring(index, 50);

                        int start = conteudo.IndexOf(">");
                        int end = conteudo.IndexOf("%");
                        string sinal = "+";

                        if (conteudo.IndexOf("changeup") > 0)
                        {
                            start = conteudo.IndexOf(";");
                            sinal = "+";
                        }
                        else if (conteudo.IndexOf("changedown") > 0)
                        {
                            start = conteudo.IndexOf(";");
                            sinal = "-";
                        }

                        return String.Concat(sinal, conteudo.Substring(start + 1, end - start));
                    }
                    else
                        return "Nada encontrado";
                }
            }
            catch (WebException ex)
            {
                var stream = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                return stream;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tickers"></param>
        /// <returns></returns>
        public static void GetTickersPrices(params String[] tickers)
        {
            foreach (string ticker in tickers)
            {
                Console.WriteLine(String.Concat(ticker, ":", GetTickerPrice(ticker)));
            }
        }
    }
}
