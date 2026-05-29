// ======================================================
// SESSION 1 - Exercise 1
// Null Safety
// ======================================================

string? region = null;

string? upperRegion = region?.ToUpper();

Console.WriteLine($"Region (conditional): {upperRegion}");

string displayRegion = region ?? "Unassigned";

Console.WriteLine($"Region (coalesced): {displayRegion}");

region ??= "Addis Ababa";

Console.WriteLine($"Region (assigned): {region}");

Console.WriteLine();


// ======================================================
// SESSION 1 - TMS Variables
// ======================================================

string studentName = "Abeba";
string studentId = "STU-001";
int enrollmentCount = 3;

decimal grantAmount = 1999.99m;

DateTime enrolledAt = DateTime.UtcNow;

string? campusRegion = null;

Console.WriteLine($"Student: {studentName} ({studentId})");
Console.WriteLine($"Courses: {enrollmentCount}");
Console.WriteLine($"Grant: {grantAmount:F2}");
Console.WriteLine($"Enrolled: {enrolledAt:yyyy-MM-dd}");
Console.WriteLine($"Campus: {campusRegion ?? "Not assigned"}");

Console.WriteLine();


// ======================================================
// SESSION 1 - Exercise 2
// Primitive Types
// ======================================================

double legacyGrant = 1999.99;
double legacyTotal = legacyGrant * 100_000;

Console.WriteLine($"Total allocated (double): {legacyTotal:R}");

Console.WriteLine();

decimal grantPerStudent = 1999.99m;
decimal totalAllocation = grantPerStudent * 100_000m;

Console.WriteLine($"Total allocated (decimal): {totalAllocation}");
Console.WriteLine($"Total allocated (formatted): {totalAllocation:F2}");

Console.WriteLine();


// ======================================================
// SESSION 1 - Exercise 3
// Records
// ======================================================

var enrollment = new EnrollmentRecord(
    "STU-001",
    "CS-401",
    DateTime.UtcNow
);

Console.WriteLine(enrollment);

// enrollment.CourseCode = "HACKED";

var corrected = enrollment with
{
    CourseCode = "CS-402"
};

Console.WriteLine(corrected);

var duplicate = new EnrollmentRecord(
    "STU-001",
    "CS-401",
    enrollment.EnrolledAt
);

Console.WriteLine($"Same data? {enrollment == duplicate}");

Console.WriteLine();


// ======================================================
// SESSION 1 - Course Validation
// ======================================================

var course = new Course
{
    Code = "CS-401",
    Title = "Advanced C#",
    Capacity = 30
};

Console.WriteLine(
    $"Course: {course.Title} (Capacity: {course.Capacity})"
);

try
{
    course.Capacity = -5;
}
catch (ArgumentOutOfRangeException ex)
{
    Console.WriteLine($"Caught: {ex.Message}");
}

try
{
    course.Title = "";
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Caught: {ex.Message}");
}

Console.WriteLine();


// ======================================================
// SESSION 1 - Student Model
// ======================================================

var s = new Student
{
    Id = "S1",
    Name = "Abeba",
    Age = 20,
    GPA = 3.8m
};

Console.WriteLine($"Student: {s.Name}, GPA: {s.GPA}");

Console.WriteLine();


// ======================================================
// SESSION 1 - Interfaces & Polymorphism
// ======================================================

void PrintGradeReport(IEnumerable<IGradable> assessments)
{
    Console.WriteLine("--- Grade Report ---");

    foreach (var item in assessments)
    {
        Console.WriteLine(
            $"{item.Title}: {item.CalculateGrade():F2}%"
        );
    }
}

IGradable[] cohortAssessments =
[
    new Quiz
    {
        Title = "C# Basics",
        CorrectAnswers = 18,
        TotalQuestions = 20
    },

    new LabAssignment
    {
        Title = "Registration API",
        FunctionalityScore = 90m,
        CodeQualityScore = 85m
    }
];

PrintGradeReport(cohortAssessments);

Console.WriteLine();


// ======================================================
// SESSION 2 - Exercise 4
// Guard Clauses & Pattern Matching
// ======================================================

var service = new EnrollmentService();

var validStudent = new Student
{
    Id = "S1",
    Name = "Abeba",
    Age = 20,
    GPA = 3.8m
};

var validCourse = new Course
{
    Code = "CS-401",
    Title = "Advanced C#",
    Capacity = 30
};

var result = service.ProcessRegistration(
    validStudent,
    validCourse
);

Console.WriteLine(
    $"Enrolled: {result.StudentId} in {result.CourseCode}"
);

// Null student test
try
{
    service.ProcessRegistration(null, validCourse);
}
catch (ArgumentNullException ex)
{
    Console.WriteLine($"Guard caught: {ex.ParamName}");
}

// Full course test
var fullCourse = new Course
{
    Code = "CS-402",
    Title = "Full Course",
    Capacity = 1
};

fullCourse.EnrolledCount = 1;

try
{
    service.ProcessRegistration(validStudent, fullCourse);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Business rule: {ex.Message}");
}

Console.WriteLine();


// ======================================================
// SESSION 2 - Exercise 5
// Collections & LINQ
// ======================================================

List<Student> students =
[
    new Student { Id = "S1", Name = "Abeba", Age = 22, GPA = 3.8m },
    new Student { Id = "S2", Name = "Kidane", Age = 21, GPA = 2.4m },
    new Student { Id = "S3", Name = "Dawit", Age = 20, GPA = 3.1m },
    new Student { Id = "S4", Name = "Sara", Age = 23, GPA = 3.9m },
    new Student { Id = "S5", Name = "Frehiwot", Age = 19, GPA = 2.0m },
    new Student { Id = "S6", Name = "Yonas", Age = 24, GPA = 3.5m },
    new Student { Id = "S7", Name = "Meron", Age = 22, GPA = 1.8m },
    new Student { Id = "S8", Name = "Tesfaye", Age = 21, GPA = 2.9m }
];

// Honors leaderboard
var leaderboard = students
    .Where(s => s.GPA >= 3.5m)
    .OrderByDescending(s => s.GPA)
    .Select(s => s.Name)
    .ToList();

Console.WriteLine(
    $"Found {leaderboard.Count} Honors Students:"
);

foreach (var name in leaderboard)
{
    Console.WriteLine($"- {name}");
}

// Average GPA
decimal averageGpa = students.Average(s => s.GPA);

Console.WriteLine(
    $"\nClass Average GPA: {averageGpa:F2}"
);

// Group by academic standing
var standingGroups = students.GroupBy(s => s.GPA switch
{
    >= 3.5m => "Honors",
    >= 2.5m => "Good Standing",
    >= 2.0m => "Probation",
    _ => "Academic Warning"
});

Console.WriteLine("\n--- Academic Standing Report ---");

foreach (var group in standingGroups)
{
    Console.WriteLine($"\n{group.Key} ({group.Count()}):");

    foreach (var student in group)
    {
        Console.WriteLine($" {student.Name} GPA: {student.GPA}");
    }
}

// Spread operator
string[] backendCourses =
[
    "C#",
    "ASP.NET Core"
];

string[] frontendCourses =
[
    "TypeScript",
    "Angular"
];

string[] allCourses =
[
    ..backendCourses,
    ..frontendCourses,
    "Capstone"
];

Console.WriteLine(
    $"\nFull curriculum: {string.Join(", ", allCourses)}"
);