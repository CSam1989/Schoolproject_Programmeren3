using System;

namespace BLL_Opdracht_PR.Helpers
{
    public static class Extentions
    {
        /// <summary>
        ///     Extention Method (DateTime)
        ///     Is een extentie op een class waar jezelf niet aankunt
        ///     wordt opgeroepen door een datetime variabele
        ///     (dit is bedoeld voor een leeftijd van een geboortedatum af te leiden)
        ///     Te gebruiken zoals => geboortedatum.age()
        /// </summary>

        #region DateTime Extentions
        public static int Age(this DateTime dateOfBirth)
        {
            if (DateTime.Now.Month < dateOfBirth.Month ||
                (DateTime.Now.Month == dateOfBirth.Month &&
                DateTime.Now.Day < dateOfBirth.Day))
            {
                return DateTime.Now.Year - dateOfBirth.Year - 1;
            }
            else
                return DateTime.Now.Year - dateOfBirth.Year;
        }

        //Unit Testing Method
        // Hier heb ik geopteerd om de methode te dupliceren, niet de ideale oplossing maar zo krijg ik wel een betrouwbare test
        public static int Age(this DateTime dateOfBirth, IDateTimeHelper dateTimeHelper = null)
        {
            dateTimeHelper = dateTimeHelper ?? new DateTimeHelper();

            var currentDate = dateTimeHelper.GetDateTimeNow();

            if (currentDate.Month < dateOfBirth.Month ||
                (currentDate.Month == dateOfBirth.Month &&
                currentDate.Day < dateOfBirth.Day))
            {
                return currentDate.Year - dateOfBirth.Year - 1;
            }
            else
                return currentDate.Year - dateOfBirth.Year;
        }
        #endregion
    }
}
