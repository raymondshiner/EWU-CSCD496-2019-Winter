using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Api.ViewModels
{
    public class GiftViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int OrderOfImportance { get; set; }
        public string Url { get; set; }

        public GiftViewModel()
        {

        }

        public static GiftViewModel ToViewModel(Gift gift)
        {
            if (gift == null) throw new ArgumentNullException(nameof(gift));

            var result = new GiftViewModel
            {
                Id = gift.Id,
                Title = gift.Title,
                Description = gift.Description,
                OrderOfImportance = gift.OrderOfImportance,
                Url = gift.Url,
            };

            return result;
        }
    }
}
