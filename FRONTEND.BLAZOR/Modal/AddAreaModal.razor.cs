using AntDesign;
using BAL.Services.Contracts;
using BOL.ComponentModels.MyAccount.ListingWizard;
using BOL.SHARED;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace FRONTEND.BLAZOR.Modal
{
    public partial class AddAreaModal
    {
        [Parameter] public LWAddressVM ListWizAddressVM { get; set; }
        [Parameter] public EventCallback EventGetLocalitiesByPincodeId { get; set; }
        [Inject] private ISharedService sharedService { get; set; }
        [Inject] Helper helper { get; set; }

        public bool showAreaModal { get; set; } = false;
        public string AreaName { get; set; }

        public async Task HidePincodeModal()
        {
            showAreaModal = false;
            await Task.Delay(5);
        }

        public async Task ShowPincodeModal()
        {
            if (ListWizAddressVM.PincodeId <= 0)
            {
                helper.ShowNotification(_notice, $"Pleae select Country, State, City, Locality and Pincode first.", NotificationType.Info);
                return;
            }
            showAreaModal = true;
            await Task.Delay(5);
        }

        public async Task CreateAreaAsync()
        {
            if (string.IsNullOrEmpty(AreaName) || ListWizAddressVM.PincodeId <= 0)
            {
                helper.ShowNotification(_notice, $"Country, State, City, Area and Pincode must be selected and Area Name must not be blank.", NotificationType.Info);
                return;
            }
            try
            {
                var areaExist = await sharedService.GetAreaByAreaName(AreaName);
                if (areaExist != null)
                {
                    helper.ShowNotification(_notice, $"Area {AreaName} already exists.", NotificationType.Info);
                    return;
                }

                Area area = new Area
                {
                    Name = AreaName,
                    PincodeID = ListWizAddressVM.PincodeId,
                    LocationId = ListWizAddressVM.LocalityId
                };

                await sharedService.AddAsync(area);
                var pincode = await sharedService.GetPincodeByPincodeId(ListWizAddressVM.PincodeId);

                if (EventGetLocalitiesByPincodeId.HasDelegate)
                    await EventGetLocalitiesByPincodeId.InvokeAsync();

                await HidePincodeModal();
                helper.ShowNotification(_notice, $"Area {AreaName} created inside pincode {pincode.PincodeNumber}.");
                AreaName = string.Empty;
            }
            catch (Exception exc)
            {
                helper.ShowNotification(_notice, exc.Message, NotificationType.Error);
            }
        }
    }
}
