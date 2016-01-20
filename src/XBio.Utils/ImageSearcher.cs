using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace XBio.Utils
{
    public class ImageSearcher
    {
        public void GoogleTest()
        {
            var googleSearch = @"https://" + @"www.google.com/search?q=bechtel&hl=en&biw=1920&bih=979&source=lnms&tbm=isch&sa=X&ved=0ahUKEwj6yd_Q6LXKAhWM7iYKHd5tDegQ_AUICSgD#hl=en&tbm=isch&q=bechtel+png";
            var result = PerformGet(googleSearch);
            result.ForEach(Console.WriteLine);
        }

        private List<string> PerformGet(string uri)
        {
            var values = new List<string>();
            using (var webClient = new WebClient())
            {
                //webClient.Credentials = _credentials;

                try
                {
                    using (var stream = webClient.OpenRead(uri))
                    {
                        if (stream != null)
                            using (var streamReader = new StreamReader(stream))
                            {
                                while (!streamReader.EndOfStream)
                                {
                                    values.Add(streamReader.ReadLine());
                                }
                            }
                    }
                }
                catch (WebException)
                {
                    Console.WriteLine(uri);
                    throw;
                }
            }
            return values;
        }

    }
}
