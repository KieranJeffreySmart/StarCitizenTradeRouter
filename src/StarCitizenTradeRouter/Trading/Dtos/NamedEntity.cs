namespace StarCitizenTradeRouter.Trading.Dtos
{
    public class NamedEntity: ITradingEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}