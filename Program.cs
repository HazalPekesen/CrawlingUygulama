using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net;
using System.IO;
using System;

namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Uri url = new Uri("https://www.sahibinden.com/"); // parse isleminin uygulanacagi web sayfasinin linkini Uri tipinde verdik.
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;  // turkce ve unicode sorununu onlemek amaciyla encoding yapıldı.
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string html = client.DownloadString(url);  // siteye baglanarak tum sayfanın html icerigini parse ediyoruz.
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();  // htmlAgilityPack kutup. kullanarak htmldocument olusturuldu
            document.LoadHtml(html);  // parse edilen html icerigini loadHtml methodu ile html doc uzerine yaziyoruz

            HtmlNodeCollection titles = document.DocumentNode.SelectNodes(@"//*[@id=""container""]/div[3]/div/div[3]/div[3]/ul/");  // ilgili tagın XPath adresi kullanıldı
            foreach (var item in titles)
            {
                foreach (var innerItem in item.SelectNodes("li")) //her ul un içindeki li de dön
                {
                    foreach (var items in innerItem.SelectNodes("a//span")) // her li nin içindeki a elementinin altındaki spanlarda dön
                    {
                        StreamWriter Yaz = new StreamWriter(@"C:\Users\pc\Desktop\deneme.txt");  // txt dosyası oluşturuldu, yolu belirtildi
                        string baslik = items.InnerText;
                        Console.WriteLine(baslik);
                        Yaz.WriteLine(baslik);
                        Yaz.Close();
                    }
                }
            }
            Console.Read();
        }
    }
}
