using MagicVillaApi.Models.Dto;

namespace MagicVillaApi.Data
{
    public static class VillaStore
    {
        public static List<VillaDto> villaList =new List<VillaDto>
        {
            new VillaDto {Id = 1,Name = "Poll View"},
            new VillaDto {Id = 2,Name = "Beach View"}
        };
    }
}
