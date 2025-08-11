using System.ComponentModel.DataAnnotations;

namespace FundRaising.Data.Common
{
    public class Entity<TKey>
    {
        public TKey Id { get; set; }
    }
}
