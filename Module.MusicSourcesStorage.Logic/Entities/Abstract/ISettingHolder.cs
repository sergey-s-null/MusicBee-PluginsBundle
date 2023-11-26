namespace Module.MusicSourcesStorage.Logic.Entities.Abstract;

public interface ISettingHolder<T>
{
    string Area { get; }
    string Id { get; }
    T Value { get; set; }
}