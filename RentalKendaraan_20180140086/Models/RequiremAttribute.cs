using System;

namespace RentalKendaraan_20180140086.Models
{
    internal class RequiremAttribute : Attribute
    {
        public string ErrorMessage { get; set; }
    }
}