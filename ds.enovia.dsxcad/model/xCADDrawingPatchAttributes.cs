using ds.enovia.common;
using System.Text.Json.Serialization;

namespace ds.enovia.dsxcad.model
{
    public class xCADDrawingPatchAttributes : SerializableJsonObject
    {
        public string title { get; set; }
        public string description { get; set; }
        public string cestamp { get; set; }

        [JsonPropertyName("dseno:EnterpriseAttributes")]
        public DSEnoEnterpriseAttributes EnterpriseAttributes { get; set; }
    }
}
