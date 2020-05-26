namespace DAL_OPDRACHT_PR3.Resources
{
    //Interface aanmaken voor DI te kunnen gebruiken
    public interface IUnitOfWork
    {
        UitgavenRepo UitgavenRepo { get; }

        GezinRepo GezinRepo { get; }

        KortingRepo KortingRepo { get; }

        bool SaveAll();
    }
}
