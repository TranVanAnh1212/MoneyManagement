//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MoneyManagemementModel.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class F_Expense
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public System.DateTime Date { get; set; }
        public decimal Price { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string Img { get; set; }
        public string Description { get; set; }
        public Nullable<long> CategoryID { get; set; }
        public string Note { get; set; }
    
        public virtual Category Category { get; set; }
    }
}