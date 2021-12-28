using System;
using System.ComponentModel.DataAnnotations;

namespace Shared
{
    public interface IIdentityModel
    {
        [Key]
        public int Id { get; set; } 
    }
}
