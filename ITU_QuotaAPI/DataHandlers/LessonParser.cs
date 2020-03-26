using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ITU_QuotaAPI.DataHandlers
{
	/// <summary>
	/// Instance Class that Handles the Operations for Lesson Informations
	/// </summary>
	public class LessonParser
	{
		private readonly string _baseUrl;
		private readonly HttpClient _client;
		private List<Lesson> _classTable;

		/// <summary>
		/// HTML class of the main table
		/// </summary>
		public string TableClass { get; set; } = "dersprg";

		/// <summary>
		/// HTML name attribute of the code selection list
		/// </summary>
		public string DropdownName { get; set; } = "bolum";

		/// <summary>
		/// 
		/// </summary>
		/// <param name="baseUrl">Base URl of the Source(set to "http://www.sis.itu.edu.tr/tr/ders_programlari/LSprogramlar/prg.php" by default)</param>
		public LessonParser ( string baseUrl = null )
		{
			_baseUrl = baseUrl ?? "http://www.sis.itu.edu.tr/tr/ders_programlari/LSprogramlar/prg.php";

			_client = new HttpClient();
		}

		/// <summary>
		/// Parse the table for the given Course Category
		/// </summary>
		/// <param name="lessonCode">Prefix of the Course Code</param>
		/// <returns></returns>
		public async Task<IEnumerable<Lesson>> RetrieveTableAsync ( string lessonCode )
		{
			UriBuilder uriBuilder = new UriBuilder(_baseUrl);
			uriBuilder.Query = $"fb={lessonCode}";

			var result = _client.GetAsync(uriBuilder.Uri).Result;

			HtmlDocument html = new HtmlDocument();

			using (var sr = new StreamReader(result.Content.ReadAsStreamAsync().Result))
				html.LoadHtml(sr.ReadToEnd());

			List<Lesson> classTable = new List<Lesson>();

			var table = html.DocumentNode.Descendants(0).Where(x => x.HasClass(TableClass)).First();
			var rows = table.SelectNodes("tr").ToList();

			rows.RemoveRange(0, 2);
			foreach (var row in rows)
			{
				List<string> data = new List<string>();
				foreach (var col in row.SelectNodes("td"))
				{
					data.Add(col.InnerText);
				}

				classTable.Add(new Lesson
				{
					CRN = Convert.ToInt32(data[0]),
					Code = data[1],
					Title = data[2],
					Instructor = data[3],
					Building = data[4],
					Day = data[5],
					Time = data[6],
					Room = data[7],
					Capacity = Convert.ToInt32(data[8]),
					Enroled = Convert.ToInt32(data[9]),
					Reservations = data[10],
					Restrictions = data[11].Split(','),
					Prerequisities = data[12],
					ClassRestrictions = data[13]
				});
			}
			_classTable = classTable;
			return classTable.AsEnumerable();
		}
		/// <summary>
		/// Get a specified class from the priorly parsed table.
		/// </summary>
		/// <param name="CRN">CRN of the desired class</param>
		/// <returns></returns>
		public Lesson ParseLesson ( int CRN )
		{
			if (_classTable == null)
				return null;

			return _classTable.Find(x => x.CRN == CRN) as Lesson;
		}

		/// <summary>
		/// Avaiable Class Code Options from Website
		/// </summary>
		/// <returns></returns>
		public IEnumerable<string> ParseLessonCodes ()
		{
			var result = _client.GetAsync(_baseUrl).Result;

			HtmlDocument html = new HtmlDocument();

			using (var sr = new StreamReader(result.Content.ReadAsStreamAsync().Result))
				html.LoadHtml(sr.ReadToEnd());

			var listNode = html.DocumentNode.SelectSingleNode($"//select[@name='{DropdownName}']");

			var dropdownList = listNode.SelectNodes("option").Select(x => x.InnerText).ToList();
			dropdownList.RemoveAt(0);
			return dropdownList.AsEnumerable();
		}

		/// <summary>
		/// Get the time of last system update
		/// </summary>
		/// <returns></returns>
		public DateTime LastUpdated ()
		{
			string pseudoClassCode = ParseLessonCodes().First();

			UriBuilder uriBuilder = new UriBuilder(_baseUrl);
			uriBuilder.Query = $"fb={pseudoClassCode}";

			var result = _client.GetAsync(uriBuilder.Uri).Result;

			HtmlDocument html = new HtmlDocument();

			using (var sr = new StreamReader(result.Content.ReadAsStreamAsync().Result))
				html.LoadHtml(sr.ReadToEnd());

			var lastUpdated = html.DocumentNode.SelectNodes("//text()").Last(x => x.InnerText.Contains("/")).InnerText;
			string[] dateTime = lastUpdated.Trim().Split('/');

			string[] time = dateTime[1].Trim().Split(':');
			string[] date = dateTime[0].Trim().Split('-');

			return new DateTime(Convert.ToInt32(date[2]), Convert.ToInt32(date[1]), Convert.ToInt32(date[0]),
				Convert.ToInt32(time[0]), Convert.ToInt32(time[1]), Convert.ToInt32(time[2]));
		}
	}
}
