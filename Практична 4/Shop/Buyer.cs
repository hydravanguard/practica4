struct Buyer
{
    public int CardNumber;
    public int Money;
    public int PurchasedCount;

    public Buyer(int cardNumber, int money)
    {
        CardNumber = cardNumber;
        Money = money;
        PurchasedCount = 0;
    }
}