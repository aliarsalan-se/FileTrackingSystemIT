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
    
    public partial class File_Record
    {
        public int ID { get; set; }
        public int File_Number { get; set; }
        public string File_Subject { get; set; }
        public string Remarks { get; set; }
        public DateTime Send_Date { get; set; }
        public TimeSpan Send_Time { get; set; }
        public string SenderName { get; set; }
        public string SendTo_Name { get; set; }
        public string SendTo_Department { get; set; }
    }
}
