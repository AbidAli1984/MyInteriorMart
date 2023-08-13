using AntDesign;
using BAL.Services.Contracts;
using BOL.ComponentModels.MyAccount.ListingWizard;
using BOL.SHARED;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace FRONTEND.BLAZOR.Modal
{
    public partial class AddLocalityModal
    {
        [Parameter] public LWAddressVM ListWizAddressVM { get; set; }
        [Parameter] public EventCallback EventGetLocalitiesByCityId { get; set; }
        [Inject] private ISharedService sharedService { get; set; }
        [Inject] Helper helper { get; set; }

        public bool showLocalityModal { get; set; } = false;
        public string LocalityName { get; set; }

        public async Task HideLocalityModal()
        {
            showLocalityModal = false;
            await Task.Delay(5);
        }

        public async Task ShowLocalityModal()
        {
            if (ListWizAddressVM.CityId <= 0)
            {
                helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", $"Please select Country, State and City first");
                return;
            }
            showLocalityModal = true;
            await Task.Delay(5);
        }

        public async Task CreateLocalityAsync()
        {
            if (string.IsNullOrEmpty(LocalityName) || ListWizAddressVM.CityId <= 0)
            {
                helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", $"Country, State and City must be selected and locality name must not be blank.");
                return;
            }
            try
            {
                var localityExist = await sharedService.GetLocalityByLocalityName(LocalityName);
                if (localityExist != null)
                {
                    helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", $"Locality {LocalityName} already exists.");
                    return;
                }

                Location locality = new Location
                {
                    CityID = ListWizAddressVM.CityId,
                    Name = LocalityName
                };

                await sharedService.AddAsync(locality);
                var city = await sharedService.GetCityByCityId(ListWizAddressVM.CityId);

                if (EventGetLocalitiesByCityId.HasDelegate)
                    await EventGetLocalitiesByCityId.InvokeAsync();
                
                await HideLocalityModal();
                helper.ShowNotification(_notice, NotificationType.Success, NotificationPlacement.BottomRight, "Success", $"Locality {LocalityName} created inside city {city.Name}.");
                LocalityName = string.Empty;
            }
            catch (Exception exc)
            {
                helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", exc.Message);
            }
        }
    }
}
