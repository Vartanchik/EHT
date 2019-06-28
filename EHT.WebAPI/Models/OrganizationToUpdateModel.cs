using EHT.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EHT.WebAPI.Models
{
    public class OrganizationToUpdateModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public OrganizationType OrganizationType { get; set; }
        [Required]
        public string Owner { get; set; }
    }
}
