using System;
using System.Collections.Generic;

namespace RentalKendaraan_20180140086.Models
{
    public partial class Pinjaman
    {
        public int IdPeminjaman { get; set; }
        public DateTime? TglPeminjaman { get; set; }
        public int? IdKendaraan { get; set; }
        public int? IdCustomer { get; set; }
        public int? IdJaminan { get; set; }
        public int? Biaya { get; set; }
    }
}
