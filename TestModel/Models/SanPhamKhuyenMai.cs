//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestModel.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SanPhamKhuyenMai
    {
        public string MaKM { get; set; }
        public string MaSP { get; set; }
        public string MoTa { get; set; }
        public Nullable<int> GiamGia { get; set; }
    
        public virtual KhuyenMai KhuyenMai { get; set; }
        public virtual SanPham SanPham { get; set; }
    }
}
