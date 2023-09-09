using AntDesign;
using BAL.Services.Contracts;
using BOL.ComponentModels.MyAccount.ListingWizard;
using BOL.SHARED;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace FRONTEND.BLAZOR.Modal
{
    public partial class AddPincodeModal
    {
        [Parameter] public LWAddressVM ListWizAddressVM { get; set; }
        [Parameter] public EventCallback EventGetPincodesByAreaId { get; set; }
        [Inject] private ISharedService sharedService { get; set; }
        [Inject] Helper helper { get; set; }
        public bool showPincodeModal { get; set; } = false;
        public int PinNumber { get; set; }

        public async Task HidePincodeModal()
        {
            showPincodeModal = false;
            await Task.Delay(5);
        }

        public async Task ShowPincodeModal()
        {
            if (ListWizAddressVM.LocalityId <= 0)
            {
                helper.ShowNotification(_notice, $"Pleae select Country, State, City and Locality first.", NotificationType.Info);
                return;
            }
            showPincodeModal = true;
            await Task.Delay(5);
        }

        public async Task CreatePincodeAsync()
        {
            if (PinNumber <= 0 || ListWizAddressVM.LocalityId <= 0)
            {
                helper.ShowNotification(_notice, $"Country, State, City and Area must be selected and pincode number must not be blank.", NotificationType.Info);
                return;
            }
            try
            {
                var pincodeExist = await sharedService.GetPincodeByPinNumber(PinNumber);
                if (pincodeExist != null)
                {
                    helper.ShowNotification(_notice, $"Pincode {PinNumber} already exists.", NotificationType.Info);
                    return;
                }

                Pincode pincode = new Pincode
                {
                    LocationId = ListWizAddressVM.LocalityId,
                    PincodeNumber = PinNumber
                };

                await sharedService.AddAsync(pincode);
                var station = await sharedService.GetLocalityByLocalityId(ListWizAddressVM.LocalityId);

                if (EventGetPincodesByAreaId.HasDelegate)
                    await EventGetPincodesByAreaId.InvokeAsync();

                await HidePincodeModal();
                helper.ShowNotification(_notice, $"Pincode {PinNumber} created inside {station.Name}.");
                PinNumber = 0;
            }
            catch (Exception exc)
            {
                helper.ShowNotification(_notice, exc.Message, NotificationType.Error);
            }
        }
    }
}
