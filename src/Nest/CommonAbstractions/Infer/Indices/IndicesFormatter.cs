using System.Collections.Generic;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	internal class IndicesFormatter : IJsonFormatter<Indices>
	{
		public Indices Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.BeginArray)
			{
				reader.ReadNextBlock();
				return null;
			}

			var indices = new List<IndexName>();
			var count = 0;
			while (reader.ReadIsInArray(ref count))
			{
				var index = reader.ReadString();
				indices.Add(index);
			}
			return new Indices(indices);
		}

		public void Serialize(ref JsonWriter writer, Indices value, IJsonFormatterResolver formatterResolver) {
			if (value == null) {
				writer.WriteNull();
				return;
			}


			var settings = formatterResolver.GetConnectionSettings();
			writer.WriteBeginArray();
			for (var index = 0; index < value.Count; index++) {
				if (index > 0)
					writer.WriteValueSeparator();

				var indexName = value[index];
				writer.WriteString(indexName.GetString(settings));
			}
			writer.WriteEndArray();
		}
	}
}
