using GalaSoft.MvvmLight.Views;
using System.Collections.Generic;

namespace BLL_Opdracht_PR.Services
{
    // Interface aanmaken voor DI te kunnen gebruiken
    // Deze erft over van interface NavigationService (van Mvvm Light)
    public interface IFrameNavigationService : INavigationService
    {
        object Parameter { get; }

        List<string> Historic { get; }
    }
}
