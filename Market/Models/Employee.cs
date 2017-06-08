using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Market.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(30, ErrorMessage = "El campo {0} debe estar entre {2} y {1}", MinimumLength = 3)]
        public string FirstName { get; set; }

        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(30, ErrorMessage = "El campo {0} debe de estar entre {2} y {1}", MinimumLength = 3)]
        public string LastName { get; set; }

        [Display(Name = "Salario")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString ="{0:C2}",ApplyFormatInEditMode =false)]
        public decimal Salary { get; set; }

        [Display(Name ="Porcentaje de bonificación")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString ="{0:P2}",ApplyFormatInEditMode = false)]
        public float BonusPercent { get; set; }

        [Display(Name ="Fecha de nacimiento")]
        [DataType( DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy/MM/dd}",ApplyFormatInEditMode =true)]
        public DateTime DateOfBirth { get; set; }

        [Display(Name ="Hora de inicio")]
        [Required(ErrorMessage ="El campo {0} es obligatorio")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString ="{0:hh:mm tt}",ApplyFormatInEditMode =true)]
        public DateTime StartTime { get; set; }

        [Display(Name ="Correo")]
        [DataType(DataType.EmailAddress)]
        public string EMail { get; set; }

       [DataType(DataType.Url)]
        public string URL { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int DocumentTypeID { get; set; }

        public virtual DocumentType DocumentType { get; set; }

    }
}