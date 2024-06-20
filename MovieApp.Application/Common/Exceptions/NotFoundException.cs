namespace MovieApp.Infrastructure.Common.Exceptions
{
    internal class CastomeNotFoundException : Exception
    {
        public CastomeNotFoundException(string name, int? Id)
       : base($"Object: {name} = {Id} not found.") { }

        //public NotFoundException(string name, object key)
        //    : base($"Entity \"{name}\" ({key}) not found.") { }
    }
}
