using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Cluster
{
	public class TasksPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line13()
		{
			// tag::166bcfc6d5d39defec7ad6aa44d0914b[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::166bcfc6d5d39defec7ad6aa44d0914b[]

			response0.MatchesExample(@"GET _tasks \<1>");

			response1.MatchesExample(@"GET _tasks?nodes=nodeId1,nodeId2 \<2>");

			response2.MatchesExample(@"GET _tasks?nodes=nodeId1,nodeId2&actions=cluster:* \<3>");
		}

		[U]
		[SkipExample]
		public void Line67()
		{
			// tag::33610800d9de3c3e6d6b3c611ace7330[]
			var response0 = new SearchResponse<object>();
			// end::33610800d9de3c3e6d6b3c611ace7330[]

			response0.MatchesExample(@"GET _tasks/oTUltX4IQMOUUVeiohTt8A:124");
		}

		[U]
		[SkipExample]
		public void Line78()
		{
			// tag::29824032d7d64512d17458fdd687b1f6[]
			var response0 = new SearchResponse<object>();
			// end::29824032d7d64512d17458fdd687b1f6[]

			response0.MatchesExample(@"GET _tasks?parent_task_id=oTUltX4IQMOUUVeiohTt8A:123");
		}

		[U]
		[SkipExample]
		public void Line91()
		{
			// tag::8f4a7f68f2ca3698abdf20026a2d8c5f[]
			var response0 = new SearchResponse<object>();
			// end::8f4a7f68f2ca3698abdf20026a2d8c5f[]

			response0.MatchesExample(@"GET _tasks?actions=*search&detailed");
		}

		[U]
		[SkipExample]
		public void Line153()
		{
			// tag::93fb59d3204f37af952198b331fb6bb7[]
			var response0 = new SearchResponse<object>();
			// end::93fb59d3204f37af952198b331fb6bb7[]

			response0.MatchesExample(@"GET _tasks/oTUltX4IQMOUUVeiohTt8A:12345?wait_for_completion=true&timeout=10s");
		}

		[U]
		[SkipExample]
		public void Line163()
		{
			// tag::77447e2966708e92f5e219d43ac3f00d[]
			var response0 = new SearchResponse<object>();
			// end::77447e2966708e92f5e219d43ac3f00d[]

			response0.MatchesExample(@"GET _tasks?actions=*reindex&wait_for_completion=true&timeout=10s");
		}

		[U]
		[SkipExample]
		public void Line172()
		{
			// tag::927fc3b86302afb2fc41785261771663[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::927fc3b86302afb2fc41785261771663[]

			response0.MatchesExample(@"GET _cat/tasks");

			response1.MatchesExample(@"GET _cat/tasks?detailed");
		}

		[U]
		[SkipExample]
		public void Line186()
		{
			// tag::d89d36741d906a71eca6c144e8d83889[]
			var response0 = new SearchResponse<object>();
			// end::d89d36741d906a71eca6c144e8d83889[]

			response0.MatchesExample(@"POST _tasks/oTUltX4IQMOUUVeiohTt8A:12345/_cancel");
		}

		[U]
		[SkipExample]
		public void Line196()
		{
			// tag::612c2e975f833de9815651135735eae5[]
			var response0 = new SearchResponse<object>();
			// end::612c2e975f833de9815651135735eae5[]

			response0.MatchesExample(@"POST _tasks/_cancel?nodes=nodeId1,nodeId2&actions=*reindex");
		}

		[U]
		[SkipExample]
		public void Line208()
		{
			// tag::bd3d710ec50a151453e141691163af72[]
			var response0 = new SearchResponse<object>();
			// end::bd3d710ec50a151453e141691163af72[]

			response0.MatchesExample(@"GET _tasks?group_by=parents");
		}

		[U]
		[SkipExample]
		public void Line216()
		{
			// tag::a3ce0cfe2176f3d8a36959a5916995f0[]
			var response0 = new SearchResponse<object>();
			// end::a3ce0cfe2176f3d8a36959a5916995f0[]

			response0.MatchesExample(@"GET _tasks?group_by=none");
		}
	}
}