namespace StarCitizenTradeRouter.Trading.Dtos
{
    public interface ITradingEntity<TKey>
    {
        public TKey Id { get; set; }
    }
}
