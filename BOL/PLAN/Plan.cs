using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.PLAN
{
    [Table("Plan", Schema = "plan")]
    public class Plan
    {
        [Key]
        [Display(Name = "PlanID")]
        public int PlanID { get; set; }

        [Display(Name = "Plan Name", Prompt = "Free, Silver & Gold etc.")]
        [Required(ErrorMessage = "Plan Name Required")]
        public string Name { get; set; }

        [Display(Name = "Priority", Prompt = "0, 1, 2 etc.")]
        [Required(ErrorMessage = "Priority Required")]
        public int Priority { get; set; }

        [Display(Name = "Monthly Price", Prompt = "Rs.2400 Monthly")]
        [Required(ErrorMessage = "Monthly Price Required")]
        public int MonthlyPrice { get; set; }

        [Display(Name = "Categories", Prompt = "1,3 & 5 etc.")]
        [Required(ErrorMessage = "Categories Required")]
        public int Categories { get; set; }

        [Display(Name = "Offers", Prompt = "1,3 & 5 etc.")]
        [Required(ErrorMessage = "Offers Required")]
        public int Offers { get; set; }

        [Display(Name = "Products", Prompt = "10, 25, 50, 100 etc.")]
        [Required(ErrorMessage = "Products Required")]
        public int Products { get; set; }

        [Display(Name = "Job Postings", Prompt = "10, 25, 50, 100 etc.")]
        [Required(ErrorMessage = "Job Postings Required")]
        public int JobPostings { get; set; }

        [Display(Name = "Recent Projects", Prompt = "10, 25, 50, 100 etc.")]
        [Required(ErrorMessage = "Recent Projects Required")]
        public int RecentProjects { get; set; }

        [Display(Name = "City", Prompt = "1, 2, 3, 5etc.")]
        [Required(ErrorMessage = "City Required")]
        public int City { get; set; }

        [Display(Name = "Priority Listing", Prompt = "Enabled Or Disabled.")]
        public bool PriorityListing { get; set; }

        [Display(Name = "Sms Notifications", Prompt = "Enabled Or Disabled.")]
        public bool SmsNotifications { get; set; }

        [Display(Name = "Email Notifications", Prompt = "Enabled Or Disabled.")]
        public bool EmailNotifications { get; set; }

        [Display(Name = "User History", Prompt = "Enabled Or Disabled.")]
        public bool UserHistory { get; set; }

        [Display(Name = "Analytics", Prompt = "Enabled Or Disabled.")]
        public bool Analytics { get; set; }

        [Display(Name = "Membership Badge", Prompt = "Enabled Or Disabled.")]
        public bool MembershipBadge { get; set; }

        [Display(Name = "Photo Gallery", Prompt = "Enabled Or Disabled.")]
        public bool PhotoGallery { get; set; }

        [Display(Name = "Multiple Locations", Prompt = "Enabled Or Disabled.")]
        public bool MultipleLocations { get; set; }

        [Display(Name = "One Hour Service", Prompt = "Enabled Or Disabled.")]
        public bool OneHourService { get; set; }

        [Display(Name = "Team", Prompt = "Enabled Or Disabled.")]
        public bool Team { get; set; }

        [Display(Name = "Monopoly Products", Prompt = "Enabled Or Disabled.")]
        public bool MonopolyProducts { get; set; }

        [Display(Name = "Branches", Prompt = "Enabled Or Disabled.")]
        public bool Branches { get; set; }

        [Display(Name = "Client Logo", Prompt = "Enabled Or Disabled.")]
        public bool ClientLogo { get; set; }

        [Display(Name = "Partner Profile", Prompt = "Enabled Or Disabled.")]
        public bool PartnerProfile { get; set; }

        [Display(Name = "Messages Inbox", Prompt = "Enabled Or Disabled.")]
        public bool MessagesInbox { get; set; }

        [Display(Name = "Brand Showcase", Prompt = "Enabled Or Disabled.")]
        public bool BrandShowcase { get; set; }

        [Display(Name = "Preferred Places", Prompt = "Enabled Or Disabled.")]
        public bool PreferredPlaces { get; set; }

        [Display(Name = "Wallet", Prompt = "Enabled Or Disabled.")]
        public bool Wallet { get; set; }

        [Display(Name = "Working Hours", Prompt = "Enabled Or Disabled.")]
        public bool WorkingHours { get; set; }

        [Display(Name = "Payment Methods", Prompt = "Enabled Or Disabled.")]
        public bool PaymentMethods { get; set; }

        [Display(Name = "Custom Services", Prompt = "Enabled Or Disabled.")]
        public bool CustomServices { get; set; }

        [Display(Name = "Social Sites", Prompt = "Enabled Or Disabled.")]
        public bool SocialSites { get; set; }

        [Display(Name = "Certification", Prompt = "Enabled Or Disabled.")]
        public bool Certification { get; set; }

        [Display(Name = "Profile", Prompt = "Enabled Or Disabled.")]
        public bool Profile { get; set; }

        [Display(Name = "Specialisation", Prompt = "Enabled Or Disabled.")]
        public bool Specialisation { get; set; }

        [Display(Name = "Address", Prompt = "Enabled Or Disabled.")]
        public bool Address { get; set; }

        [Display(Name = "Multiple Assemblies", Prompt = "Enabled Or Disabled.")]
        public bool MultipleAssemblies { get; set; }

        [Display(Name = "Multiple Cities", Prompt = "Enabled Or Disabled.")]
        public bool MultipleCities { get; set; }

        [Display(Name = "Company", Prompt = "Enabled Or Disabled.")]
        public bool Company { get; set; }

        [Display(Name = "Communication", Prompt = "Enabled Or Disabled.")]
        public bool Communication { get; set; }

        [Display(Name = "Social Share Buttons", Prompt = "Enabled Or Disabled.")]
        public bool SocialShareButtons { get; set; }

        [Display(Name = "Reviews", Prompt = "Enabled Or Disabled.")]
        public bool Reviews { get; set; }

        [Display(Name = "Like & Dislike", Prompt = "Enabled Or Disabled.")]
        public bool LikeDislike { get; set; }

        [Display(Name = "Follow Listing", Prompt = "Enabled Or Disabled.")]
        public bool FollowListing { get; set; }

        [Display(Name = "CSS Class Name", Prompt = "primary, secondary etc.")]
        [Required(ErrorMessage = "CSSClass Required")]
        public string CSSClassName { get; set; }
    }
}
