//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Patient
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Patient()
        {
            this.Appointments = new HashSet<Appointment>();
        }

        [Display(Name = "Health card number*")]
        [Required]
        [RegularExpression("[0-9]{20,20}", ErrorMessage = "The health card number must contain exactly 20 digits!")]
        public string cardNumber { get; set; }

        [Display(Name = "First name")]
        public string firstName { get; set; }

        [Display(Name = "Last name")]
        public string lastName { get; set; }

        [Display(Name = "Day of birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime birthDate { get; set; }

        [Display(Name = "Email*")]
        [DataType(DataType.EmailAddress)]
        [Required]
        public string email { get; set; }
        public Nullable<int> idMedic { get; set; }

        [Display(Name = "CNP*")]
        [Required]
        [RegularExpression("[0-9]{13,13}", ErrorMessage = "The CNP must contain exactly 13 digits!")]
        public string CNP { get; set; }
        public Nullable<int> idmedicalRecords { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual Medic Medic { get; set; }
        public virtual medicalRecord medicalRecord { get; set; }
    }
}
