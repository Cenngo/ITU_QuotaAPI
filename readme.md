![enter image description here](https://img.shields.io/badge/Made%20With-C%23-blueviolet?style=for-the-badge)
![enter image description here](https://img.shields.io/github/license/Cenngo/ITU_QuotaAPI?style=flat-square)
<a href="https://www.nuget.org/packages/ITU_QuotaAPI/"><img alt="Nuget" src="https://img.shields.io/nuget/v/ITU_QuotaAPI?logo=nuget&style=flat-square"></a>
<a href="https://www.nuget.org/packages/ITU_QuotaAPI/"><img alt="Nuget" src="https://img.shields.io/nuget/dt/ITU_QuotaAPI?color=orange&logo=nuget&style=flat-square"></a>

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
