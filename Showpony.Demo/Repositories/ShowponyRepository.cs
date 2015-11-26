using System.Data.SqlClient;
using System.Linq;
using System.Web.Configuration;
using Dapper;
using System.Collections.Generic;

namespace Showpony.Demo.Repositories
{
    public class ShowponyRepository
    {
        public void RecordShowponyStarted(string experiment, string variant)
        {
            if (SelectShowponyStats(experiment, variant) == null)
                InsertShowponyStats(experiment, variant);

            IncrementShowponyStartedStats(experiment, variant);
        }

        public void RecordShowponyCheckpoint(string experiment, string variant)
        {
            // record
        }

        public void RecordShowponyEnded(string experiment, string variant)
        {
            if (SelectShowponyStats(experiment, variant) == null)
                InsertShowponyStats(experiment, variant);

            IncrementShowponyEndedStats(experiment, variant);
        }

        public IEnumerable<ShowponyStats> SelectShowponyStats(string experiment)
        {
            var connectionString = WebConfigurationManager.ConnectionStrings["ShowponyStats"].ConnectionString;
            using (var conn = new SqlConnection(connectionString))
            {
                const string sql = "SELECT * FROM Stats WHERE Experiment=@experiment";
                var stats = conn.Query<ShowponyStats>(sql, new { experiment });
                return stats;
            }
        }

        private void InsertShowponyStats(string experiment, string variant)
        {
            var connectionString = WebConfigurationManager.ConnectionStrings["ShowponyStats"].ConnectionString;
            using (var conn = new SqlConnection(connectionString))
            {
                const string sql = "INSERT INTO Stats VALUES(@experiment, @variant, 0, 0)";
                conn.Execute(sql, new { experiment, variant });
            }
        }

        private ShowponyStats SelectShowponyStats(string experiment, string variant)
        {
            var connectionString = WebConfigurationManager.ConnectionStrings["ShowponyStats"].ConnectionString;
            using (var conn = new SqlConnection(connectionString))
            {
                const string sql = "SELECT * FROM Stats WHERE Experiment=@experiment AND Variant=@variant";
                var stats = conn.Query<ShowponyStats>(sql, new { experiment, variant });
                return stats.FirstOrDefault();
            }
        }

        private void IncrementShowponyStartedStats(string experiment, string variant)
        {
            var connectionString = WebConfigurationManager.ConnectionStrings["ShowponyStats"].ConnectionString;
            using (var conn = new SqlConnection(connectionString))
            {
                const string sql = "UPDATE Stats SET Started=Started+1 WHERE Experiment=@experiment and Variant=@variant";
                conn.Execute(sql, new { experiment, variant });
            }
        }

        private void IncrementShowponyEndedStats(string experiment, string variant)
        {
            var connectionString = WebConfigurationManager.ConnectionStrings["ShowponyStats"].ConnectionString;
            using (var conn = new SqlConnection(connectionString))
            {
                const string sql = "UPDATE Stats SET Ended=Ended+1 WHERE Experiment=@experiment and Variant=@variant";
                conn.Execute(sql, new { experiment, variant });
            }
        }
    }
}