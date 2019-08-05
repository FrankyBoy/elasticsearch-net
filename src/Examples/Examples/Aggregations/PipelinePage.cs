using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Aggregations
{
	public class PipelinePage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line53()
		{
			// tag::ec20b1c236955a545476eeeea747d9de[]
			var response0 = new SearchResponse<object>();
			// end::ec20b1c236955a545476eeeea747d9de[]

			response0.MatchesExample(@"POST /_search
			{
			    ""aggs"": {
			        ""my_date_histo"":{
			            ""date_histogram"":{
			                ""field"":""timestamp"",
			                ""calendar_interval"":""day""
			            },
			            ""aggs"":{
			                ""the_sum"":{
			                    ""sum"":{ ""field"": ""lemmings"" } \<1>
			                },
			                ""the_deriv"":{
			                    ""derivative"":{ ""buckets_path"": ""the_sum"" } \<2>
			                }
			            }
			        }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line83()
		{
			// tag::11be7655fdafcf4c1454a0e9ad8ddf63[]
			var response0 = new SearchResponse<object>();
			// end::11be7655fdafcf4c1454a0e9ad8ddf63[]

			response0.MatchesExample(@"POST /_search
			{
			    ""aggs"" : {
			        ""sales_per_month"" : {
			            ""date_histogram"" : {
			                ""field"" : ""date"",
			                ""calendar_interval"" : ""month""
			            },
			            ""aggs"": {
			                ""sales"": {
			                    ""sum"": {
			                        ""field"": ""price""
			                    }
			                }
			            }
			        },
			        ""max_monthly_sales"": {
			            ""max_bucket"": {
			                ""buckets_path"": ""sales_per_month>sales"" \<1>
			            }
			        }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line121()
		{
			// tag::f3dd309ab027e86048b476b54f0d4ca1[]
			var response0 = new SearchResponse<object>();
			// end::f3dd309ab027e86048b476b54f0d4ca1[]

			response0.MatchesExample(@"POST /_search
			{
			    ""aggs"": {
			        ""my_date_histo"": {
			            ""date_histogram"": {
			                ""field"":""timestamp"",
			                ""calendar_interval"":""day""
			            },
			            ""aggs"": {
			                ""the_deriv"": {
			                    ""derivative"": { ""buckets_path"": ""_count"" } \<1>
			                }
			            }
			        }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line147()
		{
			// tag::2afc1231679898bd864d06679d9e951b[]
			var response0 = new SearchResponse<object>();
			// end::2afc1231679898bd864d06679d9e951b[]

			response0.MatchesExample(@"POST /sales/_search
			{
			  ""size"": 0,
			  ""aggs"": {
			    ""histo"": {
			      ""date_histogram"": {
			        ""field"": ""date"",
			        ""calendar_interval"": ""day""
			      },
			      ""aggs"": {
			        ""categories"": {
			          ""terms"": {
			            ""field"": ""category""
			          }
			        },
			        ""min_bucket_selector"": {
			          ""bucket_selector"": {
			            ""buckets_path"": {
			              ""count"": ""categories._bucket_count"" \<1>
			            },
			            ""script"": {
			              ""source"": ""params.count != 0""
			            }
			          }
			        }
			      }
			    }
			  }
			}");
		}
	}
}