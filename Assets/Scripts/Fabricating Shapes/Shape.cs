using UnityEngine;

public class Shape : PersistableObject
{
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public int ShapeId
    {
        get
        {
            return shapeId;
        }
        set
        {
            if (shapeId == int.MinValue && value != int.MinValue)
            {
                shapeId = value;
            }
            else
            {
                Debug.LogError("Not allowed to change shapeId.");
            }
        }
    }

    public int MaterialId { get; private set; }

    public void SetMaterial(Material material, int materialId)
    {
        meshRenderer.material = material;
        MaterialId = materialId;
    }

    int shapeId = int.MinValue;

    Color colour;

    static int colorPropertyId = Shader.PropertyToID("_Color");
    static MaterialPropertyBlock sharedPropertyBlock;

    public void SetColour(Color colour)
    {
        this.colour = colour;
        if(sharedPropertyBlock == null)
        {
            sharedPropertyBlock = new MaterialPropertyBlock();
        }
        sharedPropertyBlock.SetColor(colorPropertyId, colour);
        meshRenderer.SetPropertyBlock(sharedPropertyBlock);
    }

    public override void Save(GameDataWriter writer)
    {
        base.Save(writer);
        writer.Write(colour);
    }

    public override void Load(GameDataReader reader)
    {
        base.Load(reader);
        SetColour(reader.Version > 0? reader.ReadColour() : Color.white);
    }

}
