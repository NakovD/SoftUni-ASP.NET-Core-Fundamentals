namespace Library.Data
{
    public static class DataConstants
    {
        public class BookConstants
        {
            public const int TitleMinLength = 10;

            public const int TitleMaxLength = 50;

            public const int AuthorMinLength = 5;

            public const int AuthorMaxLength = 50;

            public const int DescriptionMinLength = 5;

            public const int DescriptionMaxLength = 5000;

            public const double RatingMin = 0.00;

            public const double RatingMax = 10.00;
        }

        public class CategoryConstants
        {
            public const int NameMinLength = 5;

            public const int NameMaxLength = 50;
        }
    }
}
