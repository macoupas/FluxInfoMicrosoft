using FluxInfo.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluxInfo.Metier;
using System.Xml.Linq;
using System.Diagnostics;

namespace RssParser
{
    public class RssParserWithLinQ : IRssParser
    {

        public EventHandler<Rss> rssChanged;

        public Rss ParserRSS(string url)
        {

            try
            {
                XDocument rssXml = XDocument.Load(url);
                List<Item> items = rssXml.Descendants("item").Select(elmtItem => new Item()
                {
                    Titre = elmtItem.Element(Item.XML_ITEM_TITLE).Value,
                    Description = elmtItem.Element(Item.XML_ITEM_DESCRIPTION).Value,
                    Lien = elmtItem.Element(Item.XML_ITEM_LINK).Value,
                    Date =  Convert.ToDateTime(elmtItem.Element("pubDate").Value)
                }).ToList();

                int index = 0;
                foreach(var i in rssXml.Descendants("item"))
                {
                    if(i.Element("enclosure") != null)
                    {
                        if (i.Element("enclosure").Attribute("url") != null)
                        {
                            items[index].LienImage = i.Element("enclosure").Attribute("url").Value;
                        } else
                        {
                            items[index].LienImage = "https://i.ytimg.com/vi/VDZm3Y0onmA/maxresdefault.jpg";
                        }
                    }
                    else
                    {
                        items[index].LienImage = "https://i.ytimg.com/vi/VDZm3Y0onmA/maxresdefault.jpg";
                    }
                    index++;
                }

                Channel channel = rssXml.Descendants("channel").Select(elmtChannel => new Channel()
                {
                    Title = elmtChannel.Element(Channel.XML_CHANNEL_TITLE).Value,
                    Lien = elmtChannel.Element(Channel.XML_CHANNEL_LINK).Value,
                    Description = elmtChannel.Element(Channel.XML_CHANNEL_DESCRIPTION).Value
                }).Single();

                channel.AjouterItems(items);

                Rss rssDeserialized = rssXml.Descendants("rss").Select(elmtRss => new Rss()
                {
                    Version = elmtRss.Attribute("version").Value,
                    Channel = channel
                }).Single();

                rssChanged?.Invoke(this, rssDeserialized);

                return rssDeserialized;
            } catch (ArgumentException ae)
            {
                Debug.WriteLine(ae.Message);
                throw new Exception("Uri invalide");
            } catch ( System.Net.WebException we)
            {
                Debug.WriteLine(we.Message);
                throw new Exception("Url invalide");
            }
            
        }
    }
}