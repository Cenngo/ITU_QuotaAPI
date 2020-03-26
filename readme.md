# .NET Wrapper for ITU Class Schedule

* Parse All Lesson Informations for Different Course Codes as Tables
* Sort by CRN' s for Lesson Specific Informations
* Get All Available Lesson Codes
* Get Last Update Time of the System

## Getting Started
```csharp
var client = new LessonParser();

//Getting the whole table
IEnumerable<Lesson> lessons = client.RetrieveTableAsync("EHB").GetAwaiter().GetResult();

//Getting Lesson Specific Information
Lesson lesson = client.ParseLesson(20280);
//or
Lesson lesson = client.ParseLesson("ATA", 20280);

//Getting Lesson Codes provided by ITU
IEnumerable codes = client.ParseLessonCodes();

///Getting the Last Update Time of the System
DateTime time client.LastUpdated();
```
