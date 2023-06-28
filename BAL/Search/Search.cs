using BOL.LISTING;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.LISTING;
using DAL.SHARED;
using DAL.CATEGORIES;
using System.Linq;
using DAL.AUDIT;
using BAL.Audit;
using Microsoft.AspNetCore.Identity;
using BAL.Listings;
using Microsoft.EntityFrameworkCore;
using BOL.VIEWMODELS;
using BOL.AUDITTRAIL;
using BOL.CATEGORIES;
using DAL.Models;

namespace BAL.Search
{
    public class Search : ISearch
    {
        private readonly ListingDbContext listingContext;
        private readonly SharedDbContext addressContext;
        private readonly CategoriesDbContext categoryContext;
        private readonly AuditDbContext auditContext;
        private readonly SharedDbContext sharedContext;
        private readonly IHistoryAudit historyAudit;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IListingManager listingManager;
        public Search(ListingDbContext listingContext, SharedDbContext addressContext, CategoriesDbContext categoryContext, AuditDbContext auditContext, 
            SharedDbContext sharedContext, IHistoryAudit historyAudit, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IListingManager listingManager)
        {
            this.listingContext = listingContext;
            this.addressContext = addressContext;
            this.categoryContext = categoryContext;
            this.auditContext = auditContext;
            this.sharedContext = sharedContext;
            this.historyAudit = historyAudit;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.listingManager = listingManager;
        }

        public string GetCity()
        {
            // Shafi: Get client location details using https://geolocation-db.com/ free service with unlimited calls forever
            var webClient = new System.Net.WebClient();
            var data = webClient.DownloadString("https://geolocation-db.com/json");
            dynamic d = JsonConvert.DeserializeObject<dynamic>(data);
            string city = d["city"];
            // End:
            return city;
        }

        public void CreateSearchHistory(string searchTerm, int searchTermID, string userGuid)
        {
            DateTime timeZoneDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

            // Shafi: Get client location details using https://geolocation-db.com/ free service with unlimited calls forever
            var webClient = new System.Net.WebClient();
            var data = webClient.DownloadString("https://geolocation-db.com/json");
            dynamic d = JsonConvert.DeserializeObject<dynamic>(data);
            string countryCode = d["country_code"];
            string country = d["country_name"];
            string city = d["city"];
            string postal = d["postal"];
            string state = d["state"];
            string ipv4 = d["IPv4"];
            string latitude = d["latitude"];
            string longitude = d["longitude"];
            // End:

            SearchHistory history = new SearchHistory();
            history.Country = country;
            history.CountryCode = countryCode;
            history.City = city;
            history.State = state;
            history.Pincode = postal;
            history.IPAddress = ipv4;
            history.Latitude = latitude;
            history.Longitude = longitude;
            history.UserGuid = userGuid;
            history.VisitDate = timeZoneDate;
            history.VisitTime = timeZoneDate;
            history.SearchTerm = searchTerm;
            history.SearchTermID = searchTermID;
            auditContext.Add(history);
            auditContext.SaveChanges();
        }

        public void IncrementListingImpression(IEnumerable<SearchListingViewModel> searchResult)
        {
            IList<int> listingIds = new List<int>();

            foreach (var item in searchResult)
            {
                listingIds.Add(item.ListingViewCount.ListingID);
            }

            foreach(var id in listingIds)
            {
                var listingViewCount = listingContext.ListingViewCount.Find(id);
                listingViewCount.ImpressionCount += 1;
                listingContext.Update(listingViewCount);
                listingContext.SaveChanges();
            }
        }

        public void AddNewSearchTerm(string searchTerm)
        {
            DateTime timeZoneDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

            NewSearchTerm term = new NewSearchTerm()
            {
                SearchTerm = searchTerm,
                SearchDateTime = timeZoneDate
            };

            listingContext.Add(term);
            listingContext.SaveChanges();
        }

