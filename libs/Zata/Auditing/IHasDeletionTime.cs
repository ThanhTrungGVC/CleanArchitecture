using Zata.Data;

namespace Zata.Auditing
{
    /// <summary>
    /// A standard interface to add DeletionTime property to a class. It also makes the class soft delete (see Volo.Abp.ISoftDelete).
    /// </summary>
    public interface IHasDeletionTime : ISoftDelete
    {
        /// <summary>
        /// Deletion time
        /// </summary>
        DateTime? DeletionTime { get; set; }
    }
}
