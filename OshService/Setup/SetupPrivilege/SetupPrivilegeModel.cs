using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OshService.Setup.SetupPrivilege;

[Table("setup_privilege")]
public class SetupPrivilegeModel
{
    [Key]
    [Column("locked")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Locked { get; set; }
}
