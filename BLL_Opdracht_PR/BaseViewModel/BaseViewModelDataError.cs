using GalaSoft.MvvmLight;
using System;
using System.ComponentModel;
using System.Reflection;

namespace BLL_Opdracht_PR.BaseViewModel
{
    public abstract class BaseViewModelDataError : ViewModelBase, IDataErrorInfo
    {
        /// <summary>
        ///     Misschien een beetje onnodig hier omdat Mvvm Light al een base viewmodel klas heeft
        ///     Maar stond in de opdracht ;-)
        ///     Niet helemaal onnodig omdat ik IDataErrorInfo nog ergens moest implementeren
        /// </summary>

        #region Properties
        public abstract string this[string columnName] { get; }

        public string Error
        {
            get
            {
                string result = "";
                string error = "";

                foreach (PropertyInfo prop in this.GetType().GetProperties())
                {
                    error = this[prop.Name];

                    if (!string.IsNullOrEmpty(error))
                    {
                        result = error + Environment.NewLine;
                    }
                }
                return result;
            }
        }
        #endregion

        #region Methods
        public bool IsValid()
        {
            if (string.IsNullOrEmpty(this.Error))
                return true;
            return false;

        }

        public abstract void LeegmakenTextBoxen();
        #endregion
    }
}
