using MagicVilla_VillaAPI.Models.DTOs;

namespace MagicVilla_VillaAPI
{
    public static class VillaStore
    {
        public static List<VillaDto> villaDtos = 
          new List<VillaDto> {
                new VillaDto{Id = 1, Name = "Pool View", Sqft= 100, Occupancy = 4 },
                new VillaDto{Id = 2, Name = "Beach View", Sqft = 300, Occupancy = 3 }
          };
    }
}
