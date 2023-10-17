using HtmlAgilityPack;
using System.Data;

namespace SportsResultNotifier
{
    public class Scrapper
    {
        public HtmlDocument Document { get; set; }
        public List<string> Columns { get; set; }
        public List<List<string>> Rows { get; set; }
        public Scrapper()
        {
            string url = "https://www.basketball-reference.com/boxscores/";
            HtmlWeb web = new HtmlWeb();
            Document = web.Load(url);
        }

        public void GetColumns(string seperator)
        {
            Columns = Document.DocumentNode
                                    .SelectNodes($"//*[@id=\"confs_standings_{seperator}\"]/thead/tr/th")
                                    .Select(x => x.InnerText)
                                    .ToList();
        }
        public void GetRows(string seperator)
        {
            Rows = Document.DocumentNode
                                    .SelectNodes($"//*[@id=\"confs_standings_{seperator}\"]/tbody/tr")
                                    .Select(row => row.ChildNodes
                                        .Select(cell => cell.InnerText)
                                        .ToList())
                                    .ToList();
        }
        public DataTable BuildTable(string seperator)
        {
            DataTable data = new();

            GetColumns(seperator);
            GetRows(seperator);

            Columns.ForEach(x => data.Columns.Add(x));
            foreach(var row in Rows)
            {
                data.Rows.Add(row[0], row[1], row[2], row[3], row[4], row[5], row[6]);
            }

            return data;
        }
    }
}
