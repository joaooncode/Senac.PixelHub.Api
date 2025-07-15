using Senac.PixelHub.Domain.Entities.Enums;

namespace Senac.PixelHub.Domain.Entities
{
    public class GameEntity
    {
        public long Id { get; set; }
        
        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsAvailable { get; set; }

        public string Responsible { get; set; }
        
        public bool IsOverdue { get; set; }

        public DateTime WithdrawalDate { get; set; }

        public CategoriesEnum Category { get; set; }

    }
}
