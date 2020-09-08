using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Emit;

namespace PatientLibrary
{
    public interface IPatient
    {
        string name { get; set; }
        string address { get; set; }

        List<PatientProblem> lstproblems { get; set; }
    }
    
    public class PatientDTO
    {        
        public virtual string name { get; set; }

        public virtual string address { get; set; }

        public virtual List<PatientProblem> lstproblems { get; set; }

    }
    public class Patient : PatientDTO, IPatient
    {

        public int Id { set; get; }
        

        [Required(ErrorMessage ="Please put name")]
        public override string name { get; set; }
        

        [Required(ErrorMessage="Please put address")]
        public override string address { get; set; }

        public override List<PatientProblem> lstproblems { get; set; }

        public Patient()
        {
            this.lstproblems = new List<PatientProblem>();
        }

        public virtual bool Validate()
        {
            if(name.Length > 0)
            {
                return true;
            }
            return false;
        }
    }

    public class PatientAddress : Patient, IPatient
    {
        public override bool Validate()
        {
               return true;
          
        }
        public PatientAddress ShallowCopy()
        {
            return (PatientAddress)this.MemberwiseClone();
        }

    }

    public class PatientProblem
    {
        public int Id { set; get; }
        public string problem { get; set; }
        public List<Treatment> treatments { get; set; }
        public PatientProblem()
        {
            this.treatments = new List<Treatment>();
        }

    }

    public class Treatment
    {
        public int Id { set; get; }
        public string treatmentName { get; set; }
        public string treatmentDose { get; set; }
    }
}