        public async Task<IEnumerable<SearchListingViewModel>> Category(string term, int id, string city)
        {
            if (term == "CI")
            {
                var result = from list in listingContext.Listing
                             join comm in listingContext.Communication
                             on list.ListingID equals comm.ListingID

                             join add in listingContext.Address
                             on list.ListingID equals add.ListingID

                             join cat in listingContext.Categories
                                   .Where(c => c.FirstCategoryID == id)
                                   on list.ListingID equals cat.ListingID

                             join view in listingContext.ListingViewCount
                             
                             on list.ListingID equals view.ListingID

                             join spec in listingContext.Specialisation
                                   on list.ListingID equals spec.ListingID

                             join work in listingContext.WorkingHours
                                   on list.ListingID equals work.ListingID

                             join pay in listingContext.PaymentMode
                                   on list.ListingID equals pay.ListingID

                             select new SearchListingViewModel
                             {
                                 Listing = list,
                                 Address = add,
                                 Categories = cat,
                                 Specialisation = spec,
                                 WorkingHours = work,
                                 PaymentMode = pay,
                                 ListingViewCount = view
                             };

                var firstCategory = await categoryContext.FirstCategory.FindAsync(id);

                if (firstCategory != null)
                {
                    firstCategory.SearchCount = firstCategory.SearchCount + 1;
                    categoryContext.Update(firstCategory);
                    await categoryContext.SaveChangesAsync();

                }

                return await result.ToListAsync();
            }
            else if (term == "CII")
            {
                var result = from list in listingContext.Listing
                             join comm in listingContext.Communication
                             on list.ListingID equals comm.ListingID

                             join add in listingContext.Address
                             on list.ListingID equals add.ListingID

                             join cat in listingContext.Categories
                                   .Where(c => c.SecondCategoryID == id)
                                   on list.ListingID equals cat.ListingID

                             join view in listingContext.ListingViewCount
                             
                             on list.ListingID equals view.ListingID

                             join spec in listingContext.Specialisation
                                   on list.ListingID equals spec.ListingID

                             join work in listingContext.WorkingHours
                                   on list.ListingID equals work.ListingID

                             join pay in listingContext.PaymentMode
                                   on list.ListingID equals pay.ListingID

                             select new SearchListingViewModel
                             {
                                 Listing = list,
                                 Address = add,
                                 Categories = cat,
                                 Specialisation = spec,
                                 WorkingHours = work,
                                 PaymentMode = pay,
                                 ListingViewCount = view
                             };

                var secondCategory = await categoryContext.SecondCategory.FindAsync(id);

                if (secondCategory != null)
                {
                    secondCategory.SearchCount = secondCategory.SearchCount + 1;
                    categoryContext.Update(secondCategory);
                    await categoryContext.SaveChangesAsync();
                }

                return await result.ToListAsync();
            }
            else if (term == "CIII")
            {
                var result = from list in listingContext.Listing
                             join comm in listingContext.Communication
                             on list.ListingID equals comm.ListingID

                             join add in listingContext.Address
                             on list.ListingID equals add.ListingID

                             join cat in listingContext.Categories
                                   .Where(c => c.ThirdCategories.Contains(Convert.ToString(id)))
                                   on list.ListingID equals cat.ListingID

                             join view in listingContext.ListingViewCount
                             
                             on list.ListingID equals view.ListingID

                             join spec in listingContext.Specialisation
                                   on list.ListingID equals spec.ListingID

                             join work in listingContext.WorkingHours
                                   on list.ListingID equals work.ListingID

                             join pay in listingContext.PaymentMode
                                   on list.ListingID equals pay.ListingID

                             select new SearchListingViewModel
                             {
                                 Listing = list,
                                 Address = add,
                                 Categories = cat,
                                 Specialisation = spec,
                                 WorkingHours = work,
                                 PaymentMode = pay,
                                 ListingViewCount = view
                             };

                var thirdCategory = await categoryContext.ThirdCategory.FindAsync(id);
                if (thirdCategory != null)
                {
                    thirdCategory.SearchCount = thirdCategory.SearchCount + 1;
                    categoryContext.Update(thirdCategory);
                    await categoryContext.SaveChangesAsync();
                }

                return await result.ToListAsync();
            }
            else if (term == "CIV")
            {
                var result = from list in listingContext.Listing
                             join comm in listingContext.Communication
                             on list.ListingID equals comm.ListingID

                             join add in listingContext.Address
                             on list.ListingID equals add.ListingID

                             join cat in listingContext.Categories
                                   .Where(c => c.FourthCategories.Contains(Convert.ToString(id)))
                                   on list.ListingID equals cat.ListingID

                             join view in listingContext.ListingViewCount
                             on list.ListingID equals view.ListingID

                             join spec in listingContext.Specialisation
                                   on list.ListingID equals spec.ListingID

                             join work in listingContext.WorkingHours
                                   on list.ListingID equals work.ListingID

                             join pay in listingContext.PaymentMode
                                   on list.ListingID equals pay.ListingID

                             select new SearchListingViewModel
                             {
                                 Listing = list,
                                 Address = add,
                                 Categories = cat,
                                 Specialisation = spec,
                                 WorkingHours = work,
                                 PaymentMode = pay,
                                 ListingViewCount = view
                             };

                var fourthCategory = await categoryContext.FourthCategory.FindAsync(id);
                if (fourthCategory != null)
                {
                    fourthCategory.SearchCount = fourthCategory.SearchCount + 1;
                    categoryContext.Update(fourthCategory);
                    await categoryContext.SaveChangesAsync();
                }

                return await result.ToListAsync();
            }
            else if (term == "CV")
            {
                var result = from list in listingContext.Listing
                             join comm in listingContext.Communication
                             on list.ListingID equals comm.ListingID

                             join add in listingContext.Address
                             on list.ListingID equals add.ListingID

                             join cat in listingContext.Categories
                                   .Where(c => c.FifthCategories.Contains(Convert.ToString(id)))
                                   on list.ListingID equals cat.ListingID

                             join view in listingContext.ListingViewCount
                             on list.ListingID equals view.ListingID

                             join spec in listingContext.Specialisation
                                   on list.ListingID equals spec.ListingID

                             join work in listingContext.WorkingHours
                                   on list.ListingID equals work.ListingID

                             join pay in listingContext.PaymentMode
                                   on list.ListingID equals pay.ListingID

                             select new SearchListingViewModel
                             {
                                 Listing = list,
                                 Address = add,
                                 Categories = cat,
                                 Specialisation = spec,
                                 WorkingHours = work,
                                 PaymentMode = pay,
                                 ListingViewCount = view
                             };

                var fifthCategory = await categoryContext.FifthCategory.FindAsync(id);
                if (fifthCategory != null)
                {
                    fifthCategory.SearchCount = fifthCategory.SearchCount + 1;
                    categoryContext.Update(fifthCategory);
                    await categoryContext.SaveChangesAsync();
                }

                return await result.ToListAsync();
            }
            else if (term == "CVI")
            {
                var result = from list in listingContext.Listing
                             join comm in listingContext.Communication
                             on list.ListingID equals comm.ListingID

                             join add in listingContext.Address
                             on list.ListingID equals add.ListingID

                             join cat in listingContext.Categories
                                   .Where(c => c.SixthCategories.Contains(Convert.ToString(id)))
                                   on list.ListingID equals cat.ListingID

                             join view in listingContext.ListingViewCount
                             on list.ListingID equals view.ListingID

                             join spec in listingContext.Specialisation
                                   on list.ListingID equals spec.ListingID

                             join work in listingContext.WorkingHours
                                   on list.ListingID equals work.ListingID

                             join pay in listingContext.PaymentMode
                                   on list.ListingID equals pay.ListingID

                             select new SearchListingViewModel
                             {
                                 Listing = list,
                                 Address = add,
                                 Categories = cat,
                                 Specialisation = spec,
                                 WorkingHours = work,
                                 PaymentMode = pay,
                                 ListingViewCount = view
                             };

                var sixthCategory = await categoryContext.SixthCategory.FindAsync(id);
                if (sixthCategory != null)
                {
                    sixthCategory.SearchCount = sixthCategory.SearchCount + 1;
                    categoryContext.Update(sixthCategory);
                    await categoryContext.SaveChangesAsync();
                }

                return await result.ToListAsync();
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public async Task<IEnumerable<SearchListingViewModel>> Keyword(string term, int id, string city)
        {
            if (term == "KI")
            {
                var firstCategoryId = await categoryContext.KeywordFirstCategory.Where(i => i.KeywordFirstCategoryID == id).Select(i => i.FirstCategoryID).FirstOrDefaultAsync();

                var result = from list in listingContext.Listing
                             join comm in listingContext.Communication
                             on list.ListingID equals comm.ListingID

                             join add in listingContext.Address
                             on list.ListingID equals add.ListingID

                             join cat in listingContext.Categories
                                   .Where(c => c.FirstCategoryID == firstCategoryId)
                                   on list.ListingID equals cat.ListingID

                             join view in listingContext.ListingViewCount
                             on list.ListingID equals view.ListingID

                             join spec in listingContext.Specialisation
                                   on list.ListingID equals spec.ListingID

                             join work in listingContext.WorkingHours
                                   on list.ListingID equals work.ListingID

                             join pay in listingContext.PaymentMode
                                   on list.ListingID equals pay.ListingID

                             select new SearchListingViewModel
                             {
                                 Listing = list,
                                 Address = add,
                                 Categories = cat,
                                 Specialisation = spec,
                                 WorkingHours = work,
                                 PaymentMode = pay,
                                 ListingViewCount = view
                             };

                var firstCatKeyword = await categoryContext.KeywordFirstCategory.FindAsync(id);
                if (firstCatKeyword != null)
                {
                    firstCatKeyword.SearchCount = firstCatKeyword.SearchCount + 1;
                    categoryContext.Update(firstCatKeyword);
                    await categoryContext.SaveChangesAsync();
                }

                return await result.ToListAsync();
            }
            else if (term == "KII")
            {
                var secondCategoryId = await categoryContext.KeywordSecondCategory.Where(i => i.KeywordSecondCategoryID == id).Select(i => i.SecondCategoryID).FirstOrDefaultAsync();

                var result = from list in listingContext.Listing
                             join comm in listingContext.Communication
                             on list.ListingID equals comm.ListingID

                             join add in listingContext.Address
                             on list.ListingID equals add.ListingID

                             join cat in listingContext.Categories
                                   .Where(c => c.SecondCategoryID == secondCategoryId)
                                   on list.ListingID equals cat.ListingID

                             join view in listingContext.ListingViewCount
                             on list.ListingID equals view.ListingID

                             join spec in listingContext.Specialisation
                                   on list.ListingID equals spec.ListingID

                             join work in listingContext.WorkingHours
                                   on list.ListingID equals work.ListingID

                             join pay in listingContext.PaymentMode
                                   on list.ListingID equals pay.ListingID

                             select new SearchListingViewModel
                             {
                                 Listing = list,
                                 Address = add,
                                 Categories = cat,
                                 Specialisation = spec,
                                 WorkingHours = work,
                                 PaymentMode = pay,
                                 ListingViewCount = view
                             };

                var secondCatKeyword = await categoryContext.KeywordSecondCategory.FindAsync(id);
                if (secondCatKeyword != null)
                {
                    secondCatKeyword.SearchCount = secondCatKeyword.SearchCount + 1;
                    categoryContext.Update(secondCatKeyword);
                    await categoryContext.SaveChangesAsync();
                }

                return await result.ToListAsync();
            }
            else if (term == "KIII")
            {
                var secondCategoryId = await categoryContext.KeywordThirdCategory.Where(i => i.KeywordThirdCategoryID == id).Select(i => i.SecondCategoryID).FirstOrDefaultAsync();

                var result = from list in listingContext.Listing
                             join comm in listingContext.Communication
                             on list.ListingID equals comm.ListingID

                             join add in listingContext.Address
                             on list.ListingID equals add.ListingID

                             join cat in listingContext.Categories
                                   .Where(c => c.ThirdCategories.Contains(Convert.ToString(secondCategoryId)))
                                   on list.ListingID equals cat.ListingID

                             join view in listingContext.ListingViewCount
                             on list.ListingID equals view.ListingID

                             join spec in listingContext.Specialisation
                                   on list.ListingID equals spec.ListingID

                             join work in listingContext.WorkingHours
                                   on list.ListingID equals work.ListingID

                             join pay in listingContext.PaymentMode
                                   on list.ListingID equals pay.ListingID

                             select new SearchListingViewModel
                             {
                                 Listing = list,
                                 Address = add,
                                 Categories = cat,
                                 Specialisation = spec,
                                 WorkingHours = work,
                                 PaymentMode = pay,
                                 ListingViewCount = view
                             };

                var thirdCatKeyword = await categoryContext.KeywordThirdCategory.FindAsync(id);
                if (thirdCatKeyword != null)
                {
                    thirdCatKeyword.SearchCount = thirdCatKeyword.SearchCount + 1;
                    categoryContext.Update(thirdCatKeyword);
                    await categoryContext.SaveChangesAsync();
                }

                return await result.ToListAsync();
            }
            else if (term == "KIV")
            {
                var secondCategoryId = await categoryContext.KeywordFourthCategory.Where(i => i.KeywordFourthCategoryID == id).Select(i => i.SecondCategoryID).FirstOrDefaultAsync();

                var result = from list in listingContext.Listing
                             join comm in listingContext.Communication
                             on list.ListingID equals comm.ListingID

                             join add in listingContext.Address
                             on list.ListingID equals add.ListingID

                             join cat in listingContext.Categories
                                   .Where(c => c.FourthCategories.Contains(Convert.ToString(secondCategoryId)))
                                   on list.ListingID equals cat.ListingID

                             join view in listingContext.ListingViewCount
                             on list.ListingID equals view.ListingID

                             join spec in listingContext.Specialisation
                                   on list.ListingID equals spec.ListingID

                             join work in listingContext.WorkingHours
                                   on list.ListingID equals work.ListingID

                             join pay in listingContext.PaymentMode
                                   on list.ListingID equals pay.ListingID

                             select new SearchListingViewModel
                             {
                                 Listing = list,
                                 Address = add,
                                 Categories = cat,
                                 Specialisation = spec,
                                 WorkingHours = work,
                                 PaymentMode = pay,
                                 ListingViewCount = view
                             };

                var fourthCatKeyword = await categoryContext.KeywordFourthCategory.FindAsync(id);
                if (fourthCatKeyword != null)
                {
                    fourthCatKeyword.SearchCount = fourthCatKeyword.SearchCount + 1;
                    categoryContext.Update(fourthCatKeyword);
                    await categoryContext.SaveChangesAsync();
                }

                return await result.ToListAsync();
            }
            else if (term == "KV")
            {
                var secondCategoryId = await categoryContext.KeywordFifthCategory.Where(i => i.KeywordFifthCategoryID == id).Select(i => i.SecondCategoryID).FirstOrDefaultAsync();

                var result = from list in listingContext.Listing
                             join comm in listingContext.Communication
                             on list.ListingID equals comm.ListingID

                             join add in listingContext.Address
                             on list.ListingID equals add.ListingID

                             join cat in listingContext.Categories
                                   .Where(c => c.FifthCategories.Contains(Convert.ToString(secondCategoryId)))
                                   on list.ListingID equals cat.ListingID

                             join view in listingContext.ListingViewCount
                             on list.ListingID equals view.ListingID

                             join spec in listingContext.Specialisation
                                   on list.ListingID equals spec.ListingID

                             join work in listingContext.WorkingHours
                                   on list.ListingID equals work.ListingID

                             join pay in listingContext.PaymentMode
                                   on list.ListingID equals pay.ListingID

                             select new SearchListingViewModel
                             {
                                 Listing = list,
                                 Address = add,
                                 Categories = cat,
                                 Specialisation = spec,
                                 WorkingHours = work,
                                 PaymentMode = pay,
                                 ListingViewCount = view
                             };

                var fifthCatKeyword = await categoryContext.KeywordFifthCategory.FindAsync(id);
                if (fifthCatKeyword != null)
                {
                    fifthCatKeyword.SearchCount = fifthCatKeyword.SearchCount + 1;
                    categoryContext.Update(fifthCatKeyword);
                    await categoryContext.SaveChangesAsync();
                }

                return await result.ToListAsync();
            }
            else if (term == "KVI")
            {
                var secondCategoryId = await categoryContext.KeywordSixthCategory.Where(i => i.KeywordSixthCategoryID == id).Select(i => i.SecondCategoryID).FirstOrDefaultAsync();

                var result = from list in listingContext.Listing
                             join comm in listingContext.Communication
                             on list.ListingID equals comm.ListingID

                             join add in listingContext.Address
                             on list.ListingID equals add.ListingID

                             join cat in listingContext.Categories
                                   .Where(c => c.SixthCategories.Contains(Convert.ToString(secondCategoryId)))
                                   on list.ListingID equals cat.ListingID

                             join view in listingContext.ListingViewCount
                             on list.ListingID equals view.ListingID

                             join spec in listingContext.Specialisation
                                   on list.ListingID equals spec.ListingID

                             join work in listingContext.WorkingHours
                                   on list.ListingID equals work.ListingID

                             join pay in listingContext.PaymentMode
                                   on list.ListingID equals pay.ListingID

                             select new SearchListingViewModel
                             {
                                 Listing = list,
                                 Address = add,
                                 Categories = cat,
                                 Specialisation = spec,
                                 WorkingHours = work,
                                 PaymentMode = pay,
                                 ListingViewCount = view
                             };

                var sixthCatKeyword = await categoryContext.KeywordSixthCategory.FindAsync(id);
                if (sixthCatKeyword != null)
                {
                    sixthCatKeyword.SearchCount = sixthCatKeyword.SearchCount + 1;
                    categoryContext.Update(sixthCatKeyword);
                    await categoryContext.SaveChangesAsync();
                }

                return await result.ToListAsync();
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public async Task<IEnumerable<SearchListingViewModel>> Address(string term, int id)
        {
            if (term == "CON")
            {
                var result = from list in listingContext.Listing
                             join comm in listingContext.Communication
                             on list.ListingID equals comm.ListingID

                             join add in listingContext.Address
                             .Where(c => c.CountryID == id)
                             on list.ListingID equals add.ListingID

                             join cat in listingContext.Categories
                             on list.ListingID equals cat.ListingID

                             join view in listingContext.ListingViewCount
                             on list.ListingID equals view.ListingID

                             join spec in listingContext.Specialisation
                                   on list.ListingID equals spec.ListingID

                             join work in listingContext.WorkingHours
                                   on list.ListingID equals work.ListingID

                             join pay in listingContext.PaymentMode
                                   on list.ListingID equals pay.ListingID

                             select new SearchListingViewModel
                             {
                                 Listing = list,
                                 Address = add,
                                 Categories = cat,
                                 Specialisation = spec,
                                 WorkingHours = work,
                                 PaymentMode = pay,
                                 ListingViewCount = view
                             };

                return await result.ToListAsync();
            }
            else if (term == "STA")
            {
                var result = from list in listingContext.Listing
                             join comm in listingContext.Communication
                             on list.ListingID equals comm.ListingID

                             join add in listingContext.Address
                             .Where(c => c.StateID == id)
                             on list.ListingID equals add.ListingID

                             join view in listingContext.ListingViewCount
                             on list.ListingID equals view.ListingID

                             join cat in listingContext.Categories
                             on list.ListingID equals cat.ListingID

                             join spec in listingContext.Specialisation
                                   on list.ListingID equals spec.ListingID

                             join work in listingContext.WorkingHours
                                   on list.ListingID equals work.ListingID

                             join pay in listingContext.PaymentMode
                                   on list.ListingID equals pay.ListingID

                             select new SearchListingViewModel
                             {
                                 Listing = list,
                                 Address = add,
                                 Categories = cat,
                                 Specialisation = spec,
                                 WorkingHours = work,
                                 PaymentMode = pay,
                                 ListingViewCount = view
                             };

                return await result.ToListAsync();
            }
            else if (term == "CIT")
            {
                var result = from list in listingContext.Listing
                             join comm in listingContext.Communication
                             on list.ListingID equals comm.ListingID

                             join add in listingContext.Address
                             .Where(c => c.City == id)
                             on list.ListingID equals add.ListingID

                             join view in listingContext.ListingViewCount
                             on list.ListingID equals view.ListingID

                             join cat in listingContext.Categories
                             on list.ListingID equals cat.ListingID

                             join spec in listingContext.Specialisation
                                   on list.ListingID equals spec.ListingID

                             join work in listingContext.WorkingHours
                                   on list.ListingID equals work.ListingID

                             join pay in listingContext.PaymentMode
                                   on list.ListingID equals pay.ListingID

                             select new SearchListingViewModel
                             {
                                 Listing = list,
                                 Address = add,
                                 Categories = cat,
                                 Specialisation = spec,
                                 WorkingHours = work,
                                 PaymentMode = pay,
                                 ListingViewCount = view
                             };

                return await result.ToListAsync();
            }
            else if (term == "ASB")
            {
                var result = from list in listingContext.Listing
                             join comm in listingContext.Communication
                             on list.ListingID equals comm.ListingID

                             join add in listingContext.Address
                             .Where(c => c.AssemblyID == id)
                             on list.ListingID equals add.ListingID

                             join cat in listingContext.Categories
                             on list.ListingID equals cat.ListingID

                             join view in listingContext.ListingViewCount
                             on list.ListingID equals view.ListingID

                             join spec in listingContext.Specialisation
                                   on list.ListingID equals spec.ListingID

                             join work in listingContext.WorkingHours
                                   on list.ListingID equals work.ListingID

                             join pay in listingContext.PaymentMode
                                   on list.ListingID equals pay.ListingID

                             select new SearchListingViewModel
                             {
                                 Listing = list,
                                 Address = add,
                                 Categories = cat,
                                 Specialisation = spec,
                                 WorkingHours = work,
                                 PaymentMode = pay,
                                 ListingViewCount = view
                             };

                return await result.ToListAsync();
            }
            else if (term == "ARE")
            {
                var result = from list in listingContext.Listing
                             join comm in listingContext.Communication
                             on list.ListingID equals comm.ListingID

                             join add in listingContext.Address
                             .Where(c => c.LocalityID == id)
                             on list.ListingID equals add.ListingID

                             join view in listingContext.ListingViewCount
                             on list.ListingID equals view.ListingID

                             join cat in listingContext.Categories
                             on list.ListingID equals cat.ListingID

                             join spec in listingContext.Specialisation
                                   on list.ListingID equals spec.ListingID

                             join work in listingContext.WorkingHours
                                   on list.ListingID equals work.ListingID

                             join pay in listingContext.PaymentMode
                                   on list.ListingID equals pay.ListingID

                             select new SearchListingViewModel
                             {
                                 Listing = list,
                                 Address = add,
                                 Categories = cat,
                                 Specialisation = spec,
                                 WorkingHours = work,
                                 PaymentMode = pay,
                                 ListingViewCount = view
                             };

                return await result.ToListAsync();
            }
            else if (term == "PIN")
            {
                var result = from list in listingContext.Listing
                             join comm in listingContext.Communication
                             on list.ListingID equals comm.ListingID

                             join add in listingContext.Address
                             .Where(c => c.PincodeID == id)
                             on list.ListingID equals add.ListingID

                             join view in listingContext.ListingViewCount
                             on list.ListingID equals view.ListingID

                             join cat in listingContext.Categories
                             on list.ListingID equals cat.ListingID

                             join spec in listingContext.Specialisation
                                   on list.ListingID equals spec.ListingID

                             join work in listingContext.WorkingHours
                                   on list.ListingID equals work.ListingID

                             join pay in listingContext.PaymentMode
                                   on list.ListingID equals pay.ListingID

                             select new SearchListingViewModel
                             {
                                 Listing = list,
                                 Address = add,
                                 Categories = cat,
                                 Specialisation = spec,
                                 WorkingHours = work,
                                 PaymentMode = pay,
                                 ListingViewCount = view
                             };

                return await result.ToListAsync();
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
