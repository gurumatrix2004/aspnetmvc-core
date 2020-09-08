using Microsoft.AspNetCore.SignalR;
using PatientLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class PatientAddViewModel
    {
        public List<PatientAddress> allPatients { set; get; }
        public PatientAddress currentPatient { set; get; }

        public List<ValidationResult> errresults;
        public PatientProblem currentPatientProblem { set; get; }
        

        public PatientAddViewModel()
        {
            errresults = new List<ValidationResult>();
            CopyMe = new PatientAddress();
        }

        public PatientAddress CopyMe
        {
            set;get;
        }
    }
}
