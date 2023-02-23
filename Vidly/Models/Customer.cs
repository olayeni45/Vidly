namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSubscribedToNewsLetter { get; set; }
        public MembershipType MembershipType { get; set; }

        //Entity framework recognizes this as the foreign key of the Class MembershipType
        public byte MembershipTypeId { get; set; }
    }
}