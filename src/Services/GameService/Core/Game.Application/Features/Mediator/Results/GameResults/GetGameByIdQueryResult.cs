namespace Game.Application.Features.Mediator.Results.GameResults
{
    public class GetGameByIdQueryResult
    {
        public string Id { get; set; }
        public string GameName { get; set; }
        public string GameAuthor { get; set; }
        public decimal Price { get; set; }
        public string VideoPath { get; set; }
        public string GameInfo { get; set; }
        public string MinimumSystemRequirements { get; set; }
        public string RecommendedSystemRequirements { get; set; }
        public string Description { get; set; }
        public string UserID { get; set; }
        public string CategoryID { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
