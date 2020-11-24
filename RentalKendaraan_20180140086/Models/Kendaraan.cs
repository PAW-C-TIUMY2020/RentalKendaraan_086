using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentalKendaraan_20180140086.Models
{
    public partial class Kendaraan
    {
        public int IdKendaraan { get; set; }

        [Required(ErrorMessage = "Nama tidak boleh kosong")]
        public string NamaKendaraan { get; set; }

        [Required(ErrorMessage = "No polisi wajib diisi")]
        public string NoPolisi { get; set; }

        [Required(ErrorMessage = "No STNK wajib diisi")]
        public string NoStnk { get; set; }
        public int? IdJenisKendaraan { get; set; }

        [Required(ErrorMessage = "Ketersediaan wajib diisi")]
        public string Ketersediaan { get; set; }
        public object IdJenisKendaraanNavigation { get; internal set; }
    }
}
