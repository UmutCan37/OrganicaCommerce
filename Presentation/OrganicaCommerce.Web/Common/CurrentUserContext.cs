namespace OrganicaCommerce.Web.Common
{
    public static class CurrentUserContext
    {
        // Not: Projede authentication/login akışı bulunmadığı için,
        // demo amaçlı sabit bir kullanıcı kimliği kullanılmaktadır.
        public static readonly Guid UserId = Guid.Parse("11111111-1111-1111-1111-111111111111");
    }
}