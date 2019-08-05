using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.QueryDsl
{
	public class GeoBoundingBoxQueryPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line11()
		{
			// tag::b4ef55e48f137e8f67f82b42a047c8f6[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::b4ef55e48f137e8f67f82b42a047c8f6[]

			response0.MatchesExample(@"PUT /my_locations
			{
			    ""mappings"": {
			        ""properties"": {
			            ""pin"": {
			                ""properties"": {
			                    ""location"": {
			                        ""type"": ""geo_point""
			                    }
			                }
			            }
			        }
			    }
			}");

			response1.MatchesExample(@"PUT /my_locations/_doc/1
			{
			    ""pin"" : {
			        ""location"" : {
			            ""lat"" : 40.12,
			            ""lon"" : -71.34
			        }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line44()
		{
			// tag::49abe3273ac51f14cd4b5f1aaa7f6833[]
			var response0 = new SearchResponse<object>();
			// end::49abe3273ac51f14cd4b5f1aaa7f6833[]

			response0.MatchesExample(@"GET my_locations/_search
			{
			    ""query"": {
			        ""bool"" : {
			            ""must"" : {
			                ""match_all"" : {}
			            },
			            ""filter"" : {
			                ""geo_bounding_box"" : {
			                    ""pin.location"" : {
			                        ""top_left"" : {
			                            ""lat"" : 40.73,
			                            ""lon"" : -74.1
			                        },
			                        ""bottom_right"" : {
			                            ""lat"" : 40.01,
			                            ""lon"" : -71.12
			                        }
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line134()
		{
			// tag::2cbaaab829728c46359d2f68b71c446e[]
			var response0 = new SearchResponse<object>();
			// end::2cbaaab829728c46359d2f68b71c446e[]

			response0.MatchesExample(@"GET my_locations/_search
			{
			    ""query"": {
			        ""bool"" : {
			            ""must"" : {
			                ""match_all"" : {}
			            },
			            ""filter"" : {
			                ""geo_bounding_box"" : {
			                    ""pin.location"" : {
			                        ""top_left"" : [-74.1, 40.73],
			                        ""bottom_right"" : [-71.12, 40.01]
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line162()
		{
			// tag::bbf04a7f7a8858e911d6a53fe88127b0[]
			var response0 = new SearchResponse<object>();
			// end::bbf04a7f7a8858e911d6a53fe88127b0[]

			response0.MatchesExample(@"GET my_locations/_search
			{
			    ""query"": {
			        ""bool"" : {
			            ""must"" : {
			                ""match_all"" : {}
			            },
			            ""filter"" : {
			                ""geo_bounding_box"" : {
			                    ""pin.location"" : {
			                        ""top_left"" : ""40.73, -74.1"",
			                        ""bottom_right"" : ""40.01, -71.12""
			                    }
			                }
			            }
			    }
			}
			}");
		}

		[U]
		[SkipExample]
		public void Line188()
		{
			// tag::417dcb29f5547d4de9d75d8b6a7a53c8[]
			var response0 = new SearchResponse<object>();
			// end::417dcb29f5547d4de9d75d8b6a7a53c8[]

			response0.MatchesExample(@"GET my_locations/_search
			{
			    ""query"": {
			        ""bool"" : {
			            ""must"" : {
			                ""match_all"" : {}
			            },
			            ""filter"" : {
			                ""geo_bounding_box"" : {
			                    ""pin.location"" : {
			                        ""wkt"" : ""BBOX (-74.1, -71.12, 40.73, 40.01)""
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line213()
		{
			// tag::d84695e3db2c92cd3faebf729e482bf0[]
			var response0 = new SearchResponse<object>();
			// end::d84695e3db2c92cd3faebf729e482bf0[]

			response0.MatchesExample(@"GET my_locations/_search
			{
			    ""query"": {
			        ""bool"" : {
			            ""must"" : {
			                ""match_all"" : {}
			            },
			            ""filter"" : {
			                ""geo_bounding_box"" : {
			                    ""pin.location"" : {
			                        ""top_left"" : ""dr5r9ydj2y73"",
			                        ""bottom_right"" : ""drj7teegpus6""
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line248()
		{
			// tag::32ffcae9e1d13df0b7295c349d9145ec[]
			var response0 = new SearchResponse<object>();
			// end::32ffcae9e1d13df0b7295c349d9145ec[]

			response0.MatchesExample(@"GET my_locations/_search
			{
			    ""query"": {
			        ""geo_bounding_box"" : {
			            ""pin.location"" : {
			                ""top_left"" : ""dr"",
			                ""bottom_right"" : ""dr""
			            }
			        }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line278()
		{
			// tag::370750b2f51bd097f4578e5b105babdf[]
			var response0 = new SearchResponse<object>();
			// end::370750b2f51bd097f4578e5b105babdf[]

			response0.MatchesExample(@"GET my_locations/_search
			{
			    ""query"": {
			        ""bool"" : {
			            ""must"" : {
			                ""match_all"" : {}
			            },
			            ""filter"" : {
			                ""geo_bounding_box"" : {
			                    ""pin.location"" : {
			                        ""top"" : 40.73,
			                        ""left"" : -74.1,
			                        ""bottom"" : 40.01,
			                        ""right"" : -71.12
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line328()
		{
			// tag::15eee00f09d2290e0f350d420029906e[]
			var response0 = new SearchResponse<object>();
			// end::15eee00f09d2290e0f350d420029906e[]

			response0.MatchesExample(@"GET my_locations/_search
			{
			    ""query"": {
			        ""bool"" : {
			            ""must"" : {
			                ""match_all"" : {}
			            },
			            ""filter"" : {
			                ""geo_bounding_box"" : {
			                    ""pin.location"" : {
			                        ""top_left"" : {
			                            ""lat"" : 40.73,
			                            ""lon"" : -74.1
			                        },
			                        ""bottom_right"" : {
			                            ""lat"" : 40.10,
			                            ""lon"" : -71.12
			                        }
			                    },
			                    ""type"" : ""indexed""
			                }
			            }
			        }
			    }
			}");
		}
	}
}