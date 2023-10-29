using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Module.MusicSourcesStorage.Database.Models;

[ComplexType]
public sealed class MusicSourceAdditionalInfoModel
{
    [Required] public string Name { get; set; } = string.Empty;
}