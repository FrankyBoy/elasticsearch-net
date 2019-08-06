using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Docs
{
	public class BulkPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line73()
		{
			// tag::ae9ccfaa146731ab9176df90670db1c2[]
			var response0 = new SearchResponse<object>();
			// end::ae9ccfaa146731ab9176df90670db1c2[]

			response0.MatchesExample(@"POST _bulk
			{ ""index"" : { ""_index"" : ""test"", ""_id"" : ""1"" } }
			{ ""field1"" : ""value1"" }
			{ ""delete"" : { ""_index"" : ""test"", ""_id"" : ""2"" } }
			{ ""create"" : { ""_index"" : ""test"", ""_id"" : ""3"" } }
			{ ""field1"" : ""value3"" }
			{ ""update"" : {""_id"" : ""1"", ""_index"" : ""test""} }
			{ ""doc"" : {""field2"" : ""value2""} }");
		}

		[U(Skip = "Example not implemented")]
		public void Line265()
		{
			// tag::8cd00a3aba7c3c158277bc032aac2830[]
			var response0 = new SearchResponse<object>();
			// end::8cd00a3aba7c3c158277bc032aac2830[]

			response0.MatchesExample(@"POST _bulk
			{ ""update"" : {""_id"" : ""1"", ""_index"" : ""index1"", ""retry_on_conflict"" : 3} }
			{ ""doc"" : {""field"" : ""value""} }
			{ ""update"" : { ""_id"" : ""0"", ""_index"" : ""index1"", ""retry_on_conflict"" : 3} }
			{ ""script"" : { ""source"": ""ctx._source.counter += params.param1"", ""lang"" : ""painless"", ""params"" : {""param1"" : 1}}, ""upsert"" : {""counter"" : 1}}
			{ ""update"" : {""_id"" : ""2"", ""_index"" : ""index1"", ""retry_on_conflict"" : 3} }
			{ ""doc"" : {""field"" : ""value""}, ""doc_as_upsert"" : true }
			{ ""update"" : {""_id"" : ""3"", ""_index"" : ""index1"", ""_source"" : true} }
			{ ""doc"" : {""field"" : ""value""} }
			{ ""update"" : {""_id"" : ""4"", ""_index"" : ""index1""} }
			{ ""doc"" : {""field"" : ""value""}, ""_source"": true}");
		}
	}
}