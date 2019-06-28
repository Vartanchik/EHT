using EHT.DAL.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EHT.BLL.DTOs
{
    public class OrganizationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public OrganizationType OrganizationType { get; set; }
        public string Owner { get; set; }
    }
}
