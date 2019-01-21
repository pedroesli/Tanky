﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
Leonardo da Luz Pinto's Pixel Art Filters Applicator 

Copyright (C) 2018 Leo Luz - leodluz@yahoo.com/leoluzprog@gmail.com/leohotluz@gmail.com

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.

*/
public class Camera2DFilters : MonoBehaviour
{
    public bool AutoconfigureCameraToPixelPerfect;
    public int InternalVerticalResolution = 224;
    private Vector2 InternalResolution = new Vector2(256, 224);
    public enum _2DFilter { HardEdges, SoftEdges, Interpolated, Bicubic, DDT, _2XSal, _2XSalLevel2, /*_2XSalLevel2TwoPasses,*/ XBRLevel2Fast, XBRLevel2NoBlend, XBRLevel2SmallDetails, XBRLevel3, _5XBR3_7, /*XBRLevel3MultiPass, */ _2XBRZ, _3XBRZ, _4XBRZ, _5XBRZ, _6XBRZ, HQ2X, HQ3X, HQ4X, CRTAperture, CRTCaligari, CRTHyllian };
    public _2DFilter Filter;
    public enum OutputResolition { ScreenResolution, Internal1X, Internal2X, Internal3X, Internal4X, Internal5X, Internal6X }
    public OutputResolition outputResolution;
    private Material mat;

    private int passes=2;
    private RenderTexture NativeResolutionRenderTexture;
    private RenderTexture FiltredRenderTexture;

    public bool ShowOptionsOnGUI=true;

    void OnGUI()
    {
        if (!Application.isPlaying)
            Start();

        if (!ShowOptionsOnGUI)
            return;
        
        GUILayout.BeginArea(new Rect(10, 10, 200, 768));

        if(GUILayout.Button("Hard Edges"))
        {
            Filter = _2DFilter.HardEdges;
            Start();
        }
        else if(GUILayout.Button("Soft Edges"))
        {
            Filter = _2DFilter.SoftEdges;
            outputResolution = OutputResolition.Internal2X;
            Start();
        }
        else if (GUILayout.Button("Interpolated"))
        {
            Filter = _2DFilter.Interpolated;
            Start();
        }
        else if (GUILayout.Button("Bicubic"))
        {
            Filter = _2DFilter.Bicubic;
            outputResolution = OutputResolition.ScreenResolution;
            Start();
        }
        else if (GUILayout.Button("DDT"))
        {
            Filter = _2DFilter.DDT;
            outputResolution = OutputResolition.ScreenResolution;
            Start();
        }

        else if (GUILayout.Button("2XSal"))
        {
            Filter = _2DFilter._2XSal;
            outputResolution = OutputResolition.Internal2X;
            Start();
        }
        else if (GUILayout.Button("2XSal Level 2"))
        {
            Filter = _2DFilter._2XSalLevel2;
            outputResolution = OutputResolition.Internal2X;
            Start();
        }
        else if (GUILayout.Button("XBR Level 2 Fast"))
        { 
            Filter = _2DFilter.XBRLevel2Fast;
            outputResolution = OutputResolition.ScreenResolution;
            Start();
        }
        else if (GUILayout.Button("XBR Level 2 No Blend"))
        {
            Filter = _2DFilter.XBRLevel2NoBlend;
            outputResolution = OutputResolition.ScreenResolution;
            Start();
        }
        else if (GUILayout.Button("XBR Level 2 Small Details"))
        {
            Filter = _2DFilter.XBRLevel2SmallDetails;
            outputResolution = OutputResolition.ScreenResolution;
            Start();
        }
        else if (GUILayout.Button("XBR Level 3"))
        {
            Filter = _2DFilter.XBRLevel3;
            outputResolution = OutputResolition.ScreenResolution;
            Start();
        }
        else if (GUILayout.Button("2XBRZ"))
        {
            Filter = _2DFilter._2XBRZ;
            outputResolution = OutputResolition.Internal2X;
            Start();
        }
        else if (GUILayout.Button("3XBRZ"))
        {
            Filter = _2DFilter._3XBRZ;
            outputResolution = OutputResolition.Internal3X;
            Start();
        }
        else if (GUILayout.Button("4XBRZ"))
        {
            Filter = _2DFilter._4XBRZ;
            outputResolution = OutputResolition.Internal4X;
            Start();
        }
        else if (GUILayout.Button("5XBRZ"))
        {
            Filter = _2DFilter._5XBRZ;
            outputResolution = OutputResolition.Internal5X;
            Start();
        }
        else if (GUILayout.Button("6XBRZ"))
        {
            Filter = _2DFilter._6XBRZ;
            outputResolution = OutputResolition.Internal6X;
            Start();
        }
        else if (GUILayout.Button("CRT Aperture"))
        {
            Filter = _2DFilter.CRTAperture;
            outputResolution = OutputResolition.ScreenResolution;
            Start();
        }
        else if (GUILayout.Button("CRT Caligari"))
        {
            Filter = _2DFilter.CRTCaligari;
            outputResolution = OutputResolition.ScreenResolution;
            Start();
        }
        else if (GUILayout.Button("CRT Hyllian"))
        {
            Filter = _2DFilter.CRTHyllian;
            outputResolution = OutputResolition.ScreenResolution;
            Start();
        }
        GUILayout.Space(10);
        if (GUILayout.Button("Output Screen Resolution"))
        {
            outputResolution = OutputResolition.ScreenResolution;
            Start();
        }
        if (GUILayout.Button("Output Internal 1x Resolution"))
        {
            outputResolution = OutputResolition.Internal1X;
            Start();
        }
        if (GUILayout.Button("Output Internal 2x Resolution"))
        {
            outputResolution = OutputResolition.Internal2X;
            Start();
        }
        if (GUILayout.Button("Output Internal 3x Resolution"))
        {
            outputResolution = OutputResolition.Internal3X;
            Start();
        }
        if (GUILayout.Button("Output Internal 4x Resolution"))
        {
            outputResolution = OutputResolition.Internal4X;
            Start();
        }
        if (GUILayout.Button("Output Internal 5x Resolution"))
        {
            outputResolution = OutputResolition.Internal5X;
            Start();
        }
        if (GUILayout.Button("Output Internal 6x Resolution"))
        {
            outputResolution = OutputResolition.Internal6X;
            Start();
        }

        GUILayout.Label("Output resolution: " + (InternalResolution * passes));
        GUILayout.EndArea();
    }

