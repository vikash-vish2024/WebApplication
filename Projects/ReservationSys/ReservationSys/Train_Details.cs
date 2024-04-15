//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ReservationSys
{
    using System;
    using System.Collections.Generic;
    
    public partial class Train_Details
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Train_Details()
        {
            this.Booked_Ticket = new HashSet<Booked_Ticket>();
            this.Canceled_Ticket = new HashSet<Canceled_Ticket>();
            this.Class_Fare = new HashSet<Class_Fare>();
            this.Seat_Availability = new HashSet<Seat_Availability>();
        }
    
        public decimal Train_No { get; set; }
        public string Train_Name { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public string Train_Status { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Booked_Ticket> Booked_Ticket { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Canceled_Ticket> Canceled_Ticket { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Class_Fare> Class_Fare { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Seat_Availability> Seat_Availability { get; set; }
    }
}
