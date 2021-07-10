using ds.enovia.common.model;

namespace ds.enovia.dsdo.model
{
    public class PartDerivedOutputRefObject : BusinessObjectId
    {
        public PartDerivedOutputRefObject(string _ownerId)
        {
            source = "3DSpace";
            type = "VPMReference";
            id = _ownerId;
            relativePath = $"resource/v1/dsxcad/dsxcad:Part/{_ownerId}";
        }
    }

    public class DrawingDerivedOutputRefObject : BusinessObjectId
    {
        public DrawingDerivedOutputRefObject(string _ownerId)
        {
            source = "3DSpace";
            type = "Drawing";
            id = _ownerId;
            relativePath = $"resource/v1/dsxcad/dsxcad:Drawing/{_ownerId}";
        }
    }
}
