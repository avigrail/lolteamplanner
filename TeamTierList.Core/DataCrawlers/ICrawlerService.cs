using System;
using System.Collections.Generic;

namespace TeamTierList.Core
{
    public interface ICrawlerService
    {
        List<Champion> GetData();
    }
}