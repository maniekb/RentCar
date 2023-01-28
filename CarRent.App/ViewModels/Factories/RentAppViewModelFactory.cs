using CarRent.App.Navigators;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRent.App.ViewModels.Factories
{
    public class RentAppViewModelFactory : IRentAppViewModelFactory
    {
        private readonly CreateViewModel<CarOfferViewModel> _createOfferViewModel;

        public RentAppViewModelFactory(CreateViewModel<CarOfferViewModel> createOfferViewModel)
        {
            _createOfferViewModel = createOfferViewModel;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.USER_OFFER:
                    return _createOfferViewModel();
                default:
                    throw new ArgumentException("The ViewType does not have a ViewModel.", "viewType");
            }
        }
    }
}
