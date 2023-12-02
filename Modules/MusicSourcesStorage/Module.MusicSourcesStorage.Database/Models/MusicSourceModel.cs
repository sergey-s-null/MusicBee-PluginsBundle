namespace Module.MusicSourcesStorage.Database.Models;

public abstract class MusicSourceModel
{
    public int Id { get; set; }
    public MusicSourceAdditionalInfoModel AdditionalInfo { get; set; } = new();

    public List<FileModel> Files { get; set; } = new();
}