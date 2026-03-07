using SPR411_Shop.ViewModels;

namespace SPR411_Shop.Services
{
    public static class CartService
    {
        private static string Key = "fj4298ty4f9248yfwfoi92u4yf1fwhj";

        public static void SetItems(ISession session, IEnumerable<SessionCartItemVM> items)
        {
            session.Set(Key, items);
        }

        public static List<SessionCartItemVM> GetItems(ISession session)
        {
            var items = session.Get<List<SessionCartItemVM>>(Key);

            return items != null ? items : [];
        }

        public static int GetCount(ISession session)
        {
            return GetItems(session).Count;
        }

        public static void AddToCart(ISession session, int productId)
        {
            var items = GetItems(session);
            if(!IsInCart(session, productId))
            {
                items.Add(new SessionCartItemVM { ProductId = productId });
            }
            session.Set(Key, items);
        }

        public static void RemoveFromCart(ISession session, int productId)
        {
            var items = GetItems(session);
            if (IsInCart(session, productId))
            {
                items = items.Where(i => i.ProductId != productId).ToList();
            }
            session.Set(Key, items);
        }

        public static bool IsInCart(ISession session, int productId)
        {
            return GetItems(session).Any(i => i.ProductId == productId);
        }
    }
}
