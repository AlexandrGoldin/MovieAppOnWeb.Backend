using Ardalis.GuardClauses;
using MovieApp.Infrastructure.Entities;
using MovieApp.Infrastructure.Interfaces;
namespace MovieApp.ApplicationCore.Entities.BuyerAggregate
{
    public class Buyer : BaseEntity, IAggregateRoot
    {
        public string IdentityGuid { get; private set; }

        private PaymentMethod _paymentMethod { get; set; } //= new PaymentMethod();

        public PaymentMethod PaymentMethod => _paymentMethod;

#pragma warning disable CS8618 // Required by Entity Framework
        private Buyer() { }

        public Buyer(string identity) : this()
        {
            Guard.Against.NullOrEmpty(identity, nameof(identity));
            IdentityGuid = identity;
        }
    }
}
