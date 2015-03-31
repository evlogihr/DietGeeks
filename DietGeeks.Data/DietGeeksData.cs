namespace DietGeeks.Data
{
    public class DietGeeksData : IDietGeeksData
    {
        private readonly IDietGeeksDbContext context;

        public DietGeeksData()
            : this(new DietGeeksDbContext())
        { }

        public DietGeeksData(DietGeeksDbContext context)
        {
            this.context = context;
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}
