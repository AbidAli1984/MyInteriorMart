using System;
using System.Collections.Generic;
using System.Text;

namespace BOL.SHARED
{
    public class PageVM
    {
        public int TotalData { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int PageSize 
        { 
            get { return 10; }
        }

        public int TotalPages 
        { 
            get
            {
                int pages = TotalData / PageSize;

                if (TotalData % PageSize > 0)
                    pages++;

                return pages;
            }
        }
        public int FromPageNo
        {
            get
            {
                return TotalPages > 0 ?  ((CurrentPage - 1) * PageSize) : 0;
            }
        }
    }
}
