using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace DiagnoseMe.Models
{
    public class DiagnosisResult
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public string DiseaseName { get; set; }
        public string Description { get; set; }
        public string Recommendations { get; set; }
        public DateOnly DateOnly { get; set; }

        public DiagnosisResult() { }
        public DiagnosisResult(int userId, string diseaseName, string description, string recommendations, DateOnly dateOnly)
        {
            UserId = userId;
            DiseaseName = diseaseName;
            Description = description;
            Recommendations = recommendations;
            DateOnly = dateOnly;
        }
    }
}
