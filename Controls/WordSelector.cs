using ReCallVocabulary.Data_Access;

namespace ReCallVocabulary.Controls
{
    public class WordSelector : DataTemplateSelector
    {
        public required DataTemplate FirstPriorityWord { get; set; }
        public required DataTemplate OtherWord { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return ((Data_Access.Phrase)item).CreationDate < DateFileHandler.GetDates()[1]  ? OtherWord : FirstPriorityWord;
        }
    }
}
