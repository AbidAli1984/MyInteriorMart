using AntDesign;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRONTEND.BLAZOR.Modules.Search
{
    public partial class BasicSearch
    {
        public class BasicSearchVM
        {
            public string Type { get; set; }
            public string Parameter { get; set; }
        }

        public BasicSearchVM SelectedBasicSearchVM { get; set; }

        //public List<BasicSearchVM> ListBasicSearchVM;

        //public async Task<IEnumerable<BasicSearchVM>> Search(string searchText)
        //{
        //    return await Task.FromResult(ListBasicSearchVM.Where(x => x.Type))
        //}
    }
}
