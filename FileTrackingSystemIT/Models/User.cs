//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FileTrackingSystemIT.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string Email { get; set; }
        public string Picture { get; set; }
        public string Phone { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
    
        public virtual Role Role { get; set; }
    }
}
