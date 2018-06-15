using FluxInfo.Metier;
using System;

namespace FluxInfo.Parser
{
    public interface IRssParser
    {
        Rss ParserRSS(String url);
    }
}
