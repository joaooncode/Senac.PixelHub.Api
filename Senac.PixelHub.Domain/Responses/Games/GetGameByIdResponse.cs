using System.Runtime.CompilerServices;
using Senac.PixelHub.Domain.Entities.Enums;

namespace Senac.PixelHub.Domain.Responses.Games
{
    public class GetGameByIdResponse
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsAvailable { get; set; }

        public string Responsible { get; set; }

        public DateTime ReturnDate { get; set; }

        public CategoriesEnum Category { get; set; }
    }
}
