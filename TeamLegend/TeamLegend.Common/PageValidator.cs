namespace TeamLegend.Common
{
    public static class PageValidator
    {
        public static int ValidatePage(int page, int collectionCount, int numberOfEntitiesOnPage)
        {
            if (page < 1)
                return 1;

            if ((page * numberOfEntitiesOnPage) - numberOfEntitiesOnPage > collectionCount)
            {
                if (collectionCount % numberOfEntitiesOnPage != 0)
                {
                    page = (collectionCount / numberOfEntitiesOnPage) + 1;

                    return page;
                }
            }

            return page;
        }
    }
}
