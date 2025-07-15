namespace Senac.PixelHub.Domain.Responses.Games
{
    public class GetAllGamesResponse
    {
        public string Title { get; set; }

        public bool IsAvailable { get; set; }

        public bool IsOverdue { get; set; }

        public DateTime WithdrawalDate { get; set; }

    }
}
