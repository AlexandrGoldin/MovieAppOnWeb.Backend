namespace MovieApp.Infrastructure.Common.Exceptions
{
    internal class NotFoundException : Exception
    {
        public NotFoundException(string name, int? Id)
       : base($"Object: {name} = {Id} not found.") { }

        //public NotFoundException(string name, object key)
        //    : base($"Entity \"{name}\" ({key}) not found.") { }
    }
}
