
// Exercise 1 - Null Safety

string? region = null;

string? upperRegion = region?.ToUpper();

Console.WriteLine($"Region (conditional): {upperRegion}");

string displayRegion = region ?? "Unassigned";

Console.WriteLine($"Region (coalesced): {displayRegion}");

region ??= "Addis Ababa";

Console.WriteLine($"Region (assigned): {region}");

Console.WriteLine();

////////////
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



// Exercise 2 - Primitive Types

decimal grantPerStudent = 1999.99m;
decimal totalAllocation = grantPerStudent * 100_000m;

Console.WriteLine($"Total allocated (decimal): {totalAllocation}");
Console.WriteLine($"Total allocated (formatted): {totalAllocation:F2}");

Console.WriteLine();


// Exercise 3 - EnrollmentRecord

// destrucitive and will cause an error
var enrollment = new EnrollmentRecord(
    "STU-001",
    "CS-401",
    DateTime.UtcNow
);



// Non-destructive mutation
var corrected = enrollment with
{
    CourseCode = "CS-402"
};

Console.WriteLine($"Corrected: {corrected}");

// Value equality === dupliacate and enrollment have same data
var duplicate = new EnrollmentRecord(
    "STU-001",
    "CS-401",
    enrollment.EnrolledAt
);

Console.WriteLine($"Same data? {enrollment == duplicate}");

Console.WriteLine();

/////////

var course = new Course
{
    Code = "CS-401",
    Title = "Advanced C#",
    Capacity = 30
};

Console.WriteLine(
    $"Course: {course.Title} (Capacity: {course.Capacity})"
);

// Invalid capacity
try
{
    course.Capacity = -5;
}
catch (ArgumentOutOfRangeException ex)
{
    Console.WriteLine($"Caught: {ex.Message}");
}

// Invalid title
try
{
    course.Title = "";
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Caught: {ex.Message}");
}

Console.WriteLine();

// Exercise 3 Part 3 - Student Model


var s = new Student
{
    Id = "S1",
    Name = "Abeba",
    Age = 20,
    GPA = 3.8m
};

Console.WriteLine($"Student: {s.Name}, GPA: {s.GPA}");



//  Interfaces & Polymorphism

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