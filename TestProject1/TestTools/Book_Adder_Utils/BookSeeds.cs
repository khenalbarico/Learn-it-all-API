namespace TestProject1.TestTools.Book_Adder_Utils;

static class BookSeeds
{
    public static readonly IReadOnlyList<BookSeed> All =
    [
        //Skip-Complete
        new(
            Prefix:      "VT",
            Category:    "CollegeCourse",
            Title:       "Veterinary",
            Description: "Master the essentials of veterinary medicine with this comprehensive collection of learning materials designed for aspiring veterinarians, students, and animal health professionals. Covering a wide range of topics including clinical pathology, laboratory techniques, histology, immunology, pharmacology, animal behavior, surgery, parasitology, and large animal medicine, this resource provides valuable knowledge to strengthen both theoretical understanding and practical skills. Perfect for academic support, review, and continuous learning in the field of veterinary science.\r\n",
            Price:    70,
            Skip: true
        ),
        //Skip-Complete
        new(
            Prefix:   "PNLER",
            Category: "Reviewer",
            Title:    "P.N.L.E Reviewer",
            Description: "Prepare for nursing school exams and professional licensure with this comprehensive P.N.L.E reviewer packed with essential nursing topics and practice materials. This collection covers community health nursing, medical-surgical nursing, maternal and child health, psychiatric nursing, nursing fundamentals, and more. It also includes practice exams and question-and-answer sets with rationales to help strengthen critical thinking and test-taking skills. Ideal for nursing students, reviewees, and aspiring nurses who want to build confidence and deepen their understanding of core nursing concepts.\r\n",
            Price:    70,
            Skip: true
        ),
        //Skip-Complete
        new(
            Prefix:   "PSYR",
            Category: "Reviewer",
            Title:    "Psychometrician Reviewer",
            Description: "Ace your Psychometrician licensure exam with this comprehensive reviewer designed to help students and aspiring professionals strengthen their knowledge in psychology and psychological assessment. This learning material covers key areas such as psychological assessment, abnormal psychology, industrial psychology, Filipino psychology, theory of personality, and general psychometrician concepts. It also includes practice question-and-answer sets to enhance critical thinking, test familiarity, and exam readiness. Ideal for review, self-study, and building confidence for the board examination.\r\n",
            Price:    70,
            Skip: true
         ),
    ];
}
