using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	internal class IndicesMultiSyntaxFormatter : IJsonFormatter<Indices>
	{
		public Indices Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() == JsonToken.String)
			{
				Indices indices = reader.ReadString();
				return indices;
			}

			reader.ReadNextBlock();
			return null;
		}

		public void Serialize(ref JsonWriter writer, Indices value, IJsonFormatterResolver formatterResolver) {
			if (value == null) {
				writer.WriteNull();
				return;
			}

			var connectionSettings = formatterResolver.GetConnectionSettings();
			writer.WriteString(((IUrlParameter)value).GetString(connectionSettings));
		}
	}
}
