using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zata.Auditing;
using Zata.Entities;

namespace Zata.ProductionManager.Domain.Entities
{
    [Table("product")]
    public class Product : Entity<Guid>, IHasCreationTime, IHasModificationTime
    {
        /// <summary>
        /// Tên sản phẩm
        /// </summary>
        [NotNull]
        public string Name { get; set; } = string.Empty;

        public DateTime CreationTime { get; private set; }

        public DateTime? LastModificationTime { get; set; }
    }
}
