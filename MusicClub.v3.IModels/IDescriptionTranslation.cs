namespace MusicClub.v3.IModels
{
    public interface IDescriptionTranslation
    {
        string Text { get; set; }

        int LanguageId { get; set; }

        int DescriptionId { get; set; }
    }
}
