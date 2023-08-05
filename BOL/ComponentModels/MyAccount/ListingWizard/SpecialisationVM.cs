using BOL.LISTING;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOL.ComponentModels.MyAccount.ListingWizard
{
    public class SpecialisationVM
    {
        public bool AcceptTenderWork { get; set; } = false;
        public bool Bank { get; set; } = false;
        public bool BeautyParlors { get; set; } = false;
        public bool Bungalow { get; set; } = false;
        public bool CallCenter { get; set; } = false;
        public bool Church { get; set; } = false;
        public bool Company { get; set; } = false;
        public bool ComputerInstitute { get; set; } = false;
        public bool Dispensary { get; set; } = false;
        public bool ExhibitionStall { get; set; } = false;
        public bool Factory { get; set; } = false;
        public bool Farmhouse { get; set; } = false;
        public bool Gurudwara { get; set; } = false;
        public bool Gym { get; set; } = false;
        public bool HealthClub { get; set; } = false;
        public bool Home { get; set; } = false;
        public bool Hospital { get; set; } = false;
        public bool Hotel { get; set; } = false;
        public bool Laboratory { get; set; } = false;
        public bool Mandir { get; set; } = false;
        public bool Mosque { get; set; } = false;
        public bool Office { get; set; } = false;
        public bool Plazas { get; set; } = false;
        public bool ResidentialSociety { get; set; } = false;
        public bool Resorts { get; set; } = false;
        public bool Restaurants { get; set; } = false;
        public bool Saloons { get; set; } = false;
        public bool Shop { get; set; } = false;
        public bool ShoppingMall { get; set; } = false;
        public bool Showroom { get; set; } = false;
        public bool Warehouse { get; set; } = false;

        public void SetViewModel(Specialisation specialisation)
        {
            AcceptTenderWork = specialisation.AcceptTenderWork;
            Bank = specialisation.Banks;
            BeautyParlors = specialisation.BeautyParlors;
            Bungalow = specialisation.Bungalow;
            CallCenter = specialisation.CallCenter;
            Church = specialisation.Church;
            Company = specialisation.Company;
            ComputerInstitute = specialisation.ComputerInstitute;
            Dispensary = specialisation.Dispensary;
            ExhibitionStall = specialisation.ExhibitionStall;
            Factory = specialisation.Factory;
            Farmhouse = specialisation.Farmhouse;
            Gurudwara = specialisation.Gurudwara;
            Gym = specialisation.Gym;
            HealthClub = specialisation.HealthClub;
            Home = specialisation.Home;
            Hospital = specialisation.Hospital;
            Hotel = specialisation.Hotel;
            Laboratory = specialisation.Laboratory;
            Mandir = specialisation.Mandir;
            Mosque = specialisation.Mosque;
            Office = specialisation.Office;
            Plazas = specialisation.Plazas;
            ResidentialSociety = specialisation.ResidentialSociety;
            Resorts = specialisation.Resorts;
            Restaurants = specialisation.Restaurants;
            Saloons = specialisation.Salons;
            Shop = specialisation.Shop;
            ShoppingMall = specialisation.ShoppingMall;
            Showroom = specialisation.Showroom;
            Warehouse = specialisation.Warehouse;
        }

        public void SetContextModel(Specialisation specialisation)
        {
            specialisation.AcceptTenderWork = AcceptTenderWork;
            specialisation.Banks = Bank;
            specialisation.BeautyParlors = BeautyParlors;
            specialisation.Bungalow = Bungalow;
            specialisation.CallCenter = CallCenter;
            specialisation.Church = Church;
            specialisation.Company = Company;
            specialisation.ComputerInstitute = ComputerInstitute;
            specialisation.Dispensary = Dispensary;
            specialisation.ExhibitionStall = ExhibitionStall;
            specialisation.Factory = Factory;
            specialisation.Farmhouse = Farmhouse;
            specialisation.Gurudwara = Gurudwara;
            specialisation.Gym = Gym;
            specialisation.HealthClub = HealthClub;
            specialisation.Home = Home;
            specialisation.Hospital = Hospital;
            specialisation.Hotel = Hotel;
            specialisation.Laboratory = Laboratory;
            specialisation.Mandir = Mandir;
            specialisation.Mosque = Mosque;
            specialisation.Office = Office;
            specialisation.Plazas = Plazas;
            specialisation.ResidentialSociety = ResidentialSociety;
            specialisation.Resorts = Resorts;
            specialisation.Restaurants = Restaurants;
            specialisation.Salons = Saloons;
            specialisation.Shop = Shop;
            specialisation.ShoppingMall = ShoppingMall;
            specialisation.Showroom = Showroom;
            specialisation.Warehouse = Warehouse;
        }

        public void SelectOrUnselectAll(bool isSelect = true)
        {
            AcceptTenderWork = isSelect;
            Bank = isSelect;
            BeautyParlors = isSelect;
            Bungalow = isSelect;
            CallCenter = isSelect;
            Church = isSelect;
            Company = isSelect;
            ComputerInstitute = isSelect;
            Dispensary = isSelect;
            ExhibitionStall = isSelect;
            Factory = isSelect;
            Farmhouse = isSelect;
            Gurudwara = isSelect;
            Gym = isSelect;
            HealthClub = isSelect;
            Home = isSelect;
            Hospital = isSelect;
            Hotel = isSelect;
            Laboratory = isSelect;
            Mandir = isSelect;
            Mosque = isSelect;
            Office = isSelect;
            Plazas = isSelect;
            ResidentialSociety = isSelect;
            Resorts = isSelect;
            Restaurants = isSelect;
            Saloons = isSelect;
            Shop = isSelect;
            ShoppingMall = isSelect;
            Showroom = isSelect;
            Warehouse = isSelect;
        }

        public bool isValid()
        {
            return AcceptTenderWork|| Bank || BeautyParlors || Bungalow || CallCenter || Church || Company || ComputerInstitute ||
                Dispensary || ExhibitionStall || Factory|| Farmhouse|| Gurudwara|| Gym || HealthClub || Home|| Hospital ||
                Hotel || Laboratory || Mandir || Mosque || Office || Plazas || ResidentialSociety || Resorts || Restaurants ||
                Saloons || Shop || ShoppingMall || Showroom || Warehouse;
        }
    }    
}
