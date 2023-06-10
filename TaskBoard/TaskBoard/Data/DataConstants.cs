﻿namespace TaskBoard.Data
{
    public static class DataConstants
    {
        public static class Task
        {
            public const int TaskTitleMinLength = 5;

            public const int TaskTitleMaxLength = 70;

            public const int TaskDescriptionMinLength = 10;

            public const int TaskDescriptionMaxLength = 1000;
        }

        public static class Board
        {
            public const int BoardNameMinLength = 3;

            public const int BoardNameMaxLength = 30;
        }
    }
}
