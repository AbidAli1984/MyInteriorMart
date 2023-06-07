using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BOL.LISTING;
using BOL.VIEWMODELS;

namespace BAL.Search
{
    public interface ISearch
    {
        public string GetCity();
        public Task<IEnumerable<SearchListingViewModel>> Category(string term, int id, string city);
        public Task<IEnumerable<SearchListingViewModel>> Keyword(string term, int id, string city);
        public Task<IEnumerable<SearchListingViewModel>> Address(string term, int id);
        public void CreateSearchHistory(string term, int id, string userGuid);
        public void IncrementListingImpression(IEnumerable<SearchListingViewModel> searchResult);
        public void AddNewSearchTerm(string searchTerm);
    }
}
