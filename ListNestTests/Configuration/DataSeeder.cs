using ListNest.Database;

namespace ListNestTests.Configuration
{
    public class DataSeeder
    {
        public void CreateSampleDataset(ListNestDbContext context)
        {
            foreach (var list in DbValues.Lists)
                context.Lists.Add(list);

            foreach (var listItem in DbValues.ListItems)
                context.ListItems.Add(listItem);
        }
    }
}
