namespace DAL_OPDRACHT_PR3.Migrations
{
    using DAL_OPDRACHT_PR3.Models;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL_OPDRACHT_PR3.ProjectContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DAL_OPDRACHT_PR3.ProjectContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            AddKortingen(context);
            AddGemeente(context);
            AddGezin(context);
            AddUitgaven(context);
            AddPersonen(context);
        }

        private void AddKortingen(ProjectContext context)
        {
            if (!context.Kortingen.Any())
            {
                var kortingData = System.IO.File.ReadAllText("C:/Users/user/Documents/Hik Geel/Programmeren/CSharp/Prog3/_Project/DAL_Opdracht_PR3(2)/DAL_Opdracht_PR3/JsonData/Korting.json");

                var kortingen = JsonConvert.DeserializeObject<List<Korting>>(kortingData);

                foreach (var korting in kortingen)
                {
                    context.Kortingen.AddOrUpdate(korting);
                }

                context.SaveChanges();
            }
        }

        private void AddGemeente(ProjectContext context)
        {
            if (!context.Gemeentes.Any())
            {
                var gemeenteData = System.IO.File.ReadAllText("C:/Users/user/Documents/Hik Geel/Programmeren/CSharp/Prog3/_Project/DAL_Opdracht_PR3(2)/DAL_Opdracht_PR3/JsonData/Gemeente_generated.json");
                var gemeenten = JsonConvert.DeserializeObject<List<Gemeente>>(gemeenteData);

                foreach (var gemeente in gemeenten)
                {
                    context.Gemeentes.AddOrUpdate(gemeente);
                }

                context.SaveChanges();
            }
        }

        private void AddGezin(ProjectContext context)
        {
            if (!context.Gezinnen.Any())
            {
                var gezinData = System.IO.File.ReadAllText("C:/Users/user/Documents/Hik Geel/Programmeren/CSharp/Prog3/_Project/DAL_Opdracht_PR3(2)/DAL_Opdracht_PR3/JsonData/Gezin_generated.json");

                var gezinnen = JsonConvert.DeserializeObject<List<Gezin>>(gezinData);

                foreach (var gezin in gezinnen)
                {
                    context.Gezinnen.AddOrUpdate(gezin);
                }

                context.SaveChanges();
            }
        }

        private void AddUitgaven(ProjectContext context)
        {
            if (!context.Uitgaven.Any())
            {
                var uitgavenData = System.IO.File.ReadAllText("C:/Users/user/Documents/Hik Geel/Programmeren/CSharp/Prog3/_Project/DAL_Opdracht_PR3(2)/DAL_Opdracht_PR3/JsonData/Uitgaven_generated.json");

                var uitgaven = JsonConvert.DeserializeObject<List<Uitgave>>(uitgavenData);

                foreach (var uitgave in uitgaven)
                {
                    context.Uitgaven.AddOrUpdate(uitgave);
                }

                context.SaveChanges();
            }
        }

        private void AddPersonen(ProjectContext context)
        {
            if (!context.Personen.Any())
            {
                var personenData = System.IO.File.ReadAllText("C:/Users/user/Documents/Hik Geel/Programmeren/CSharp/Prog3/_Project/DAL_Opdracht_PR3(2)/DAL_Opdracht_PR3/JsonData/Persoon_generated.json");

                var personen = JsonConvert.DeserializeObject<List<Persoon>>(personenData);

                foreach (var persoon in personen)
                {
                    context.Personen.AddOrUpdate(persoon);
                }

                context.SaveChanges();
            }
        }
    }
}
