using System.Collections.Generic;

namespace BLL_Opdracht_PR.Logic
{
    //Interface aanmaken voor DI te kunnen gebruiken
    public interface ITeBetalenLogic
    {
        decimal GetTotaleNietVerekendeUitgaven();

        Dictionary<string, decimal> GetTeBetalenPerGezin();

        void SetAllUitgavenToVerekend();

    }
}
