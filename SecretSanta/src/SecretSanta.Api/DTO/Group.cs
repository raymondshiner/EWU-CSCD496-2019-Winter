using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Api.DTO
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Group()
        {

        }

        public Group(Domain.Models.Group group)
        {
            if (group == null) throw new ArgumentNullException(nameof(group));
            
            Id = group.Id;
            Name = group.Name;
        }

        public static Domain.Models.Group ToEntity(DTO.Group group)
        {
            if (group == null) throw new ArgumentNullException(nameof(group));

            var theGroup = new Domain.Models.Group
            {
                Id = group.Id,
                Name = group.Name
            };

            return theGroup;
        }
    }

   
}
