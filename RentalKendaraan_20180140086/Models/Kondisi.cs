using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentalKendaraan_20180140086.Models
{
    public partial class Kondisi
    {
        public int IdKondisi { get; set; }

        [Required(ErrorMessage = "Nama tidak boleh kosong")]
        public string NamaKondisi { get; set; }
    }
}
