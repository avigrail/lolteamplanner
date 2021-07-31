using System;
using System.Collections.Generic;

namespace TeamPicks
{
    public interface ICrawlerService
    {
        List<Champion> GetData();
    }
}