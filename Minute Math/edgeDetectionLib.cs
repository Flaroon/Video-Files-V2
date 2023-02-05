using UnityEngine;

public static class FlaroonEdgeDetection
{
    public static Texture2D EdgeTexture(Texture2D inputTexture, int edgeWidth, float tolerance) {
        Vector2Int sides = new Vector2Int(inputTexture.width, inputTexture.height);
        Color[] pixels = inputTexture.GetPixels();
        
        Color color = Color.red;
        float greyColor = color.grayscale; // value from 0 to 1

        Texture2D tex = new Texture2D(sides.x, sides.y);
        
        for (int x = 0; x < sides.x; x++) {
            for (int y = 0, iY = sides.y - 1; y < sides.y - edgeWidth && iY >= edgeWidth; y++, iY--) {
                
                float a = pixels[x * sides.y + y].grayscale, a1 = pixels[y * sides.x + x].grayscale
                    , a2 = pixels[x * sides.y + iY].grayscale, a3 = pixels[iY * sides.x + x].grayscale;
                
                int val = 0, val1 = 0, val2 = 0, val3 = 0;
                for (int i = 0; i < edgeWidth - 1; i++) {
                    val += a - pixels[x * sides.y + (y+i)].grayscale > tolerance ? 1 : -1;
                    val1 += a1 - pixels[(y+i) * sides.x + x].grayscale > tolerance ? 1 : -1;
                    val2 += a2 - pixels[x * sides.y + (iY-i)].grayscale > tolerance ? 1 : -1;
                    val3 += a3 - pixels[(iY - i) * sides.x + x].grayscale > tolerance ? 1 : -1;
                }
                
                if (val > 0) tex.SetPixel(y, x, Color.black);
                if (val1 >= 0) tex.SetPixel(x, y, Color.black);
                if (val2 >= 0) tex.SetPixel(iY, x, Color.black);
                if (val3 >= 0) tex.SetPixel(x, iY, Color.black);
            }
        }

        tex.Apply();
        return tex;
    }
}
