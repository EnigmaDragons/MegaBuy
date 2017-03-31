namespace MegaBuy.Money
{
    public interface IRent
    {
        void Pay(IAmount amount);
        void Increase(IAmount amount);
    }
}