    void Start()
    {
        InternalResolution = new Vector2(Mathf.RoundToInt(((float)InternalVerticalResolution / (float)Screen.height)* (float)Screen.width), InternalVerticalResolution);
        if (InternalResolution.x % 2 != 0)
        {
            InternalResolution.x--;
        }
        if (InternalResolution.y % 2 != 0)
        {
            InternalResolution.y--;
        }
        if (AutoconfigureCameraToPixelPerfect)
        {
            var cam = GetComponent<Camera>();
            cam.orthographicSize = ((float)InternalVerticalResolution / 1000f) * 5f;
            cam.orthographic = true;
        }
        //print(Screen.width + " - " + Screen.height + " -- " + InternalResolution);
        switch (outputResolution)
        {
            case OutputResolition.ScreenResolution:
                passes = 0;
                break;
            case OutputResolition.Internal1X:
                passes = 1;
                break;
            case OutputResolition.Internal2X:
                passes = 2;
                break;
            case OutputResolition.Internal3X:
                passes = 3;
                break;
            case OutputResolition.Internal4X:
                passes = 4;
                break;
            case OutputResolition.Internal5X:
                passes = 5;
                break;
            case OutputResolition.Internal6X:
                passes = 6;
                break;
            default:
                break;
        }
        switch (Filter)
        {
            case _2DFilter.HardEdges:
                passes = 0;
                mat = new Material(Shader.Find("Unlit/Texture"));
                break;
            case _2DFilter.SoftEdges:
                mat = new Material(Shader.Find("Unlit/Texture"));
                break;
            case _2DFilter.Interpolated:
                mat = new Material(Shader.Find("Unlit/Texture"));
                passes = 1;
                break;
            case _2DFilter.Bicubic:
                mat = new Material(Shader.Find("Pixel Art Filters/Bicubic"));
                break;
            case _2DFilter.DDT:
                mat = new Material(Shader.Find("Pixel Art Filters/DDT"));
                break;
            case _2DFilter._2XSal:
                mat = new Material(Shader.Find("Pixel Art Filters/2xSal"));
                break;
            case _2DFilter._2XSalLevel2:
                mat = new Material(Shader.Find("Pixel Art Filters/2xSal-Level-2"));
                break;
            case _2DFilter.XBRLevel2Fast:
                mat = new Material(Shader.Find("Pixel Art Filters/XBR-LV2-Fast"));
                break;
            case _2DFilter.XBRLevel2NoBlend:
                mat = new Material(Shader.Find("Pixel Art Filters/XBR-LV2-NoBlend"));
                break;
            case _2DFilter.XBRLevel2SmallDetails:
                mat = new Material(Shader.Find("Pixel Art Filters/XBR-LV2-Small-Details"));
                break;
            case _2DFilter.XBRLevel3:
                mat = new Material(Shader.Find("Pixel Art Filters/XBR-LV3"));
                break;
            case _2DFilter._5XBR3_7:
                mat = new Material(Shader.Find("Pixel Art Filters/5XBR3.7"));
                break;
            case _2DFilter._2XBRZ:
                mat = new Material(Shader.Find("Pixel Art Filters/2xBRZ"));
                break;
            case _2DFilter._3XBRZ:
                mat = new Material(Shader.Find("Pixel Art Filters/3xBRZ"));
                break;
            case _2DFilter._4XBRZ:
                mat = new Material(Shader.Find("Pixel Art Filters/4xBRZ"));
                break;
            case _2DFilter._5XBRZ:
                mat = new Material(Shader.Find("Pixel Art Filters/5xBRZ"));
                break;
            case _2DFilter._6XBRZ:
                mat = new Material(Shader.Find("Pixel Art Filters/6xBRZ"));
                break;
            case _2DFilter.HQ2X:
                mat = new Material(Shader.Find("Pixel Art Filters/6xBRZ"));
                break;
            case _2DFilter.HQ3X:
                mat = new Material(Shader.Find("Pixel Art Filters/6xBRZ"));
                break;
            case _2DFilter.HQ4X:
                mat = new Material(Shader.Find("Pixel Art Filters/6xBRZ"));
                break;
            case _2DFilter.CRTAperture:
                mat = new Material(Shader.Find("Pixel Art Filters/CRT Aperture"));
                break;
            case _2DFilter.CRTCaligari:
                mat = new Material(Shader.Find("Pixel Art Filters/CRT Caligari"));
                break;
            case _2DFilter.CRTHyllian:
                mat = new Material(Shader.Find("Pixel Art Filters/CRT Hyllian"));
                break;
            default:
                break;
        }

        if (passes == 0)
        {
            NativeResolutionRenderTexture = new RenderTexture((int)InternalResolution.x, (int)InternalResolution.y, 0);
            NativeResolutionRenderTexture.filterMode = FilterMode.Point;
            NativeResolutionRenderTexture.Create();
        } else
        {
            NativeResolutionRenderTexture = new RenderTexture((int)(InternalResolution.x), (int)InternalResolution.y, 0);
            NativeResolutionRenderTexture.filterMode = FilterMode.Point;
            NativeResolutionRenderTexture.Create();
            FiltredRenderTexture = new RenderTexture((int)(InternalResolution.x*passes), (int)(InternalResolution.y* passes), 0);
            FiltredRenderTexture.Create();
        }
    }
    void OnDrawGizmos()
    {
        if (AutoconfigureCameraToPixelPerfect)
        {
            var cam = GetComponent<Camera>();
            cam.orthographicSize = ((float)InternalVerticalResolution / 1000f) * 5f;
            cam.orthographic = true;
        }
    }

    void OnPreRender()
    {
        GetComponent<Camera>().targetTexture = NativeResolutionRenderTexture;
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        src.filterMode = FilterMode.Point;
        mat.SetVector("texture_size", InternalResolution);
        mat.SetTexture("decal", src);
        mat.SetTexture("_BackgroundTexture", src);
        mat.SetTexture("_MainTex", src);

        if (passes == 0)
        {
            GetComponent<Camera>().targetTexture = null;
            Graphics.Blit(src, null as RenderTexture, mat);
        }
        else
        {
            Graphics.Blit(src, FiltredRenderTexture, mat);
            GetComponent<Camera>().targetTexture = null;
          //  Graphics.Blit(src, null as RenderTexture);
            Graphics.Blit(FiltredRenderTexture, null as RenderTexture);
        }
    }
}