using BOL.BANNERADS;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOL.ComponentModels.Pages
{
    public class IndexVM
    {
        public IList<HomeBanner> HomeBannerTop { get; set; }
        public IList<HomeBanner> HomeBannerMiddle1 { get; set; }
        public IList<HomeBanner> HomeBannerMiddle2 { get; set; }
        public IList<HomeBanner> HomeBannerBottom { get; set; }
        public int HomeBannerTopLimit {
            get
            {
                return HomeBannerTop.Count > 4 ? HomeBannerTop.Count : 4;
            }
        }
        public int HomeBannerMiddle1Limit {
            get
            {
                return HomeBannerMiddle1.Count > 2 ? HomeBannerMiddle1.Count : 2;
            }
        }
        public int HomeBannerMiddle2Limit
        {
            get
            {
                return HomeBannerMiddle2.Count > 2 ? HomeBannerMiddle2.Count : 2;
            }
        }
        public int HomeBannerBottomLimit
        {
            get
            {
                return HomeBannerBottom.Count > 2 ? HomeBannerBottom.Count : 2;
            }
        }
    }
}
