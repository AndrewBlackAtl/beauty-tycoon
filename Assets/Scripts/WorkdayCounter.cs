public class WorkdayData
{
    public int VisitorsPerDay { get; private set; }
    public float MoneyPerDay { get; private set; }
    public int ReputationPerDay { get; private set; }


    public void AddVisitor() 
    {
        VisitorsPerDay++;
    }

    public void AddMoney(float value) 
    {
        MoneyPerDay += value;
    }

    public void AddReputation(int value) 
    {
        ReputationPerDay += value;
    }
}


public class WorkdayCounter : IWorkdayMember
{
    public WorkdayData Data { get; private set; }

    public void OnDayEnd()
    {

    }

    public void OnDayStart()
    {
        Data = new WorkdayData();
    }

    public void OnVisitorServed(IServiceResult result) 
    {
        Data.AddVisitor();
        Data.AddMoney(result.MoneyChange);
        Data.AddReputation(result.ReputationChange);
    }
}