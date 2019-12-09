namespace Gucm.Application.ViewModel
{

    public sealed class UpdateGdprCommand : GdprCommand
    {
        public int Id { get; set; }

        public string Gdpr { get; set; }

        public override bool IsValid()
        {
            return true;
        }
    }
}
