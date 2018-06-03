using System;

namespace MeditSolution.Responses
{
    public class NextMeditationResponse
    {
        public class Level1FrMan
        {
            public string originalFileName { get; set; }
            public string path { get; set; }
            public int length { get; set; }
            public string mimeType { get; set; }
        }

        public class Level1FrWoman
        {
            public string originalFileName { get; set; }
            public string path { get; set; }
            public int length { get; set; }
            public string mimeType { get; set; }
        }

        public class Level1EnMan
        {
            public string originalFileName { get; set; }
            public string path { get; set; }
            public int length { get; set; }
            public string mimeType { get; set; }
        }

        public class Level1EnWoman
        {
            public string originalFileName { get; set; }
            public string path { get; set; }
            public int length { get; set; }
            public string mimeType { get; set; }
        }

        public class Level2FrMan
        {
            public string originalFileName { get; set; }
            public string path { get; set; }
            public int length { get; set; }
            public string mimeType { get; set; }
        }

        public class Level2FrWoman
        {
            public string originalFileName { get; set; }
            public string path { get; set; }
            public int length { get; set; }
            public string mimeType { get; set; }
        }

        public class Level2EnMan
        {
            public string originalFileName { get; set; }
            public string path { get; set; }
            public int length { get; set; }
            public string mimeType { get; set; }
        }

        public class Level2EnWoman
        {
            public string originalFileName { get; set; }
            public string path { get; set; }
            public int length { get; set; }
            public string mimeType { get; set; }
        }

        public class Level3FrMan
        {
            public string originalFileName { get; set; }
            public string path { get; set; }
            public int length { get; set; }
            public string mimeType { get; set; }
        }

        public class Level3FrWoman
        {
            public string originalFileName { get; set; }
            public string path { get; set; }
            public int length { get; set; }
            public string mimeType { get; set; }
        }

        public class Level3EnMan
        {
            public string originalFileName { get; set; }
            public string path { get; set; }
            public int length { get; set; }
            public string mimeType { get; set; }
        }

        public class Level3EnWoman
        {
            public string originalFileName { get; set; }
            public string path { get; set; }
            public int length { get; set; }
            public string mimeType { get; set; }
        }

        public class LevelUp
        {
            public string _id { get; set; }
            public string label { get; set; }
            public string description { get; set; }
            public string label_en { get; set; }
            public string description_en { get; set; }
            public int length { get; set; }
            public Level1FrMan level_1_fr_man { get; set; }
            public Level1FrWoman level_1_fr_woman { get; set; }
            public Level1EnMan level_1_en_man { get; set; }
            public Level1EnWoman level_1_en_woman { get; set; }
            public Level2FrMan level_2_fr_man { get; set; }
            public Level2FrWoman level_2_fr_woman { get; set; }
            public Level2EnMan level_2_en_man { get; set; }
            public Level2EnWoman level_2_en_woman { get; set; }
            public Level3FrMan level_3_fr_man { get; set; }
            public Level3FrWoman level_3_fr_woman { get; set; }
            public Level3EnMan level_3_en_man { get; set; }
            public Level3EnWoman level_3_en_woman { get; set; }
            public string programId { get; set; }
            public string lastUpdatedAt { get; set; }
            public string createdAt { get; set; }
        }

        public class OtherMeditation
        {
            public string _id { get; set; }
            public string label { get; set; }
            public string description { get; set; }
            public string label_en { get; set; }
            public string description_en { get; set; }
            public int length { get; set; }
            public Level1FrMan level_1_fr_man { get; set; }
            public Level1FrWoman level_1_fr_woman { get; set; }
            public Level1EnMan level_1_en_man { get; set; }
            public Level1EnWoman level_1_en_woman { get; set; }
            public Level2FrMan level_2_fr_man { get; set; }
            public Level2FrWoman level_2_fr_woman { get; set; }
            public Level2EnMan level_2_en_man { get; set; }
            public Level2EnWoman level_2_en_woman { get; set; }
            public Level3FrMan level_3_fr_man { get; set; }
            public Level3FrWoman level_3_fr_woman { get; set; }
            public Level3EnMan level_3_en_man { get; set; }
            public Level3EnWoman level_3_en_woman { get; set; }
            public string programId { get; set; }
            public string lastUpdatedAt { get; set; }
            public string createdAt { get; set; }

        }

        public class Example
        {
            public LevelUp levelUp { get; set; }
            public OtherMeditation otherMeditation { get; set; }
        }

    }

}
