using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Sprites;

/// <summary>
/// A variant of the Image that behaves differently when the Image type is sliced but the image is too small: Borders get shortened instead of stretched leading to a mirror effect
/// </summary>
public class ImageSlicedMirror : Image {
    
    /// <summary>
    /// Update the UI renderer mesh.
    /// </summary>
    protected override void OnPopulateMesh(VertexHelper toFill)
    {
        if (overrideSprite == null) {
            base.OnPopulateMesh(toFill);
            return;
        }
		if (hasBorder && type == Type.Sliced) {
			GenerateSlicedFilledSprite(toFill);
			return;
		}

		base.OnPopulateMesh(toFill);
    }

    static readonly Vector2[] s_VertScratch = new Vector2[4];
    static readonly Vector2[] s_UVScratch = new Vector2[4];

	private void GenerateSlicedFilledSprite(VertexHelper toFill)
    {
        Vector4 outer, inner, padding, border;

        if (overrideSprite != null)
        {
            outer = DataUtility.GetOuterUV(overrideSprite);
            inner = DataUtility.GetInnerUV(overrideSprite);
            padding = DataUtility.GetPadding(overrideSprite);
            border = overrideSprite.border;
        }
        else
        {
            outer = Vector4.zero;
            inner = Vector4.zero;
            padding = Vector4.zero;
            border = Vector4.zero;
        }

        Rect rect = GetPixelAdjustedRect();
        border = GetAdjustedBorders(border / pixelsPerUnit, rect);
        padding = padding / pixelsPerUnit;

        SetSlicedVerts(rect, border, padding);
		SetSlicedUVs(outer, inner, border);

        toFill.Clear();

        for (int x = 0; x < 3; ++x)
        {
            int x2 = x + 1;

            for (int y = 0; y < 3; ++y)
            {
				if (!fillCenter && x == 1 && y == 1)
                    continue;

                int y2 = y + 1;

                AddQuad(toFill,
                    new Vector2(s_VertScratch[x].x, s_VertScratch[y].y),
                    new Vector2(s_VertScratch[x2].x, s_VertScratch[y2].y),
                    color,
                    new Vector2(s_UVScratch[x].x, s_UVScratch[y].y),
                    new Vector2(s_UVScratch[x2].x, s_UVScratch[y2].y));
            }
        }
    }

    void SetSlicedVerts(Rect rect, Vector4 border, Vector4 padding) {
        s_VertScratch[0] = new Vector2(padding.x, padding.y);
        s_VertScratch[3] = new Vector2(rect.width - padding.z, rect.height - padding.w);

        s_VertScratch[1].x = border.x;
        s_VertScratch[1].y = border.y;
        s_VertScratch[2].x = rect.width - border.z;
        s_VertScratch[2].y = rect.height - border.w;

        for (int i = 0; i < 4; ++i) {
            s_VertScratch[i].x += rect.x;
            s_VertScratch[i].y += rect.y;
        }
    }

    void SetSlicedUVs(Vector4 outer, Vector4 inner, Vector4 border) {
		bool xAxisTooShort, yAxisTooShort;

		xAxisTooShort = (border.x < overrideSprite.border.x || border.z < overrideSprite.border.z);
		yAxisTooShort = (border.y < overrideSprite.border.y || border.w < overrideSprite.border.w);

		if (!xAxisTooShort && !yAxisTooShort) {
			s_UVScratch[0] = new Vector2(outer.x, outer.y);
			s_UVScratch[1] = new Vector2(inner.x, inner.y);
			s_UVScratch[2] = new Vector2(inner.z, inner.w);
			s_UVScratch[3] = new Vector2(outer.z, outer.w);
			return;
		}

		float xUVmultiplier = xAxisTooShort ? border.x / overrideSprite.border.x : 1f;
		float yUVmultiplier = yAxisTooShort ? border.y / overrideSprite.border.y : 1f;

		s_UVScratch[0] = new Vector2(outer.x, outer.y);
		s_UVScratch[1] = new Vector2(inner.x * xUVmultiplier, inner.y * yUVmultiplier);
		s_UVScratch[2] = new Vector2(outer.z - (outer.z - inner.z) * xUVmultiplier, outer.w - (outer.w - inner.w) * yUVmultiplier);
		s_UVScratch[3] = new Vector2(outer.z, outer.w);
    }


	static void AddQuad(VertexHelper vertexHelper, Vector2 posMin, Vector2 posMax, Color32 color, Vector2 uvMin, Vector2 uvMax)
	{
		int startIndex = vertexHelper.currentVertCount;

		vertexHelper.AddVert(new Vector3(posMin.x, posMin.y, 0), color, new Vector2(uvMin.x, uvMin.y));
		vertexHelper.AddVert(new Vector3(posMin.x, posMax.y, 0), color, new Vector2(uvMin.x, uvMax.y));
		vertexHelper.AddVert(new Vector3(posMax.x, posMax.y, 0), color, new Vector2(uvMax.x, uvMax.y));
		vertexHelper.AddVert(new Vector3(posMax.x, posMin.y, 0), color, new Vector2(uvMax.x, uvMin.y));

		vertexHelper.AddTriangle(startIndex, startIndex + 1, startIndex + 2);
		vertexHelper.AddTriangle(startIndex + 2, startIndex + 3, startIndex);
	}

	Vector4 GetAdjustedBorders(Vector4 border, Rect rect)
	{
		for (int axis = 0; axis <= 1; axis++)
		{
			// If the rect is smaller than the combined borders, then there's not room for the borders at their normal size.
			// In order to avoid artefacts with overlapping borders, we scale the borders down to fit.
			float combinedBorders = border[axis] + border[axis + 2];

			if (rect.size[axis] < combinedBorders && combinedBorders != 0)
			{
				float borderScaleRatio = rect.size[axis] / combinedBorders;
				border[axis] *= borderScaleRatio;
				border[axis + 2] *= borderScaleRatio;
			}
		}
		return border;
	}
}
