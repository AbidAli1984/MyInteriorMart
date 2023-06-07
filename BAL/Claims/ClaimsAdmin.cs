using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BOL.CLAIMS.Admin;
using Microsoft.AspNetCore.Identity;

namespace BAL.Claims
{
    public class ClaimsAdmin : IClaimsAdmin
    {
        private readonly RoleManager<IdentityRole> roleManager;
        public ClaimsAdmin(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public string ExcepctionMsg { get; set; }
        public IList<AdminClaim.Dashboard> Dashboard()
        {
            IList<AdminClaim.Dashboard> dashboard = new List<AdminClaim.Dashboard> {
                new AdminClaim.Dashboard {
                    ID = 1,
                    Name = "Realtime",
                    Type = "Admin.Dashboard",
                    View = "Admin.Dashboard.Realtime.View"
                },
                new AdminClaim.Dashboard {
                    ID = 2,
                    Name = "Listings",
                    Type = "Admin.Dashboard",
                    View = "Admin.Dashboard.Listings.View"
                },
                new AdminClaim.Dashboard {
                    ID = 3,
                    Name = "Users",
                    Type = "Admin.Dashboard",
                    View = "Admin.Dashboard.Users.View"
                },
                new AdminClaim.Dashboard {
                    ID = 4,
                    Name = "Analytics",
                    Type = "Admin.Dashboard",
                    View = "Admin.Dashboard.Analytics.View"
                },
                new AdminClaim.Dashboard {
                    ID = 5, Name = "Notifications",
                    Type = "Admin.Dashboard",
                    View = "Admin.Dashboard.Notifications.View"
                },
                new AdminClaim.Dashboard {
                    ID = 6,
                    Name = "Search",
                    Type = "Admin.Dashboard",
                    View = "Admin.Dashboard.Search.View"
                },
                new AdminClaim.Dashboard {
                    ID = 7,
                    Name = "Enquiries",
                    Type = "Admin.Dashboard",
                    View = "Admin.Dashboard.Enquiries.View"
                },
                new AdminClaim.Dashboard {
                    ID = 8,
                    Name = "Marketing",
                    Type = "Admin.Dashboard",
                    View = "Admin.Dashboard.Marketing.View"
                },
                new AdminClaim.Dashboard {
                    ID = 9,
                    Name = "Billing",
                    Type = "Admin.Dashboard",
                    View = "Admin.Dashboard.Billing.View"
                },
                new AdminClaim.Dashboard {
                    ID = 10,
                    Name = "Staff",
                    Type = "Admin.Dashboard",
                    View = "Admin.Dashboard.Staff.View"
                }
            };

            return dashboard.OrderBy(i => i.ID).ToList();
        }

        public IList<AdminClaim.UserManager> UserManager()
        {
            IList<AdminClaim.UserManager> userManager = new List<AdminClaim.UserManager> {
                // Shafi: View all users and their profile
                new AdminClaim.UserManager {
                    ID = 1,
                    Name = "Users",
                    Type = "Admin.Users",
                    ViewAll = "Admin.Users.ViewAll",
                    ViewProfile = "Admin.Users.ViewProfile"
                },
                // Shafi: Manage roles
                new AdminClaim.UserManager {
                    ID = 2,
                    Name = "Roles",
                    Type = "Admin.Users",
                    ViewAll = "Admin.Roles.ViewAll",
                    Read = "Admin.Roles.Read",
                    Edit = "Admin.Roles.Edit",
                    Delete = "Admin.Roles.Delete",
                    Create = "Admin.Roles.Create",
                    AssignRoleToUser = "Admin.Roles.AssignRoleToUser",
                    RemoveUserFromRole = "Admin.Roles.RemoveUserFromRole",
                    ListUsersInRole = "Admin.Roles.ListUsersInRole",
                    AssignClaimToRole = "Admin.Roles.AssignClaimToRole"
                },
                // Shafi: Manage role categories
                new AdminClaim.UserManager {
                    ID = 3,
                    Name = "Role Categories",
                    Type = "Admin.Users",
                    ViewAll = "Admin.RoleCategories.ViewAll",
                    Read = "Admin.RoleCategories.Read",
                    Edit = "Admin.RoleCategories.Edit",
                    Delete = "Admin.RoleCategories.Delete",
                    Create = "Admin.RoleCategories.Create",
                },
                // Shafi: Block Users
                new AdminClaim.UserManager {
                    ID = 4,
                    Name = "Block Users",
                    Type = "Admin.Users",
                    BlockUser = "Admin.Users.BlockUser",
                    ViewAll = "Admin.Users.ViewAllBlockedUser"
                },
                // Shafi: Unblock Users
                new AdminClaim.UserManager {
                    ID = 5,
                    Name = "Unblock Users",
                    Type = "Admin.Users",
                    UnblockUser = "Admin.Users.UnblockUser",
                    ViewAll = "Admin.Users.ViewAllUnblockedUser"
                },
                // Shafi: Manage Role Categories & Roles
                new AdminClaim.UserManager {
                    ID = 5,
                    Name = "Role Categories And Roles",
                    Type = "Admin.Users",
                    ViewAll = "Admin.RoleCatNRole.ViewAll",
                    Read = "Admin.RoleCatNRole.Read",
                    Create = "Admin.RoleCatNRole.Create",
                    Edit = "Admin.RoleCatNRole.Edit",
                    Delete = "Admin.RoleCatNRole.Delete"
                }
            };

            return userManager.OrderBy(i => i.ID).ToList();
        }

        public IList<AdminClaim.Listing> Listing()
        {

            IList<AdminClaim.Listing> listing = new List<AdminClaim.Listing>
            {
            // Shafi: Listing
            new AdminClaim.Listing
            {
                ID = 1,
                Name = "Listings",
                Type = "Admin.Listings",
                ViewAll = "Admin.Listing.ViewAll",
                Read = "Admin.Listing.Read",
                Create = "Admin.Listing.Create",
                Edit = "Admin.Listing.Edit",
                Delete = "Admin.Listing.Delete",
                Approve = "Admin.Listing.Approve"
            },
            // Shafi: Review
            new AdminClaim.Listing
            {
                ID = 2,
                Name = "Review",
                Type = "Admin.Listings",
                ViewAll = "Admin.Review.ViewAll",
                Read = "Admin.Review.Read",
                Create = "Admin.Review.Create",
                Edit = "Admin.Review.Edit",
                Delete = "Admin.Review.Delete",
                Approve = "Admin.Review.Approve"
            },
            // Shafi: Likes
            new AdminClaim.Listing
            {
                ID = 3,
                Name = "Like",
                Type = "Admin.Listings",
                ViewAll = "Admin.Like.ViewAll",
                Read = "Admin.Like.Read",
                Create = "Admin.Like.Create",
                Edit = "Admin.Like.Edit",
                Delete = "Admin.Like.Delete",
                Approve = "Admin.Like.Approve"
            },
            // Shafi: Subscribe
            new AdminClaim.Listing
            {
                ID = 3,
                Name = "Subscribe",
                Type = "Admin.Listings",
                ViewAll = "Admin.Subscribe.ViewAll",
                Read = "Admin.Subscribe.Read",
                Create = "Admin.Subscribe.Create",
                Edit = "Admin.Subscribe.Edit",
                Delete = "Admin.Subscribe.Delete",
                Approve = "Admin.Subscribe.Approve"
            },
            // Shafi: Bookmark
            new AdminClaim.Listing
            {
                ID = 4,
                Name = "Bookmark",
                Type = "Admin.Listings",
                ViewAll = "Admin.Bookmark.ViewAll",
                Read = "Admin.Bookmark.Read",
                Create = "Admin.Bookmark.Create",
                Edit = "Admin.Bookmark.Edit",
                Delete = "Admin.Bookmark.Delete",
                Approve = "Admin.Bookmark.Approve"
            }
            };

            return listing.OrderBy(i => i.ID).ToList();
        }

        public IList<AdminClaim.Localities> Locality()
        {
            IList<AdminClaim.Localities> locality = new List<AdminClaim.Localities> {
                // Shafi: Country
                new AdminClaim.Localities {
                    ID = 1,
                    Name = "Country",
                    Type = "Admin.Locality",
                    ViewAll = "Admin.Country.ViewAll",
                    Read = "Admin.Country.Read",
                    Create = "Admin.Country.Create",
                    Edit = "Admin.Country.Edit",
                    Delete = "Admin.Country.Delete"
                },
                // Shafi: State
                new AdminClaim.Localities {
                    ID = 2,
                    Name = "State",
                    Type = "Admin.Locality",
                    ViewAll = "Admin.State.ViewAll",
                    Read = "Admin.State.Read",
                    Create = "Admin.State.Create",
                    Edit = "Admin.State.Edit",
                    Delete = "Admin.State.Delete"
                },
                // Shafi: City
                new AdminClaim.Localities {
                    ID = 3,
                    Name = "City",
                    Type = "Admin.Locality",
                    ViewAll = "Admin.City.ViewAll",
                    Read = "Admin.City.Read",
                    Create = "Admin.City.Create",
                    Edit = "Admin.City.Edit",
                    Delete = "Admin.City.Delete"
                },
                // Shafi: Assembly
                new AdminClaim.Localities {
                    ID = 4,
                    Name = "Assembly",
                    Type = "Admin.Locality",
                    ViewAll = "Admin.Assembly.ViewAll",
                    Read = "Admin.Assembly.Read",
                    Create = "Admin.Assembly.Create",
                    Edit = "Admin.Assembly.Edit",
                    Delete = "Admin.Assembly.Delete"
                },
                // Shafi: Pincode
                new AdminClaim.Localities {
                    ID = 5,
                    Name = "Pincode",
                    Type = "Admin.Locality",
                    ViewAll = "Admin.Pincode.ViewAll",
                    Read = "Admin.Pincode.Read",
                    Create = "Admin.Pincode.Create",
                    Edit = "Admin.Pincode.Edit",
                    Delete = "Admin.Pincode.Delete"
                },
                // Shafi: Area
                new AdminClaim.Localities {
                    ID = 6,
                    Name = "Area",
                    Type = "Admin.Locality",
                    ViewAll = "Admin.Area.ViewAll",
                    Read = "Admin.Area.Read",
                    Create = "Admin.Area.Create",
                    Edit = "Admin.Area.Edit",
                    Delete = "Admin.Area.Delete"
                }
            };

            return locality.OrderBy(i => i.ID).ToList();
        }

        public IList<AdminClaim.Categories> Category()
        {
            IList<AdminClaim.Categories> category = new List<AdminClaim.Categories> {
                // Shafi: First
                new AdminClaim.Categories {
                    ID = 1,
                    Name = "First Category",
                    Type = "Admin.Categories",
                    ViewAll = "Admin.FirstCategory.ViewAll",
                    Read = "Admin.FirstCategory.Read",
                    Create = "Admin.FirstCategory.Create",
                    Edit = "Admin.FirstCategory.Edit",
                    Delete = "Admin.FirstCategory.Delete"
                },
                // Shafi: Second
                new AdminClaim.Categories {
                    ID = 2,
                    Name = "Second Category",
                    Type = "Admin.Categories",
                    ViewAll = "Admin.SecondCategory.ViewAll",
                    Read = "Admin.SecondCategory.Read",
                    Create = "Admin.SecondCategory.Create",
                    Edit = "Admin.SecondCategory.Edit",
                    Delete = "Admin.SecondCategory.Delete"
                },
                // Shafi: Third
                new AdminClaim.Categories {
                    ID = 3,
                    Name = "Third Category",
                    Type = "Admin.Categories",
                    ViewAll = "Admin.ThirdCategory.ViewAll",
                    Read = "Admin.ThirdCategory.Read",
                    Create = "Admin.ThirdCategory.Create",
                    Edit = "Admin.ThirdCategory.Edit",
                    Delete = "Admin.ThirdCategory.Delete"
                },
                // Shafi: Fourth
                new AdminClaim.Categories {
                    ID = 4,
                    Name = "Fourth Category",
                    Type = "Admin.Categories",
                    ViewAll = "Admin.FourthCategory.ViewAll",
                    Read = "Admin.FourthCategory.Read",
                    Create = "Admin.FourthCategory.Create",
                    Edit = "Admin.FourthCategory.Edit",
                    Delete = "Admin.FourthCategory.Delete"
                },
                // Shafi: Fifth
                new AdminClaim.Categories {
                    ID = 5,
                    Name = "Fifth Category",
                    Type = "Admin.Categories",
                    ViewAll = "Admin.FifthCategory.ViewAll",
                    Read = "Admin.FifthCategory.Read",
                    Create = "Admin.FifthCategory.Create",
                    Edit = "Admin.FifthCategory.Edit",
                    Delete = "Admin.FifthCategory.Delete"
                },
                // Shafi: Sixth
                new AdminClaim.Categories {
                    ID = 6,
                    Name = "Sixth Category",
                    Type = "Admin.Categories",
                    ViewAll = "Admin.SixthCategory.ViewAll",
                    Read = "Admin.SixthCategory.Read",
                    Create = "Admin.SixthCategory.Create",
                    Edit = "Admin.SixthCategory.Edit",
                    Delete = "Admin.SixthCategory.Delete"
                }
            };

            return category.OrderBy(i => i.ID).ToList();
        }

        public IList<AdminClaim.Miscellaneous> Miscellaneous()
        {
            IList<AdminClaim.Miscellaneous> miscellaneous = new List<AdminClaim.Miscellaneous> {
                // Shafi: First
                new AdminClaim.Miscellaneous {
                    ID = 1,
                    Name = "Designation",
                    Type = "Admin.Miscellaneous",
                    ViewAll = "Admin.Designation.ViewAll",
                    Read = "Admin.Designation.Read",
                    Create = "Admin.Designation.Create",
                    Edit = "Admin.Designation.Edit",
                    Delete = "Admin.Designation.Delete"
                },
                // Shafi: Second
                new AdminClaim.Miscellaneous {
                    ID = 2,
                    Name = "Nature of Business",
                    Type = "Admin.Miscellaneous",
                    ViewAll = "Admin.NatureofBusiness.ViewAll",
                    Read = "Admin.NatureofBusiness.Read",
                    Create = "Admin.NatureofBusiness.Create",
                    Edit = "Admin.NatureofBusiness.Edit",
                    Delete = "Admin.NatureofBusiness.Delete"
                },
                // Shafi: Third
                new AdminClaim.Miscellaneous {
                    ID = 3,
                    Name = "Turnover",
                    Type = "Admin.Miscellaneous",
                    ViewAll = "Admin.Turnover.ViewAll",
                    Read = "Admin.Turnover.Read",
                    Create = "Admin.Turnover.Create",
                    Edit = "Admin.Turnover.Edit",
                    Delete = "Admin.Turnover.Delete"
                }
            };

            return miscellaneous.OrderBy(i => i.ID).ToList();
        }

        public IList<AdminClaim.Keywords> Keywords()
        {
            IList<AdminClaim.Keywords> keywords = new List<AdminClaim.Keywords> {
                // Shafi: First
                new AdminClaim.Keywords {
                    ID = 1,
                    Name = "First Category Keyword",
                    Type = "Admin.Keywords",
                    ViewAll = "Admin.FirstCategoryKeyword.ViewAll",
                    Read = "Admin.FirstCategoryKeyword.Read",
                    Create = "Admin.FirstCategoryKeyword.Create",
                    Edit = "Admin.FirstCategoryKeyword.Edit",
                    Delete = "Admin.FirstCategoryKeyword.Delete"
                },
                // Shafi: Second
                new AdminClaim.Keywords {
                    ID = 2,
                    Name = "Second Category Keyword",
                    Type = "Admin.Keywords",
                    ViewAll = "Admin.SecondCategoryKeyword.ViewAll",
                    Read = "Admin.SecondCategoryKeyword.Read",
                    Create = "Admin.SecondCategoryKeyword.Create",
                    Edit = "Admin.SecondCategoryKeyword.Edit",
                    Delete = "Admin.SecondCategoryKeyword.Delete"
                },
                // Shafi: Third
                new AdminClaim.Keywords {
                    ID = 3,
                    Name = "Third Category Keyword",
                    Type = "Admin.Keywords",
                    ViewAll = "Admin.ThirdCategoryKeyword.ViewAll",
                    Read = "Admin.ThirdCategoryKeyword.Read",
                    Create = "Admin.ThirdCategoryKeyword.Create",
                    Edit = "Admin.ThirdCategoryKeyword.Edit",
                    Delete = "Admin.ThirdCategoryKeyword.Delete"
                },
                // Shafi: Fourth
                new AdminClaim.Keywords {
                    ID = 4,
                    Name = "Fourth Category Keyword",
                    Type = "Admin.Keywords",
                    ViewAll = "Admin.FourthCategoryKeyword.ViewAll",
                    Read = "Admin.FourthCategoryKeyword.Read",
                    Create = "Admin.FourthCategoryKeyword.Create",
                    Edit = "Admin.FourthCategoryKeyword.Edit",
                    Delete = "Admin.FourthCategoryKeyword.Delete"
                },
                // Shafi: Fifth
                new AdminClaim.Keywords {
                    ID = 5,
                    Name = "Fifth Category Keyword",
                    Type = "Admin.Keywords",
                    ViewAll = "Admin.FifthCategoryKeyword.ViewAll",
                    Read = "Admin.FifthCategoryKeyword.Read",
                    Create = "Admin.FifthCategoryKeyword.Create",
                    Edit = "Admin.FifthCategoryKeyword.Edit",
                    Delete = "Admin.FifthCategoryKeyword.Delete"
                },
                // Shafi: Fifth
                new AdminClaim.Keywords {
                    ID = 6,
                    Name = "Sixth Category Keyword",
                    Type = "Admin.Keywords",
                    ViewAll = "Admin.SixthCategoryKeyword.ViewAll",
                    Read = "Admin.SixthCategoryKeyword.Read",
                    Create = "Admin.SixthCategoryKeyword.Create",
                    Edit = "Admin.SixthCategoryKeyword.Edit",
                    Delete = "Admin.SixthCategoryKeyword.Delete"
                }
            };

            return keywords.OrderBy(i => i.ID).ToList();
        }

        public IList<AdminClaim.Pages> Pages()
        {
            IList<AdminClaim.Pages> pages = new List<AdminClaim.Pages> {
                // Shafi: First
                new AdminClaim.Pages {
                    ID = 1,
                    Name = "Page",
                    Type = "Admin.Pages",
                    ViewAll = "Admin.Page.ViewAll",
                    Read = "Admin.Page.Read",
                    Create = "Admin.Page.Create",
                    Edit = "Admin.Page.Edit",
                    Delete = "Admin.Page.Delete"
                }
            };

            return pages.OrderBy(i => i.ID).ToList();
        }

        public IList<AdminClaim.Notifications> Notifications()
        {
            IList<AdminClaim.Notifications> notifications = new List<AdminClaim.Notifications> {
                // Shafi: First
                new AdminClaim.Notifications {
                    ID = 1,
                    Name = "Notifications",
                    Type = "Admin.Notifications",
                    ViewAll = "Admin.Notification.ViewAll",
                    Read = "Admin.Notification.Read",
                    Create = "Admin.Notification.Create",
                    Edit = "Admin.Notification.Edit",
                    Delete = "Admin.Notification.Delete",
                    ReceiveSMS = "Admin.Notification.ReceiveSMS",
                    ReceiveEmail = "Admin.Notification.ReceiveEmail"
                }
            };

            return notifications.OrderBy(i => i.ID).ToList();
        }

        public IList<AdminClaim.Slideshow> Slideshow()
        {
            IList<AdminClaim.Slideshow> slideshow = new List<AdminClaim.Slideshow> {
                // Shafi: First
                new AdminClaim.Slideshow {
                    ID = 1,
                    Name = "Slideshow",
                    Type = "Admin.Slideshow",
                    ViewAll = "Admin.Slideshow.ViewAll",
                    Read = "Admin.Slideshow.Read",
                    Create = "Admin.Slideshow.Create",
                    Edit = "Admin.Slideshow.Edit",
                    Delete = "Admin.Slideshow.Delete"
                }
            };

            return slideshow.OrderBy(i => i.ID).ToList();
        }

        public IList<AdminClaim.HistoryAndCache> HistoryAndCache()
        {
            IList<AdminClaim.HistoryAndCache> historyAndCache = new List<AdminClaim.HistoryAndCache> {
                // Shafi: First
                new AdminClaim.HistoryAndCache {
                    ID = 1,
                    Name = "User History",
                    Type = "Admin.HistoryAndCache",
                    ViewAll = "Admin.UserHistory.ViewAll",
                    Read = "Admin.UserHistory.Read",
                    Create = "Admin.UserHistory.Create",
                    Edit = "Admin.UserHistory.Edit",
                    Delete = "Admin.UserHistory.Delete"
                },
                new AdminClaim.HistoryAndCache {
                    ID = 2,
                    Name = "Listing History",
                    Type = "Admin.HistoryAndCache",
                    ViewAll = "Admin.ListingHistory.ViewAll",
                    Read = "Admin.ListingHistory.Read",
                    Create = "Admin.ListingHistory.Create",
                    Edit = "Admin.ListingHistory.Edit",
                    Delete = "Admin.ListingHistory.Delete"
                },
                new AdminClaim.HistoryAndCache {
                    ID = 3,
                    Name = "Cache Server",
                    Type = "Admin.HistoryAndCache",
                    ViewAll = "Admin.CacheServer.ViewAll",
                    ClearCache = "Admin.CacheServer.Read"
                },
                new AdminClaim.HistoryAndCache {
                    ID = 4,
                    Name = "Suggestions",
                    Type = "Admin.HistoryAndCache",
                    ViewAll = "Admin.Suggestion.ViewAll",
                    Read = "Admin.Suggestion.Read",
                    Create = "Admin.Suggestion.Create",
                    Edit = "Admin.Suggestion.Edit",
                    Delete = "Admin.Suggestion.Delete",
                    DeleteList = "Admin.Suggestion.DeleteList",
                    ClearCache = "Admin.Suggestion.ClearCache",
                    ViewCache = "Admin.Suggestion.ViewCache"
                }
            };

            return historyAndCache.OrderBy(i => i.ID).ToList();
        }

        // Shafi: Get claim by name
        public AdminClaim.Dashboard claimDashboard(string name)
        {
            return this.Dashboard().Where(i => i.Name == name).FirstOrDefault();
        }
        public AdminClaim.UserManager claimUser(string name)
        {
            return this.UserManager().Where(i => i.Name == name).FirstOrDefault();
        }

        public AdminClaim.Listing claimListing(string name)
        {
            return this.Listing().Where(i => i.Name == name).FirstOrDefault();
        }

        public AdminClaim.Localities claimLocality(string name)
        {
            return this.Locality().Where(i => i.Name == name).FirstOrDefault();
        }

        public AdminClaim.Categories claimCategory(string name)
        {
            return this.Category().Where(i => i.Name == name).FirstOrDefault();
        }

        public AdminClaim.Miscellaneous claimMiscellaneous(string name)
        {
            return this.Miscellaneous().Where(i => i.Name == name).FirstOrDefault();
        }

        public AdminClaim.Keywords claimKeyword(string name)
        {
            return this.Keywords().Where(i => i.Name == name).FirstOrDefault();
        }

        public AdminClaim.Pages claimPage(string name)
        {
            return this.Pages().Where(i => i.Name == name).FirstOrDefault();
        }

        public AdminClaim.Notifications claimNotification(string name)
        {
            return this.Notifications().Where(i => i.Name == name).FirstOrDefault();
        }

        public AdminClaim.Slideshow claimSlideshow(string name)
        {
            return this.Slideshow().Where(i => i.Name == name).FirstOrDefault();
        }

        public AdminClaim.HistoryAndCache claimHistoryAndCache(string name)
        {
            return this.HistoryAndCache().Where(i => i.Name == name).FirstOrDefault();
        }
        // End:

        // Shafi: Check if role has claim
        public async Task<bool> CheckIfRoleHasClaim(string roleId, string claimType, string claim)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            var getAllClaimsForRole = await roleManager.GetClaimsAsync(role);
            var roleHasClaim = getAllClaimsForRole.Where(i => i.Type == claimType && i.Value == claim).FirstOrDefault();
            if (roleHasClaim != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // End:

        // Shafi: Assign Claims To Role
        public async Task AssignClaimsToRole(string roleId, string claimType, string claimList)
        {
            try
            {
                // Shafi: If claim value is not equal to null execute this
                if (claimList != null)
                {
                    try
                    {
                        // Shafi: Convert comma seprated string to string[] array
                        string[] newClaims = claimList.Split(',');
                        // End:

                        // Shafi: Get role
                        var role = await roleManager.FindByIdAsync(roleId);
                        // End:

                        // Shafi: Get all claims for role
                        var getAllClaimsForRole = await roleManager.GetClaimsAsync(role);
                        // End:

                        // Shafi: Get all claims where claim type == claimType
                        var existingClaimForClaimType = getAllClaimsForRole.Where(i => i.Type == claimType).ToList();
                        // End:

                        // Shafi: Create claims from existingClaimList
                        List<Claim> existingClaimsList = new List<Claim>();
                        foreach (var item in existingClaimForClaimType)
                        {
                            Claim claim = new Claim(item.Type, item.Value);
                            existingClaimsList.Add(claim);
                        };
                        // End:

                        // Shafi: Remove existingClaimsList from role
                        foreach (var claim in existingClaimsList)
                        {
                            await roleManager.RemoveClaimAsync(role, claim);
                        }
                        // End:

                        // Shafi: Create claims for newClaimList
                        List<Claim> newClaimsList = new List<Claim>();
                        foreach (var item in newClaims)
                        {
                            Claim claim = new Claim(claimType, item);
                            newClaimsList.Add(claim);
                        }
                        // End:

                        // Shafi: Add newClaimsList to role
                        foreach (var claim in newClaimsList)
                        {
                            await roleManager.AddClaimAsync(role, claim);
                        }
                        // End:
                    }
                    catch (Exception exc)
                    {
                        ExcepctionMsg = exc.Message;
                    }
                }
                // Shafi: If claim value is equal to null execute this
                else 
                {
                    try
                    {
                        // Shafi: Get role
                        var role = await roleManager.FindByIdAsync(roleId);
                        // End:

                        // Shafi: Get all claims for role
                        var getAllClaimsForRole = await roleManager.GetClaimsAsync(role);
                        // End:

                        // Shafi: Get all claims where claim type == claimType
                        var existingClaimForClaimType = getAllClaimsForRole.Where(i => i.Type == claimType).ToList();
                        // End:

                        // Shafi: Create claims from existingClaimList
                        List<Claim> existingClaimsList = new List<Claim>();
                        foreach (var item in existingClaimForClaimType)
                        {
                            Claim claim = new Claim(item.Type, item.Value);
                            existingClaimsList.Add(claim);
                        };
                        // End:

                        // Shafi: Remove existingClaimsList from role
                        foreach (var claim in existingClaimsList)
                        {
                            await roleManager.RemoveClaimAsync(role, claim);
                        }
                        // End:
                    }
                    catch (Exception exc)
                    {
                        ExcepctionMsg = exc.Message;
                    }
                }
            }
            catch(Exception exc)
            {
                ExcepctionMsg = exc.Message;
            }
        }
        // End:
    }
}
